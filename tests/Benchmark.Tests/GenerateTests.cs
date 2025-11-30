using Generator.Benchmark;

namespace Benchmark.Tests;

internal class GenerateTests
{
    [Test]
    public async Task BaseTest()
    {
        (var trees, var diags) = new Generate().Base();
        await Assert.That(trees).IsNotNull();
        await Assert.That(diags).IsEmpty();
        await Assert.That(trees).Count().IsEqualTo(1);
    }

    [Test]
    public async Task BshoxTest()
    {
        (var trees, var diags) = new Generate().BshoxGenerator();
        await Assert.That(trees).IsNotNull();
        await Assert.That(diags).IsEmpty();
        await Assert.That(trees).Count().IsEqualTo(3);
    }

    [Test]
    public async Task JsonTest()
    {
        (var trees, var diags) = new Generate().JsonGenerator();
        await Assert.That(trees).IsNotNull();
        await Assert.That(diags).IsEmpty();
        await Assert.That(trees).Count().IsEqualTo(8);
    }
}
