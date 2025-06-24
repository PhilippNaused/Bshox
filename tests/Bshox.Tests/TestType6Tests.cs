using TestModels;

namespace Bshox.Tests;

public class TestType6Tests
{
    [Test]
    public async Task Test1()
    {
        var value = new TestType6(1, "hello");
        await Serializer6.TestType6.TestSerialization(value, "0901140568656C6C6F00");
    }

    [Test]
    public async Task Test2()
    {
        TestType6 value = default;
        await Serializer6.TestType6.TestSerialization(value, "00");
    }

    [Test]
    public async Task Test3()
    {
        var value = (7, new TestType6(1, "hello"));
        await Serializer6.ValueTupleInt32TestType6.TestSerialization(value, "0907160901140568656C6C6F0000");
    }
}
