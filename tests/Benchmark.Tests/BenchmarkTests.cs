namespace Benchmark.Tests;

internal class BenchmarkTests
{
    [Test]
    public async Task Deserialize()
    {
        var b = new DeserializeCompare();
        b.Setup();

        await Assert.That(b._bshoxData.Length).IsEqualTo(3011);
#if NETFRAMEWORK
        await Assert.That(b._jsonData.Length).IsEqualTo(9865);
#else
        await Assert.That(b._jsonData.Length).IsEqualTo(9421);
#endif
        await Assert.That(b._messageData.Length).IsEqualTo(4463);
        await Assert.That(b._protoData.Length).IsEqualTo(4273);
        await Assert.That(b._googleData.Length).IsEqualTo(4273);

        // Deserialize methods
        var value = b.Bshox();
        await Assert.That(value).IsEquivalentTo(b.data);

        value = b.Json();
        await Assert.That(value).IsEquivalentTo(b.data);

        value = b.ProtoBuf();
        await Assert.That(value).IsEquivalentTo(b.data);

        value = b.MessagePack();
        await Assert.That(value).IsEquivalentTo(b.data);

        var value2 = b.Google();
        await Assert.That(value2).IsEquivalentTo(b.data2);
    }

    [Test]
    public async Task Parser()
    {
        var b = new Parser();
        var parsedValue = b.Parse();
        await Assert.That(parsedValue).IsEquivalentTo(b.Value);

        var fromBytes = b.FromBytes();
        await Assert.That(fromBytes).IsEquivalentTo(b.Value);

        await Assert.That(parsedValue.ToString()).IsEqualTo(b.Text);
    }

    [Test]
    [Arguments(1)]
    [Arguments(2)]
    [Arguments(1_000)]
    [Arguments(10_000)]
    public void Experimental(int times)
    {
        var b = new Experimental();
        for (int i = 0; i < times; i++)
        {
            b.Test1();
        }
    }
}
