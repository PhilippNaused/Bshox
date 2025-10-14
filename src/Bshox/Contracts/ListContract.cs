namespace Bshox.Contracts;

/// <summary>
/// A Bshox contract for a <see cref="List{T}"/>.
/// </summary>
internal sealed class ListContract<T>(BshoxContract<T> contract) : BshoxContract<List<T>>(BshoxCode.Array) where T : notnull
{
    /// <inheritdoc />
    public override void Serialize(ref BshoxWriter writer, scoped ref readonly List<T> value)
    {
        int count = value.Count;
        writer.WriteArrayHeader(count, contract.Encoding);

        for (int i = 0; i < count; i++)
        {
            T item = value[i];
            contract.Serialize(ref writer, in item);
        }
    }

    /// <inheritdoc />
    public override void Deserialize(ref BshoxReader reader, out List<T> value)
    {
        int count = reader.ReadArrayHeader(out var encoding);
        BshoxException.ThrowIfWrongEncoding(encoding, contract.Encoding);

        value = new List<T>(count);
        for (int i = 0; i < count; i++)
        {
            contract.Deserialize(ref reader, out T item);
            value.Add(item);
        }
    }
}
