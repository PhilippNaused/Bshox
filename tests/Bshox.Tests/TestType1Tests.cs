using TestModels;

namespace Bshox.Tests;

public class TestType1Tests
{
    private static readonly Guid _guid = Guid.Parse("00112233-4455-6677-8899-AABBCCDDEEFF");

    [Test]
    public async Task Hex()
    {
        var value = new TestType1
        {
            Value1 = -42,
            Value2 = 42f,
            Value3 = _guid
        };

        await Serializer1.TestType1.TestSerialization(value, "09D6FF0312422800001C1000112233445566778899AABBCCDDEEFF00");

        await Serializer1.TestType1.TestProtoScope(value, """
            {
              1: 65494
              2: 42.0i32
              3: `00112233445566778899AABBCCDDEEFF`
            }
            """);
    }

    [Test]
    public async Task DefaultHex()
    {
        var value = new TestType1
        {
            Value1 = default,
            Value2 = default,
            Value3 = default
        };

        await Serializer1.TestType1.TestSerialization(value, "00");

        await Serializer1.TestType1.TestProtoScope(value, "{ }");
    }

    [Test]
    public async Task ListRoundTrip()
    {
        List<TestType1> list = [];

        for (int i = 0; i < 10; i++)
        {
            list.Add(new TestType1
            {
                Value1 = (short)i,
                Value2 = i,
                Value3 = default
            });
        }

        await Serializer1.ListTestType1.TestSerialization(list);

        await Serializer1.ListTestType1.TestProtoScope(list, """
            [
              { }
              {
                1: 1
                2: 1.0i32
              }
              {
                1: 2
                2: 2.0i32
              }
              {
                1: 3
                2: 3.0i32
              }
              {
                1: 4
                2: 4.0i32
              }
              {
                1: 5
                2: 5.0i32
              }
              {
                1: 6
                2: 6.0i32
              }
              {
                1: 7
                2: 7.0i32
              }
              {
                1: 8
                2: 8.0i32
              }
              {
                1: 9
                2: 9.0i32
              }
            ]
            """);

        await Serializer1.ListTestType1.TestProtoScope([], "[]");
    }

    [Test]
    public async Task ArrayRoundTrip()
    {
        TestType1[] list = new TestType1[10];

        for (int i = 0; i < 10; i++)
        {
            list[i] = new TestType1
            {
                Value1 = (short)i,
                Value2 = i,
                Value3 = default
            };
        }

        await Serializer1.TestType1Array.TestSerialization(list);
    }

    [Test]
    public async Task ListBase64()
    {
        List<TestType1> list = [];

        for (int i = 0; i < 3; i++)
        {
            list.Add(new TestType1
            {
                Value1 = (short)i,
                Value2 = default,
                Value3 = default
            });
        }

        await Serializer1.ListTestType1.TestSerialization(list, "1E00090100090200");
    }
}
