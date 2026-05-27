using System.Buffers;
using System.ComponentModel;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Bshox.Internals;

namespace Bshox;

/// <summary>
/// Forward-only wrapper around <see cref="ReadOnlySequence{T}"/> for reading Bshox data.
/// </summary>
public ref partial struct BshoxReader
{
#if REF_FIELD
    // This should be a "ref readonly" field, but that messes with some of the melter methods we use.
    private ref byte _ref; // reference to the current position in the data
    private int _length; // length of the remaining data starting from _ref
#else
    private ReadOnlySpan<byte> _span;
#endif

    private readonly int SpanLength
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#if REF_FIELD
        get => _length;
#else
        get => _span.Length;
#endif
    }

    // This should be a "ref readonly", but that messes with some of the melter methods we use.
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private readonly ref byte GetRef()
    {
#if REF_FIELD
        return ref _ref;
#else
        return ref MemoryMarshal.GetReference(_span);
#endif
    }

    private readonly ReadOnlySpan<byte> GetSpan(int length)
    {
        Debug.Assert(length >= 0, "length >= 0");
#if REF_FIELD
        return MemoryMarshal.CreateReadOnlySpan(in _ref, length);
#else
        return _span.Slice(0, length);
#endif
    }

    private readonly ReadOnlySequence<byte> _sequence;

    private SequencePosition _next;

    private readonly bool _usingSequence;

    private bool _moreData;

    private int _depth;

    /// <summary>
    /// Gets the total number of bytes processed by the reader.
    /// </summary>
    public long Consumed { readonly get; private set; }

    /// <summary>
    /// Gets the number of bytes remaining to be read.
    /// </summary>
    public readonly long Remaining => Length - Consumed;

    /// <summary>
    /// The total length of the data being read.
    /// </summary>
    public readonly long Length { get; }

    /// <summary>
    /// The current depth of nested objects and arrays.<br/>
    /// If this value exceeds <see cref="BshoxOptions.MaxDepth"/>, a <see cref="BshoxException"/> will be thrown.
    /// </summary>
    public readonly int CurrentDepth => _depth;

    /// <summary>
    /// The options used by this reader.
    /// </summary>
    public readonly BshoxOptions Options { get; }

    private BshoxReader(BshoxOptions? options)
    {
        Options = options ?? BshoxOptions.Default;
    }

    /// <summary>
    /// Creates a new reader that reads from the specified <paramref name="sequence"/>.
    /// </summary>
    /// <param name="sequence">The sequence of bytes to read from.</param>
    /// <param name="options">Options to customize the deserialization process. If <see langword="null"/>, <see cref="BshoxOptions.Default"/> is used.</param>
    public BshoxReader(ReadOnlySequence<byte> sequence, BshoxOptions? options = null) : this(options)
    {
        Consumed = 0;
        if (sequence.IsSingleSegment)
        {
            _usingSequence = false;
            var span = sequence.First.Span;
#if REF_FIELD
            _ref = ref MemoryMarshal.GetReference(span);
            _length = span.Length;
#else
            _span = span;
#endif
            Length = span.Length;
            _moreData = !span.IsEmpty;
            Check();
            return;
        }
        _usingSequence = true;
        _sequence = sequence;
        _next = sequence.Start;
        Length = sequence.Length;
        _moreData = true;
        GetNextSpan();
        Check();
    }

    /// <summary>
    /// Creates a new reader that reads from the specified <paramref name="memory"/>.
    /// </summary>
    /// <param name="memory">The memory to read from.</param>
    /// <param name="options">Options to customize the deserialization process. If <see langword="null"/>, <see cref="BshoxOptions.Default"/> is used.</param>
    public BshoxReader(ReadOnlyMemory<byte> memory, BshoxOptions? options = null) : this(options)
    {
        Consumed = 0;
        _usingSequence = false;
        Length = memory.Length;
#if REF_FIELD
        _ref = ref MemoryMarshal.GetReference(memory.Span);
        _length = memory.Length;
#else
        _span = memory.Span;
#endif
        _moreData = !memory.IsEmpty;
        Check();
    }

    /// <summary>
    /// throws a <see cref="NotSupportedException"/> if called.
    /// </summary>
    [Obsolete("Do not use the parameterless constructor.", error: true)] // triggers a compile-time error if this constructor is called
    [EditorBrowsable(EditorBrowsableState.Never)] // hides this constructor from IntelliSense
    [ExcludeFromCodeCoverage]
    public BshoxReader() => throw new NotSupportedException("Parameterless constructor is not supported.");

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal T ReadUnsafe<T>() where T : unmanaged
#if NET9_0_OR_GREATER
        , allows ref struct
#endif
    {
        ReadUnsafe(out T value);
        return value;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal unsafe void ReadUnsafe<T>(scoped out T value) where T : unmanaged
#if NET9_0_OR_GREATER
        , allows ref struct
#endif
    {
        if (SpanLength >= sizeof(T))
        {
#if REF_FIELD
            value = Unsafe.ReadUnaligned<T>(in _ref);
#else
            value = Unsafe.ReadUnaligned<T>(ref MemoryMarshal.GetReference(_span));
#endif
            Advance(sizeof(T));
            return;
        }

        value = ReadUnsafeSlow<T>();
    }

    /// <summary>
    /// Reads a length-prefixed UTF-8 encoded string.
    /// </summary>
    public string ReadString()
    {
        int byteLength = checked((int)ReadVarInt32());
        if (byteLength == 0)
        {
            Check();
            return string.Empty;
        }

        if (SpanLength >= byteLength)
        {
            // Fast path: all bytes to decode appear in the same span.
            string value = EncodingHelper.Utf8NoBom.GetString(GetSpan(byteLength));
            Advance(byteLength);
            Check();
            return value;
        }

        return ReadStringSlow(byteLength);
    }

    /// <summary>
    /// Throws an <see cref="BshoxException"/> if there are not enough bytes remaining in the buffer.
    /// </summary>
    /// <exception cref="BshoxException">Thrown when there are not enough bytes remaining in the buffer.</exception>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private readonly void CheckBufferSize(long byteLength)
    {
        if (Remaining < byteLength)
        {
            throw EndOfStream();
        }
    }

    [MethodImpl(MethodImplOptions.NoInlining)]
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
            int bytesRead = Math.Min(remainingByteLength, SpanLength);
            remainingByteLength -= bytesRead;
            bool flush = remainingByteLength == 0;
#if NETCOREAPP
            initializedChars += decoder.GetChars(GetSpan(bytesRead), charArray.AsSpan(initializedChars), flush);
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
        ArrayPool<char>.Shared.Return(charArray); // TODO: use try-catch
        Check();
        return value;
    }

    private unsafe T ReadUnsafeSlow<T>() where T : unmanaged
#if NET9_0_OR_GREATER
        , allows ref struct
#endif
    {
        Debug.Assert(SpanLength < sizeof(T), "SpanLength < sizeof(T)");
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
        if (SpanLength > 1) // hot path
        {
            // we have at least 2 bytes in the span, so we can read one without running out of data
#if REF_FIELD
            byte value = _ref;
            _ref = ref Unsafe.Add(ref _ref, 1);
            _length--;
#else
            byte value = _span[0];
            _span = _span.Slice(1);
#endif
            Consumed++;
            Check();
            return value;
        }
        Check();
        return ReadByteSlow();
    }

    [MethodImpl(MethodImplOptions.NoInlining)] // cold path
    private byte ReadByteSlow()
    {
        Debug.Assert(SpanLength < 2, "SpanLength < 2");
        if (!_moreData)
        {
            Debug.Assert(SpanLength == 0, "SpanLength == 0");
            throw EndOfStream();
        }

        Debug.Assert(SpanLength == 1, "SpanLength == 1");
#if REF_FIELD
        byte value = _ref;
        _ref = ref Unsafe.Add(ref _ref, 1);
        _length--;
#else
        byte value = _span[0];
        _span = _span.Slice(1);
#endif
        Consumed++;

        Debug.Assert(SpanLength == 0, "SpanLength == 0");
        if (_usingSequence)
        {
            GetNextSpan();
        }
        else
        {
            _moreData = false;
        }

        Check();
        return value;
    }

    /// <summary>
    /// <see cref="ReadOnlySequence{T}.Enumerator.MoveNext"/>
    /// </summary>
    private void GetNextSpan()
    {
        Debug.Assert(_moreData, nameof(_moreData));
        Debug.Assert(_usingSequence, nameof(_usingSequence));
        if (TryMoveNext())
        {
            if (SpanLength == 0)
            {
                Debug.Fail("Expected non-empty span");
                throw new InvalidOperationException("Sequence returned an empty segment.");
            }
            Check();
            return;
        }
#if REF_FIELD
        _ref = ref Unsafe.NullRef<byte>();
        _length = 0;
#else
        _span = [];
#endif
        _moreData = false;
        Check();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private bool TryMoveNext()
    {
        if (_next.GetObject() == null)
        {
            return false;
        }

        bool success = _sequence.TryGet(ref _next, out var memory);
        if (success)
        {
#if REF_FIELD
            _ref = ref MemoryMarshal.GetReference(memory.Span);
            _length = memory.Length;
#else
            _span = memory.Span;
#endif
        }
        return success;
    }

    /// <summary>
    /// Move the reader ahead by the specified number of bytes.
    /// </summary>
    public void Advance(int count)
    {
        if (SpanLength > count)
        {
            AdvanceFast(count);
            Consumed += count;
        }
        else if (_usingSequence)
        {
            AdvanceSlow(count);
        }
        else if (SpanLength == count)
        {
#if REF_FIELD
            _ref = ref Unsafe.NullRef<byte>();
            _length = 0;
#else
            _span = [];
#endif
            Consumed += count;
            _moreData = false;
        }
        else
        {
            throw EndOfStream();
        }
        Check();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private void AdvanceFast(int count)
    {
        Debug.Assert(SpanLength >= count, "SpanLength >= count");
#if REF_FIELD
        ArgumentOutOfRangeException.ThrowIfNegative(count);
        _ref = ref Unsafe.Add(ref _ref, count);
        _length -= count;
#else
        _span = _span.Slice(count);
#endif
    }

    [MethodImpl(MethodImplOptions.NoInlining)]
    private void AdvanceSlow(int count)
    {
        Debug.Assert(_usingSequence, nameof(_usingSequence));
        Debug.Assert(SpanLength <= count, "SpanLength <= count");
        ArgumentOutOfRangeException.ThrowIfNegative(count);
        if (count > Remaining)
        {
            throw EndOfStream();
        }

        while (_moreData)
        {
            int spanLength = SpanLength;

            if (spanLength > count)
            {
                AdvanceFast(count);
                Consumed += count;
                count = 0;
                break;
            }

            count -= spanLength;
            Debug.Assert(count >= 0, "count >= 0");

            Consumed += spanLength;
            GetNextSpan();

            if (count == 0)
            {
                break;
            }
        }

        Check();

        if (count != 0)
        {
            Debug.Fail("Ran into dead code");
            throw EndOfStream();
        }
    }

    /// <summary>
    /// Copies bytes from the reader into the specified <paramref name="destination"/> span and advances the reader by the number of bytes copied.
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void CopyTo(scoped Span<byte> destination)
    {
        if (SpanLength >= destination.Length)
        {
            // fast path: all bytes to copy appear in the current span
            GetSpan(destination.Length).CopyTo(destination);
            Advance(destination.Length);
            Check();
            return;
        }
        if (_usingSequence)
        {
            // slow path: we need to copy bytes across multiple spans
            CopyToSlow(destination);
            Check();
            return;
        }
        throw EndOfStream();
    }

    [MethodImpl(MethodImplOptions.NoInlining)]
    private void CopyToSlow(scoped Span<byte> destination)
    {
        Debug.Assert(_usingSequence, nameof(_usingSequence));
        CheckBufferSize(destination.Length);

        Debug.Assert(SpanLength < destination.Length, "SpanLength < destination.Length");
#if REF_FIELD
        GetSpan(SpanLength).CopyTo(destination);
#else
        _span.CopyTo(destination);
#endif
        int copied = SpanLength;

        while (_moreData)
        {
            GetNextSpan();
            if (!_moreData)
            {
                Debug.Fail("Ran into dead code"); // CheckBufferSize should prevent this
                throw EndOfStream();
            }
            int toCopy = Math.Min(SpanLength, destination.Length - copied);
            GetSpan(toCopy).CopyTo(destination.Slice(copied));
            copied += toCopy;
            Debug.Assert(copied <= destination.Length, "copied <= destination.Length");
            if (copied == destination.Length)
            {
                AdvanceFast(toCopy);
                if (SpanLength == 0)
                    GetNextSpan();
                Consumed += copied;
                return;
            }
        }

        Debug.Fail("Ran into dead code");
        throw EndOfStream();
    }

    [MethodImpl(MethodImplOptions.NoInlining)] // cold path
    private static BshoxException EndOfStream()
    {
        var inner = new EndOfStreamException();
        return new(inner.Message, inner);
    }

    /// <summary>
    /// Creates a scope to track depth of nested objects and arrays.<br/>
    /// Calling this method increments the current depth by <c>1</c> and returns a <see cref="DepthLockScope"/> that will decrement the depth when disposed.<br/>
    /// This method must be used in a <see langword="using"/> statement to ensure proper depth tracking.
    /// </summary>
    /// <remarks>
    /// e.g.:
    /// <code lang="csharp">
    /// using (reader.DepthLock())
    /// {
    ///   // Read nested object or array here.
    /// }
    /// </code>
    /// </remarks>
    public DepthLockScope DepthLock() => DepthLockScope.Create(ref _depth, Options.MaxDepth);
}
