using System.Buffers;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;

namespace Bshox.Internals;

/// <summary>
/// Inspired by the internal .NET implementation of PooledByteBufferWriter.
/// <see href="https://github.com/dotnet/runtime/blob/release/8.0/src/libraries/Common/src/System/Text/Json/PooledByteBufferWriter.cs"/>
/// </summary>
internal sealed class PooledByteBufferWriter : IBufferWriter<byte>, IDisposable
{
    private sealed class Segment : ReadOnlySequenceSegment<byte>
    {
        internal readonly byte[] Buffer;

        public Segment(long index, byte[] buffer, int length)
        {
            Buffer = buffer;
            Memory = buffer.AsMemory(0, length);
            RunningIndex = index;
        }

        public int Length => Memory.Length;

        internal void SetNext(Segment segment)
        {
            Next = segment;
        }
    }

    private readonly List<Segment> _segments = [];
    private byte[] _buffer;
    private int _index;
    private long _segLength;

    private const int MinimumBufferSize = 0;
    private const int DefaultBufferSize = 16 * 1024;

    // Value copied from Array.MaxLength in System.Private.CoreLib/src/libraries/System.Private.CoreLib/src/System/Array.cs.
    public const int MaximumBufferSize = 0X7FFFFFC7;

    public PooledByteBufferWriter(int initialCapacity = DefaultBufferSize)
    {
        Debug.Assert(initialCapacity > 0, "initialCapacity > 0");

        _buffer = ArrayPool<byte>.Shared.Rent(initialCapacity);
    }

    public ReadOnlySequence<byte> GetReadOnlySequence()
    {
        if (_segments.Count == 0)
        {
            return new ReadOnlySequence<byte>(_buffer, 0, _index);
        }
        var first = _segments[0];
        var last = _segments[^1];
        var lastSegment = new Segment(_segLength, _buffer, _index);
        last.SetNext(lastSegment);
        return new ReadOnlySequence<byte>(first, 0, lastSegment, lastSegment.Length);
    }

    public byte[] ToArray()
    {
        var array = new byte[Length];
        int offset = 0;
        if (_segments.Count > 0)
        {
            foreach (var segment in _segments)
            {
                Buffer.BlockCopy(segment.Buffer, 0, array, offset, segment.Length);
                offset += segment.Length;
            }
        }
        Buffer.BlockCopy(_buffer, 0, array, offset, _index);
        return array;
    }

    public long Length => _segLength + _index;

    /// <summary>
    /// Returns the rented buffer back to the pool
    /// </summary>
    public void Dispose()
    {
        ArrayPool<byte>.Shared.Return(_buffer, true);
        _buffer = [];
        _index = 0;

        foreach (var segment in _segments)
        {
            ArrayPool<byte>.Shared.Return(segment.Buffer, true);
        }
        _segments.Clear();
        _segLength = 0;
    }

    public void Advance(int count)
    {
        Debug.Assert(count >= 0, "count >= 0");
        Debug.Assert(_index <= _buffer.Length - count, $"{_index} <= {_buffer.Length} - {count}");
        _index += count;
    }

    [ExcludeFromCodeCoverage]
    Memory<byte> IBufferWriter<byte>.GetMemory(int sizeHint)
    {
        Debug.Fail("Dead code!");
        CheckAndResizeBuffer(sizeHint);
        return _buffer.AsMemory(_index);
    }

    public Span<byte> GetSpan(int sizeHint = MinimumBufferSize)
    {
        CheckAndResizeBuffer(sizeHint);
        return _buffer.AsSpan(_index);
    }

    internal void WriteToStream(Stream destination)
    {
        foreach (var segment in _segments)
        {
            destination.Write(segment.Buffer, 0, segment.Length);
        }
        destination.Write(_buffer, 0, _index);
    }

#if NETCOREAPP
    internal async ValueTask WriteToStreamAsync(Stream destination, CancellationToken cancellationToken)
    {
        foreach (var segment in _segments)
        {
            await destination.WriteAsync(segment.Memory, cancellationToken).ConfigureAwait(false);
        }
        await destination.WriteAsync(_buffer.AsMemory(_index), cancellationToken).ConfigureAwait(false);
    }
#else
    internal async Task WriteToStreamAsync(Stream destination, CancellationToken cancellationToken)
    {
        foreach (var segment in _segments)
        {
            await destination.WriteAsync(segment.Buffer, 0, segment.Length, cancellationToken).ConfigureAwait(false);
        }
        await destination.WriteAsync(_buffer, 0, _index, cancellationToken).ConfigureAwait(false);
    }
#endif

    private void CheckAndResizeBuffer(int sizeHint)
    {
        Debug.Assert(sizeHint >= 0, "sizeHint >= 0");
        Debug.Assert(_buffer.Length >= _index, "_buffer.Length >= _index");

        int currentLength = _buffer.Length;
        int availableSpace = currentLength - _index;

        if (sizeHint > availableSpace)
        {
            ResizeBuffer(sizeHint);
        }
    }

    [MethodImpl(MethodImplOptions.NoInlining)] // cold path
    private void ResizeBuffer(int sizeHint)
    {
        sizeHint = Math.Max(sizeHint, DefaultBufferSize);

        var segment = new Segment(_segLength, _buffer, _index);
        if (_segments.Count > 0)
        {
            var last = _segments[^1];
            Debug.Assert(last is not null, "last is not null");
            last!.SetNext(segment);
        }
        else
        {
            Debug.Assert(_segments.Count == 0, "_segments.Count == 0");
            Debug.Assert(_segLength == 0, "_segLength == 0");
        }

        _segments.Add(segment);
        _segLength += _index;

        _buffer = ArrayPool<byte>.Shared.Rent(sizeHint);
        _index = 0;

        Debug.Assert(_buffer.Length >= sizeHint, "_buffer.Length >= sizeHint");
    }
}
