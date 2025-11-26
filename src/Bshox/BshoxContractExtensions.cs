using System.Buffers;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Reflection;
using Bshox.Internals;

namespace Bshox;

public static class BshoxContractExtensions
{
    #region Serialize

    public static void Serialize<T>(this BshoxContract<T> contract, IBufferWriter<byte> buffer, scoped in T value, BshoxOptions? options = null)
    {
        var writer = new BshoxWriter(buffer, options);
        contract.Serialize(ref writer, in value);
        writer.Flush();
    }

    public static void Serialize<T>(this BshoxContract<T> contract, Stream stream, scoped in T value, BshoxOptions? options = null)
    {
        // TODO: add optimization for MemoryStream
        using var buffer = new PooledByteBufferWriter();
        contract.Serialize(buffer, in value, options);
        buffer.WriteToStream(stream);
    }

    public static byte[] Serialize<T>(this BshoxContract<T> contract, scoped in T value, BshoxOptions? options = null)
    {
        using var buffer = new PooledByteBufferWriter();
        contract.Serialize(buffer, in value, options);
        return buffer.ToArray();
    }

    // TODO: add async version

    #endregion Serialize

    #region Deserialize

    public static T Deserialize<T>(this BshoxContract<T> contract, in ReadOnlySequence<byte> sequence, BshoxOptions? options = null)
    {
        var reader = new BshoxReader(sequence, options);
        contract.Deserialize(ref reader, out T value);
        return value;
    }

    public static T Deserialize<T>(this BshoxContract<T> contract, ReadOnlyMemory<byte> memory, BshoxOptions? options = null)
    {
        var reader = new BshoxReader(memory, options);
        contract.Deserialize(ref reader, out T value);
        return value;
    }

    public static T Deserialize<T>(this BshoxContract<T> contract, Stream stream, BshoxOptions? options = null)
    {
        if (stream is MemoryStream memoryStream && contract.TryDeserialize(memoryStream, out T? value, options))
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

    public static async Task<T> DeserializeAsync<T>(this BshoxContract<T> contract, Stream stream, BshoxOptions? options = null, CancellationToken cancellationToken = default)
    {
        if (stream is MemoryStream memoryStream && contract.TryDeserialize(memoryStream, out T? value, options))
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

    internal static bool TryGetBuffer(MemoryStream memoryStream, out ArraySegment<byte> buffer)
    {
        if (memoryStream.TryGetBuffer(out buffer))
        {
            return true;
        }

        // the _exposable flag is not set.
        // => use reflection to get the private _buffer field
        byte[]? array = null;
        int? origin = null;
        try
        {
            array = typeof(MemoryStream).GetField("_buffer", BindingFlags.NonPublic | BindingFlags.Instance)?.GetValue(memoryStream) as byte[];
            var obj = typeof(MemoryStream).GetField("_origin", BindingFlags.NonPublic | BindingFlags.Instance)?.GetValue(memoryStream);
            if (obj is int originValue)
            {
                origin = originValue;
            }
        }
#pragma warning disable CA1031 // Do not catch general exception types
        catch
#pragma warning restore CA1031 // Do not catch general exception types
        {
            Debug.Fail("Failed to get the _buffer field from MemoryStream.");
        }
        if (array is not null && origin is not null)
        {
            buffer = new ArraySegment<byte>(array, origin.Value, (int)memoryStream.Length);
            return true;
        }

        buffer = default;
        return false;
    }

    private static bool TryDeserialize<T>(this BshoxContract<T> contract, MemoryStream memoryStream, [NotNullWhen(true)] out T? value, BshoxOptions? options = null)
    {
        if (TryGetBuffer(memoryStream, out var buffer))
        {
            var memory = buffer.AsMemory((int)memoryStream.Position);
            var reader = new BshoxReader(memory, options);
            contract.Deserialize(ref reader, out value!);
            memoryStream.Position += reader.Consumed;
            return true;
        }

        value = default;
        return false;
    }

    #endregion Deserialize
}
