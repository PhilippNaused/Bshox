using System.Buffers;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using Bshox.Internals;

namespace Bshox;

public ref partial struct BshoxWriter(IBufferWriter<byte> buffer, BshoxOptions options = default)
{
    private const int MinBufferSize = 256;

#if NET8_0_OR_GREATER
    private ref byte _ref; // reference to the underlying buffer
    private int _length; // remaining space in the buffer
    private int _unflushed; // unflushed bytes
#else
    private int _index;
    private Span<byte> _span;
#endif

    private int _depth;

    public readonly int CurrentDepth => _depth;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Span<byte> GetSpan(int sizeHint)
    {
#if NET8_0_OR_GREATER
        ref byte r = ref GetRef(sizeHint);
        Debug.Assert(_length >= sizeHint, "_length >= sizeHint");
        return System.Runtime.InteropServices.MemoryMarshal.CreateSpan(ref r, _length);
#else
        Debug.Assert(_index <= _span.Length, "_index <= _span.Length");
        if (_span.Length - _index >= sizeHint)
        {
            Debug.Assert(_span.Length - _index >= sizeHint, "_span.Length - _index >= sizeHint");
            return _span.Slice(_index);
        }
        if (_index > 0)
        {
            Flush();
        }
        Debug.Assert(_index == 0, "_index == 0");
        _span = buffer.GetSpan(Math.Max(sizeHint, MinBufferSize));
        return _span;
#endif
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal ref byte GetRef(int sizeHint)
    {
        Debug.Assert(sizeHint > 0, "sizeHint > 0");
#if NET8_0_OR_GREATER
        Debug.Assert(_length >= 0, "length >= 0");
        if (_length >= sizeHint)
        {
            return ref _ref;
        }
        if (_unflushed > 0)
        {
            Flush();
        }
        Debug.Assert(_unflushed == 0, "_unflushed == 0");
        var span = buffer.GetSpan(Math.Max(sizeHint, MinBufferSize));
        _ref = ref span[0];
        _length = span.Length;
        Debug.Assert(_length >= sizeHint, "_length >= sizeHint");
        return ref _ref;
#else
        Debug.Assert(_index <= _span.Length, "_index <= _span.Length");
        if (_span.Length - _index >= sizeHint)
        {
            Debug.Assert(_span.Length - _index >= sizeHint, "_span.Length - _index >= sizeHint");
            return ref _span[_index];
        }
        if (_index > 0)
        {
            Flush();
        }
        Debug.Assert(_index == 0, "_index == 0");
        _span = buffer.GetSpan(Math.Max(sizeHint, MinBufferSize));
        Debug.Assert(_span.Length >= sizeHint, "_span.Length >= sizeHint");
        return ref _span[0];
#endif
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Advance(int count)
    {
#if NET8_0_OR_GREATER
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

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Flush()
    {
#if NET8_0_OR_GREATER
        Debug.Assert(!Unsafe.IsNullRef(ref _ref), "!Unsafe.IsNullRef(ref _ref)");
        Debug.Assert(_unflushed >= 0, "_unflushed >= 0");
        buffer.Advance(_unflushed);
        _unflushed = 0;
#else
        Debug.Assert(_index <= _span.Length, "_index <= _span.Length");
        buffer.Advance(_index);
        _index = 0;
        _span = default;
#endif
    }

#pragma warning disable CS0618 // Type or member is obsolete
    public DepthLockScope DepthLock() => DepthLockScope.Create(ref _depth, options.MaxDepth);
#pragma warning restore CS0618 // Type or member is obsolete
}
