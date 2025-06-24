using Bshox.Internals;
using Bshox.Utils;

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
        TestUtils.WriteContext(stream);
        var reader = new BshoxReader(stream.WrittenMemory);
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
        TestUtils.WriteContext(stream);
        var reader = new BshoxReader(stream.WrittenMemory);
        ulong x = reader.ReadVarInt64();
        await Assert.That(x).IsEqualTo(l);
    }

    [Test]
    [MatrixDataSource]
    public async Task TagRoundTrip([Matrix(1u, 2u, 15u, byte.MaxValue, BshoxConstants.MaxKey)] uint key, [Matrix] BshoxCode type)
    {
        using var stream = new PooledByteBufferWriter();
        var writer = new BshoxWriter(stream);
        writer.WriteTag(key, type);
        writer.Flush();
        TestUtils.WriteContext(stream);
        var reader = new BshoxReader(stream.WrittenMemory);
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
        TestUtils.WriteContext(stream);
        var reader = new BshoxReader(stream.WrittenMemory);
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
        TestUtils.WriteContext(stream);
        var reader = new BshoxReader(stream.WrittenMemory);
        string x = reader.ReadString();
        await Assert.That(x).IsEqualTo(s);
    }
}
