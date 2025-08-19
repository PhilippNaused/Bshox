using System.Diagnostics;
using System.Runtime.CompilerServices;
using Bshox.Internals;
using Bshox.TestUtils;

namespace Bshox.Tests;

internal sealed class WriterTests : IDisposable
{
    #region Boilerplate

    private readonly Random rng = new(42);
    private readonly PooledByteBufferWriter buffer = new();

    private BshoxWriter GetWriter() => new(buffer);

    public void Dispose() => Reset();

    private byte[] GetOutput() => buffer.WrittenMemory.ToArray();

    private void Reset()
    {
        buffer.Dispose();
    }

    private BshoxReader GetReader()
    {
        const int SegmentSize = 3;
        var seq = SequenceSegmenter.MakeSegmentedSequence(buffer.WrittenMemory, SegmentSize);
        Debug.Assert(seq.Length == buffer.WrittenMemory.Length, "seq.Length == buffer.WrittenMemory.Length");
        Debug.Assert(seq.IsSingleSegment == (buffer.WrittenMemory.Length <= SegmentSize), "seq.IsSingleSegment == (buffer.WrittenMemory.Length <= SegmentSize)");
        return new BshoxReader(seq);
    }

    private static string Hex(byte[] bytes) => BitConverter.ToString(bytes);

    #endregion Boilerplate

    #region Integers

    #region UInt32

    [Test]
    [Arguments(1)]
    [Arguments(10_000)]
    public async Task WriteVarInt32_RoundTrip(int count)
    {
        var array = new uint[count];
        var writer = GetWriter();
        for (int i = 0; i < count; i++)
        {
            array[i] = rng.NextScaledUInt();
            writer.WriteVarInt32(array[i]);
        }
        writer.Flush();

        var reader = GetReader();
        var array2 = new uint[count];
        for (int i = 0; i < count; i++)
        {
            array2[i] = reader.ReadVarInt32();
        }
        await Assert.That(array2).IsEquivalentTo(array);
    }

    [Test]
    [Arguments(0u, "00")]
    [Arguments(1u, "01")]
    [Arguments(127u, "7F")]
    [Arguments(128u, "80-01")]
    [Arguments(BshoxConsts.Max3ByteUint, "FF-FF-7F")]
    [Arguments(uint.MaxValue, "FF-FF-FF-FF-0F")]
    public async Task WriteVarInt32_Hex(uint value, string expected)
    {
        var writer = GetWriter();
        writer.WriteVarInt32(value);
        writer.Flush();

        var array = GetOutput();
        await Assert.That(Hex(array)).IsEqualTo(expected);
        var decoded = GetReader().ReadVarInt32();
        await Assert.That(decoded).IsEqualTo(value);
    }

    #endregion UInt32

    #region Int32

    [Test]
    [Arguments(1)]
    [Arguments(10_000)]
    public async Task WriteZigZagVarInt32_RoundTrip(int count)
    {
        var array = new int[count];
        var writer = GetWriter();
        for (int i = 0; i < count; i++)
        {
            array[i] = rng.NextScaledInt();
            writer.WriteZigZagVarInt32(array[i]);
        }
        writer.Flush();

        var reader = GetReader();
        var array2 = new int[count];
        for (int i = 0; i < count; i++)
        {
            array2[i] = reader.ReadZigZagVarInt32();
        }
        await Assert.That(array2).IsEquivalentTo(array);
    }

    [Test]
    [Arguments(0, "00")]
    [Arguments(1, "02")]
    [Arguments(127, "FE-01")]
    [Arguments(128, "80-02")]
    [Arguments(int.MinValue, "FF-FF-FF-FF-0F")]
    [Arguments(int.MaxValue, "FE-FF-FF-FF-0F")]
    public async Task WriteZigZagVarInt32_Hex(int value, string expected)
    {
        var writer = GetWriter();
        writer.WriteZigZagVarInt32(value);
        writer.Flush();

        var array = GetOutput();
        await Assert.That(Hex(array)).IsEqualTo(expected);
        var decoded = GetReader().ReadZigZagVarInt32();
        await Assert.That(decoded).IsEqualTo(value);
    }

    #endregion Int32

    #region UInt64

    [Test]
    [Arguments(1)]
    [Arguments(10_000)]
    public async Task WriteVarInt64_RoundTrip(int count)
    {
        var array = new ulong[count];
        var writer = GetWriter();
        for (int i = 0; i < count; i++)
        {
            array[i] = rng.NextScaledULong();
            writer.WriteVarInt64(array[i]);
        }
        writer.Flush();

        var reader = GetReader();
        var array2 = new ulong[count];
        for (int i = 0; i < count; i++)
        {
            array2[i] = reader.ReadVarInt64();
        }
        await Assert.That(array2).IsEquivalentTo(array);
    }

    [Test]
    [Arguments(0u, "00")]
    [Arguments(1u, "01")]
    [Arguments(127u, "7F")]
    [Arguments(128u, "80-01")]
    [Arguments(BshoxConsts.Max3ByteUint, "FF-FF-7F")]
    [Arguments(uint.MaxValue, "FF-FF-FF-FF-0F")]
    [Arguments(ulong.MaxValue, "FF-FF-FF-FF-FF-FF-FF-FF-FF-01")]
    public async Task WriteVarInt64_Hex(ulong value, string expected)
    {
        var writer = GetWriter();
        writer.WriteVarInt64(value);
        writer.Flush();

        var array = GetOutput();
        await Assert.That(Hex(array)).IsEqualTo(expected);
        var decoded = GetReader().ReadVarInt64();
        await Assert.That(decoded).IsEqualTo(value);
    }

    #endregion UInt64

    #region Int64

    [Test]
    [Arguments(1)]
    [Arguments(10_000)]
    public async Task WriteZigZagVarInt64_RoundTrip(int count)
    {
        var array = new long[count];
        var writer = GetWriter();
        for (int i = 0; i < count; i++)
        {
            array[i] = rng.NextScaledLong();
            writer.WriteZigZagVarInt64(array[i]);
        }
        writer.Flush();

        var reader = GetReader();
        var array2 = new long[count];
        for (int i = 0; i < count; i++)
        {
            array2[i] = reader.ReadZigZagVarInt64();
        }
        await Assert.That(array2).IsEquivalentTo(array);
    }

    [Test]
    [Arguments(0, "00")]
    [Arguments(1, "02")]
    [Arguments(127, "FE-01")]
    [Arguments(128, "80-02")]
    [Arguments(long.MinValue, "FF-FF-FF-FF-FF-FF-FF-FF-FF-01")]
    [Arguments(long.MaxValue, "FE-FF-FF-FF-FF-FF-FF-FF-FF-01")]
    public async Task WriteZigZagVarInt64_Hex(long value, string expected)
    {
        var writer = GetWriter();
        writer.WriteZigZagVarInt64(value);
        writer.Flush();

        var array = GetOutput();
        await Assert.That(Hex(array)).IsEqualTo(expected);
        var decoded = GetReader().ReadZigZagVarInt64();
        await Assert.That(decoded).IsEqualTo(value);
    }

    #endregion Int64

    #endregion Integers

    #region Floats

    #region Double

    [Test]
    [Arguments(1)]
    [Arguments(10_000)]
    public async Task WriteDouble_RoundTrip(int count)
    {
        var array = new double[count];
        var writer = GetWriter();
        for (int i = 0; i < count; i++)
        {
            array[i] = rng.NextDouble2();
            writer.WriteDouble(array[i]);
        }
        writer.Flush();

        var reader = GetReader();
        var array2 = new double[count];
        for (int i = 0; i < count; i++)
        {
            array2[i] = reader.ReadDouble();
        }
        await Assert.That(array2).IsEquivalentTo(array);
    }

    [Test]
    [Arguments(0d, "00-00-00-00-00-00-00-00")]
    [Arguments(1d, "3F-F0-00-00-00-00-00-00")]
    [Arguments(double.Epsilon, "00-00-00-00-00-00-00-01")]
    [Arguments(double.MaxValue, "7F-EF-FF-FF-FF-FF-FF-FF")]
    [Arguments(double.MinValue, "FF-EF-FF-FF-FF-FF-FF-FF")]
    [Arguments(double.NaN, "FF-F8-00-00-00-00-00-00")]
    public async Task WriteDouble_Hex(double value, string expected)
    {
        var writer = GetWriter();
        writer.WriteDouble(value);
        writer.Flush();

        var array = GetOutput();
        await Assert.That(Hex(array)).IsEqualTo(expected);
        var decoded = GetReader().ReadDouble();
        await Assert.That(decoded).IsEqualTo(value);
    }

    #endregion Double

    #region Single

    [Test]
    [Arguments(1)]
    [Arguments(10_000)]
    public async Task WriteSingle_RoundTrip(int count)
    {
        var array = new float[count];
        var writer = GetWriter();
        for (int i = 0; i < count; i++)
        {
            array[i] = (float)rng.NextDouble();
            writer.WriteSingle(array[i]);
        }
        writer.Flush();

        var reader = GetReader();
        var array2 = new float[count];
        for (int i = 0; i < count; i++)
        {
            array2[i] = reader.ReadSingle();
        }
        await Assert.That(array2).IsEquivalentTo(array);
    }

    [Test]
    [Arguments(0f, "00-00-00-00")]
    [Arguments(1f, "3F-80-00-00")]
    [Arguments(float.Epsilon, "00-00-00-01")]
    [Arguments(float.MaxValue, "7F-7F-FF-FF")]
    [Arguments(float.MinValue, "FF-7F-FF-FF")]
    [Arguments(float.NaN, "FF-C0-00-00")]
    public async Task WriteSingle_Hex(float value, string expected)
    {
        var writer = GetWriter();
        writer.WriteSingle(value);
        writer.Flush();

        var array = GetOutput();
        await Assert.That(Hex(array)).IsEqualTo(expected);
        var decoded = GetReader().ReadSingle();
        await Assert.That(decoded).IsEqualTo(value);
    }

    #endregion Single

    #endregion Floats

    #region Other

    #region String

    [Test]
    [Arguments(1)]
    [Arguments(10_000)]
    public async Task WriteString_RoundTrip(int count)
    {
        var array = new string[count];
        var writer = GetWriter();
        for (int i = 0; i < count; i++)
        {
            array[i] = rng.NextString();
            writer.WriteString(array[i]);
        }
        writer.Flush();

        var reader = GetReader();
        var array2 = new string[count];
        for (int i = 0; i < count; i++)
        {
            array2[i] = reader.ReadString();
        }
        await Assert.That(array2).IsEquivalentTo(array);
    }

    [Test]
    [Arguments(1)]
    [Arguments(100)]
    [Arguments(10_000)]
    [Arguments(30_000)]
    public async Task WriteString_Size(int length)
    {
        string value = rng.NextString(length);
        var writer = GetWriter();
        writer.WriteString(value);
        writer.Flush();

        var decoded = GetReader().ReadString();
        await Assert.That(decoded).IsEqualTo(value);
    }

    [Test]
    [Arguments("", "00")]
    [Arguments("\0", "01-00")]
    [Arguments("\xff", "02-C3-BF")]
    [Arguments("Hello, World!", "0D-48-65-6C-6C-6F-2C-20-57-6F-72-6C-64-21")]
    public async Task WriteString_Hex(string value, string expected)
    {
        var writer = GetWriter();
        writer.WriteString(value);
        writer.Flush();

        var array = GetOutput();
        await Assert.That(Hex(array)).IsEqualTo(expected);
        var decoded = GetReader().ReadString();
        await Assert.That(decoded).IsEqualTo(value);
    }

    #endregion String

    #region Guid

    [Test]
    [Repeat(10)]
    public async Task WriteGuid_Hex()
    {
        Guid guid = Guid.NewGuid();
        byte sizeOf = (byte)Unsafe.SizeOf<Guid>();
        string expected = $"{sizeOf:X}{guid:N}".ToUpperInvariant();
        var writer = GetWriter();
        DefaultContracts.Guid.Serialize(ref writer, in guid);
        writer.Flush();

        var output = GetOutput().ToHex();
        await Assert.That(output).IsEqualTo(expected);
    }

    [Test]
    [Repeat(10)]
    public async Task WriteGuid_RoundTrip()
    {
        Guid guid = Guid.NewGuid();
        var writer = GetWriter();
        DefaultContracts.Guid.Serialize(ref writer, in guid);
        writer.Flush();

        var reader = GetReader();
        DefaultContracts.Guid.Deserialize(ref reader, out var decoded);
        await Assert.That(decoded).IsEqualTo(guid);
    }

    #endregion Guid

    #endregion Other

    #region Unmanaged

    [Test]
    [Arguments(byte.MaxValue, "FF")]
    [Arguments(uint.MinValue, "00-00-00-00")]
    //[Arguments(TimeSpan.Zero, "00-00-00-00-00-00-00-00")]
#pragma warning disable TUnit0300 // Generic type or method may not be AOT-compatible
    public async Task WriteUnsafe_Hex<T>(T value, string expected) where T : unmanaged
#pragma warning restore TUnit0300 // Generic type or method may not be AOT-compatible
    {
        var writer = GetWriter();
        writer.WriteUnsafe(in value);
        writer.Flush();

        var array = GetOutput();
        await Assert.That(array).HasCount(Unsafe.SizeOf<T>());
        await Assert.That(Hex(array)).IsEqualTo(expected);
    }

    private const int TestBufferSize = 40;

    private unsafe struct Buffer1
    {
        private fixed byte _x[TestBufferSize];
        public ref byte Byte => ref _x[0];
    }

    [Test]
    public async Task WriteUnsafe_FixedSizeBuffer()
    {
        var value = new Buffer1
        {
            Byte = 0xFF
        };

        var writer = GetWriter();
        writer.WriteUnsafe(in value);
        writer.Flush();

        var array = GetOutput();
        await Assert.That(array).HasCount(TestBufferSize);
        await Assert.That(array[0]).IsEqualTo((byte)0xFF);
        await Assert.That(array.Skip(1)).ContainsOnly(b => b == 0);
    }

#if NET8_0_OR_GREATER
    [InlineArray(TestBufferSize)]
    private struct Buffer2
    {
        private byte _element0;
    }

    [Test]
    public async Task WriteUnsafe_InlineArray()
    {
        var value = new Buffer2();
        value[0] = 0xFF;

        var writer = GetWriter();
        writer.WriteUnsafe(in value);
        writer.Flush();

        var array = GetOutput();
        await Assert.That(array).HasCount(TestBufferSize);
        await Assert.That(array[0]).IsEqualTo((byte)0xFF);
        await Assert.That(array.Skip(1)).ContainsOnly(b => b == 0);
    }
#endif

    #endregion Unmanaged
}
