using System.Buffers;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Bshox.Internals;

namespace Bshox;

/// <summary>
/// Forward-only wrapper around <see cref="ReadOnlySequence{T}"/> for reading Bshox data.
/// </summary>
public ref partial struct BshoxReader
{
    private ReadOnlySpan<byte> _span;

    private readonly ReadOnlySequence<byte> _sequence;

    private SequencePosition _next;

    private readonly bool _usingSequence;

    private bool _moreData;

    private readonly BshoxOptions _options;

    private int _depth;

    /// <summary>
    /// Gets the total number of bytes processed by the reader.
    /// </summary>
    public long Consumed { get; private set; }

    public readonly long Remaining => Length - Consumed;

    public long Length { get; }

    public readonly int CurrentDepth => _depth;

    private BshoxReader(BshoxOptions? options)
    {
        _options = options ?? BshoxOptions.Default;
    }

    public BshoxReader(ReadOnlySequence<byte> sequence, BshoxOptions? options = null) : this(options)
    {
        Consumed = 0;
        if (sequence.IsSingleSegment)
        {
            _usingSequence = false;
            _span = sequence.First.Span;
            Length = _span.Length;
            _moreData = !_span.IsEmpty;
            return;
        }
        _usingSequence = true;
        _sequence = sequence;
        _next = sequence.Start;
        Length = sequence.Length;
        _moreData = true;
        GetNextSpan();
        Debug.Assert(_moreData != _span.IsEmpty, "_moreData != _span.IsEmpty");
    }

    public BshoxReader(ReadOnlyMemory<byte> memory, BshoxOptions? options = null) : this(options)
    {
        Consumed = 0;
        _usingSequence = false;
        Length = memory.Length;
        _span = memory.Span;
        _moreData = !memory.IsEmpty;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal unsafe T ReadUnsafe<T>() where T : unmanaged
#if NET9_0_OR_GREATER
        , allows ref struct
#endif
    {
        if (_span.Length >= sizeof(T))
        {
            T value = Unsafe.ReadUnaligned<T>(ref MemoryMarshal.GetReference(_span));
            Advance(sizeof(T));
            return value;
        }

        return ReadUnsafeSlow<T>();
    }

    /// <summary>
    /// Reads a length-prefixed UTF-8 encoded string.
    /// </summary>
    public string ReadString()
    {
        int byteLength = checked((int)ReadVarInt32());

        if (_span.Length >= byteLength)
        {
            // Fast path: all bytes to decode appear in the same span.
            string value = EncodingHelper.Utf8NoBom.GetString(_span.Slice(0, byteLength));
            Advance(byteLength);
            return value;
        }

        return ReadStringSlow(byteLength);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private readonly void CheckBufferSize(long byteLength)
    {
        if (Remaining < byteLength)
        {
            throw EndOfStream();
        }
    }

    private string ReadStringSlow(int byteLength)
    {
        CheckBufferSize(byteLength);

        // We need to decode bytes incrementally across multiple spans.
        int maxCharLength = EncodingHelper.Utf8NoBom.GetMaxCharCount(byteLength);
        char[] charArray = ArrayPool<char>.Shared.Rent(maxCharLength);
        System.Text.Decoder decoder = EncodingHelper.Utf8NoBom.GetDecoder();

        int remainingByteLength = byteLength;
        int initializedChars = 0;
        while (remainingByteLength > 0)
        {
            int bytesRead = Math.Min(remainingByteLength, _span.Length);
            remainingByteLength -= bytesRead;
            bool flush = remainingByteLength == 0;
#if NETCOREAPP
            initializedChars += decoder.GetChars(_span.Slice(0, bytesRead), charArray.AsSpan(initializedChars), flush);
#else
            unsafe
            {
                fixed (byte* pUnreadSpan = _span)
                fixed (char* pCharArray = &charArray[initializedChars])
                {
                    initializedChars += decoder.GetChars(pUnreadSpan, bytesRead, pCharArray, charArray.Length - initializedChars, flush);
                }
            }
#endif
            Advance(bytesRead);
        }

        string value = new(charArray, 0, initializedChars);
        ArrayPool<char>.Shared.Return(charArray);
        return value;
    }

    private unsafe T ReadUnsafeSlow<T>() where T : unmanaged
#if NET9_0_OR_GREATER
        , allows ref struct
#endif
    {
        Debug.Assert(_span.Length < sizeof(T), "_span.Length < sizeof(T)");
        CheckBufferSize(sizeof(T));
        T value = default;
        Span<byte> span = new(&value, sizeof(T));
        CopyToSlow(span);
        return value;
    }

    /// <summary>
    /// Read the next byte and advances the reader by <c>1</c>.
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public byte ReadByte()
    {
        if (!_moreData)
        {
            throw EndOfStream();
        }

        Debug.Assert(!_span.IsEmpty, "!_span.IsEmpty");
        byte value = _span[0];
        _span = _span.Slice(1);
        Consumed++;

        if (_span.IsEmpty)
        {
            if (_usingSequence)
            {
                GetNextSpan();
            }
            else
            {
                _moreData = false;
            }
        }

        return value;
    }

    private void GetNextSpan()
    {
        Debug.Assert(_moreData, nameof(_moreData));
        Debug.Assert(_usingSequence, nameof(_usingSequence));
        while (TryMoveNext())
        {
            if (!_span.IsEmpty)
            {
                return;
            }
        }
        _span = [];
        _moreData = false;
    }

    private bool TryMoveNext()
    {
        if (_next.GetObject() == null)
        {
            return false;
        }

        bool success = _sequence.TryGet(ref _next, out var memory);
        if (success)
        {
            _span = memory.Span;
        }
        return success;
    }

    /// <summary>
    /// Move the reader ahead by the specified number of bytes.
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Advance(int count)
    {
        if (_span.Length > count && count >= 0)
        {
            _span = _span.Slice(count);
            Consumed += count;
        }
        else if (_usingSequence)
        {
            AdvanceSlow(count);
        }
        else if (_span.Length == count)
        {
            _span = [];
            Consumed += count;
            _moreData = false;
        }
        else
        {
#if NET8_0_OR_GREATER
            ArgumentOutOfRangeException.ThrowIfNegative(count);
#else
            if (count < 0)
                throw new ArgumentOutOfRangeException(nameof(count));
#endif
            throw EndOfStream();
        }
    }

    private void AdvanceSlow(int count)
    {
        Debug.Assert(_usingSequence, nameof(_usingSequence));
#if NET8_0_OR_GREATER
        ArgumentOutOfRangeException.ThrowIfNegative(count);
#else
        if (count < 0)
            throw new ArgumentOutOfRangeException(nameof(count));
#endif

        Consumed += count;
        while (_moreData)
        {
            int remaining = _span.Length;

            if (remaining > count)
            {
                _span = _span.Slice(count);
                count = 0;
                break;
            }

            count -= remaining;
            Debug.Assert(count >= 0, "count >= 0");

            GetNextSpan();

            if (count == 0)
            {
                break;
            }
        }

        if (count != 0)
        {
            throw EndOfStream();
        }
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void CopyTo(Span<byte> destination)
    {
        if (_span.Length >= destination.Length)
        {
            _span.Slice(0, destination.Length).CopyTo(destination);
            _span = _span.Slice(destination.Length);
            Consumed += destination.Length;
            if (_span.IsEmpty)
            {
                if (_usingSequence)
                    GetNextSpan();
                else
                    _moreData = false;
            }
            return;
        }
        if (_usingSequence)
        {
            CopyToSlow(destination);
            return;
        }
        throw EndOfStream();
    }

    private void CopyToSlow(Span<byte> destination)
    {
        Debug.Assert(_usingSequence, nameof(_usingSequence));
        CheckBufferSize(destination.Length);

        Debug.Assert(_span.Length < destination.Length, "_span.Length < destination.Length");
        _span.CopyTo(destination);
        int copied = _span.Length;

        while (_moreData)
        {
            GetNextSpan();
            if (!_moreData)
            {
                Debug.Assert(copied < destination.Length, "copied < destination.Length");
                throw EndOfStream();
            }
            int toCopy = Math.Min(_span.Length, destination.Length - copied);
            _span.Slice(0, toCopy).CopyTo(destination.Slice(copied));
            copied += toCopy;
            Debug.Assert(copied <= destination.Length, "copied <= destination.Length");
            if (copied == destination.Length)
            {
                _span = _span.Slice(toCopy);
                if (_span.IsEmpty)
                    GetNextSpan();
                Consumed += copied;
                return;
            }
        }

        Debug.Fail("Ran into dead code");
        throw EndOfStream();
    }

    private static BshoxException EndOfStream()
    {
        var inner = new EndOfStreamException();
        return new(inner.Message, inner);
    }

#pragma warning disable CS0618 // Type or member is obsolete
    public DepthLockScope DepthLock() => DepthLockScope.Create(ref _depth, _options.MaxDepth);
#pragma warning restore CS0618 // Type or member is obsolete
}
