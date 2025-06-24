using Bshox.Contracts;

namespace Bshox.Tests;

internal class ContractTests
{
    [Test]
    [MatrixDataSource]
    public async Task Booleans([Matrix] bool i)
    {
        await DefaultContracts.Boolean.TestSerialization(i);
    }

    [Test]
    [MethodDataSource(typeof(ExampleData), nameof(ExampleData.SBytes))]
    public async Task SBytes(sbyte i)
    {
        await DefaultContracts.SByte.TestSerialization(i);
    }

    [Test]
    [MethodDataSource(typeof(ExampleData), nameof(ExampleData.Bytes))]
    public async Task Bytes(byte i)
    {
        await DefaultContracts.Byte.TestSerialization(i);
    }

    [Test]
    [MethodDataSource(typeof(ExampleData), nameof(ExampleData.Shorts))]
    public async Task Shorts(short i)
    {
        await DefaultContracts.Int16.TestSerialization(i);
    }

    [Test]
    [MethodDataSource(typeof(ExampleData), nameof(ExampleData.UShorts))]
    public async Task UShorts(ushort i)
    {
        await DefaultContracts.UInt16.TestSerialization(i);
    }

    [Test]
    [MethodDataSource(typeof(ExampleData), nameof(ExampleData.Ints))]
    public async Task Ints(int i)
    {
        await DefaultContracts.Int32.TestSerialization(i);
    }

    [Test]
    [MethodDataSource(typeof(ExampleData), nameof(ExampleData.Ints))]
    public async Task Int32Z(int i)
    {
        await DefaultContracts.Int32Z.TestSerialization(i);
    }

    [Test]
    [MethodDataSource(typeof(ExampleData), nameof(ExampleData.UInts))]
    public async Task UInts(uint i)
    {
        await DefaultContracts.UInt32.TestSerialization(i);
    }

    [Test]
    [MethodDataSource(typeof(ExampleData), nameof(ExampleData.Longs))]
    public async Task Longs(long i)
    {
        await DefaultContracts.Int64.TestSerialization(i);
    }

    [Test]
    [MethodDataSource(typeof(ExampleData), nameof(ExampleData.Longs))]
    public async Task Int64Z(long i)
    {
        await DefaultContracts.Int64Z.TestSerialization(i);
    }

    [Test]
    [MethodDataSource(typeof(ExampleData), nameof(ExampleData.ULongs))]
    public async Task ULongs(ulong i)
    {
        await DefaultContracts.UInt64.TestSerialization(i);
    }

    [Test]
    [MethodDataSource(typeof(ExampleData), nameof(ExampleData.Floats))]
    public async Task Floats(float i)
    {
        await DefaultContracts.Single.TestSerialization(i);
    }

    [Test]
    public async Task FloatArray()
    {
        var array = ExampleData.Floats().ToArray();
        await new ArrayContract<float>(DefaultContracts.Single).TestSerialization(array);
    }

    [Test]
    public async Task DoubleArray()
    {
        var array = ExampleData.Doubles().ToArray();
        await new ArrayContract<double>(DefaultContracts.Double).TestSerialization(array);
    }

    [Test]
    [MethodDataSource(typeof(ExampleData), nameof(ExampleData.Doubles))]
    public async Task Doubles(double i)
    {
        await DefaultContracts.Double.TestSerialization(i);
    }

    [Test]
    [MethodDataSource(typeof(ExampleData), nameof(ExampleData.Chars))]
    public async Task Chars(char i)
    {
        await DefaultContracts.Char.TestSerialization(i);
    }

    [Test]
    [MethodDataSource(typeof(ExampleData), nameof(ExampleData.Strings))]
    public async Task String(string s)
    {
        await DefaultContracts.String.TestSerialization(s);
    }

    [Test]
    [MethodDataSource(typeof(ExampleData), nameof(ExampleData.Guids))]
    public async Task Guid(Guid s)
    {
        await DefaultContracts.Guid.TestSerialization(s);
    }

    [Test]
    [MethodDataSource(typeof(ExampleData), nameof(ExampleData.DateTimes))]
    public async Task DateTime(DateTime s)
    {
        await DefaultContracts.DateTime.TestSerialization(s);
    }

    [Test]
    [MethodDataSource(typeof(ExampleData), nameof(ExampleData.TimeSpans))]
    public async Task TimeSpan(TimeSpan s)
    {
        await DefaultContracts.TimeSpan.TestSerialization(s);
    }

    [Test]
    [MethodDataSource(typeof(ExampleData), nameof(ExampleData.ByteArrays))]
    public async Task ByteArray(byte[] s)
    {
        await DefaultContracts.ByteArray.TestSerialization(s);
    }
}
