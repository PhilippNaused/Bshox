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
        GetRef(1) = value;
        Advance(1);
    }

    /// <summary>
    /// Writes an unsigned integer using variable-length encoding.
    /// </summary>
    public void WriteVarInt32(uint value)
    {
        Check();
        ref byte bytes = ref GetRef(5);
        int index = 0;
        while (value > 0x7Fu)
        {
            Unsafe.Add(ref bytes, index++) = (byte)(value | ~0x7Fu);
            value >>= 7;
        }
        Unsafe.Add(ref bytes, index) = (byte)value;
        Debug.Assert(index + 1 <= 5, "index + 1 <= 5");
        Advance(index + 1);
    }

    /// <summary>
    /// Writes an unsigned integer using variable-length encoding.
    /// </summary>
    public void WriteVarInt64(ulong value)
    {
        Check();
        ref byte bytes = ref GetRef(10);
        int index = 0;
        while (value > 0x7Fu)
        {
            Unsafe.Add(ref bytes, index++) = (byte)((uint)value | ~0x7Fu);
            value >>= 7;
        }
        Unsafe.Add(ref bytes, index) = (byte)value;
        Debug.Assert(index + 1 <= 10, "index + 1 <= 10");
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
        if (value.Length <= 127 / 3)
        {
            // Max expansion: each char -> 3 bytes, so 127 bytes max of data, +1 for length prefix
            ref byte bytes = ref GetRef(128);
            int actualByteCount = EncodingHelper.Utf8Encode(value.AsSpan(), ref Unsafe.Add(ref bytes, 1), 127);
            bytes = (byte)actualByteCount;
            Advance(actualByteCount + 1);
        }
        else
        {
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
        if (length <= 0)
        {
            WriteByte(0);
        }
        else
        {
            WriteVarInt32((uint)length);
            Unsafe.CopyBlock(ref GetRef(length), ref value[0], (uint)length);
            Advance(length);
        }
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
