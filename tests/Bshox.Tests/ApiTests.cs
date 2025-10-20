using Bshox.Attributes;
using Bshox.TestUtils;

namespace Bshox.Tests;

[BshoxSerializer(typeof(int[]))]
internal partial class TestSerializer;

internal class ApiTests
{
    private static readonly int[] s_Array = [1, 2, 3, 4, 5];

    // 40 = (byte)BshoxCode.VarInt ^ (s_Array.Length << 3)
    private static readonly byte[] s_Expected = [40, 1, 2, 3, 4, 5];

    [Test]
    public async Task MissingContract()
    {
        await Assert.That(delegate
        {
            return TestSerializer.Instance.GetContract<long[]>();
        }).Throws<BshoxException>().WithMessage("No serialization contract for \"System.Int64[]\" could be found.");
    }

    [Test]
    public async Task SerializeToArray()
    {
        byte[] result = TestSerializer.Int32Array.Serialize(s_Array);
        await Assert.That(result).IsSequenceEqualTo(s_Expected);
    }

    [Test]
    public async Task SerializeToArray2()
    {
        byte[] result = TestSerializer.Instance.Serialize(s_Array, typeof(int[]));
        await Assert.That(result).IsSequenceEqualTo(s_Expected);
    }

    [Test]
    public async Task SerializeToMemoryStream()
    {
        var stream = new MemoryStream();
        TestSerializer.Int32Array.Serialize(stream, s_Array);
        await Assert.That(stream.ToArray()).IsSequenceEqualTo(s_Expected);
    }

    [Test]
    public async Task SerializeToNonSeekingStream()
    {
        var ms = new MemoryStream();
        using var stream = new NonSeekableStream(ms);
        TestSerializer.Int32Array.Serialize(stream, s_Array);
        await Assert.That(ms.ToArray()).IsSequenceEqualTo(s_Expected);
    }

    [Test]
    public async Task SerializeToMemoryStream2()
    {
        var stream = new MemoryStream();
        TestSerializer.Instance.Serialize(stream, s_Array, typeof(int[]));
        await Assert.That(stream.ToArray()).IsSequenceEqualTo(s_Expected);
    }

    [Test]
    public async Task SerializeToBufferedStream()
    {
        var ms = new MemoryStream();
        using var stream = new BufferedStream(ms, 1);
        TestSerializer.Int32Array.Serialize(stream, s_Array);
        await stream.FlushAsync();
        await Assert.That(ms.ToArray()).IsSequenceEqualTo(s_Expected);
    }

    [Test]
    public async Task SerializeToBufferedStream2()
    {
        var ms = new MemoryStream();
        using var stream = new BufferedStream(ms, 1);
        TestSerializer.Instance.Serialize(stream, s_Array, typeof(int[]));
        await stream.FlushAsync();
        await Assert.That(ms.ToArray()).IsSequenceEqualTo(s_Expected);
    }

    [Test]
    public async Task SerializeToBufferWriter()
    {
        using var writer = new Internals.PooledByteBufferWriter();
        TestSerializer.Int32Array.Serialize(writer, s_Array);
        await Assert.That(writer.ToArray()).IsSequenceEqualTo(s_Expected);
    }

    [Test]
    public async Task SerializeToBufferWriter2()
    {
        using var writer = new Internals.PooledByteBufferWriter();
        TestSerializer.Instance.Serialize(writer, s_Array, typeof(int[]));
        await Assert.That(writer.ToArray()).IsSequenceEqualTo(s_Expected);
    }

    [Test]
    public async Task DeserializeFromMemory()
    {
        var mem = s_Expected.AsMemory();
        int[] result = TestSerializer.Int32Array.Deserialize(mem);
        await Assert.That(result).IsSequenceEqualTo(s_Array);
    }

    [Test]
    public async Task DeserializeFromMemory2()
    {
        var mem = s_Expected.AsMemory();
        var result = (int[])TestSerializer.Instance.Deserialize(mem, typeof(int[]));
        await Assert.That(result).IsSequenceEqualTo(s_Array);
    }

    [Test]
    public async Task DeserializeFromStream()
    {
        var stream = new MemoryStream(s_Expected);
        int[] result = TestSerializer.Int32Array.Deserialize(stream);
        await Assert.That(result).IsSequenceEqualTo(s_Array);
    }

    [Test]
    public async Task DeserializeFromNonSeekingStream()
    {
        var ms = new MemoryStream(s_Expected);
        using var stream = new NonSeekableStream(ms);
        int[] result = TestSerializer.Int32Array.Deserialize(stream);
        await Assert.That(result).IsSequenceEqualTo(s_Array);
    }

    [Test]
    [Arguments(false)]
    [Arguments(true)]
    public async Task DeserializeFromMemoryStreamOffset(bool publiclyVisible)
    {
        const int offset = 70;
        byte[] offsetArray = new byte[offset + s_Expected.Length + 42];
        s_Expected.CopyTo(offsetArray, offset);
        const int offset2 = 20;
        var stream = new MemoryStream(offsetArray, offset - offset2, s_Expected.Length + offset2, true, publiclyVisible);
        stream.Position += offset2; // skip some bytes
        int[] result = TestSerializer.Int32Array.Deserialize(stream);
        await Assert.That(stream.Position).IsEqualTo(s_Expected.Length + offset2);
        await Assert.That(result).IsSequenceEqualTo(s_Array);
    }

    [Test]
    public async Task DeserializeFromStream2()
    {
        var stream = new MemoryStream(s_Expected);
#pragma warning disable CA1849 // Call async methods when in an async method
        var result = (int[])TestSerializer.Instance.Deserialize(stream, typeof(int[]));
#pragma warning restore CA1849 // Call async methods when in an async method
        await Assert.That(result).IsSequenceEqualTo(s_Array);
    }

    [Test]
    public async Task DeserializeFromStreamAsync()
    {
        var stream = new MemoryStream(s_Expected);
        var result = await TestSerializer.Int32Array.DeserializeAsync(stream);
        await Assert.That(result).IsSequenceEqualTo(s_Array);
    }

    [Test]
    public async Task DeserializeFromStreamAsync2()
    {
        var stream = new MemoryStream(s_Expected);
        var result = (int[])await TestSerializer.Instance.DeserializeAsync(stream, typeof(int[]));
        await Assert.That(result).IsSequenceEqualTo(s_Array);
    }

    [Test]
    public async Task DeserializeFromBufferedStream()
    {
        var stream = new MemoryStream(s_Expected);
        using var buffered = new BufferedStream(stream, 1);
        int[] result = TestSerializer.Int32Array.Deserialize(buffered);
        await Assert.That(result).IsSequenceEqualTo(s_Array);
    }

    [Test]
    public async Task DeserializeFromBufferedStream2()
    {
        var stream = new MemoryStream(s_Expected);
        using var buffered = new BufferedStream(stream, 1);
#pragma warning disable CA1849 // Call async methods when in an async method
        var result = (int[])TestSerializer.Instance.Deserialize(buffered, typeof(int[]));
#pragma warning restore CA1849 // Call async methods when in an async method
        await Assert.That(result).IsSequenceEqualTo(s_Array);
    }

    [Test]
    public async Task DeserializeFromBufferedStreamAsync()
    {
        var stream = new MemoryStream(s_Expected);
        using var buffered = new BufferedStream(stream, 1);
        int[] result = await TestSerializer.Int32Array.DeserializeAsync(buffered);
        await Assert.That(result).IsSequenceEqualTo(s_Array);
    }

    [Test]
    public async Task DeserializeFromBufferedStreamAsync2()
    {
        var stream = new MemoryStream(s_Expected);
        using var buffered = new BufferedStream(stream, 1);
        var result = (int[])await TestSerializer.Instance.DeserializeAsync(buffered, typeof(int[]));
        await Assert.That(result).IsSequenceEqualTo(s_Array);
    }

    [Test]
    public async Task DeserializeFromReadOnlySequence()
    {
        var sequence = SequenceSegmenter.MakeSegmentedSequence(s_Expected, 1);
        int[] result = TestSerializer.Int32Array.Deserialize(sequence);
        await Assert.That(result).IsSequenceEqualTo(s_Array);
    }

    [Test]
    public async Task DeserializeFromReadOnlySequence2()
    {
        var sequence = SequenceSegmenter.MakeSegmentedSequence(s_Expected, 1);
        var result = (int[])TestSerializer.Instance.Deserialize(sequence, typeof(int[]));
        await Assert.That(result).IsSequenceEqualTo(s_Array);
    }
}
