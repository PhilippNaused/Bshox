#if NET8_0_OR_GREATER
#define USE_REF
#endif

using System.Buffers;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using Bshox.Internals;

namespace Bshox;

/// <summary>
/// Forward-only wrapper around a <see cref="IBufferWriter{T}"/> for writing Bshox data.
/// </summary>
public ref partial struct BshoxWriter
{
    private const int MinBufferSize = 256;

#if USE_REF
    private ref byte _ref; // reference to the underlying buffer
    private int _length; // remaining space in the buffer
    private int _unflushed; // unflushed bytes
#else
    private int _index;
    private Span<byte> _span;
#endif

    private readonly IBufferWriter<byte> _buffer;
    private int _depth;

    /// <summary>
    /// The options used by this writer.
    /// </summary>
    public BshoxOptions Options { get; }

    /// <summary>
    /// The current depth of nested objects and arrays.<br/>
    /// If this value exceeds <see cref="BshoxOptions.MaxDepth"/>, a <see cref="BshoxException"/> will be thrown.
    /// </summary>
    public readonly int CurrentDepth => _depth;

    /// <summary>
    /// Creates a new writer that writes to the specified <paramref name="buffer"/>.
    /// </summary>
    /// <param name="buffer">The buffer to write to</param>
    /// <param name="options">The options to use. If <c>null</c>, <see cref="BshoxOptions.Default"/> is used.</param>
    public BshoxWriter(IBufferWriter<byte> buffer, BshoxOptions? options = null)
    {
        _buffer = buffer;
        Options = options ?? BshoxOptions.Default;
    }

    /// <summary>
    /// Returns a span of bytes to write to. <see cref="Advance(int)"/> must be called afterwards to notify the writer of the number of bytes written.
    /// </summary>
    /// <param name="sizeHint">The minimum size of the buffer</param>
    /// <returns>A writable buffer of length <paramref name="sizeHint"/> or longer</returns>
    /// <seealso cref="IBufferWriter{T}.GetSpan(int)"/>
    /// <seealso cref="GetRef(int)"/>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Span<byte> GetSpan(int sizeHint)
    {
#if USE_REF
        ref byte r = ref GetRef(sizeHint);
        Debug.Assert(_length >= sizeHint, "_length >= sizeHint");
        return System.Runtime.InteropServices.MemoryMarshal.CreateSpan(ref r, _length);
#else
        Debug.Assert(_index <= _span.Length, "_index <= _span.Length");
        if (_span.Length - _index >= sizeHint)
        {
            Debug.Assert(_span.Length - _index >= sizeHint, "_span.Length - _index >= sizeHint");
            return _span.Slice(_index); // hot path
        }
        if (_index > 0)
        {
            Flush();
        }
        Debug.Assert(_index == 0, "_index == 0");
        _span = _buffer.GetSpan(Math.Max(sizeHint, MinBufferSize));
        Debug.Assert(_span.Length >= sizeHint, "_span.Length >= sizeHint");
        return _span;
#endif
    }

    /// <summary>
    /// Returns a reference to a writable buffer of at least <paramref name="sizeHint"/> bytes. <see cref="Advance(int)"/> must be called afterwards to notify the writer of the number of bytes written.
    /// </summary>
    /// <param name="sizeHint">The minimum size of the buffer</param>
    /// <returns>A reference to the first byte of writable buffer of length <paramref name="sizeHint"/> or longer</returns>
    /// <seealso cref="GetSpan(int)"/>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal ref byte GetRef(int sizeHint)
    {
        Debug.Assert(sizeHint > 0, "sizeHint > 0");
#if USE_REF
        Debug.Assert(_length >= 0, "length >= 0");
        if (_length >= sizeHint)
        {
            return ref _ref; // hot path
        }
        if (_unflushed > 0)
        {
            Flush();
        }
        Debug.Assert(_unflushed == 0, "_unflushed == 0");
        var span = _buffer.GetSpan(Math.Max(sizeHint, MinBufferSize));
        _ref = ref span[0];
        _length = span.Length;
        Debug.Assert(_length >= sizeHint, "_length >= sizeHint");
        return ref _ref;
#else
        return ref GetSpan(sizeHint)[0];
#endif
    }

    /// <summary>
    /// Tells the writer that <paramref name="count"/> bytes have been written to the buffer obtained from <see cref="GetSpan(int)"/> or <see cref="GetRef(int)"/>.
    /// </summary>
    /// <param name="count">The number of bytes that have been written to the buffer.</param>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Advance(int count)
    {
#if USE_REF
        _ref = ref Unsafe.Add(ref _ref, count);
        _length -= count;
        _unflushed += count;
        Debug.Assert(_length >= 0, "_length >= 0");
        Debug.Assert(!Unsafe.IsNullRef(ref _ref), "!Unsafe.IsNullRef(ref _ref)");
#else
        Debug.Assert(_index + count <= _span.Length);
        _index += count;
#endif
    }

    /// <summary>
    /// Flushes the internal buffer to the underlying <see cref="IBufferWriter{T}"/>.
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Flush()
    {
#if USE_REF
        Debug.Assert(!Unsafe.IsNullRef(ref _ref), "!Unsafe.IsNullRef(ref _ref)");
        Debug.Assert(_unflushed >= 0, "_unflushed >= 0");
        _buffer.Advance(_unflushed);
        _unflushed = 0;
#else
        Debug.Assert(_index <= _span.Length, "_index <= _span.Length");
        _buffer.Advance(_index);
        _index = 0;
        _span = default;
#endif
    }

    /// <summary>
    /// Creates a scope to track depth of nested objects and arrays.<br/>
    /// Calling this method increments the current depth by <c>1</c> and returns a <see cref="DepthLockScope"/> that will decrement the depth when disposed.<br/>
    /// This method must be used in a <c>using</c> statement to ensure proper depth tracking.
    /// </summary>
    /// <example>
    /// <code lang="csharp">
    /// using (writer.DepthLock())
    /// {
    ///   // Read nested object or array here.
    /// }
    /// </code>
    /// </example>
#pragma warning disable CS0618 // Type or member is obsolete
    public DepthLockScope DepthLock() => DepthLockScope.Create(ref _depth, Options.MaxDepth);
#pragma warning restore CS0618 // Type or member is obsolete
}
