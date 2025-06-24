using TestModels;

namespace Bshox.Tests;

public class TestType9Tests
{
    [Test]
    public async Task Serialize()
    {
        var value = new TestClass9A.TestType9
        {
            Value = 42
        };

        await Serializer9.TestClass9ATestType9.TestSerialization(value);
    }
}
