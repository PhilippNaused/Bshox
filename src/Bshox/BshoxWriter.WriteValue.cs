#pragma warning disable CS0282 // False positive

using System.Buffers.Binary;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using Bshox.Internals;

namespace Bshox;

public ref partial struct BshoxWriter
{
    /// <summary>
    /// Writes a byte to the underlying stream.
    /// </summary>
    public void WriteByte(byte value)
    {
        Check();
#if REF_FIELD
        GetRef(1) = value;
#else
        GetSpan(1)[0] = value;
#endif
        Advance(1);
    }

    private const uint BitMask7 = 0b0111_1111u; // 127

    /// <summary>
    /// Writes an unsigned 32-bit integer using variable-length encoding.
    /// </summary>
    public void WriteVarInt32(uint value)
    {
        const int maxSize = 5; // max bytes needed to encode a 32-bit integer with variable-length encoding
        ref byte bytes = ref GetRef(maxSize);
        bytes = (byte)value;
        if (value <= BitMask7)
        {
            Advance(1);
            return;
        }
        int index = 0;
        do
        {
            Unsafe.Add(ref bytes, index++) = (byte)(value | ~BitMask7);
            value >>= 7;
        }
        while (value > BitMask7);
        Unsafe.Add(ref bytes, index) = (byte)value;
        Debug.Assert(index + 1 <= maxSize, "index + 1 <= maxSize");
        Advance(index + 1);
    }

    /// <summary>
    /// Writes an unsigned 64-bit integer using variable-length encoding.
    /// </summary>
    public void WriteVarInt64(ulong value)
    {
        if (value <= uint.MaxValue)
        {
            WriteVarInt32((uint)value);
            return;
        }
        Check();
        const int maxSize = 10; // max bytes needed to encode a 64-bit integer with variable-length encoding
        ref byte bytes = ref GetRef(maxSize);
        int index = 0;
        while (value > BitMask7)
        {
            Unsafe.Add(ref bytes, index++) = (byte)((uint)value | ~BitMask7);
            value >>= 7;
        }
        Unsafe.Add(ref bytes, index) = (byte)value;
        Debug.Assert(index + 1 <= maxSize, "index + 1 <= maxSize");
        Debug.Assert(index + 1 >= 5, "index + 1 >= 5"); // if it was less than 5, it would have been handled by WriteVarInt32
        Advance(index + 1);
    }

    /// <summary>
    /// Writes a signed integer using variable-length zigzag encoding.
    /// </summary>
    public void WriteZigZagVarInt32(int value) => WriteVarInt32(EncodingHelper.ZigZag32(value));

    /// <summary>
    /// Writes a signed integer using variable-length zigzag encoding.
    /// </summary>
    public void WriteZigZagVarInt64(long value) => WriteVarInt64(EncodingHelper.ZigZag64(value));

    /// <summary>
    /// Writes a tag consisting of a key and encoding type.
    /// </summary>
    /// <param name="key"></param>
    /// <param name="encoding"></param>
    public void WriteTag(uint key, BshoxCode encoding)
    {
        Debug.Assert(key >= BshoxConstants.MinKey, "key >= BshoxConstants.MinKey");
        Debug.Assert(key <= BshoxConstants.MaxKey, "key <= BshoxConstants.MaxKey");
        WriteVarInt32((key << 3) | (uint)encoding);
    }

    /// <summary>
    /// Writes an array header consisting of the element count and element encoding type.
    /// </summary>
    /// <param name="count"></param>
    /// <param name="elementEncoding"></param>
    public void WriteArrayHeader(int count, BshoxCode elementEncoding)
    {
        Debug.Assert(count >= 0, "count >= 0");
        WriteVarInt64(((ulong)count << 3) | (ulong)elementEncoding);
    }

    /// <summary>
    /// Writes a <see cref="double"/> using the <see cref="BshoxCode.Fixed8"/> encoding.
    /// </summary>
    public unsafe void WriteDouble(double value) => WriteUInt64(*(ulong*)&value);

    /// <summary>
    /// Writes a <see cref="ulong"/> using the <see cref="BshoxCode.Fixed8"/> encoding.
    /// </summary>
    public void WriteUInt64(ulong value)
    {
        Check();
        if (Options.ReverseEndianness)
        {
            value = BinaryPrimitives.ReverseEndianness(value);
        }
        Unsafe.WriteUnaligned(ref GetRef(sizeof(ulong)), value);
        Advance(sizeof(ulong));
    }

    /// <summary>
    /// Writes a <see cref="float"/> using the <see cref="BshoxCode.Fixed4"/> encoding.
    /// </summary>
    public unsafe void WriteSingle(float value) => WriteUInt32(*(uint*)&value);

    /// <summary>
    /// Writes a <see cref="uint"/> using the <see cref="BshoxCode.Fixed4"/> encoding.
    /// </summary>
    public void WriteUInt32(uint value)
    {
        Check();
        if (Options.ReverseEndianness)
        {
            value = BinaryPrimitives.ReverseEndianness(value);
        }
        Unsafe.WriteUnaligned(ref GetRef(sizeof(uint)), value);
        Advance(sizeof(uint));
    }

    /// <summary>
    /// Writes a <see cref="string"/> as a UTF-8 encoded byte array using the <see cref="BshoxCode.Prefixed"/> encoding.
    /// </summary>
    public void WriteString(string value)
    {
        if (value.Length == 0)
        {
            WriteByte(0);
            return;
        }

        Check();
        const int maxHotPathSize = 127; // largest int that can be encoded in 1 byte.
#if !NET8_0_OR_GREATER
        const int maxCharExpansion = 3; // max bytes per char in UTF-8
        const int maxHotPathLength = maxHotPathSize / maxCharExpansion; // max chars that can be encoded in 1 byte prefix, with worst case expansion (42).
        if (value.Length <= maxHotPathLength) // 42
        {
            // hot path for short strings that can be encoded with a 1 byte prefix, with worst case expansion. This avoids the overhead of calculating the byte count.
            // 127 bytes max of data, +1 for length prefix
            ref byte bytes = ref GetRef(maxHotPathSize + 1);
            // write the payload first:
            var actualByteCount = EncodingHelper.Utf8Encode(value.AsSpan(), ref Unsafe.Add(ref bytes, 1), maxHotPathSize);
            // then, write the length prefix:
            bytes = (byte)actualByteCount;
            Advance(actualByteCount + 1);
            return;
        }
#endif
#if NET8_0_OR_GREATER // TryGetBytes only exists in .NET 8+ ;(
        if (value.Length <= maxHotPathSize)
        {
            // less hot path for strings that can be encoded with a 1 byte prefix, if they are mostly ASCII (i.e. 1 byte per char).
            var span = GetSpan(maxHotPathSize + 1);
            if (EncodingHelper.Utf8NoBom.TryGetBytes(value, span.Slice(1), out var actualByteCount))
            {
                if (actualByteCount <= maxHotPathSize)
                {
                    span[0] = (byte)actualByteCount;
                    Advance(actualByteCount + 1);
                    return;
                }
                // string didn't fit in 127 bytes due to non-ASCII chars.
                // But now, we know the actual byte count and the string is already encoded!
                WriteStringInner2(value, actualByteCount, span);
                return;
            }

            WaitingForAdvance(false); // equivalent to Advance(0), but faster.
            // fall through to the cold path.
        }
#endif
        {
            // cold path
            int byteCount = EncodingHelper.Utf8NoBom.GetByteCount(value);
            WriteStringInner(value, byteCount);
        }
    }

    // This helper is called when the string is already encoded at span[1..], but the prefix takes more than 1 byte.
    private void WriteStringInner2(string value, int byteCount, Span<byte> span)
    {
        CheckWaitingForAdvance(true);
        WaitingForAdvance(false); // equivalent to Advance(0), but faster.
        Debug.Assert(byteCount > 127, "byteCount > 127"); // needs more than 1 byte of prefix
        Debug.Assert(value.Length <= 127, "value.Length <= 127"); // should have been handled by the cold path
        Debug.Assert(span.Length >= byteCount + 1, "span.Length >= byteCount + 1"); // span is large enough to contain the encoded string and a 1 byte prefix.

        var prefixSize = EncodingHelper.GetVarIntLength((uint)byteCount); // actual prefix size.
        Debug.Assert(prefixSize > 1, "prefixSize > 1");
        Debug.Assert(prefixSize <= 5, "prefixSize <= 5");
        if (prefixSize + byteCount <= span.Length)
        {
            // move the already encoded data to make room for the larger prefix.
            span.Slice(1, byteCount).CopyTo(span.Slice(prefixSize));
            WriteVarInt32((uint)byteCount); // this calls Advance(prefixSize) internally, so we don't need to call it ourselves.
            WaitingForAdvance(true);
            Advance(byteCount);
            return;
        }
        WriteStringInner(value, byteCount);
    }

    private void WriteStringInner(string value, int byteCount)
    {
        WriteVarInt32((uint)byteCount);
        ref byte bytes = ref GetRef(byteCount);
        var actualByteCount = EncodingHelper.Utf8Encode(value.AsSpan(), ref bytes, byteCount);
        Debug.Assert(actualByteCount == byteCount, "actualByteCount == byteCount");
        Advance(byteCount);
    }

    /// <summary>
    /// Writes a byte array using the <see cref="BshoxCode.Prefixed"/> encoding.
    /// </summary>
    public void WriteByteArray(byte[] value)
    {
        Check();
        int length = value.Length;
        if (length == 0)
        {
            WriteByte(0);
        }
        else
        {
            WriteVarInt32((uint)length);
            WriteBytes(value);
        }
    }

    /// <summary>
    /// Writes the bytes to the buffer without a prefix.
    /// </summary>
    public void WriteBytes(ReadOnlySpan<byte> source)
    {
        Check();
        if (source.Length == 0)
            return;
        // copy as much as possible into the current buffer, then flush and get a new buffer if needed, repeat until all data is copied.
        var dest = GetSpan(0);
        int toCopy = Math.Min(dest.Length, source.Length);
        source.Slice(0, toCopy).CopyTo(dest);
        Advance(toCopy);
        source = source.Slice(toCopy);
        dest = GetSpan(source.Length);
        source.CopyTo(dest);
        Advance(source.Length);
    }

    internal void WriteUnsafe<T>(ref readonly T value) where T : unmanaged
#if NET9_0_OR_GREATER
        , allows ref struct
#endif
    {
        Check();
        int size = sizeof(T);
        Unsafe.WriteUnaligned(ref GetRef(size), value);
        Advance(size);
    }
}
