using System.Buffers;
using System.Runtime.CompilerServices;

namespace Bshox.TestUtils;

/// <summary>
/// A simple implementation of <see cref="IBufferWriter{T}"/> that writes to a fixed-size buffer.
/// </summary>
/// <remarks>
/// Useful for testing and benchmarking where you want to avoid the overhead of memory allocation.
/// </remarks>
public sealed class FixedBufferWriter(Memory<byte> memory) : IBufferWriter<byte>
{
    public FixedBufferWriter(int size) : this(new byte[size]) { }
    public FixedBufferWriter() : this(16 * 1024) { }

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
        if (sizeHint <= _memory.Length)
        {
            return _memory;
        }

        throw Error(sizeHint, _memory.Length);
        [MethodImpl(MethodImplOptions.NoInlining)]
        static InvalidOperationException Error(int sizeHint, int available) => new($"Not enough space in the buffer. Required: {sizeHint}, Available: {available}");
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
