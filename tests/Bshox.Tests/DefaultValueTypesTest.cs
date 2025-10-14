using Bshox.Utils;
using TestModels;

namespace Bshox.Tests;

public class DefaultValueTypesTest
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

        await DefaultValueTypeSerializer.DefaultValueType1.TestSerialization(value, "00");
    }

    [Test]
    public async Task DeserializeDefault2()
    {
        var value = new DefaultValueType2
        {
            Value = "",
        };

        await DefaultValueTypeSerializer.DefaultValueType2.TestSerialization(value, "00");

        value = new DefaultValueType2
        {
            Value = null!,
        };

        var text = DefaultValueTypeSerializer.DefaultValueType2.ToBshoxString(value);
        await Assert.That(text).IsEqualTo("{ }");
    }

    [Test]
    public async Task DeserializeDefault3()
    {
        var value = new DefaultValueType3
        {
            Value = null!,
        };

        await DefaultValueTypeSerializer.DefaultValueType3.TestSerialization(value, "00");

        value = new DefaultValueType3
        {
            Value = "",
        };

        await DefaultValueTypeSerializer.DefaultValueType3.TestSerialization(value, "0B0000");
    }
}
