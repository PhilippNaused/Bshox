using System.Runtime.CompilerServices;

namespace Bshox.Internals;

internal static class Utils
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static T[] Allocate<T>(int length)
    {
#if NETCOREAPP
        return GC.AllocateUninitializedArray<T>(length);
#else
        return new T[length];
#endif
    }
}
