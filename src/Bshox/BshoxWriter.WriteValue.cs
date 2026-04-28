#if NET8_0_OR_GREATER
#define USE_REF // runtime supports ref fields.
#endif

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
#if USE_REF
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
        const int maxCharExpansion = 3; // max bytes per char in UTF-8
        const int maxHotPathLength = maxHotPathSize / maxCharExpansion; // max chars that can be encoded in 1 byte prefix, with worst case expansion (42).
        if (value.Length <= maxHotPathLength)
        {
            // hot path for short strings that can be encoded with a 1 byte prefix, with worst case expansion. This avoids the overhead of calculating the byte count.
            // TODO: optimize for strings with best-case expansion (1 byte per char) by checking for ASCII chars and encoding them directly without calculating the byte count.
            // e.g.: use Ascii.FromUtf16

            // Max expansion: each char -> 3 bytes, so 127 bytes max of data, +1 for length prefix
            ref byte bytes = ref GetRef(maxHotPathSize + 1);
            // write the payload first:
            int actualByteCount = EncodingHelper.Utf8Encode(value.AsSpan(), ref Unsafe.Add(ref bytes, 1), maxHotPathSize);
            // then, write the length prefix:
            bytes = (byte)actualByteCount;
            Advance(actualByteCount + 1);
        }
        else
        {
            // cold path
            int byteCount = EncodingHelper.Utf8NoBom.GetByteCount(value);
            WriteVarInt32((uint)byteCount);
            ref byte bytes = ref GetRef(byteCount);
            int actualByteCount = EncodingHelper.Utf8Encode(value.AsSpan(), ref bytes, byteCount);
            Debug.Assert(actualByteCount == byteCount, "actualByteCount == byteCount");
            Advance(byteCount);
        }
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

    internal unsafe void WriteUnsafe<T>(ref readonly T value) where T : unmanaged
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
