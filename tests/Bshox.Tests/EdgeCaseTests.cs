using Bshox.TestUtils;
using TestModels;

namespace Bshox.Tests;

public class EdgeCaseTests
{
    [Test]
    public async Task NestedContract()
    {
        // A list of arrays of lists of arrays of TestType2
        List<List<TestType2[]>[]> list =
        [
            [
                [
                    [
                        new TestType2
                        {
                            Value1 = 1,
                            Value2 = "1"
                        },
                        new TestType2
                        {
                            Value1 = 2,
                            Value2 = "2"
                        }
                    ]
                ]
            ],
            [
                [
                    [
                        new TestType2
                        {
                            Value1 = 3,
                            Value2 = "3"
                        },
                        new TestType2
                        {
                            Value1 = 4,
                            Value2 = "4"
                        }
                    ]
                ]
            ]
        ];

        var ms = new MemoryStream();
        Serializer2.ListListTestType2ArrayArray.Serialize(ms, list);

        var hex = ms.ToArray().ToHex();
        await Assert.That(hex).IsEqualTo("140C0C150801130131000802130132000C0C15080313013300080413013400");

        ms.Position = 0;
        var list2 = Serializer2.ListListTestType2ArrayArray.Deserialize(ms);

        // TUnit cannot compare nested lists, so we'll just have to do it manually ;(
        await Assert.That(list2).Count().IsEqualTo(list.Count);
        for (int i = 0; i < list.Count; i++)
        {
            await Assert.That(list2[i]).Count().IsEqualTo(list[i].Length);
            for (int j = 0; j < list[i].Length; j++)
            {
                await Assert.That(list2[i][j]).Count().IsEqualTo(list[i][j].Count);
                for (int k = 0; k < list[i][j].Count; k++)
                {
                    await Assert.That(list2[i][j][k]).Count().IsEqualTo(list[i][j][k].Length);
                    for (int l = 0; l < list[i][j][k].Length; l++)
                    {
                        await Assert.That(list2[i][j][k][l].Value1).IsEqualTo(list[i][j][k][l].Value1);
                    }
                }
            }
        }
    }

    [Test]
    public async Task NullArray()
    {
        await Assert.ThrowsAsync<NullReferenceException>(async () => await DefaultContracts.Array(DefaultContracts.String).TestSerialization(null!));
    }

    [Test]
    public async Task NullList()
    {
        await Assert.ThrowsAsync<NullReferenceException>(async () => await DefaultContracts.List(DefaultContracts.String).TestSerialization(null!));
    }
}
