using System.Buffers;

namespace Bshox.Contracts;

/// <summary>
/// A Bshox contract for a <see cref="List{T}"/>.
/// </summary>
internal sealed class ListContract<T>(BshoxContract<T> contract) : BshoxContract<List<T>>(BshoxCode.Array) where T : notnull
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
    public override void Serialize(ref BshoxWriter writer, scoped ref readonly List<T> value)
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

        for (int i = 0; i < count; i++)
        {
            T item = value[i];
            contract.Serialize(ref writer, in item);
        }
    }

    private void SerializeSpan(ref BshoxWriter writer, scoped ref readonly List<T> value)
    {
#if NETCOREAPP
        var span = System.Runtime.InteropServices.CollectionsMarshal.AsSpan(value);
        _spanContract!.Serialize(ref writer, span);
#else
        int count = value.Count;
        var array = ArrayPool<T>.Shared.Rent(count);
        try
        {
            value.CopyTo(array);
            _spanContract!.Serialize(ref writer, array.AsSpan(0, count));
        }
        finally
        {
            ArrayPool<T>.Shared.Return(array, clearArray);
        }
#endif
    }

    /// <inheritdoc />
    public override void Deserialize(ref BshoxReader reader, out List<T> value)
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

        value = new List<T>(count);
        for (int i = 0; i < count; i++)
        {
            contract.Deserialize(ref reader, out T item);
            value.Add(item);
        }
    }

    private List<T> DeserializeSpan(ref BshoxReader reader, int count)
    {
        var array = ArrayPool<T>.Shared.Rent(count);
        try
        {
            _spanContract!.Deserialize(ref reader, array.AsSpan(0, count));
            return new List<T>(new ArraySegment<T>(array, 0, count));
        }
        finally
        {
            ArrayPool<T>.Shared.Return(array, clearArray);
        }
    }
}
