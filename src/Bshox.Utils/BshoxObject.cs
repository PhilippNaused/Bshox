using System.Collections;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Text;

namespace Bshox.Utils;

#pragma warning disable CA1710 // Identifiers should have correct suffix
public sealed class BshoxObject() : BshoxValue(BshoxCode.SubObject), ICollection<KeyValuePair<uint, BshoxValue>>
#pragma warning restore CA1710
{
    private readonly List<KeyValuePair<uint, BshoxValue>> _values = [];

    public static BshoxObject Read(ref BshoxReader reader)
    {
        var obj = new BshoxObject();
        while (true)
        {
            uint key = reader.ReadTag(out var encoding);
            if (key == 0)
            {
                BshoxException.ThrowIfWrongEncoding(encoding, BshoxCode.Null);
                break;
            }
            obj.Add(key, Read(ref reader, encoding));
        }
        return obj;
    }

    /// <inheritdoc />
    public override void Write(ref BshoxWriter writer)
    {
        using var _ = writer.DepthLock();
#if NET6_0_OR_GREATER
        foreach ((uint key, BshoxValue value) in _values)
        {
#else
        foreach (var pair in _values)
        {
            uint key = pair.Key;
            BshoxValue value = pair.Value;
#endif
            writer.WriteTag(key, value.Encoding);
            value.Write(ref writer);
        }
        writer.WriteByte(0);
    }

    internal override void Write(StringBuilder text, ref uint indent)
    {
        if (Count == 0)
        {
            _ = text.AppendFormat("{0} {1}", Constants.StartObject, Constants.EndObject);
            return;
        }
        _ = text.Append(Constants.StartObject).AppendLine();
        indent++;
#if NET6_0_OR_GREATER
        foreach ((uint key, BshoxValue value) in _values)
        {
#else
        foreach (var pair in _values)
        {
            uint key = pair.Key;
            BshoxValue value = pair.Value;
#endif
            WriteIndent(text, indent);
            _ = text.Append(key.ToString("D", CultureInfo.InvariantCulture))
                .Append(": ");
            value.Write(text, ref indent);
            _ = text.AppendLine(); // TODO: use only LF
        }

        indent--;
        WriteIndent(text, indent);
        _ = text.Append(Constants.EndObject);
    }

    public void Add(uint key, BshoxValue value) => _values.Add(new KeyValuePair<uint, BshoxValue>(key, value));

    /// <inheritdoc />
    public void Clear() => _values.Clear();

    public bool ContainsKey(uint key) => _values.Any(kv => kv.Key == key);

    public bool Remove(uint key)
    {
        var index = _values.FindIndex(kv => kv.Key == key);
        if (index >= 0)
        {
            _values.RemoveAt(index);
            return true;
        }
        return false;
    }

    public bool TryGetValue(uint key, [NotNullWhen(true)] out BshoxValue? value)
    {
        KeyValuePair<uint, BshoxValue> kv = _values.Find(kv => kv.Key == key);
        value = kv.Value;
        return value is not null;
    }

    /// <inheritdoc />
    public int Count => _values.Count;

    public BshoxValue this[uint key]
    {
        get
        {
            KeyValuePair<uint, BshoxValue> kv = _values.Find(kv => kv.Key == key);
            return kv.Value;
        }
    }

    #region ICollection members

    /// <inheritdoc />
    bool ICollection<KeyValuePair<uint, BshoxValue>>.IsReadOnly => false;

    /// <inheritdoc />
    bool ICollection<KeyValuePair<uint, BshoxValue>>.Contains(KeyValuePair<uint, BshoxValue> item) => (_values as ICollection<KeyValuePair<uint, BshoxValue>>).Contains(item);

    /// <inheritdoc />
    void ICollection<KeyValuePair<uint, BshoxValue>>.CopyTo(KeyValuePair<uint, BshoxValue>[] array, int arrayIndex) => (_values as ICollection<KeyValuePair<uint, BshoxValue>>).CopyTo(array, arrayIndex);

    /// <inheritdoc />
    bool ICollection<KeyValuePair<uint, BshoxValue>>.Remove(KeyValuePair<uint, BshoxValue> item) => (_values as ICollection<KeyValuePair<uint, BshoxValue>>).Remove(item);

    /// <inheritdoc />
    void ICollection<KeyValuePair<uint, BshoxValue>>.Add(KeyValuePair<uint, BshoxValue> item) => Add(item.Key, item.Value);

    /// <inheritdoc />
    public IEnumerator<KeyValuePair<uint, BshoxValue>> GetEnumerator() => _values.GetEnumerator();

    /// <inheritdoc />
    IEnumerator IEnumerable.GetEnumerator() => ((IEnumerable)_values).GetEnumerator();

    #endregion ICollection members
}
