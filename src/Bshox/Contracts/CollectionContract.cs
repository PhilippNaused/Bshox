using System.Buffers;
using System.Diagnostics;

namespace Bshox.Contracts;

/// <summary>
/// A Bshox contract for a <see cref="List{T}"/>.
/// </summary>
internal sealed class CollectionContract<TCollection, T>(BshoxContract<T> contract, Func<int, TCollection> factory, Func<IReadOnlyList<T>, TCollection> factory2)
    : BshoxContract<TCollection>(BshoxCode.Array)
    where TCollection : ICollection<T>
    where T : notnull
{
    private readonly ISpanContract<T>? _spanContract = contract as ISpanContract<T>;

    // We need to clear the array before returning it to the pool if T is a reference type, to avoid keeping objects alive longer than necessary.
#if NETCOREAPP
    private static readonly bool clearArray = System.Runtime.CompilerServices.RuntimeHelpers.IsReferenceOrContainsReferences<T>();
#else
    // The required API doesn't exist in .NET Standard 2.0, so we check for non-primitives instead.
    private static readonly bool clearArray = !typeof(T).IsPrimitive;
#endif

    /// <inheritdoc />
    public override void Serialize(ref BshoxWriter writer, scoped ref readonly TCollection value)
    {
        int count = value.Count;
        writer.WriteArrayHeader(count, contract.Encoding);

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
            value.CopyTo(array, 0);
            SerializeList(ref writer, array, count);
        }
        finally
        {
            ArrayPool<T>.Shared.Return(array, clearArray);
        }

        // avoid calling GetEnumerator() if possible to prevent allocating memory for the enumerator
        //foreach (T item in value)
        //{
        //    contract.Serialize(ref writer, in item);
        //}
    }

    private void SerializeList(ref BshoxWriter writer, IReadOnlyList<T> value, int count)
    {
        Debug.Assert(value.Count == count, "value.Count == count");
        for (int i = 0; i < count; i++)
        {
            T item = value[i];
            contract.Serialize(ref writer, in item);
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
        int count = value.Count;
        var array = ArrayPool<T>.Shared.Rent(count);
        try
        {
            value.CopyTo(array, 0);
            _spanContract!.Serialize(ref writer, array.AsSpan(0, count));
        }
        finally
        {
            ArrayPool<T>.Shared.Return(array, clearArray);
        }
    }

    /// <inheritdoc />
    public override void Deserialize(ref BshoxReader reader, out TCollection value)
    {
        int count = reader.ReadArrayHeader(out var encoding);
        BshoxException.ThrowIfWrongEncoding(encoding, contract.Encoding);

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

        value = factory(count);
        for (int i = 0; i < count; i++)
        {
            contract.Deserialize(ref reader, out T item);
            value.Add(item);
        }
    }

    private TCollection DeserializeSpan(ref BshoxReader reader, int count)
    {
        var array = ArrayPool<T>.Shared.Rent(count);
        try
        {
            _spanContract!.Deserialize(ref reader, array.AsSpan(0, count));
            return factory2(new ArraySegment<T>(array, 0, count));
        }
        finally
        {
            ArrayPool<T>.Shared.Return(array, clearArray);
        }
    }
}
