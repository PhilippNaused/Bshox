using System.Buffers;
using System.Diagnostics.CodeAnalysis;
using Bshox.Internals;

namespace Bshox;

/// <summary>
/// The base class for all Bshox serializers.
/// </summary>
/// <remarks>
/// The code generator will generate the implementation of a derived partial type if you add the <see cref="Bshox.Attributes.BshoxSerializerAttribute"/> to it.
/// </remarks>
/// <example>
/// <code lang="csharp">
/// [BshoxSerializer(typeof(int), typeof(string))]
/// partial class Example1;
/// </code>
/// </example>
public abstract class BshoxSerializer
{
    #region Contracts

    /// <summary>
    /// Returns the contract for type <typeparamref name="T"/>
    /// </summary>
    public BshoxContract<T> GetContract<T>()
    {
        Type type = typeof(T);
        return GetContractInternal(type) as BshoxContract<T> ?? throw BshoxException.ContractNotFound(type);
    }

    /// <summary>
    /// Returns the contract for <paramref name="type"/>.
    /// </summary>
    protected abstract IBshoxContract? GetContractInternal(Type type);

    private IBshoxContract GetContract(Type type) => GetContractInternal(type) ?? throw BshoxException.ContractNotFound(type);

    #endregion Contracts

    #region Deserialize

    /// <summary>
    /// Deserializes a value of type <paramref name="returnType"/> from the specified <paramref name="sequence"/> of bytes.
    /// </summary>
    /// <param name="sequence">The <see cref="ReadOnlySequence{T}"/> of bytes containing the data to deserialize.</param>
    /// <param name="returnType">The type of the value to deserialize.</param>
    /// <param name="options">Optional settings that control deserialization behavior. If <c>null</c>, <see cref="BshoxOptions.Default"/> is used.</param>
    /// <returns>The deserialized value of type <paramref name="returnType"/>.</returns>
    public object Deserialize(in ReadOnlySequence<byte> sequence, Type returnType, BshoxOptions? options = null)
    {
        var contract = GetContract(returnType);
        var reader = new BshoxReader(sequence, options);
        contract.Deserialize(ref reader, out var value);
        return value;
    }

    /// <summary>
    /// Deserializes a value of type <paramref name="returnType"/> from the specified <paramref name="memory"/>.
    /// </summary>
    /// <param name="memory">The <see cref="ReadOnlyMemory{T}"/> containing the data to deserialize.</param>
    /// <param name="returnType">The type of the value to deserialize.</param>
    /// <param name="options">Optional settings that control deserialization behavior. If <c>null</c>, <see cref="BshoxOptions.Default"/> is used.</param>
    /// <returns>The deserialized value of type <paramref name="returnType"/>.</returns>
    public object Deserialize(ReadOnlyMemory<byte> memory, Type returnType, BshoxOptions? options = null)
    {
        var contract = GetContract(returnType);
        var reader = new BshoxReader(memory, options);
        contract.Deserialize(ref reader, out var value);
        return value;
    }

    /// <summary>
    /// Deserializes a value of type <paramref name="returnType"/> from the specified <paramref name="stream"/>.
    /// </summary>
    /// <param name="stream">The <see cref="Stream"/> containing the data to deserialize.</param>
    /// <param name="returnType">The type of the value to deserialize.</param>
    /// <param name="options">Optional settings that control deserialization behavior. If <c>null</c>, <see cref="BshoxOptions.Default"/> is used.</param>
    /// <returns>The deserialized value of type <paramref name="returnType"/>.</returns>
    public object Deserialize(Stream stream, Type returnType, BshoxOptions? options = null)
    {
        var contract = GetContract(returnType);
        if (stream is MemoryStream memoryStream && TryDeserialize(contract, memoryStream, out object? value, options))
        {
            return value;
        }

        long startPos = -1;
        if (stream.CanSeek)
        {
            startPos = stream.Position;
        }
        using var sequence = new StreamSequence(stream);
        sequence.ReadAll(); // this always reads everything from the stream

        var reader = new BshoxReader(sequence.Sequence, options);
        contract.Deserialize(ref reader, out value);
        if (stream.CanSeek)
        {
            // seek back to the position where the data actually ended
            stream.Position = startPos + reader.Consumed;
        }
        return value;
    }

    /// <summary>
    /// Asynchronously deserializes a value of type <paramref name="returnType"/> from the specified <paramref name="stream"/>.
    /// </summary>
    /// <param name="stream">The <see cref="Stream"/> containing the data to deserialize.</param>
    /// <param name="returnType">The type of the value to deserialize.</param>
    /// <param name="options">Optional settings that control deserialization behavior. If <c>null</c>, <see cref="BshoxOptions.Default"/> is used.</param>
    /// <param name="cancellationToken">A <see cref="CancellationToken"/> to observe while waiting for the task to complete.</param>
    /// <returns>The deserialized value of type <paramref name="returnType"/>.</returns>
    /// <remarks>
    /// The content of <paramref name="stream"/> is read into a buffer asynchronously. Deserializing the data is done synchronously.<br/>
    /// If <paramref name="stream"/> is a <see cref="MemoryStream"/>, its internal buffer is used directly.
    /// </remarks>
    public async Task<object> DeserializeAsync(Stream stream, Type returnType, BshoxOptions? options = null, CancellationToken cancellationToken = default)
    {
        var contract = GetContract(returnType);
        if (stream is MemoryStream memoryStream && TryDeserialize(contract, memoryStream, out object? value, options))
        {
            return value;
        }

        long startPos = -1;
        if (stream.CanSeek)
        {
            startPos = stream.Position;
        }
        using var sequence = new StreamSequence(stream);
        await sequence.ReadAllAsync(cancellationToken).ConfigureAwait(false); // this always reads everything from the stream

        var reader = new BshoxReader(sequence.Sequence, options);
        contract.Deserialize(ref reader, out value);
        if (stream.CanSeek)
        {
            // seek back to the position where the data actually ended
            stream.Position = startPos + reader.Consumed;
        }
        return value;
    }

    private static bool TryDeserialize(IBshoxContract contract, MemoryStream memoryStream, [NotNullWhen(true)] out object? value, BshoxOptions? options = null)
    {
        if (BshoxContractExtensions.TryGetBuffer(memoryStream, out var memory))
        {
            var reader = new BshoxReader(memory, options);
            contract.Deserialize(ref reader, out value);
            memoryStream.Position += reader.Consumed;
            return true;
        }

        value = default;
        return false;
    }

    #endregion Deserialize

    #region Serialize

    /// <summary>
    /// Serializes the specified <paramref name="value"/> of type <paramref name="inputType"/> to the given <paramref name="buffer"/>.
    /// </summary>
    /// <param name="buffer">The buffer writer to which the serialized data will be written.</param>
    /// <param name="value">The value to serialize.</param>
    /// <param name="inputType">The type of the value to serialize.</param>
    /// <param name="options">Optional serialization options to customize the serialization process. If <c>null</c>, <see cref="BshoxOptions.Default"/> is used.</param>
    /// <remarks>
    /// The buffer is flushed after serialization is complete.
    /// </remarks>
    public void Serialize(IBufferWriter<byte> buffer, object value, Type inputType, BshoxOptions? options = null)
    {
        var contract = GetContract(inputType);
        var writer = new BshoxWriter(buffer, options);
        contract.Serialize(ref writer, value);
        writer.Flush();
    }

    /// <summary>
    /// Serializes the specified <paramref name="value"/> of type <paramref name="inputType"/> to the given <paramref name="stream"/>.
    /// </summary>
    /// <param name="stream">The stream to which the serialized data will be written.</param>
    /// <param name="value">The value to serialize.</param>
    /// <param name="inputType">The type of the value to serialize.</param>
    /// <param name="options">Optional serialization options to customize the serialization process. If <c>null</c>, <see cref="BshoxOptions.Default"/> is used.</param>
    public void Serialize(Stream stream, object value, Type inputType, BshoxOptions? options = null)
    {
        using var buffer = new PooledByteBufferWriter();
        Serialize(buffer, value, inputType, options);
        buffer.WriteToStream(stream);
    }

    /// <summary>
    /// Serializes the specified <paramref name="value"/> of type <paramref name="inputType"/> to a <see cref="byte"/> array.
    /// </summary>
    /// <param name="value">The value to serialize.</param>
    /// <param name="inputType">The type of the value to serialize.</param>
    /// <param name="options">Optional serialization options to customize the serialization process. If <c>null</c>, <see cref="BshoxOptions.Default"/> is used.</param>
    public byte[] Serialize(object value, Type inputType, BshoxOptions? options = null)
    {
        using var buffer = new PooledByteBufferWriter();
        Serialize(buffer, value, inputType, options);
        return buffer.ToArray();
    }

    #endregion Serialize
}
