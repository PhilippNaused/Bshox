using TestModels;

namespace Bshox.Tests;

public class TestType2Tests
{
    [Test]
    public async Task SerializeHex()
    {
        var value = new TestType2
        {
            Value1 = 42,
            Value2 = "Hello, World!"
        };

        await Serializer2.TestType2.TestSerialization(value, "092A140D48656C6C6F2C20576F726C642100");
        await Serializer2.TestType2.TestProtoScope(value, """
            {
              1: 42
              2: "Hello, World!"
            }
            """);
    }

    [Test]
    public async Task SerializeListRoundTrip()
    {
        List<TestType2> list = [];

        for (int i = 0; i < 10; i++)
        {
            list.Add(new TestType2
            {
                Value1 = i,
                Value2 = $"Hello, World! {i}"
            });
        }

        await Serializer2.ListTestType2.TestSerialization(list);
    }

    [Test]
    public async Task SerializeArrayRoundTrip()
    {
        TestType2[] array = new TestType2[10];

        for (int i = 0; i < 10; i++)
        {
            array[i] = new TestType2
            {
                Value1 = i,
                Value2 = $"Hello, World! {i}"
            };
        }

        await Serializer2.TestType2Array.TestSerialization(array);
    }

    [Test]
    public async Task BshoxListBase64()
    {
        List<TestType2> list = [];

        for (int i = 0; i < 2; i++)
        {
            list.Add(new TestType2
            {
                Value1 = i,
                Value2 = $"Hello, World! {i}"
            });
        }

        await Serializer2.ListTestType2.TestSerialization(list, "16140F48656C6C6F2C20576F726C64212030000901140F48656C6C6F2C20576F726C6421203100");
    }
}
