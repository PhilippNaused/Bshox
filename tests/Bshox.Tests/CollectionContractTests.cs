namespace Bshox.Tests;

internal class CollectionContractTests
{
    private static readonly int[] ints = ExampleData.Ints().ToArray();
    private static readonly float[] floats = ExampleData.Floats().ToArray();
    private static readonly double[] doubles = ExampleData.Doubles().ToArray();

    [Test]
    public async Task IntArray()
    {
        var c = DefaultContracts.Array(DefaultContracts.Int32);
        await c.TestSerialization(ints);
        await c.TestSerialization([]);
    }

    [Test]
    public async Task IntList()
    {
        var c = DefaultContracts.List(DefaultContracts.Int32);
        await c.TestSerialization(ints.ToList());
        await c.TestSerialization([]);
    }

    [Test]
    public async Task FloatArray()
    {
        var c = DefaultContracts.Array(DefaultContracts.Single);
        await c.TestSerialization(floats);
        await c.TestSerialization([]);
    }

    [Test]
    public async Task FloatList()
    {
        var c = DefaultContracts.List(DefaultContracts.Single);
        await c.TestSerialization(floats.ToList());
        await c.TestSerialization([]);
    }

    [Test]
    public async Task DoubleArray()
    {
        var c = DefaultContracts.Array(DefaultContracts.Double);
        await c.TestSerialization(doubles);
        await c.TestSerialization([]);
    }

    [Test]
    public async Task DoubleList()
    {
        var c = DefaultContracts.List(DefaultContracts.Double);
        await c.TestSerialization(doubles.ToList());
        await c.TestSerialization([]);
    }

    [Test]
    public async Task DoubleIList()
    {
        var c = DefaultContracts.IList(DefaultContracts.Double);
        await c.TestSerialization2<IList<double>, double>(doubles);
        await c.TestSerialization2<IList<double>, double>([]);
    }

    [Test]
    public async Task DoubleICollection()
    {
        var c = DefaultContracts.ICollection(DefaultContracts.Double);
        await c.TestSerialization2<ICollection<double>, double>(doubles);
        await c.TestSerialization2<ICollection<double>, double>([]);
    }
}
