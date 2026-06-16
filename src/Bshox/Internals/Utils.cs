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

internal static class Utils<T>
{
#pragma warning disable CA1000 // static member in generic type (bad resign rule)
    public static readonly bool IsReferenceOrContainsReferences = GetIsReferenceOrContainsReferences();
#pragma warning restore CA1000

    private static bool GetIsReferenceOrContainsReferences()
    {
#if NETCOREAPP
        return RuntimeHelpers.IsReferenceOrContainsReferences<T>();
#else
        // This code is only called once per type, so it doesn't need to be very optimized.
        // We also don't need to worry about AOT, since it is not used in .NET Core.
        var type = typeof(T);
        if (HasNoReference(type))
            return false;
        if (!type.IsValueType)
            return true;
        var fields = type.GetFields(System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Public);

        // Do not use recursion here. Preventing stack overflow is too much work to be worth it.
        if (fields.All(f => HasNoReference(f.FieldType)))
            return false;

        return true;

        static bool HasNoReference(Type type)
        {
            if (type.IsPrimitive)
                return true;
            if (type.IsEnum)
                return true;
            if (type == typeof(Guid)
                || type == typeof(decimal)
                || type == typeof(DateTime)
                || type == typeof(TimeSpan)
                || type == typeof(DateTimeOffset)
                )
                return true;
            return false;
        }
#endif
    }
}
