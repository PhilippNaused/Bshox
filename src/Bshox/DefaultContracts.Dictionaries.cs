using System.Collections.Concurrent;
using System.Collections.ObjectModel;
using Bshox.Contracts;

// ReSharper disable InconsistentNaming

namespace Bshox;

public static partial class DefaultContracts
{
    /// <summary>
    /// A Bshox contract for a <see cref="System.Collections.Generic.Dictionary{TKey,TValue}"/>.
    /// </summary>
    public static BshoxContract<Dictionary<TKey, TValue>> Dictionary<TKey, TValue>(BshoxContract<TKey> keyContract, BshoxContract<TValue> valueContract) where TKey : notnull
        => new DictionaryContract<Dictionary<TKey, TValue>, TKey, TValue>(keyContract, valueContract,
            factory: static count => new Dictionary<TKey, TValue>(count));

    /// <summary>
    /// A Bshox contract for a <see cref="System.Collections.Generic.IDictionary{TKey,TValue}"/>.
    /// </summary>
    public static BshoxContract<IDictionary<TKey, TValue>> IDictionary<TKey, TValue>(BshoxContract<TKey> keyContract, BshoxContract<TValue> valueContract) where TKey : notnull
        => new DictionaryContract<IDictionary<TKey, TValue>, TKey, TValue>(keyContract, valueContract,
            factory: static count => new Dictionary<TKey, TValue>(count));

    /// <summary>
    /// A Bshox contract for a <see cref="System.Collections.Generic.IReadOnlyDictionary{TKey,TValue}"/>.
    /// </summary>
    public static BshoxContract<IReadOnlyDictionary<TKey, TValue>> IReadOnlyDictionary<TKey, TValue>(BshoxContract<TKey> keyContract, BshoxContract<TValue> valueContract) where TKey : notnull
        => new DictionaryContract2<IReadOnlyDictionary<TKey, TValue>, TKey, TValue>(keyContract, valueContract,
            factory: static dict => dict);

    /// <summary>
    /// A Bshox contract for a <see cref="System.Collections.Concurrent.ConcurrentDictionary{TKey,TValue}"/>.
    /// </summary>
    public static BshoxContract<ConcurrentDictionary<TKey, TValue>> ConcurrentDictionary<TKey, TValue>(BshoxContract<TKey> keyContract, BshoxContract<TValue> valueContract) where TKey : notnull
        => new DictionaryContract<ConcurrentDictionary<TKey, TValue>, TKey, TValue>(keyContract, valueContract,
            factory: static count => new ConcurrentDictionary<TKey, TValue>(concurrencyLevel: Environment.ProcessorCount, capacity: count));

    /// <summary>
    /// A Bshox contract for a <see cref="System.Collections.Generic.SortedDictionary{TKey,TValue}"/>.
    /// </summary>
    public static BshoxContract<SortedDictionary<TKey, TValue>> SortedDictionary<TKey, TValue>(BshoxContract<TKey> keyContract, BshoxContract<TValue> valueContract) where TKey : notnull
        => new DictionaryContract<SortedDictionary<TKey, TValue>, TKey, TValue>(keyContract, valueContract,
            factory: static _ => new SortedDictionary<TKey, TValue>());

    /// <summary>
    /// A Bshox contract for a <see cref="System.Collections.ObjectModel.ReadOnlyDictionary{TKey,TValue}"/>.
    /// </summary>
    public static BshoxContract<ReadOnlyDictionary<TKey, TValue>> ReadOnlyDictionary<TKey, TValue>(BshoxContract<TKey> keyContract, BshoxContract<TValue> valueContract) where TKey : notnull
        => new DictionaryContract2<ReadOnlyDictionary<TKey, TValue>, TKey, TValue>(keyContract, valueContract,
            factory: static dict => new ReadOnlyDictionary<TKey, TValue>(dict));
}
