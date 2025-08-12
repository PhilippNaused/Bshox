using System.Collections;
using System.Diagnostics;
using System.Text;

namespace Bshox.Utils;

public sealed class BshoxArray(int capacity) : BshoxValue(BshoxCode.Array), IList<BshoxValue>
{
    private readonly List<BshoxValue> _values = new(capacity);

    public BshoxArray() : this(0)
    {
    }

    /// <summary>
    /// The encoding of the elements in the array.
    /// This value is set when the first element is added to the array.
    /// If the array is empty, the encoding may be <see cref="BshoxCode.Null"/>.
    /// </summary>
    public BshoxCode ElementEncoding { get; private set; }

    private void UpdateEncoding(BshoxValue newValue)
    {
        if (newValue.Encoding is BshoxCode.Null)
        {
            throw new BshoxException("Null values are not allowed in arrays");
        }
        if (_values.Count == 0)
        {
            // first element, set the element encoding
            ElementEncoding = newValue.Encoding;
            return;
        }
        if (newValue.Encoding != ElementEncoding)
        {
            throw new BshoxException($"Value encoding {newValue.Encoding} does not match array element type {ElementEncoding}");
        }
    }

    public static BshoxArray Read(ref BshoxReader reader)
    {
        var count = reader.ReadArrayHeader(out var elementEncoding);
        Debug.Assert(count >= 0, "count >= 0");
        Debug.Assert(elementEncoding is not BshoxCode.Null, "elementEncoding is not BshoxCode.Null"); // The reader should have thrown an exception
        var array = new BshoxArray(count)
        {
            ElementEncoding = elementEncoding
        };

        for (var i = 0; i < count; i++)
        {
            array.Add(Read(ref reader, elementEncoding));
        }
        return array;
    }

    /// <inheritdoc />
    public override void Write(ref BshoxWriter writer)
    {
        using var _ = writer.DepthLock();
        writer.WriteArrayHeader(Count, ElementEncoding);
        foreach (var value in _values)
        {
            Debug.Assert(value.Encoding == ElementEncoding, "value.Encoding == ElementEncoding");
            value.Write(ref writer);
        }
    }

    internal override void Write(StringBuilder text, ref uint indent)
    {
        if (Count == 0)
        {
            _ = text.Append(Constants.StartArray).Append(Constants.EndArray);
            return;
        }
        _ = text.Append(Constants.StartArray).AppendLine();
        indent++;

        foreach (var value in this)
        {
            WriteIndent(text, indent);
            value.Write(text, ref indent);
            _ = text.AppendLine();
        }

        indent--;
        WriteIndent(text, indent);
        _ = text.Append(Constants.EndArray);
    }

    #region IList members

    /// <inheritdoc />
    public IEnumerator<BshoxValue> GetEnumerator() => _values.GetEnumerator();

    /// <inheritdoc />
    IEnumerator IEnumerable.GetEnumerator() => ((IEnumerable)_values).GetEnumerator();

    /// <inheritdoc />
    public void Add(BshoxValue item)
    {
        UpdateEncoding(item);
        _values.Add(item);
    }

    /// <inheritdoc />
    public void Clear() => _values.Clear();

    /// <inheritdoc />
    public bool Contains(BshoxValue item) => _values.Contains(item);

    /// <inheritdoc />
    void ICollection<BshoxValue>.CopyTo(BshoxValue[] array, int arrayIndex) => _values.CopyTo(array, arrayIndex);

    /// <inheritdoc />
    public bool Remove(BshoxValue item) => _values.Remove(item);

    /// <inheritdoc />
    public int Count => _values.Count;

    /// <inheritdoc />
    bool ICollection<BshoxValue>.IsReadOnly => false;

    /// <inheritdoc />
    public int IndexOf(BshoxValue item) => _values.IndexOf(item);

    /// <inheritdoc />
    public void Insert(int index, BshoxValue item)
    {
        UpdateEncoding(item);
        _values.Insert(index, item);
    }

    /// <inheritdoc />
    public void RemoveAt(int index) => _values.RemoveAt(index);

    /// <inheritdoc />
    public BshoxValue this[int index]
    {
        get => _values[index];
        set
        {
            UpdateEncoding(value);
            _values[index] = value;
        }
    }

    #endregion IList members
}
