namespace Bshox.Contracts;

/// <summary>
/// A Bshox contract for an array of <typeparamref name="T"/>.
/// </summary>
internal sealed class ArrayContract<T>(BshoxContract<T> contract) : BshoxContract<T[]>(BshoxCode.Array) where T : notnull
{
    private readonly ISpanContract<T>? _spanContract = contract as ISpanContract<T>;

    /// <inheritdoc />
    public override void Serialize(ref BshoxWriter writer, scoped ref readonly T[] value)
    {
        int count = value.Length;
        writer.WriteArrayHeader(count, contract.Encoding);

        if (_spanContract is not null)
        {
            _spanContract.Serialize(ref writer, value);
            return;
        }

        for (int i = 0; i < count; i++)
        {
            T item = value[i];
            contract.Serialize(ref writer, in item);
        }
    }

    /// <inheritdoc />
    public override void Deserialize(ref BshoxReader reader, out T[] value)
    {
        int count = reader.ReadArrayHeader(out var encoding);
        BshoxException.ThrowIfWrongEncoding(encoding, contract.Encoding);

        value = new T[count];

        if (_spanContract is not null)
        {
            _spanContract.Deserialize(ref reader, value);
            return;
        }

        for (int i = 0; i < count; i++)
        {
            contract.Deserialize(ref reader, out T item);
            value[i] = item;
        }
    }
}
