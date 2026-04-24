namespace Bshox.Tests;

public class BshoxOptionsTests
{
    [Test]
    public async Task DefaultValue()
    {
        await Assert.That(BshoxOptions.Default.MaxDepth).IsEqualTo(BshoxOptions.DefaultMaxDepth);
        await Assert.That(BshoxOptions.Default.DefaultBufferSize).IsEqualTo(BshoxOptions.BufferSizeDefault);
        await Assert.That(BshoxOptions.Default.ToString()).IsEqualTo("BshoxOptions { MaxDepth = 64, LittleEndian = False, DefaultBufferSize = 16384 }");
    }

    [Test]
    public async Task NegativeDepth()
    {
        await Assert.That(() => new BshoxOptions { MaxDepth = -1 }).Throws<ArgumentOutOfRangeException>();
    }

    [Test]
    public async Task NegativeBufferSize()
    {
        await Assert.That(() => new BshoxOptions { DefaultBufferSize = -1 }).Throws<ArgumentOutOfRangeException>();
    }

    [Test]
    public async Task ValidDepth()
    {
        BshoxOptions options = new()
        {
            MaxDepth = 1
        };
        await Assert.That(options.MaxDepth).IsEqualTo(1);
        await Assert.That(options.ToString()).IsEqualTo("BshoxOptions { MaxDepth = 1, LittleEndian = False, DefaultBufferSize = 16384 }");
    }

    [Test]
    public async Task ValidBufferSize()
    {
        BshoxOptions options = new()
        {
            DefaultBufferSize = 1024
        };
        await Assert.That(options.DefaultBufferSize).IsEqualTo(1024);
        await Assert.That(options.ToString()).IsEqualTo("BshoxOptions { MaxDepth = 64, LittleEndian = False, DefaultBufferSize = 1024 }");
    }
}
