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
    private ReadOnlySpan<byte> _span; // TODO: try inlining this field to bypass the redundant bounds checks.

    private readonly ReadOnlySequence<byte> _sequence;

    private SequencePosition _next;

    private readonly bool _usingSequence;

    private bool _moreData;

    private int _depth;

    /// <summary>
    /// Gets the total number of bytes processed by the reader.
    /// </summary>
    public long Consumed { get; private set; }

    /// <summary>
    /// Gets the number of bytes remaining to be read.
    /// </summary>
    public readonly long Remaining => Length - Consumed;

    /// <summary>
    /// The total length of the data being read.
    /// </summary>
    public long Length { get; }

    /// <summary>
    /// The current depth of nested objects and arrays.<br/>
    /// If this value exceeds <see cref="BshoxOptions.MaxDepth"/>, a <see cref="BshoxException"/> will be thrown.
    /// </summary>
    public readonly int CurrentDepth => _depth;

    /// <summary>
    /// The options used by this reader.
    /// </summary>
    public BshoxOptions Options { get; }

    private BshoxReader(BshoxOptions? options)
    {
        Options = options ?? BshoxOptions.Default;
    }

    /// <summary>
    /// Creates a new reader that reads from the specified <paramref name="sequence"/>.
    /// </summary>
    /// <param name="sequence">The sequence of bytes to read from.</param>
    /// <param name="options">Options to customize the deserialization process. If <c>null</c>, <see cref="BshoxOptions.Default"/> is used.</param>
    public BshoxReader(ReadOnlySequence<byte> sequence, BshoxOptions? options = null) : this(options)
    {
        Consumed = 0;
        if (sequence.IsSingleSegment)
        {
            _usingSequence = false;
            _span = sequence.First.Span;
            Length = _span.Length;
            _moreData = !_span.IsEmpty;
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
    /// <param name="options">Options to customize the deserialization process. If <c>null</c>, <see cref="BshoxOptions.Default"/> is used.</param>
    public BshoxReader(ReadOnlyMemory<byte> memory, BshoxOptions? options = null) : this(options)
    {
        Consumed = 0;
        _usingSequence = false;
        Length = memory.Length;
        _span = memory.Span;
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
        ArrayPool<char>.Shared.Return(charArray); // TODO: use try-catch
        Check();
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
            if (_span.IsEmpty)
            {
                Debug.Fail("Expected non-empty span");
                throw new InvalidOperationException("Sequence returned an empty segment.");
            }
            Check();
            return;
        }
        _span = [];
        _moreData = false;
        Check();
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
        if (_span.Length > count)
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
            throw EndOfStream();
        }
        Check();
    }

    private void AdvanceSlow(int count)
    {
        Debug.Assert(_usingSequence, nameof(_usingSequence));
        Debug.Assert(_span.Length <= count, "_span.Length <= count");
        ArgumentOutOfRangeException.ThrowIfNegative(count);
        if (count > Remaining)
        {
            throw EndOfStream();
        }

        while (_moreData)
        {
            int spanLength = _span.Length;

            if (spanLength > count)
            {
                _span = _span.Slice(count);
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
    public void CopyTo(Span<byte> destination)
    {
        if (_span.Length >= destination.Length)
        {
            // fast path: all bytes to copy appear in the current span
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
                Debug.Fail("Ran into dead code"); // CheckBufferSize should prevent this
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

    /// <summary>
    /// Creates a scope to track depth of nested objects and arrays.<br/>
    /// Calling this method increments the current depth by <c>1</c> and returns a <see cref="DepthLockScope"/> that will decrement the depth when disposed.<br/>
    /// This method must be used in a <c>using</c> statement to ensure proper depth tracking.
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
