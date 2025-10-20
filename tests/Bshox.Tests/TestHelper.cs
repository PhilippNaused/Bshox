using System.Diagnostics;
#if NETCOREAPP
using System.Diagnostics.CodeAnalysis;
#endif
using System.Text;
using Bshox.Internals;
using Bshox.Utils;
using Bshox.TestUtils;

namespace Bshox.Tests;

public static class TestHelper
{
#if NETCOREAPP
    private const DynamicallyAccessedMemberTypes DynamicallyAccessedMembers =
        DynamicallyAccessedMemberTypes.PublicFields | DynamicallyAccessedMemberTypes.NonPublicFields |
        DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties;
#endif

    public static async Task TestSerialization<
#if NETCOREAPP
        [DynamicallyAccessedMembers(DynamicallyAccessedMembers)]
#endif
    T>(this BshoxContract<T> contract, T value, string? hex = null)
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

        await AssertEqual(actual, value);

        if (hex != null)
        {
            string actualHex = bytes.ToHex();
            await Assert.That(actualHex).IsEqualTo(hex);
        }

        using var buffer = new PooledByteBufferWriter();
        var writer = new BshoxWriter(buffer);
        metaValue.Write(ref writer);
        writer.Flush();
        await AssertEqual(buffer.ToArray(), bytes);

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

    public static async Task AssertEqual<
#if NETCOREAPP
        [DynamicallyAccessedMembers(DynamicallyAccessedMembers)]
#endif
    T>(T actual, T expected, IEqualityComparer<T>? comparer = null)
    {
        if (comparer is not null)
        {
            await Assert.That(actual).IsEqualTo(expected, comparer);
        }
        else
        {
            await Assert.That(actual).IsEquivalentTo(expected);
        }
    }

    public static async Task TestProtoScope<T>(this BshoxContract<T> contract, T value, string text)
    {
        string actual = contract.ToBshoxString(value);
        await Assert.That(actual.Replace(Environment.NewLine, "\n")).IsEqualTo(text.Replace(Environment.NewLine, "\n"));
    }
}
