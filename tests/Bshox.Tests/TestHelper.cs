using System.Diagnostics;
using System.Text;
using Bshox.Internals;
using Bshox.Utils;
using Bshox.TestUtils;
using System.Runtime.CompilerServices;

namespace Bshox.Tests;

public static class TestHelper
{
    [OverloadResolutionPriority(1)]
    public static async Task TestSerialization<T>(this BshoxContract<T[]> contract, T[] value, string? hex = null)
    {
        (byte[] bytes, BshoxValue metaValue, T[] actual) = await PreTest(contract, value);

        await Assert.That(actual).IsSequenceEqualTo(value);

        await PostTest(hex, bytes, metaValue);
    }

    [OverloadResolutionPriority(1)]
    public static async Task TestSerialization<T>(this BshoxContract<List<T>> contract, List<T> value, string? hex = null)
    {
        (byte[] bytes, BshoxValue metaValue, List<T> actual) = await PreTest(contract, value);

        await Assert.That(actual).IsSequenceEqualTo(value);

        await PostTest(hex, bytes, metaValue);
    }

    [OverloadResolutionPriority(1)]
    public static async Task TestSerialization<T, T2>(this BshoxContract<Dictionary<T, T2>> contract, Dictionary<T, T2> value, string? hex = null) where T : notnull
    {
        (byte[] bytes, BshoxValue metaValue, Dictionary<T, T2> actual) = await PreTest(contract, value);

        await Assert.That<IEnumerable<KeyValuePair<T, T2>>>(actual).IsSequenceEqualTo(value);

        await PostTest(hex, bytes, metaValue);
    }

    public static async Task TestSerialization<T>(this BshoxContract<T> contract, T value, string? hex = null)
    {
        (byte[] bytes, BshoxValue metaValue, T actual) = await PreTest(contract, value);

        await Assert.That(actual).IsEqualTo(value);

        await PostTest(hex, bytes, metaValue);
    }

    private static async Task<(byte[] bytes, BshoxValue metaValue, T actual)> PreTest<T>(BshoxContract<T> contract, T value)
    {
        var bytes = contract.Serialize(in value);
        Debug.WriteLine(bytes.ToHex());

        var reader = new BshoxReader(bytes.AsMemory());
        reader.SkipValue(contract.Encoding); // will throw on decoding error
        (var r, var c) = (reader.Remaining, reader.Consumed);
        await Assert.That(r).IsEqualTo(0);
        await Assert.That(c).IsEqualTo(bytes.Length); // must read to end

        reader = new BshoxReader(bytes.AsMemory());
        var metaValue = BshoxValue.Read(ref reader, contract.Encoding); // will throw on decoding error
        (r, c) = (reader.Remaining, reader.Consumed);
        await Assert.That(r).IsEqualTo(0);
        await Assert.That(c).IsEqualTo(bytes.Length); // must read to end

        reader = new BshoxReader(bytes.AsMemory());
        contract.Deserialize(ref reader, out T actual);
        (r, c) = (reader.Remaining, reader.Consumed);
        await Assert.That(r).IsEqualTo(0);
        await Assert.That(c).IsEqualTo(bytes.Length); // must read to end
        return (bytes, metaValue, actual);
    }

    private static async Task PostTest(string? hex, byte[] bytes, BshoxValue metaValue)
    {
        if (hex != null)
        {
            string actualHex = bytes.ToHex();
            await Assert.That(actualHex).IsEqualTo(hex);
        }

        using var buffer = new PooledByteBufferWriter();
        var writer = new BshoxWriter(buffer);
        metaValue.Write(ref writer);
        writer.Flush();
        await Assert.That(buffer.ToArray()).IsSequenceEqualTo(bytes);

        string text = metaValue.ToString();
        Debug.WriteLine(text);
        var meta2 = BshoxTextParser.Parse(text);
        await Assert.That(meta2).IsOfType(metaValue.GetType());
        string text2 = meta2.ToString();
        await Assert.That(text2).IsEqualTo(text);
    }

    [Conditional("DEBUG")]
    internal static void WriteContext(PooledByteBufferWriter buffer)
    {
        WriteContext(buffer.ToArray());
    }

    [Conditional("DEBUG")]
    public static void WriteContext(ReadOnlyMemory<byte> bytes)
    {
        byte[] array = bytes.ToArray();
        WriteContext(array);
    }

    [Conditional("DEBUG")]
    public static void WriteContext(byte[] bytes)
    {
        WriteContext(Encoding.UTF8.GetString(bytes));
        WriteContext(bytes.ToHex());
        WriteContext($"Length: {bytes.Length:N0} bytes");
    }

    [Conditional("DEBUG")]
    public static void WriteContext(string text)
    {
        // TestContext.Current?.OutputWriter.WriteLine(text);
    }

    public static async Task AssertEqual<T>(T actual, T expected, IEqualityComparer<T>? comparer = null)
    {
        if (comparer is not null)
        {
            await Assert.That(actual).IsEqualTo(expected, comparer);
        }
        else
        {
            await Assert.That(actual).IsEqualTo(expected);
        }
    }

    public static async Task TestProtoScope<T>(this BshoxContract<T> contract, T value, string text)
    {
        string actual = contract.ToBshoxString(value);
        await Assert.That(actual.Replace(Environment.NewLine, "\n")).IsEqualTo(text.Replace(Environment.NewLine, "\n"));
    }
}
