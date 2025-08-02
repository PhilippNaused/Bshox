using System.Buffers;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Reflection;
using Bshox.Internals;

namespace Bshox;

public static class BshoxContractExtensions
{
    #region Serialize

    public static void Serialize<T>(this BshoxContract<T> contract, IBufferWriter<byte> buffer, scoped in T value)
    {
        var writer = new BshoxWriter(buffer);
        contract.Serialize(ref writer, in value);
        writer.Flush();
    }

    public static void Serialize<T>(this BshoxContract<T> contract, Stream stream, scoped in T value)
    {
        // TODO: add optimization for MemoryStream
        using var buffer = new PooledByteBufferWriter();
        contract.Serialize(buffer, in value);
        buffer.WriteToStream(stream);
    }

    public static byte[] Serialize<T>(this BshoxContract<T> contract, scoped in T value)
    {
        using var buffer = new PooledByteBufferWriter();
        contract.Serialize(buffer, in value);
        return buffer.WrittenMemory.ToArray();
    }

    // TODO: add async version

    #endregion Serialize

    #region Deserialize

    public static T Deserialize<T>(this BshoxContract<T> contract, in ReadOnlySequence<byte> sequence)
    {
        var reader = new BshoxReader(sequence);
        contract.Deserialize(ref reader, out T value);
        return value;
    }

    public static T Deserialize<T>(this BshoxContract<T> contract, ReadOnlyMemory<byte> memory)
    {
        var reader = new BshoxReader(memory);
        contract.Deserialize(ref reader, out T value);
        return value;
    }

    public static T Deserialize<T>(this BshoxContract<T> contract, Stream stream)
    {
        if (stream is MemoryStream memoryStream && contract.TryDeserialize(memoryStream, out T? value))
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

    public static async Task<T> DeserializeAsync<T>(this BshoxContract<T> contract, Stream stream, CancellationToken cancellationToken = default)
    {
        if (stream is MemoryStream memoryStream && contract.TryDeserialize(memoryStream, out T? value))
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

    internal static bool TryGetBuffer(MemoryStream memoryStream, out ReadOnlyMemory<byte> memory)
    {
        if (memoryStream.TryGetBuffer(out var buffer))
        {
            memory = buffer.AsMemory((int)memoryStream.Position);
            return true;
        }

        // the _exposable flag is not set.
        // => use reflection to get the private _buffer field
        byte[]? buffer2 = null;
        try
        {
            buffer2 = typeof(MemoryStream).GetField("_buffer", BindingFlags.NonPublic | BindingFlags.Instance)?.GetValue(memoryStream) as byte[];
        }
#pragma warning disable CA1031 // Do not catch general exception types
        catch
#pragma warning restore CA1031 // Do not catch general exception types
        {
            Debug.Fail("Failed to get the _buffer field from MemoryStream.");
        }
        if (buffer2 is not null)
        {
            memory = buffer2.AsMemory((int)memoryStream.Position);
            return true;
        }

        memory = default;
        return false;
    }

    private static bool TryDeserialize<T>(this BshoxContract<T> contract, MemoryStream memoryStream, [NotNullWhen(true)] out T? value)
    {
        if (TryGetBuffer(memoryStream, out var memory))
        {
            var reader = new BshoxReader(memory);
            contract.Deserialize(ref reader, out value!);
            memoryStream.Position += reader.Consumed;
            return true;
        }

        value = default;
        return false;
    }

    #endregion Deserialize
}
