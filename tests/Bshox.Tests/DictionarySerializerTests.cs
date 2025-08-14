using TestModels;

namespace Bshox.Tests;

public class DictionarySerializerTests
{
    [Test]
    public async Task Test1()
    {
        Dictionary<string, TestType7?> value = new()
        {
            ["a"] = new TestType7(1, "a"),
            ["b"] = new TestType7(2, "b"),
            ["c"] = new TestType7(3, "c"),
            [" "] = null,
        };

        await DictionarySerializer1.DictionaryStringTestType7.TestSerialization(value, "250B016115080113016100000B016215080213016200000B016315080313016300000B012000");

        await DictionarySerializer1.DictionaryStringTestType7.TestProtoScope(value, """
                                                                              [
                                                                                {
                                                                                  1: "a"
                                                                                  2: {
                                                                                    1: 1
                                                                                    2: "a"
                                                                                  }
                                                                                }
                                                                                {
                                                                                  1: "b"
                                                                                  2: {
                                                                                    1: 2
                                                                                    2: "b"
                                                                                  }
                                                                                }
                                                                                {
                                                                                  1: "c"
                                                                                  2: {
                                                                                    1: 3
                                                                                    2: "c"
                                                                                  }
                                                                                }
                                                                                {
                                                                                  1: " "
                                                                                }
                                                                              ]
                                                                              """);

        var ms = new MemoryStream();
        DictionarySerializer1.DictionaryStringTestType7.Serialize(ms, value);
        ms.Position = 0;

        var value2 = DictionarySerializer2.ListValueTupleStringTestType7.Deserialize<List<(string Key, TestType7 Value)>>(ms);

        await TestHelper.AssertEqual(value2.Count, 4);

        await TestHelper.AssertEqual(value2[0].Key, "a");
        await TestHelper.AssertEqual(value2[0].Value, new TestType7(1, "a"));

        await TestHelper.AssertEqual(value2[1].Key, "b");
        await TestHelper.AssertEqual(value2[1].Value, new TestType7(2, "b"));

        await TestHelper.AssertEqual(value2[2].Key, "c");
        await TestHelper.AssertEqual(value2[2].Value, new TestType7(3, "c"));

        await TestHelper.AssertEqual(value2[3].Key, " ");
        await TestHelper.AssertEqual(value2[3].Value, null);
    }

    [Test]
    public async Task Test2()
    {
        List<(string Key, TestType7 Value)> value =
        [
            ("a", new TestType7(1, "a")),
            ("a", new TestType7(2, "b")), // duplicate key
            ("c", new TestType7(3, "c"))
        ];

        await DictionarySerializer2.ListValueTupleStringTestType7.TestSerialization(value, "1D0B016115080113016100000B016115080213016200000B01631508031301630000");

        var ms = new MemoryStream();
        DictionarySerializer2.ListValueTupleStringTestType7.Serialize(ms, value);
        ms.Position = 0;

        try
        {
            _ = DictionarySerializer1.DictionaryStringTestType7.Deserialize(ms);
            throw new InvalidOperationException("Expected ArgumentException");
        }
        catch (ArgumentException)
        {
            // duplicate key
        }
    }
}
