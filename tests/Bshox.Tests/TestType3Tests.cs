using TestModels;

namespace Bshox.Tests;

public class TestType3Tests
{
    [Test]
    public async Task SerializeHex()
    {
        var value = new TestType3
        {
            Value1 = new TestType1
            {
                Value1 = 42,
                Value2 = default,
                Value3 = default
            },
            Value2 = new TestType2
            {
                Value1 = 42,
                Value2 = "Test"
            }
        };

        await Serializer3.TestType3.TestSerialization(value, "0E092A0016092A1404546573740000");
        await Serializer3.TestType3.TestProtoScope(value, """
            {
              1: {
                1: 42
              }
              2: {
                1: 42
                2: "Test"
              }
            }
            """);
    }
}
