using Bshox;
using Bshox.Attributes;

namespace Samples;

[BshoxContract]
record Type1
{
    [BshoxMember(1)]
    public int Value1 { get; set; }

    [BshoxMember(2)]
    public string? Value2 { get; set; }
}

[BshoxSerializable<Type1>]
partial class Example1;

class Tests
{
    [Test]
    public async Task Example1Test()
    {
        var value = new Type1 { Value1 = 42, Value2 = "Hello" };
        byte[] data = Example1.Type1.Serialize(value);
        var result = Example1.Type1.Deserialize(data);
        await Assert.That(result).IsEqualTo(value);
    }
}
