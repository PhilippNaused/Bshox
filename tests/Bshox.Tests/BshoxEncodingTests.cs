using Bshox.Internals;
using Bshox.TestUtils;

namespace Bshox.Tests;

internal sealed class BshoxEncodingTests
{
    [Test]
    [Arguments(0)]
    [Arguments(1)]
    [Arguments(-1)]
    [Arguments(int.MinValue)]
    [Arguments(int.MaxValue)]
    [Arguments(long.MinValue)]
    [Arguments(long.MaxValue)]
    [Arguments(BshoxConsts.Min3ByteInt)]
    [Arguments(BshoxConsts.Min3ByteInt - 1)]
    [Arguments(BshoxConsts.Max3ByteInt)]
    [Arguments(BshoxConsts.Max3ByteInt + 1)]
    public async Task ZigZagRoundTrip(long l)
    {
        using var stream = new PooledByteBufferWriter();
        var writer = new BshoxWriter(stream);
        writer.WriteZigZagVarInt64(l);
        writer.Flush();
        TestHelper.WriteContext(stream);
        var reader = new BshoxReader(stream.GetReadOnlySequence());
        long x = reader.ReadZigZagVarInt64();
        await Assert.That(x).IsEqualTo(l);
    }

    [Test]
    [Arguments(0u)]
    [Arguments(1u)]
    [Arguments(2u)]
    [Arguments(uint.MaxValue)]
    [Arguments(ulong.MaxValue)]
    [Arguments(uint.MaxValue >>> 4)] // largest value that fits in 4 bytes
    [Arguments(BshoxConsts.Max3ByteUint + 1)] // smallest value that requires 4 bytes
    [Arguments(BshoxConsts.Max3ByteUint)] // largest value that fits in 3 bytes
    public async Task VarIntRoundTrip(ulong l)
    {
        using var stream = new PooledByteBufferWriter();
        var writer = new BshoxWriter(stream);
        writer.WriteVarInt64(l);
        writer.Flush();
        TestHelper.WriteContext(stream);
        var reader = new BshoxReader(stream.GetReadOnlySequence());
        ulong x = reader.ReadVarInt64();
        await Assert.That(x).IsEqualTo(l);
    }

    private static readonly uint[] exampleKeys = [1u, 2u, 15u, byte.MaxValue, BshoxConstants.MaxKey];
    private static readonly BshoxCode[] exampleCodes =
#if NETCOREAPP
        Enum.GetValues<BshoxCode>();
#else
        (BshoxCode[])Enum.GetValues(typeof(BshoxCode));
#endif

    public static IEnumerable<(uint key, BshoxCode type)> TagExamples()
    {
        foreach (var key in exampleKeys)
        {
            foreach (var type in exampleCodes)
            {
                yield return (key, type);
            }
        }
    }

    [Test]
    [MethodDataSource(nameof(TagExamples))]
    public async Task TagRoundTrip(uint key, BshoxCode type)
    {
        using var stream = new PooledByteBufferWriter();
        var writer = new BshoxWriter(stream);
        writer.WriteTag(key, type);
        writer.Flush();
        TestHelper.WriteContext(stream);
        var reader = new BshoxReader(stream.GetReadOnlySequence());
        uint key2 = reader.ReadTag(out var type2);
        await Assert.That(key2).IsEqualTo(key);
        await Assert.That(type).IsEqualTo(type2);
    }

    [Test]
    [MethodDataSource(typeof(ExampleData), nameof(ExampleData.Strings))]
    public async Task StringRoundTripTest(string s)
    {
        using var stream = new PooledByteBufferWriter();
        var writer = new BshoxWriter(stream);
        writer.WriteString(s);
        writer.Flush();
        TestHelper.WriteContext(stream);
        var reader = new BshoxReader(stream.GetReadOnlySequence());
        string x = reader.ReadString();
        await Assert.That(x).IsEqualTo(s);
    }

    [Test]
    [Arguments(0)]
    [Arguments(1)]
    [Arguments(100)]
    [Arguments(1000)]
    [Arguments(10000)]
    [Arguments(100000)]
    public async Task LongStringRoundTripTest(int l)
    {
        using var stream = new PooledByteBufferWriter();
        var writer = new BshoxWriter(stream);
        string s = new Random(1).NextString(l);
        writer.WriteString(s);
        writer.Flush();
        TestHelper.WriteContext(stream);
        var reader = new BshoxReader(stream.GetReadOnlySequence());
        string x = reader.ReadString();
        await Assert.That(x).IsEqualTo(s);
    }

    public static IEnumerable<(string, uint?)> VarInts32()
    {
        yield return ("00", 0);
        yield return ("0000000000000000000000", 0);
        yield return ("00", 0);
        yield return ("01", 1);
        yield return ("7F", 127);
        yield return ("8001", 128);
        yield return ("FF01", 255);
        yield return ("FFFF7F", 2097151);
        yield return ("FFFF7FFFFFFFFFFFFF", 2097151);
        yield return ("808001", 16384);
        yield return ("FFFFFFFF0F", uint.MaxValue);
        yield return ("FFFFFFFF7F", uint.MaxValue); // This varint has 35 bits, to the result is cut off.
        yield return ("808080808001", null); // overflow
        yield return ("FFFFFFFFFFFF", null); // overflow
    }

    public static IEnumerable<(string, ulong?)> VarInts64()
    {
        yield return ("00", 0);
        yield return ("00000000000000000000000000000000000000000000", 0);
        yield return ("00", 0);
        yield return ("01", 1);
        yield return ("7F", 127);
        yield return ("8001", 128);
        yield return ("FF01", 255);
        yield return ("FFFF7F", 2097151);
        yield return ("FFFF7FFFFFFFFFFFFF", 2097151);
        yield return ("808001", 16384);
        yield return ("FFFFFFFF0F", uint.MaxValue);
        yield return ("FFFFFFFF7F", 34359738367ul);
        yield return ("8080808080808080808001", null); // overflow
        yield return ("FFFFFFFFFFFFFFFFFFFFFF", null); // overflow
    }

    [Test]
    [MethodDataSource(nameof(VarInts32))]
    public async Task VarInt32DecodingTests(string hex, uint? expected)
    {
        var reader = new BshoxReader(hex.FromHex());
        if (expected.HasValue)
        {
            uint value = reader.ReadVarInt32();
            var size = DefaultContracts.UInt32.Serialize(in value).Length;
            await Assert.That(reader.Consumed).IsEqualTo(size); // Reader must consume only as many bytes as needed
            await Assert.That(value).IsEqualTo(expected.Value);
        }
        else
        {
            try
            {
                reader.ReadVarInt32();
                Assert.Fail("Expected exception was not thrown");
            }
            catch (BshoxException)
            {
                // passed
            }
        }
    }

    [Test]
    [MethodDataSource(nameof(VarInts64))]
    public async Task VarInt64DecodingTests(string hex, ulong? expected)
    {
        var reader = new BshoxReader(hex.FromHex());
        if (expected.HasValue)
        {
            ulong value = reader.ReadVarInt64();
            var size = DefaultContracts.UInt64.Serialize(in value).Length;
            await Assert.That(reader.Consumed).IsEqualTo(size); // Reader must consume only as many bytes as needed
            await Assert.That(value).IsEqualTo(expected.Value);
        }
        else
        {
            try
            {
                reader.ReadVarInt64();
                Assert.Fail("Expected exception was not thrown");
            }
            catch (BshoxException)
            {
                // passed
            }
        }
    }
}
