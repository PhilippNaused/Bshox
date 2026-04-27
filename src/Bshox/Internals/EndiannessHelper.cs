using System.Buffers.Binary;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace Bshox.Internals;

[SkipLocalsInit]
internal static class EndiannessHelper
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static void Reverse(scoped ref Guid value)
    {
        Unsafe.As<Guid, Guid2>(ref value).Reverse();
    }

    [StructLayout(LayoutKind.Sequential, Size = 16)]
    private unsafe struct Guid2
    {
        private int _a;
        private short _b;
        private short _c;
        private fixed byte _bytes[8];

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Reverse()
        {
            _a = BinaryPrimitives.ReverseEndianness(_a);
            _b = BinaryPrimitives.ReverseEndianness(_b);
            _c = BinaryPrimitives.ReverseEndianness(_c);
        }
    }

    public static void Reverse(ReadOnlySpan<int> source, Span<int> dest)
    {
#if NET8_0_OR_GREATER
        BinaryPrimitives.ReverseEndianness(source, dest);
#else
        int c = dest.Length;
        for (int i = 0; i < c; i++)
            dest[i] = BinaryPrimitives.ReverseEndianness(source[i]);
#endif
    }

    public static void Reverse(ReadOnlySpan<long> source, Span<long> dest)
    {
#if NET8_0_OR_GREATER
        BinaryPrimitives.ReverseEndianness(source, dest);
#else
        int c = dest.Length;
        for (int i = 0; i < c; i++)
            dest[i] = BinaryPrimitives.ReverseEndianness(source[i]);
#endif
    }
}
