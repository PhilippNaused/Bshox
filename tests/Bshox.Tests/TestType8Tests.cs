using TestModels;

namespace Bshox.Tests;

public class TestType8Tests
{
    [Test]
    public async Task Test1()
    {
        var value = new TestType8();
        await Serializer8.TestType8.TestSerialization(value, "00");
    }

    [Test]
    public async Task Test2()
    {
        var value = new TestType8
        {
            Value1 = -7,
            Value2 = 8,
            Value3 = -9,
            Value4 = 10,
            Value5 = -11,
            Value6 = 12,
            Value7 = "Hello",
            Value8 = true,
            Value9 = Guid.NewGuid(),
            Value10 = float.Epsilon,
            Value11 = double.NaN,
            Value12 = 13,
            Value13 = -14,
            Value14 = '☺',
            Value15 = TimeSpan.FromMinutes(1),
            Value16 = DateTime.UtcNow,
        };
        await Serializer8.TestType8.TestSerialization(value);
    }
}

public class TestType8BTests
{
    [Test]
    public async Task Test1()
    {
        var value = new TestType8B();
        await Serializer8.TestType8B.TestSerialization(value, "00");
    }

    [Test]
    public async Task Test2()
    {
        var value = new TestType8B
        {
            Value1 = "Hello, World!"u8.ToArray()
        };
        await Serializer8.TestType8B.TestSerialization(value);
    }
}
