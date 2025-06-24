namespace Bshox.Generator.Extensions;

internal static class EnumerableExtensions
{
    public static string NewLine(this IEnumerable<string> source)
    {
        return string.Join("\n", source);
    }

    public static bool HasDuplicate<T>(this IEnumerable<T> source)
    {
        var set = new HashSet<T>();
        foreach (var item in source)
        {
            if (!set.Add(item))
            {
                return true;
            }
        }
        return false;
    }

    public static IEnumerable<TSource> DistinctBy<TSource, TKey>(this IEnumerable<TSource> source, Func<TSource, TKey> keySelector) => source.DistinctBy(keySelector, null);

    public static IEnumerable<TSource> DistinctBy<TSource, TKey>(this IEnumerable<TSource> source, Func<TSource, TKey> keySelector, IEqualityComparer<TKey>? comparer)
    {
        return DistinctByIterator(source, keySelector, comparer);
    }

    private static IEnumerable<TSource> DistinctByIterator<TSource, TKey>(IEnumerable<TSource> source, Func<TSource, TKey> keySelector, IEqualityComparer<TKey>? comparer)
    {
        using IEnumerator<TSource> enumerator = source.GetEnumerator();

        if (enumerator.MoveNext())
        {
            var set = new HashSet<TKey>(comparer);
            do
            {
                TSource element = enumerator.Current;
                if (set.Add(keySelector(element)))
                {
                    yield return element;
                }
            } while (enumerator.MoveNext());
        }
    }
}
