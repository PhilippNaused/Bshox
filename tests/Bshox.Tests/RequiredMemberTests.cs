using Bshox.Utils;
using TestModels;

namespace Bshox.Tests;

public class RequiredMemberTests
{
    [Test]
    public async Task Serialize()
    {
        var value = new TestClass11
        {
            Value1 = 0,
            Value2 = 0,
        };

        await Serializer11.TestClass11.TestSerialization(value);
        await Serializer11.TestClass11.TestProtoScope(value, """
         {
           1: 0
         }
         """);
    }

    [Test]
    public async Task TestException()
    {
        byte[] data = BshoxTextParser.Parse("""
         {
           # required member 1 is missing
           2: 7
         }
         """).ToBytes();

        var ex = Assert.Throws<BshoxException>(() => Serializer11.TestClass11.Deserialize(data));
        await Assert.That(ex.Message).IsEqualTo($"Required member \"{nameof(TestClass11.Value1)}\" (id: 1) is missing.");
    }
}
