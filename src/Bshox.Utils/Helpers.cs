using System.Runtime.CompilerServices;

namespace Bshox.Utils;

internal static class Helpers
{
    public static float AsFloat(this uint value) => Unsafe.As<uint, float>(ref value);
    public static double AsDouble(this ulong value) => Unsafe.As<ulong, double>(ref value);
}
