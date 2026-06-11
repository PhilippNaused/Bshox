using System.Collections.Concurrent;
using System.Collections.ObjectModel;
using Bshox.Contracts;

// ReSharper disable InconsistentNaming

namespace Bshox;

public static partial class DefaultContracts
{
    private static List<T> ListFactory<T>(int count) => new(count);
    private static List<T> ListFactory2<T>(IReadOnlyList<T> segment) => new(segment);

    /// <summary>
    /// A Bshox contract for a <see cref="System.Collections.Generic.List{T}"/>.
    /// </summary>
    public static BshoxContract<List<T>> List<T>(BshoxContract<T> contract) where T : notnull
        => new CollectionContract<List<T>, T>(contract, ListFactory<T>, ListFactory2);

    /// <summary>
    /// A Bshox contract for a <see cref="System.Collections.Generic.IList{T}"/>.
    /// </summary>
    public static BshoxContract<IList<T>> IList<T>(BshoxContract<T> contract) where T : notnull
        => new CollectionContract<IList<T>, T>(contract, ListFactory<T>, ListFactory2);

    /// <summary>
    /// A Bshox contract for a <see cref="System.Collections.Generic.ICollection{T}"/>.
    /// </summary>
    public static BshoxContract<ICollection<T>> ICollection<T>(BshoxContract<T> contract) where T : notnull
        => new CollectionContract<ICollection<T>, T>(contract, ListFactory<T>, ListFactory2);

    /// <summary>
    /// A Bshox contract for a <see cref="System.Collections.Generic.IReadOnlyCollection{T}"/>.
    /// </summary>
    public static BshoxContract<IReadOnlyCollection<T>> IReadOnlyCollection<T>(BshoxContract<T> contract) where T : notnull
        => new CollectionContract2<IReadOnlyCollection<T>, T>(contract, ListFactory<T>, ListFactory2);

    /// <summary>
    /// A Bshox contract for a <see cref="System.Collections.ObjectModel.Collection{T}"/>.
    /// </summary>
    public static BshoxContract<Collection<T>> Collection<T>(BshoxContract<T> contract) where T : notnull
        => new CollectionContract<Collection<T>, T>(contract,
            static capacity => new Collection<T>(),
            static list => new Collection<T>(list.ToList()));

    /// <summary>
    /// A Bshox contract for a <see cref="System.Collections.Generic.Queue{T}"/>.
    /// </summary>
    public static BshoxContract<Queue<T>> Queue<T>(BshoxContract<T> contract) where T : notnull
        => new CollectionContract2<Queue<T>, T>(contract,
            static capacity => new Queue<T>(capacity),
            static segment => new Queue<T>(segment));

    /// <summary>
    /// A Bshox contract for a <see cref="System.Collections.Generic.Stack{T}"/>.
    /// </summary>
    public static BshoxContract<Stack<T>> Stack<T>(BshoxContract<T> contract) where T : notnull
        => new CollectionContract2<Stack<T>, T>(contract,
            static capacity => new Stack<T>(capacity),
            static segment => new Stack<T>(segment));

    /// <summary>
    /// A Bshox contract for a <see cref="System.Collections.Generic.HashSet{T}"/>.
    /// </summary>
    public static BshoxContract<HashSet<T>> HashSet<T>(BshoxContract<T> contract) where T : notnull
        => new CollectionContract<HashSet<T>, T>(contract,
#if NETCOREAPP
            static capacity => new HashSet<T>(capacity),
#else
            static capacity => new HashSet<T>(),
#endif
            static segment => new HashSet<T>(segment));

    /// <summary>
    /// A Bshox contract for a <see cref="System.Collections.Concurrent.ConcurrentBag{T}"/>.
    /// </summary>
    public static BshoxContract<ConcurrentBag<T>> ConcurrentBag<T>(BshoxContract<T> contract) where T : notnull
        => new CollectionContract2<ConcurrentBag<T>, T>(contract,
            static capacity => new ConcurrentBag<T>(),
            static list => new ConcurrentBag<T>(list));

    /// <summary>
    /// A Bshox contract for a <see cref="System.Collections.Concurrent.ConcurrentQueue{T}"/>.
    /// </summary>
    public static BshoxContract<ConcurrentQueue<T>> ConcurrentQueue<T>(BshoxContract<T> contract) where T : notnull
        => new CollectionContract2<ConcurrentQueue<T>, T>(contract,
            static capacity => new ConcurrentQueue<T>(),
            static list => new ConcurrentQueue<T>(list));

    /// <summary>
    /// A Bshox contract for a <see cref="System.Collections.Concurrent.ConcurrentStack{T}"/>.
    /// </summary>
    public static BshoxContract<ConcurrentStack<T>> ConcurrentStack<T>(BshoxContract<T> contract) where T : notnull
        => new CollectionContract2<ConcurrentStack<T>, T>(contract,
            static capacity => new ConcurrentStack<T>(),
            static list => new ConcurrentStack<T>(list));

    /// <summary>
    /// A Bshox contract for an array of <typeparamref name="T"/>.
    /// </summary>
    public static BshoxContract<T[]> Array<T>(BshoxContract<T> contract) where T : notnull
        => new ArrayContract<T>(contract);
}
