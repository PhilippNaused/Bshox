using System.Buffers;
using System.Diagnostics.CodeAnalysis;

namespace Bshox.TestUtils;


[ExcludeFromCodeCoverage]
public static class SequenceSegmenter
{
    public static ReadOnlySequence<byte> MakeSegmentedSequence(ReadOnlyMemory<byte> memory, int segmentSize)
    {
        if (memory.Length <= segmentSize)
        {
            return new ReadOnlySequence<byte>(memory);
        }

        var segments = new List<MemorySegment>();
        var remaining = memory.Length;
        var offset = 0;

        while (remaining > 0)
        {
            var size = Math.Min(segmentSize, remaining);
            var segment = new MemorySegment(memory.Slice(offset, size), offset);
            segments.LastOrDefault()?.SetNext(segment);
            segments.Add(segment);
            offset += size;
            remaining -= size;
        }

        return new ReadOnlySequence<byte>(segments.First(), 0, segments.Last(), segments.Last().Memory.Length);
    }

    private sealed class MemorySegment : ReadOnlySequenceSegment<byte>
    {
        public MemorySegment(ReadOnlyMemory<byte> memory, long index)
        {
            Memory = memory;
            RunningIndex = index;
        }

        public void SetNext(MemorySegment segment)
        {
            Next = segment;
        }
    }
}
