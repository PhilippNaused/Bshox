using System.Buffers.Binary;
using System.Diagnostics;

namespace Bshox;

public ref partial struct BshoxReader
{
    /// <summary>
    /// Reads an unsigned integer using variable-length encoding.
    /// </summary>
    public ulong ReadVarInt64()
    {
        ulong value = 0;
        int shift = 0;
        byte b;
        do
        {
            b = ReadByte();
            value |= (ulong)(b & 0x7F) << shift;
            shift += 7;
        } while ((b & 0x80) != 0);
        return value;
    }

    /// <summary>
    /// Reads an unsigned integer using variable-length encoding.
    /// </summary>
    public uint ReadVarInt32()
    {
        uint value = 0;
        int shift = 0;
        byte b;
        do
        {
            b = ReadByte();
            value |= (uint)(b & 0x7F) << shift;
            shift += 7;
        } while ((b & 0x80) != 0);
        return value;
    }

    /// <summary>
    /// Reads a signed integer using variable-length zigzag encoding.
    /// </summary>
    public long ReadZigZagVarInt64()
    {
        long x = (long)ReadVarInt64();
        return (x >>> 1) ^ -(x & 1);
    }

    /// <summary>
    /// Reads a signed integer using variable-length zigzag encoding.
    /// </summary>
    public int ReadZigZagVarInt32()
    {
        int x = (int)ReadVarInt32();
        return (x >>> 1) ^ -(x & 1);
    }

    /// <summary>
    /// Reads a varint encoded tag and extracts the field key and encoding.
    /// </summary>
    /// <param name="encoding">The encoding of the field</param>
    /// <returns>The key of the field</returns>
    public uint ReadTag(out BshoxCode encoding)
    {
        uint tag = ReadVarInt32();
        encoding = (BshoxCode)(tag & 0b111);
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
    public int ReadArrayHeader(out BshoxCode encoding)
    {
        uint tag = ReadVarInt32();
        encoding = (BshoxCode)(tag & 0b111);
        uint count = tag >>> 3;
        // guard against malicious input by checking if the array is too large
        long minSize = GetMinLength(encoding) * count;
        // check for overflow
        if (minSize < 0)
            throw EndOfStream();
        CheckBufferSize(minSize);
        Debug.Assert(count <= BshoxConstants.MaxKey, "count <= BshoxConstants.MaxKey");
        Debug.Assert(count <= int.MaxValue, "count <= int.MaxValue");
        return checked((int)count);
    }

    /// <summary>
    /// Gets the minimum length of a value encoded with the specified encoding.
    /// </summary>
    private static int GetMinLength(BshoxCode encoding)
    {
        return encoding switch
        {
            BshoxCode.VarInt => 1,
            BshoxCode.Fixed4 => 4,
            BshoxCode.Fixed8 => 8,
            BshoxCode.Prefixed => 1,
            BshoxCode.Array => 1,
            BshoxCode.SubObject => 1,
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
        byte[] bytes = new byte[length];
        CopyTo(bytes);
        return bytes;
    }

    /// <summary>
    /// Skips over a value of the specified encoding.
    /// </summary>
    /// <param name="encoding"></param>
    public void SkipValue(BshoxCode encoding)
    {
        switch (encoding)
        {
            case BshoxCode.VarInt:
                _ = ReadVarInt64();
                break;
            case BshoxCode.Fixed4:
                Advance(4);
                break;
            case BshoxCode.Fixed8:
                Advance(8);
                break;
            case BshoxCode.Prefixed:
                int length = checked((int)ReadVarInt64());
                Advance(length);
                break;
            case BshoxCode.Array:
            {
                using var _ = DepthLock();
                int count = ReadArrayHeader(out BshoxCode code);
                for (int i = 0; i < count; i++)
                {
                    SkipValue(code);
                }
                break;
            }
            case BshoxCode.SubObject:
            {
                using var _ = DepthLock();
                while (true)
                {
                    uint key = ReadTag(out BshoxCode code);
                    if (key == 0)
                    {
                        BshoxException.ThrowIfWrongEncoding(code, 0);
                        break;
                    }
                    SkipValue(code);
                }
                break;
            }
            default:
                throw BshoxException.InvalidEncoding(encoding);
        }
    }
}
