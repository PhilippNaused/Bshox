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

#if !NETCOREAPP // Define the extension method only where an instance method does not already exist.
    internal static unsafe string GetString(this Encoding encoding, ReadOnlySpan<byte> bytes)
    {
        if (bytes.Length == 0)
        {
            return string.Empty;
        }

        fixed (byte* pBytes = bytes)
        {
            return encoding.GetString(pBytes, bytes.Length);
        }
    }
#endif

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
