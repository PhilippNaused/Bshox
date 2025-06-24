using TestModels;

namespace Bshox.Tests;

public class DefaultValueType1Test
{
    [Test]
    public async Task DeserializeDefault()
    {
        var value = new DefaultValueType1
        {
            Value1 = -4,
            Value2 = 3.14f,
            Value3 = "Hello, World!",
            Value4 = null,
            Value5 = null
        };

        await DefaultValueTypeSerializer.DefaultValueType1.TestSerialization(value, "2800");
    }
}
