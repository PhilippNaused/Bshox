namespace Bshox.Generator.Extensions;

#pragma warning disable IDE0051 // Remove unused private members (false positive for extension methods)

internal static class EnumerableExtensions
{
    extension(IEnumerable<string> source)
    {
        public string NewLine()
        {
            return string.Join("\n", source);
        }
    }

    extension<TSource>(IEnumerable<TSource> source)
    {
        public bool HasDuplicate()
        {
            var set = new HashSet<TSource>();
            foreach (var item in source)
            {
                if (!set.Add(item))
                {
                    return true;
                }
            }
            return false;
        }

        public IEnumerable<TSource> DistinctBy<TKey>(Func<TSource, TKey> keySelector) => source.DistinctBy(keySelector, null);

        public IEnumerable<TSource> DistinctBy<TKey>(Func<TSource, TKey> keySelector, IEqualityComparer<TKey>? comparer)
        {
            return DistinctByIterator(source, keySelector, comparer);
        }
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
