// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using System.Buffers;
using System.Diagnostics;

namespace Bshox.Internals;

/// <summary>
/// <see href="https://github.com/dotnet/runtime/blob/release/8.0/src/libraries/Common/src/System/Text/Json/PooledByteBufferWriter.cs"/>
/// </summary>
internal sealed class PooledByteBufferWriter : IBufferWriter<byte>, IDisposable
{
    private byte[] _buffer;
    private int _index;

    private const int MinimumBufferSize = 256;

    // Value copied from Array.MaxLength in System.Private.CoreLib/src/libraries/System.Private.CoreLib/src/System/Array.cs.
    public const int MaximumBufferSize = 0X7FFFFFC7;

    public PooledByteBufferWriter(int initialCapacity = MinimumBufferSize)
    {
        Debug.Assert(initialCapacity > 0, "initialCapacity > 0");

        _buffer = ArrayPool<byte>.Shared.Rent(initialCapacity);
        _index = 0;
    }

    public ReadOnlyMemory<byte> WrittenMemory
    {
        get
        {
            Debug.Assert(_index <= _buffer.Length, "_index <= _buffer.Length");
            return _buffer.AsMemory(0, _index);
        }
    }

    public int WrittenCount => _index;

    public int Capacity => _buffer.Length;

    public int FreeCapacity => _buffer.Length - _index;

    /// <summary>
    /// Returns the rented buffer back to the pool
    /// </summary>
    public void Dispose()
    {
        if (_buffer == Array.Empty<byte>())
        {
            return;
        }

        byte[] toReturn = _buffer;
        _buffer = [];
        _index = 0;
        ArrayPool<byte>.Shared.Return(toReturn, true);
    }

    public void Advance(int count)
    {
        Debug.Assert(count >= 0, "count >= 0");
        Debug.Assert(_index <= _buffer.Length - count, $"{_index} <= {_buffer.Length} - {count}");
        _index += count;
    }

    public Memory<byte> GetMemory(int sizeHint = MinimumBufferSize)
    {
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
#if NETCOREAPP
        destination.Write(WrittenMemory.Span);
#else
        destination.Write(_buffer, 0, _index);
#endif
    }

#if NETCOREAPP
    internal ValueTask WriteToStreamAsync(Stream destination, CancellationToken cancellationToken)
    {
        return destination.WriteAsync(WrittenMemory, cancellationToken);
    }
#else
    internal Task WriteToStreamAsync(Stream destination, CancellationToken cancellationToken)
    {
        return destination.WriteAsync(_buffer, 0, _index, cancellationToken);
    }
#endif

    private void CheckAndResizeBuffer(int sizeHint)
    {
        Debug.Assert(sizeHint >= 0, "sizeHint >= 0");

        int currentLength = _buffer.Length;
        int availableSpace = currentLength - _index;

        sizeHint = Math.Max(sizeHint, MinimumBufferSize);

        // If we've reached ~1GB written, grow to the maximum buffer
        // length to avoid incessant minimal growths causing perf issues.
        if (_index >= MaximumBufferSize / 2)
        {
            sizeHint = Math.Max(sizeHint, MaximumBufferSize - currentLength);
        }

        if (sizeHint > availableSpace)
        {
            int growBy = Math.Max(sizeHint, currentLength);

            int newSize = currentLength + growBy;

            if ((uint)newSize > MaximumBufferSize)
            {
                newSize = currentLength + sizeHint;
            }

            byte[] oldBuffer = _buffer;

            _buffer = ArrayPool<byte>.Shared.Rent(newSize);

            Debug.Assert(oldBuffer.Length >= _index, "oldBuffer.Length >= _index");
            Debug.Assert(_buffer.Length >= _index, "_buffer.Length >= _index");

            Span<byte> oldBufferAsSpan = oldBuffer.AsSpan(0, _index);
            oldBufferAsSpan.CopyTo(_buffer);
            oldBufferAsSpan.Clear();
            ArrayPool<byte>.Shared.Return(oldBuffer);
        }

        Debug.Assert(_buffer.Length - _index > 0, "_buffer.Length - _index > 0");
        Debug.Assert(_buffer.Length - _index >= sizeHint, "_buffer.Length - _index >= sizeHint");
    }
}
