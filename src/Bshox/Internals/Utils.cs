using System.Runtime.CompilerServices;

namespace Bshox.Internals;

internal static class Utils
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static byte[] Allocate(int length)
    {
#if NETCOREAPP
        return GC.AllocateUninitializedArray<byte>(length);
#else
        return new byte[length];
#endif
    }
}
