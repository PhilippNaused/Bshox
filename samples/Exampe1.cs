using Bshox;
using Bshox.Attributes;

namespace Samples;

[BshoxSerializer(typeof(int), typeof(string))]
partial class Example1;

class Tests
{
    [Test]
    public async Task Example1Test()
    {
        byte[] data = Example1.Int32.Serialize(42);
        await Assert.That(data).IsEquivalentTo(new byte[] { 42 });

        data = Example1.String.Serialize("Hello, World!");
        await Assert.That(data).IsEquivalentTo("\rHello, World!"u8.ToArray());
    }
}
