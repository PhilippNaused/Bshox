using System.Diagnostics.Contracts;
using System.Runtime.InteropServices;
using System.Text;

namespace Bshox.Internals;

internal static class EncodingHelper
{
    public static readonly UTF8Encoding Utf8NoBom = new(false);

    public static unsafe int Utf8Encode(ReadOnlySpan<char> chars, Span<byte> bytes)
    {
        fixed (char* charsPtr = &MemoryMarshal.GetReference(chars))
        fixed (byte* bytesPtr = &MemoryMarshal.GetReference(bytes))
        {
            return Utf8NoBom.GetBytes(charsPtr, chars.Length, bytesPtr, bytes.Length);
        }
    }

    public static unsafe int Utf8Encode(ReadOnlySpan<char> chars, ref byte bytes, int byteCount)
    {
        fixed (char* charsPtr = &MemoryMarshal.GetReference(chars))
        fixed (byte* bytesPtr = &bytes)
        {
            return Utf8NoBom.GetBytes(charsPtr, chars.Length, bytesPtr, byteCount);
        }
    }

    [Pure]
    public static int GetVarIntLength(uint value)
    {
        const uint max1 = 1 << (1 * 7);
        const uint max2 = 1 << (2 * 7);
        const uint max3 = 1 << (3 * 7);
        const uint max4 = 1 << (4 * 7);
        if (value < max1)
            return 1;
        if (value < max2)
            return 2;
        if (value < max3)
            return 3;
        if (value < max4)
            return 4;
        return 5;
    }

    public static long UnZigZag64(ulong value)
    {
        return (long)(value >> 1) ^ -(long)(value & 1);
    }

    public static ulong ZigZag64(long value)
    {
        return (ulong)((value >> 63) ^ (value << 1));
    }

    public static int UnZigZag32(uint value)
    {
        return (int)(value >> 1) ^ -(int)(value & 1);
    }

    public static uint ZigZag32(int value)
    {
        return (uint)((value >> 31) ^ (value << 1));
    }
}
