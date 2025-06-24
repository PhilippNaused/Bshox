namespace Bshox;

/// <summary>
/// A non-generic interface for <see cref="BshoxContract{T}"/>. Types must never implement this interface directly. Instead, they need to derive from <see cref="BshoxContract{T}"/>.
/// </summary>
public interface IBshoxContract
{
    /// <summary>
    /// The type that this contract is describing
    /// </summary>
    Type Type { get; }

    /// <summary>
    /// The encoding format used by this contract
    /// </summary>
    BshoxCode Encoding { get; }

    /// <summary>
    /// Serializes a value of type <see cref="Type"/>.
    /// </summary>
    /// <param name="writer">The writer that encodes the data</param>
    /// <param name="value">The value that is being serialized</param>
    void Serialize(ref BshoxWriter writer, object value);

    /// <summary>
    /// Deserializes a value of type <see cref="Type"/>.
    /// </summary>
    /// <param name="reader">The reader that data is read from</param>
    /// <param name="value">The value that was deserialized</param>
    void Deserialize(ref BshoxReader reader, out object value);
}

/// <summary>
/// The abstract base class for all Bshox contracts.
/// </summary>
/// <typeparam name="T">The type that this contract is describing</typeparam>
public abstract class BshoxContract<T> : IBshoxContract
{
    /// <param name="encoding">The encoding format used by this contract</param>
    protected BshoxContract(BshoxCode encoding)
    {
        if (encoding == BshoxCode.Null)
        {
            throw new ArgumentException($"The {nameof(encoding)} cannot be {nameof(BshoxCode.Null)}.", nameof(encoding));
        }
        Encoding = encoding;
    }

    /// <inheritdoc />
    public BshoxCode Encoding { get; }

    /// <inheritdoc />
    Type IBshoxContract.Type { get; } = typeof(T);

    /// <inheritdoc />
    void IBshoxContract.Serialize(ref BshoxWriter writer, object value)
    {
        T obj = (T)value;
        Serialize(ref writer, ref obj);
    }

    /// <inheritdoc />
    void IBshoxContract.Deserialize(ref BshoxReader reader, out object value)
    {
        Deserialize(ref reader, out T obj);
        value = obj!;
    }

    /// <summary>
    /// Serializes a value of type <typeparamref name="T"/>.
    /// </summary>
    /// <param name="writer">The writer that encodes the data</param>
    /// <param name="value">The value that is being serialized</param>
    public abstract void Serialize(ref BshoxWriter writer, scoped ref readonly T value);

    /// <summary>
    /// Deserializes a value of type <typeparamref name="T"/>.
    /// </summary>
    /// <param name="reader">The reader that data is read from</param>
    /// <param name="value">The value that was deserialized</param>
    public abstract void Deserialize(ref BshoxReader reader, out T value);
}

public interface ISpanContract<T>
{
    /// <summary>
    /// Serializes a span of values.
    /// </summary>
    /// <remarks>
    /// Calling this method is equivalent to calling <see cref="BshoxContract{T}.Serialize(ref BshoxWriter, ref readonly T)"/> for each element in the span.
    /// </remarks>
    void Serialize(ref BshoxWriter writer, ReadOnlySpan<T> values);

    /// <summary>
    /// Deserializes a span of values.
    /// </summary>
    /// <remarks>
    /// Calling this method is equivalent to calling <see cref="BshoxContract{T}.Deserialize(ref BshoxReader, out T)"/> for each element
    /// </remarks>
    void Deserialize(ref BshoxReader reader, Span<T> destination);
}
