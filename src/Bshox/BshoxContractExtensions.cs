using System.Buffers;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Reflection;
using Bshox.Internals;

namespace Bshox;

/// <summary>
/// Extension methods for <see cref="BshoxContract{T}"/>.
/// </summary>
public static class BshoxContractExtensions
{
    #region Serialize

    /// <summary>
    /// Serializes the specified <paramref name="value"/> using the provided <paramref name="contract"/> to the given <paramref name="buffer"/>.
    /// </summary>
    /// <typeparam name="T">The type of the value to serialize.</typeparam>
    /// <param name="contract">The contract that defines how to serialize values of type <typeparamref name="T"/>.</param>
    /// <param name="buffer">The buffer writer to which the serialized data will be written.</param>
    /// <param name="value">The value to serialize.</param>
    /// <param name="options">Optional serialization options to customize the serialization process. If <c>null</c>, <see cref="BshoxOptions.Default"/> is used.</param>
    /// <remarks>
    /// The buffer is flushed after serialization is complete.
    /// </remarks>
    public static void Serialize<T>(this BshoxContract<T> contract, IBufferWriter<byte> buffer, scoped in T value, BshoxOptions? options = null)
    {
        var writer = new BshoxWriter(buffer, options);
        contract.Serialize(ref writer, in value);
        writer.Flush();
    }

    /// <summary>
    /// Serializes the specified <paramref name="value"/> using the provided <paramref name="contract"/> to the given <paramref name="stream"/>.
    /// </summary>
    /// <typeparam name="T">The type of the value to serialize.</typeparam>
    /// <param name="contract">The contract that defines how to serialize values of type <typeparamref name="T"/>.</param>
    /// <param name="stream">The stream to which the serialized data will be written.</param>
    /// <param name="value">The value to serialize.</param>
    /// <param name="options">Optional serialization options to customize the serialization process. If <c>null</c>, <see cref="BshoxOptions.Default"/> is used.</param>
    public static void Serialize<T>(this BshoxContract<T> contract, Stream stream, scoped in T value, BshoxOptions? options = null)
    {
        // TODO: add optimization for MemoryStream
        using var buffer = new PooledByteBufferWriter();
        contract.Serialize(buffer, in value, options);
        buffer.WriteToStream(stream);
    }

    /// <summary>
    /// Serializes the specified <paramref name="value"/> using the provided <paramref name="contract"/> to a <see cref="byte"/> array.
    /// </summary>
    /// <typeparam name="T">The type of the value to serialize.</typeparam>
    /// <param name="contract">The contract that defines how to serialize values of type <typeparamref name="T"/>.</param>
    /// <param name="value">The value to serialize.</param>
    /// <param name="options">Optional serialization options to customize the serialization process. If <c>null</c>, <see cref="BshoxOptions.Default"/> is used.</param>
    /// <returns>A <see cref="byte"/> array containing the serialized data.</returns>
    public static byte[] Serialize<T>(this BshoxContract<T> contract, scoped in T value, BshoxOptions? options = null)
    {
        using var buffer = new PooledByteBufferWriter();
        contract.Serialize(buffer, in value, options);
        return buffer.ToArray();
    }

    // TODO: add async version

    #endregion Serialize

    #region Deserialize

    /// <summary>
    /// Deserializes a value of type <typeparamref name="T"/> from the specified <paramref name="sequence"/> of bytes using the provided <paramref name="contract"/>.
    /// </summary>
    /// <typeparam name="T">The type of the value to deserialize.</typeparam>
    /// <param name="contract">The contract that defines how to deserialize a value of type <typeparamref name="T"/>.</param>
    /// <param name="sequence">The <see cref="ReadOnlySequence{T}"/> of bytes containing the data to deserialize.</param>
    /// <param name="options">Optional settings that control deserialization behavior. If <c>null</c>, <see cref="BshoxOptions.Default"/> is used.</param>
    /// <returns>The deserialized value of type <typeparamref name="T"/>.</returns>
    public static T Deserialize<T>(this BshoxContract<T> contract, in ReadOnlySequence<byte> sequence, BshoxOptions? options = null)
    {
        var reader = new BshoxReader(sequence, options);
        contract.Deserialize(ref reader, out T value);
        return value;
    }

    /// <summary>
    /// Deserializes a value of type <typeparamref name="T"/> from the specified <paramref name="memory"/> using the provided <paramref name="contract"/>.
    /// </summary>
    /// <typeparam name="T">The type of the value to deserialize.</typeparam>
    /// <param name="contract">The contract that defines how to deserialize a value of type <typeparamref name="T"/>.</param>
    /// <param name="memory">The <see cref="ReadOnlyMemory{T}"/> containing the data to deserialize.</param>
    /// <param name="options">Optional settings that control deserialization behavior. If <c>null</c>, <see cref="BshoxOptions.Default"/> is used.</param>
    /// <returns>The deserialized value of type <typeparamref name="T"/>.</returns>
    public static T Deserialize<T>(this BshoxContract<T> contract, ReadOnlyMemory<byte> memory, BshoxOptions? options = null)
    {
        var reader = new BshoxReader(memory, options);
        contract.Deserialize(ref reader, out T value);
        return value;
    }

    /// <summary>
    /// Deserializes a value of type <typeparamref name="T"/> from the specified <paramref name="stream"/> using the provided <paramref name="contract"/>.
    /// </summary>
    /// <typeparam name="T">The type of the value to deserialize.</typeparam>
    /// <param name="contract">The contract that defines how to deserialize a value of type <typeparamref name="T"/>.</param>
    /// <param name="stream">The <see cref="Stream"/> containing the data to deserialize.</param>
    /// <param name="options">Optional settings that control deserialization behavior. If <c>null</c>, <see cref="BshoxOptions.Default"/> is used.</param>
    /// <returns>The deserialized value of type <typeparamref name="T"/>.</returns>
    /// <remarks>This method is optimized for use with <see cref="MemoryStream"/>.</remarks>
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

    /// <summary>
    /// Asynchronously deserializes a value of type <typeparamref name="T"/> from the specified <paramref name="stream"/> using the provided <paramref name="contract"/>.
    /// </summary>
    /// <typeparam name="T">The type of the value to deserialize.</typeparam>
    /// <param name="contract">The contract that defines how to deserialize a value of type <typeparamref name="T"/>.</param>
    /// <param name="stream">The <see cref="Stream"/> containing the data to deserialize.</param>
    /// <param name="options">Optional settings that control deserialization behavior. If <c>null</c>, <see cref="BshoxOptions.Default"/> is used.</param>
    /// <param name="cancellationToken">A <see cref="CancellationToken"/> to observe while waiting for the task to complete.</param>
    /// <returns>The deserialized value of type <typeparamref name="T"/>.</returns>
    /// <remarks>
    /// The content of <paramref name="stream"/> is read into a buffer asynchronously. Deserializing the data is done synchronously.<br/>
    /// If <paramref name="stream"/> is a <see cref="MemoryStream"/>, its internal buffer is used directly.
    /// </remarks>
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

    /// <summary>
    /// Attempts to return the internal buffer of the specified <paramref name="memoryStream"/> even if its exposable flag is not set.
    /// </summary>
    internal static bool TryGetBuffer(MemoryStream memoryStream, out ArraySegment<byte> buffer)
    {
        if (memoryStream.TryGetBuffer(out buffer))
        {
            return true;
        }
        if (memoryStream.GetType() != typeof(MemoryStream))
        {
            // derived type - we cannot rely on the internal structure of MemoryStream
            return false;
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
