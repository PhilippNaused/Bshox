namespace Bshox.Contracts;

/// <summary>
/// A Bshox contract for a <see cref="Dictionary{TKey,TValue}"/>.
/// </summary>
internal sealed class DictionaryContract<TKey, TValue>(BshoxContract<TKey> keyContract, BshoxContract<TValue> valueContract) : BshoxContract<Dictionary<TKey, TValue>>(BshoxCode.Array) where TKey : notnull
{
    private readonly byte keyTag = checked((byte)((1 << 3) | (uint)keyContract.Encoding));
    private readonly byte valueTag = checked((byte)((2 << 3) | (uint)valueContract.Encoding));

    /// <inheritdoc />
    public override void Serialize(ref BshoxWriter writer, scoped ref readonly Dictionary<TKey, TValue> value)
    {
        int count = value.Count;
        writer.WriteArrayHeader(count, BshoxCode.SubObject);

#if NET6_0_OR_GREATER
        foreach ((TKey key, TValue value1) in value)
        {
#else
        foreach (var pair in value)
        {
            var key = pair.Key;
            var value1 = pair.Value;
#endif
            writer.WriteByte(keyTag);
            keyContract.Serialize(ref writer, in key);
            if (value1 is not null)
            {
                writer.WriteByte(valueTag);
                valueContract.Serialize(ref writer, in value1);
            }
            writer.WriteByte(0);
        }
    }

    /// <inheritdoc />
    public override void Deserialize(ref BshoxReader reader, out Dictionary<TKey, TValue> value)
    {
        int count = reader.ReadArrayHeader(out var encoding);
        BshoxException.ThrowIfWrongEncoding(encoding, BshoxCode.SubObject);

        value = new Dictionary<TKey, TValue>(count);

        for (int i = 0; i < count; i++)
        {
            TKey? key = default;
            TValue? value1 = default;

            while (true)
            {
                var tag = reader.ReadByte();
                if (tag == 0)
                    break;
                if (tag == keyTag)
                    keyContract.Deserialize(ref reader, out key);
                else if (tag == valueTag)
                    valueContract.Deserialize(ref reader, out value1);
                else
                    throw new BshoxException($"Unexpected tag: {tag}. Must be either 0, {keyTag}, or {valueTag}.");
            }

            if (key is null)
                throw new BshoxException("Key cannot be null.");
            value.Add(key, value1!);
        }
    }
}
