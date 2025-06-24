using TestModels;

namespace Bshox.Tests;

public class RecursiveTypeTests
{
    [Test]
    public async Task NonRecursive()
    {
        var value = new RecursiveTestType
        {
            Value1 = new RecursiveTestType()
        };

        await RecursiveTestTypeSerializer.RecursiveTestType.TestSerialization(value);
    }

    [Test]
    public void Recursive()
    {
        var value = new RecursiveTestType();
        value.Value1 = value;

        Assert.Throws<BshoxException>(() => RecursiveTestTypeSerializer.RecursiveTestType.Serialize(value));
    }

    [Test]
    public async Task AlmostRecursive()
    {
        var root = new RecursiveTestType();

        var current = root;
        for (int i = 1; i < default(BshoxOptions).MaxDepth - 2; i++)
        {
            current.Value1 = new RecursiveTestType();
            current = current.Value1;
        }

        await RecursiveTestTypeSerializer.RecursiveTestType.TestSerialization(root);
    }
}
