namespace Bshox.Tests;

public class BshoxOptionsTests
{
    [Test]
    public async Task DefaultValue()
    {
        await Assert.That(BshoxOptions.Default.MaxDepth).IsEqualTo(BshoxOptions.DefaultMaxDepth);
        await Assert.That(BshoxOptions.Default.ToString()).IsEqualTo("BshoxOptions { MaxDepth = 64 }");
    }

    [Test]
    public async Task NegativeDepth()
    {
        await Assert.That(() => new BshoxOptions() { MaxDepth = -1 }).Throws<ArgumentOutOfRangeException>();
    }

    [Test]
    public async Task ValidDepth()
    {
        BshoxOptions options = new()
        {
            MaxDepth = 1
        };
        await Assert.That(options.MaxDepth).IsEqualTo(1);
        await Assert.That(options.ToString()).IsEqualTo("BshoxOptions { MaxDepth = 1 }");
    }
}
