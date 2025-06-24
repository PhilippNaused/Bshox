using System.Collections.Immutable;
using Microsoft.CodeAnalysis;

namespace Bshox.Generator.Extensions;

internal static class TypedConstantExtensions
{
    public static bool TryGetAs<T>(this ImmutableArray<KeyValuePair<string, TypedConstant>> keyValuePairs, string key, out T value)
    {
        if (keyValuePairs.TryGet(key, out TypedConstant constant))
        {
            value = (T)constant.Value!;
            return true;
        }
        value = default!;
        return false;
    }

    public static bool TryGet(this ImmutableArray<KeyValuePair<string, TypedConstant>> keyValuePairs, string key, out TypedConstant value)
    {
        for (int i = 0; i < keyValuePairs.Length; i++)
        {
            KeyValuePair<string, TypedConstant> item = keyValuePairs[i];
            if (item.Key == key)
            {
                value = item.Value;
                return true;
            }
        }
        value = default;
        return false;
    }
}
