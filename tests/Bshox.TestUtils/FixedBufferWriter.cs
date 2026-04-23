using System.Buffers;

namespace Bshox.TestUtils;

/// <summary>
/// A simple implementation of <see cref="IBufferWriter{T}"/> that writes to a fixed-size buffer.
/// </summary>
/// <remarks>
/// Useful for testing and benchmarking where you want to avoid the overhead of memory allocation.
/// </remarks>
public sealed class FixedBufferWriter(Memory<byte> memory) : IBufferWriter<byte>
{
    private Memory<byte> _memory = memory;
    private readonly Memory<byte> _originalMemory = memory;

    /// <inheritdoc />
    public void Advance(int count)
    {
        _memory = _memory.Slice(count);
    }

    /// <inheritdoc />
    public Memory<byte> GetMemory(int sizeHint = 0)
    {
        if (sizeHint > _memory.Length)
        {
            throw new InvalidOperationException($"Not enough space in the buffer. Required: {sizeHint}, Available: {_memory.Length}");
        }

        return _memory;
    }

    /// <inheritdoc />
    public Span<byte> GetSpan(int sizeHint = 0)
    {
        return GetMemory(sizeHint).Span;
    }

    public void Reset()
    {
        _memory = _originalMemory;
    }
}
