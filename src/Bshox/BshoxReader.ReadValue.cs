using System.Buffers.Binary;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using Bshox.Internals;

namespace Bshox;

public ref partial struct BshoxReader
{
    /// <summary>
    /// Reads an unsigned 64-bit integer using variable-length encoding.
    /// </summary>
    public ulong ReadVarInt64()
    {
        ulong value = 0;
        int bitShift = 0;
        byte b;
        do
        {
            b = ReadByte();
            value |= (b & 0x7Ful) << bitShift;
            bitShift += 7;
            if (bitShift > 10 * 7)
                throw BshoxException.VarIntTooLong();
        } while (b > 127);
        return value;
    }

    /// <summary>
    /// Reads an unsigned 32-bit integer using variable-length encoding.
    /// </summary>
    public uint ReadVarInt32()
    {
        uint value = ReadByte();
        if (value < 128u)
            return value; // hot path
        value &= 0x7Fu;
        if (SpanLength >= 4)
        {
            return ReadVarInt32Fast(value); // lukewarm path
        }
        return ReadVarInt32Slow(value); // cold path
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)] // lukewarm path
    private uint ReadVarInt32Fast(uint value)
    {
        Debug.Assert(SpanLength >= 4, "SpanLength >= 4");
        int shift = 0;
        bool c; // continuation bit is set
        ref byte r = ref GetRef();
        do
        {
            byte b = r;
            r = ref Unsafe.Add(ref r, 1);
            shift++;
            value |= (b & 0x7Fu) << (shift * 7);
            c = b > 127;
            if (shift > 3 && c)
                throw BshoxException.VarIntTooLong();
        } while (c);

        Advance(shift);

        return value;
    }

    [MethodImpl(MethodImplOptions.NoInlining)] // cold path
    private uint ReadVarInt32Slow(uint value)
    {
        Debug.Assert(SpanLength < 4, "SpanLength < 4");
        int bitShift = 0;
        byte b;
        do
        {
            b = ReadByte();
            bitShift += 7;
            value |= (b & 0x7Fu) << bitShift;
            if (bitShift > 5 * 7)
                throw BshoxException.VarIntTooLong();
        } while (b > 127);
        return value;
    }

    /// <summary>
    /// Reads a signed integer using variable-length zigzag encoding.
    /// </summary>
    public long ReadZigZagVarInt64()
    {
        return EncodingHelper.UnZigZag64(ReadVarInt64());
    }

    /// <summary>
    /// Reads a signed integer using variable-length zigzag encoding.
    /// </summary>
    public int ReadZigZagVarInt32()
    {
        return EncodingHelper.UnZigZag32(ReadVarInt32());
    }

    /// <summary>
    /// Reads a varint encoded tag and extracts the field key and encoding.
    /// </summary>
    /// <param name="encoding">The encoding of the field</param>
    /// <returns>The key of the field</returns>
    public uint ReadTag(out BshoxEncoding encoding)
    {
        uint tag = ReadVarInt32();
        encoding = (BshoxEncoding)(tag & 0b111);
        return tag >>> 3;
    }

    /// <summary>
    /// Reads the header of an array, returning the number of elements and the encoding of each element.
    /// </summary>
    /// <param name="encoding">The encoding type of the array elements</param>
    /// <returns>The length of the array</returns>
    /// <remarks>
    /// This method will throw an exception if the array is too large to be read safely.
    /// </remarks>
    /// <exception cref="BshoxException">Thrown when the array is too large to be read safely.</exception>
    public int ReadArrayHeader(out BshoxEncoding encoding)
    {
        uint tag = ReadVarInt32();
        encoding = (BshoxEncoding)(tag & 0b111);
        uint count = tag >>> 3;
        // guard against malicious input by checking if the array is too large
        long minSize = GetMinLength(encoding) * count;
        // check for overflow
        Debug.Assert(minSize >= 0, "minSize >= 0");
        CheckBufferSize(minSize);
        Debug.Assert(count <= BshoxConstants.MaxKey, "count <= BshoxConstants.MaxKey");
        Debug.Assert(count <= int.MaxValue, "count <= int.MaxValue");
        return checked((int)count);
    }

    /// <summary>
    /// Gets the minimum length of a value encoded with the specified encoding.
    /// </summary>
    private static uint GetMinLength(BshoxEncoding encoding)
    {
        return encoding switch
        {
            BshoxEncoding.VarInt => 1,
            BshoxEncoding.Fixed4 => 4,
            BshoxEncoding.Fixed8 => 8,
            BshoxEncoding.Prefixed => 1,
            BshoxEncoding.Array => 1,
            BshoxEncoding.Object => 1,
            _ => throw BshoxException.InvalidEncoding(encoding),
        };
    }

    /// <summary>
    /// Reads an 8-byte floating point number from the underlying stream.
    /// </summary>
    public unsafe double ReadDouble()
    {
        ulong value = ReadUInt64();
        return *(double*)&value;
    }

    /// <summary>
    /// Reads a 4-byte unsigned integer.
    /// </summary>
    public uint ReadUInt32()
    {
        uint value = ReadUnsafe<uint>();
        if (Options.ReverseEndianness)
            value = BinaryPrimitives.ReverseEndianness(value);
        return value;
    }

    /// <summary>
    /// Reads an 8-byte unsigned integer.
    /// </summary>
    public ulong ReadUInt64()
    {
        ulong value = ReadUnsafe<ulong>();
        if (Options.ReverseEndianness)
            value = BinaryPrimitives.ReverseEndianness(value);
        return value;
    }

    /// <summary>
    /// Reads a 4-byte floating point number.
    /// </summary>
    public unsafe float ReadSingle()
    {
        uint value = ReadUInt32();
        return *(float*)&value;
    }

    /// <summary>
    /// Reads length-prefixed byte array.
    /// </summary>
    public byte[] ReadByteArray()
    {
        uint length = ReadVarInt32();
        if (length == 0)
            return [];
        CheckBufferSize(length);
        byte[] bytes = Utils.Allocate<byte>(checked((int)length));
        CopyTo(bytes);
        return bytes;
    }

    /// <summary>
    /// Skips over a value of the specified encoding.
    /// </summary>
    /// <param name="encoding"></param>
    public void SkipValue(BshoxEncoding encoding)
    {
        switch (encoding)
        {
            case BshoxEncoding.VarInt:
                _ = ReadVarInt64();
                break;
            case BshoxEncoding.Fixed4:
                Advance(4);
                break;
            case BshoxEncoding.Fixed8:
                Advance(8);
                break;
            case BshoxEncoding.Prefixed:
                int length = (int)ReadVarInt32();
                if (length < 0)
                    throw BshoxException.VarIntTooLong();
                Advance(length);
                break;
            case BshoxEncoding.Array:
            {
                IncreaseDepth();
                int count = ReadArrayHeader(out BshoxEncoding code);
                for (int i = 0; i < count; i++)
                {
                    SkipValue(code);
                }
                DecreaseDepth();
                break;
            }
            case BshoxEncoding.Object:
            {
                IncreaseDepth();
                while (true)
                {
                    uint key = ReadTag(out BshoxEncoding code);
                    if (key == 0)
                    {
                        BshoxException.ThrowIfWrongEncoding(code, 0);
                        break;
                    }
                    SkipValue(code);
                }
                DecreaseDepth();
                break;
            }
            default:
                throw BshoxException.InvalidEncoding(encoding);
        }
    }
}
