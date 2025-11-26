using System.Buffers;
using System.Diagnostics.CodeAnalysis;
using Bshox.Internals;

namespace Bshox;

public abstract class BshoxSerializer
{
    #region Contracts

    public BshoxContract<T> GetContract<T>()
    {
        Type type = typeof(T);
        return GetContractInternal(type) as BshoxContract<T> ?? throw BshoxException.ContractNotFound(type);
    }

    protected abstract IBshoxContract? GetContractInternal(Type type);

    private IBshoxContract GetContract(Type type) => GetContractInternal(type) ?? throw BshoxException.ContractNotFound(type);

    #endregion Contracts

    #region Deserialize

    public object Deserialize(in ReadOnlySequence<byte> sequence, Type returnType, BshoxOptions? options = null)
    {
        var contract = GetContract(returnType);
        var reader = new BshoxReader(sequence, options);
        contract.Deserialize(ref reader, out var value);
        return value;
    }

    public object Deserialize(ReadOnlyMemory<byte> memory, Type returnType, BshoxOptions? options = null)
    {
        var contract = GetContract(returnType);
        var reader = new BshoxReader(memory, options);
        contract.Deserialize(ref reader, out var value);
        return value;
    }

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

    public void Serialize(IBufferWriter<byte> buffer, object value, Type inputType, BshoxOptions? options = null)
    {
        var contract = GetContract(inputType);
        var writer = new BshoxWriter(buffer, options);
        contract.Serialize(ref writer, value);
        writer.Flush();
    }

    public void Serialize(Stream stream, object value, Type inputType, BshoxOptions? options = null)
    {
        using var buffer = new PooledByteBufferWriter();
        Serialize(buffer, value, inputType, options);
        buffer.WriteToStream(stream);
    }

    public byte[] Serialize(object value, Type inputType, BshoxOptions? options = null)
    {
        using var buffer = new PooledByteBufferWriter();
        Serialize(buffer, value, inputType, options);
        return buffer.ToArray();
    }

    #endregion Serialize
}
