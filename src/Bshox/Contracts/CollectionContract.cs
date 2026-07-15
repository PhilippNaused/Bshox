using System.Buffers;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using Bshox.Internals;

namespace Bshox.Contracts;

/// <summary>
/// A Bshox contract for a <see cref="ICollection{T}"/> or <see cref="IReadOnlyCollection{T}"/>
/// </summary>
internal abstract class CollectionContractBase<TCollection, T>
    : BshoxContract<TCollection>
    where TCollection : IEnumerable<T>
    where T : notnull
{
    protected readonly ISpanContract<T>? _spanContract;
    protected readonly BshoxContract<T> _contract;
    protected readonly Func<int, TCollection> _factory;
    protected readonly Func<IReadOnlyList<T>, TCollection> _factory2;

    /// <summary>
    /// A Bshox contract for a <see cref="ICollection{T}"/> or <see cref="IReadOnlyCollection{T}"/>
    /// </summary>
    protected CollectionContractBase(BshoxContract<T> contract, Func<int, TCollection> factory, Func<IReadOnlyList<T>, TCollection> factory2) : base(BshoxEncoding.Array)
    {
        _contract = contract;
        _factory = factory;
        _factory2 = factory2;
        _spanContract = contract as ISpanContract<T>;
    }

    // We need to clear the array before returning it to the pool if T is a reference type, to avoid keeping objects alive longer than necessary.
    protected static readonly bool ClearArray = Utils<T>.IsReferenceOrContainsReferences;

    protected abstract int GetCount(TCollection value);

    /// <inheritdoc />
    public override void Serialize(ref BshoxWriter writer, scoped ref readonly TCollection value)
    {
        int count = GetCount(value);

        if (count == 0)
        {
            writer.WriteByte((byte)_contract.Encoding);
            return;
        }

        writer.WriteArrayHeader(count, _contract.Encoding);

        // These value have been determined experimentally.
#if NETCOREAPP
        const int minSpanLength = 3;
#else
        const int minSpanLength = 13;
#endif

        if (_spanContract is not null && count >= minSpanLength)
        {
            SerializeSpan(ref writer, in value);
            return;
        }

        if (value is IReadOnlyList<T> list)
        {
            SerializeList(ref writer, list, count);
            return;
        }

        var array = ArrayPool<T>.Shared.Rent(count);
        try
        {
            CopyTo(value, array);
            SerializeList(ref writer, array, count);
        }
        finally
        {
            ArrayPool<T>.Shared.Return(array, ClearArray);
        }
    }

    private static void CopyTo(TCollection value, T[] array)
    {
        if (value is ICollection<T> collection)
        {
            collection.CopyTo(array, 0);
            return;
        }
        else if (value is System.Collections.ICollection collection2)
        {
            // Queue<> and Stack<> implement ICollection but not ICollection<T>.
            collection2.CopyTo(array, 0);
            return;
        }
        // Every collection type should implement ICollection<T> or ICollection, but just in case, we have this fallback.
        CopyToSlow(value, array);
    }

    [ExcludeFromCodeCoverage]
    [MethodImpl(MethodImplOptions.NoInlining)]
    private static void CopyToSlow(TCollection value, T[] array)
    {
        Debug.Fail("Dead code path");
        if (value is IReadOnlyList<T> list)
        {
            for (int i = 0; i < list.Count; i++)
            {
                array[i] = list[i];
            }
        }
        else
        {
            // Use foreach as a last resort since it may allocate memory for the enumerator.
            int i = 0;
            foreach (T item in value)
            {
                array[i++] = item;
            }
        }
    }

    private void SerializeList(ref BshoxWriter writer, IReadOnlyList<T> value, int count)
    {
        Debug.Assert(value.Count >= count, "value.Count >= count");
        for (int i = 0; i < count; i++)
        {
            T item = value[i];
            _contract.Serialize(ref writer, in item);
        }
    }

    private void SerializeSpan(ref BshoxWriter writer, scoped ref readonly TCollection value)
    {
#if NETCOREAPP
        if (value is List<T> list)
        {
            var span = System.Runtime.InteropServices.CollectionsMarshal.AsSpan(list);
            _spanContract!.Serialize(ref writer, span);
            return;
        }
#endif
        int count = GetCount(value);
        var array = ArrayPool<T>.Shared.Rent(count);
        try
        {
            CopyTo(value, array);
            _spanContract!.Serialize(ref writer, array.AsSpan(0, count));
        }
        finally
        {
            ArrayPool<T>.Shared.Return(array, ClearArray);
        }
    }

    /// <inheritdoc />
    public override void Deserialize(ref BshoxReader reader, out TCollection value)
    {
        int count = reader.ReadArrayHeader(out var encoding);
        BshoxException.ThrowIfWrongEncoding(encoding, _contract.Encoding);

        if (count == 0)
        {
            value = _factory(0);
            return;
        }

        // These value have been determined experimentally.
#if NETCOREAPP
        const int minSpanLength = 11;
#else
        const int minSpanLength = 7;
#endif

        if (_spanContract is not null && count >= minSpanLength)
        {
            value = DeserializeSpan(ref reader, count);
            return;
        }

        value = DeserializeInner(ref reader, count);
    }

    protected abstract TCollection DeserializeInner(ref BshoxReader reader, int count);

    private TCollection DeserializeSpan(ref BshoxReader reader, int count)
    {
        var array = ArrayPool<T>.Shared.Rent(count);
        try
        {
            _spanContract!.Deserialize(ref reader, array.AsSpan(0, count));
            return _factory2(new ArraySegment<T>(array, 0, count));
        }
        finally
        {
            ArrayPool<T>.Shared.Return(array, ClearArray);
        }
    }
}

/// <summary>
/// A Bshox contract for a <see cref="ICollection{T}"/>
/// </summary>
internal sealed class CollectionContract<TCollection, T>(BshoxContract<T> contract, Func<int, TCollection> factory, Func<IReadOnlyList<T>, TCollection> factory2)
    : CollectionContractBase<TCollection, T>(contract, factory, factory2)
    where TCollection : ICollection<T>
    where T : notnull
{
    /// <inheritdoc />
    protected override int GetCount(TCollection value) => value.Count;

    /// <inheritdoc />
    protected override TCollection DeserializeInner(ref BshoxReader reader, int count)
    {
        Debug.Assert(count >= 0, "count >= 0");
        var collection = _factory(count);
        for (int i = 0; i < count; i++)
        {
            _contract.Deserialize(ref reader, out T item);
            collection.Add(item);
        }
        return collection;
    }
}

/// <summary>
/// A Bshox contract for a <see cref="IReadOnlyCollection{T}"/>
/// </summary>
internal sealed class CollectionContract2<TCollection, T>(BshoxContract<T> contract, Func<int, TCollection> factory, Func<IReadOnlyList<T>, TCollection> factory2)
    : CollectionContractBase<TCollection, T>(contract, factory, factory2)
    where TCollection : IReadOnlyCollection<T>
    where T : notnull
{
    /// <inheritdoc />
    protected override int GetCount(TCollection value) => value.Count;

    /// <inheritdoc />
    protected override TCollection DeserializeInner(ref BshoxReader reader, int count)
    {
        Debug.Assert(count >= 0, "count >= 0");
        var array = ArrayPool<T>.Shared.Rent(count);
        try
        {
            for (int i = 0; i < count; i++)
            {
                _contract.Deserialize(ref reader, out T item);
                array[i] = item;
            }
            return _factory2(new ArraySegment<T>(array, 0, count));
        }
        finally
        {
            ArrayPool<T>.Shared.Return(array, ClearArray);
        }
    }
}
