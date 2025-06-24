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

    public object Deserialize(in ReadOnlySequence<byte> sequence, Type returnType)
    {
        var contract = GetContract(returnType);
        var reader = new BshoxReader(sequence);
        contract.Deserialize(ref reader, out var value);
        return value;
    }

    public object Deserialize(ReadOnlyMemory<byte> memory, Type returnType)
    {
        var contract = GetContract(returnType);
        var reader = new BshoxReader(memory);
        contract.Deserialize(ref reader, out var value);
        return value;
    }

    public object Deserialize(Stream stream, Type returnType)
    {
        var contract = GetContract(returnType);
        if (stream is MemoryStream memoryStream && TryDeserialize(contract, memoryStream, out object? value))
        {
            return value;
        }

        long startPos = stream.Position;
        using var sequence = new StreamSequence(stream);
        sequence.ReadAll(); // this always reads everything from the stream

        var reader = new BshoxReader(sequence.Sequence);
        contract.Deserialize(ref reader, out value);
        if (stream.CanSeek)
        {
            // seek back to the position where the data actually ended
            stream.Position = startPos + reader.Consumed;
        }
        return value;
    }

    public async Task<object> DeserializeAsync(Stream stream, Type returnType, CancellationToken cancellationToken = default)
    {
        var contract = GetContract(returnType);
        if (stream is MemoryStream memoryStream && TryDeserialize(contract, memoryStream, out object? value))
        {
            return value;
        }

        long startPos = stream.Position;
        using var sequence = new StreamSequence(stream);
        await sequence.ReadAllAsync(cancellationToken).ConfigureAwait(false); // this always reads everything from the stream

        var reader = new BshoxReader(sequence.Sequence);
        contract.Deserialize(ref reader, out value);
        if (stream.CanSeek)
        {
            // seek back to the position where the data actually ended
            stream.Position = startPos + reader.Consumed;
        }
        return value;
    }

    private static bool TryDeserialize(IBshoxContract contract, MemoryStream memoryStream, [NotNullWhen(true)] out object? value)
    {
        if (BshoxContractExtensions.TryGetBuffer(memoryStream, out var memory))
        {
            var reader = new BshoxReader(memory);
            contract.Deserialize(ref reader, out value);
            memoryStream.Position += reader.Consumed;
            return true;
        }

        value = default;
        return false;
    }

    #endregion Deserialize

    #region Serialize

    public void Serialize(IBufferWriter<byte> buffer, object value, Type inputType)
    {
        var contract = GetContract(inputType);
        var writer = new BshoxWriter(buffer);
        contract.Serialize(ref writer, value);
        writer.Flush();
    }

    public void Serialize(Stream stream, object value, Type inputType)
    {
        using var buffer = new PooledByteBufferWriter();
        Serialize(buffer, value, inputType);
        buffer.WriteToStream(stream);
    }

    public byte[] Serialize(object value, Type inputType)
    {
        using var buffer = new PooledByteBufferWriter();
        Serialize(buffer, value, inputType);
        return buffer.WrittenMemory.ToArray();
    }

    #endregion Serialize
}
