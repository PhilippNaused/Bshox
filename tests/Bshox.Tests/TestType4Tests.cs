using TestModels;

namespace Bshox.Tests;

public class TestType4Tests
{
    [Test]
    public async Task SerializeHex()
    {
        var value = new TestType4
        {
            List = [new TestType1(), new TestType1 { Value1 = 1 }]
        };

        await Serializer4.TestType4.TestSerialization(value, "0C150008010000");
    }
}
