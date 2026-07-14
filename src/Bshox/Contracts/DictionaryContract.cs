namespace Bshox.Contracts;

/// <summary>
/// A Bshox contract for a type derived from <see cref="IDictionary{TKey,TValue}"/> or <see cref="IReadOnlyDictionary{TKey,TValue}"/>."/>
/// </summary>
internal abstract class DictionaryContractBase<TDict, TKey, TValue>(BshoxContract<TKey> keyContract, BshoxContract<TValue> valueContract)
    : BshoxContract<TDict>(BshoxCode.Array)
    where TKey : notnull
    where TDict : IEnumerable<KeyValuePair<TKey, TValue>>
{
    private readonly byte _keyTag = checked((byte)((1 << 3) | (uint)keyContract.Encoding));
    private readonly byte _valueTag = checked((byte)((2 << 3) | (uint)valueContract.Encoding));

    /// <inheritdoc />
    public override void Serialize(ref BshoxWriter writer, scoped ref readonly TDict value)
    {
        int count = GetCount(value);

        if (count == 0)
        {
            writer.WriteByte((byte)BshoxCode.SubObject);
            return;
        }

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
            writer.WriteByte(_keyTag);
            keyContract.Serialize(ref writer, in key);
            if (value1 is not null)
            {
                writer.WriteByte(_valueTag);
                valueContract.Serialize(ref writer, in value1);
            }
            writer.WriteByte(0);
        }
    }

    /// <inheritdoc />
    public override void Deserialize(ref BshoxReader reader, out TDict value)
    {
        int count = reader.ReadArrayHeader(out var encoding);
        BshoxException.ThrowIfWrongEncoding(encoding, BshoxCode.SubObject);

        value = DeserializeInner(ref reader, count);
    }

    protected abstract TDict DeserializeInner(ref BshoxReader reader, int count);

    protected abstract int GetCount(TDict value);

    protected void ReadToDictionary<T>(ref BshoxReader reader, T dict, int count) where T : IDictionary<TKey, TValue>
    {
        for (int i = 0; i < count; i++)
        {
            TKey? key = default;
            TValue? value1 = default;

            var tag = reader.ReadByte();
            if (tag == _keyTag)
            {
                keyContract.Deserialize(ref reader, out key);
                tag = reader.ReadByte();
            }
            if (tag == _valueTag)
            {
                valueContract.Deserialize(ref reader, out value1);
                tag = reader.ReadByte();
            }
            if (tag != 0)
                throw BshoxException.UnexpectedTag(tag);

            if (key is null)
                throw new BshoxException("Key cannot be null.");
            dict.Add(key, value1!);
        }
    }
}

internal sealed class DictionaryContract<TDict, TKey, TValue>(BshoxContract<TKey> keyContract, BshoxContract<TValue> valueContract, Func<int, TDict> factory)
    : DictionaryContractBase<TDict, TKey, TValue>(keyContract, valueContract)
    where TKey : notnull
    where TDict : IDictionary<TKey, TValue>
{
    protected override int GetCount(TDict value) => value.Count;

    protected override TDict DeserializeInner(ref BshoxReader reader, int count)
    {
        var dict = factory(count);
        if (count != 0)
        {
            ReadToDictionary(ref reader, dict, count);
        }
        return dict;
    }
}

internal sealed class DictionaryContract2<TDict, TKey, TValue>(BshoxContract<TKey> keyContract, BshoxContract<TValue> valueContract, Func<Dictionary<TKey, TValue>, TDict> factory)
    : DictionaryContractBase<TDict, TKey, TValue>(keyContract, valueContract)
    where TKey : notnull
    where TDict : IReadOnlyDictionary<TKey, TValue>
{
    protected override int GetCount(TDict value) => value.Count;

    protected override TDict DeserializeInner(ref BshoxReader reader, int count)
    {
        var dict = new Dictionary<TKey, TValue>(count);
        if (count != 0)
        {
            ReadToDictionary(ref reader, dict, count);
        }
        return factory(dict);
    }
}
