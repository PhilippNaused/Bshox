using TestModels;

namespace Bshox.Tests;

public class EnumType1Tests
{
    [Test]
    public async Task TestType()
    {
        var value = new EnumType1
        {
            Value = TestEnum1.Value2
        };

        await EnumSerializer1.EnumType1.TestSerialization(value, "080200");

        value = new EnumType1
        {
            Value = TestEnum1.None,
            Value2 = TestEnum2.ValueX
        };

        await EnumSerializer1.EnumType1.TestSerialization(value, "10FFFFFFFFFFFFFFFFFF0100");
    }

    [Test]
    public async Task TestTypeDefault()
    {
        var value = new EnumType1
        {
            Value = TestEnum1.None
        };

        await EnumSerializer1.EnumType1.TestSerialization(value, "00");
    }

    [Test]
    public async Task TestEnum()
    {
        await EnumSerializer1.TestEnum1.TestSerialization(TestEnum1.Value3, "03");
    }

    [Test]
    public async Task TestEnumList()
    {
        await EnumSerializer1.ListTestEnum1.TestSerialization([TestEnum1.Value1, TestEnum1.Value2, TestEnum1.Value3], "18010203");
    }
}
