using System.Collections.Concurrent;
using System.Collections.ObjectModel;
using TestModels;

namespace Bshox.Tests;

internal class CollectionContractTests
{
    [Test]
    [GenerateGenericTest(typeof(int))] // primitive without span support
    [GenerateGenericTest(typeof(float))] // primitive with span support
    [GenerateGenericTest(typeof(double))] // primitive with span support
    [GenerateGenericTest(typeof(Guid))] // value type
    [GenerateGenericTest(typeof(TestType1))] // reference type
    [Arguments("Array")]
    [Arguments("Collection")]
    [Arguments("ConcurrentBag")]
    [Arguments("ConcurrentQueue")]
    [Arguments("ConcurrentStack")]
    [Arguments("HashSet")]
    [Arguments("ICollection")]
    [Arguments("IList")]
    [Arguments("IReadOnlyCollection")]
    [Arguments("List")]
    [Arguments("Queue")]
    [Arguments("Stack")]
    public Task TestRoundtrip<T>(string name) where T : notnull
    {
        T[][] examples = GetExamples<T>();
        return name switch
        {
            "Array" => TestAll(DefaultContracts.Array, x => x, examples),
            "Collection" => TestAll(DefaultContracts.Collection, x => new Collection<T>(x), examples),
            "ConcurrentBag" => TestAll(DefaultContracts.ConcurrentBag, x => new ConcurrentBag<T>(x), examples),
            "ConcurrentQueue" => TestAll(DefaultContracts.ConcurrentQueue, x => new ConcurrentQueue<T>(x), examples),
            "ConcurrentStack" => TestAll(DefaultContracts.ConcurrentStack, x => new ConcurrentStack<T>(x), examples),
            "HashSet" => TestAll(DefaultContracts.HashSet, x => new HashSet<T>(x), examples),
            "ICollection" => TestAll(DefaultContracts.ICollection, x => x, examples),
            "IList" => TestAll(DefaultContracts.IList, x => x, examples),
            "IReadOnlyCollection" => TestAll(DefaultContracts.IReadOnlyCollection, x => x, examples),
            "List" => TestAll(DefaultContracts.List, x => new List<T>(x), examples),
            "Queue" => TestAll(DefaultContracts.Queue, x => new Queue<T>(x), examples),
            "Stack" => TestAll(DefaultContracts.Stack, x => new Stack<T>(x), examples),
            _ => throw new NotSupportedException($"Contract {name} is not supported."),
        };
    }

    private static IBshoxContract GetContract<T>()
    {
        if (typeof(T) == typeof(int))
        {
            return DefaultContracts.Int32;
        }
        if (typeof(T) == typeof(float))
        {
            return DefaultContracts.Single;
        }
        if (typeof(T) == typeof(double))
        {
            return DefaultContracts.Double;
        }
        if (typeof(T) == typeof(TestType1))
        {
            return Serializer1.TestType1;
        }
        if (typeof(T) == typeof(Guid))
        {
            return DefaultContracts.Guid;
        }
        throw new NotSupportedException($"Type {typeof(T)} is not supported.");
    }

    private static async Task TestAll<TCollection, T>(Func<BshoxContract<T>, BshoxContract<TCollection>> contractFactory, Func<T[], TCollection> converter, T[][] examples)
        where TCollection : IEnumerable<T>
    {
        var contract1 = (BshoxContract<T>)GetContract<T>();
        var contract2 = contractFactory(contract1);
        foreach (var example in examples)
        {
            TCollection data = converter(example);
            await contract2.TestSerialization2<TCollection, T>(data);
        }
    }

    private static T[][] GetExamples<T>()
    {
        if (typeof(T) == typeof(int))
        {
            return (T[][])(object)(int[][])[
                [],
                [1],
                ExampleData.Ints().ToArray(),
            ];
        }
        if (typeof(T) == typeof(float))
        {
            return (T[][])(object)(float[][])[
                [],
                [1f],
                ExampleData.Floats().ToArray(),
            ];
        }
        if (typeof(T) == typeof(double))
        {
            return (T[][])(object)(double[][])[
                [],
                [1d],
                ExampleData.Doubles().ToArray(),
            ];
        }
        if (typeof(T) == typeof(TestType1))
        {
            return (T[][])(object)(TestType1[][])[
                [],
                [new TestType1()],
                [new TestType1 {Value1 = 1}, new TestType1 {Value2 = 2}, new TestType1 {Value3 = Guid.NewGuid()}],
            ];
        }
        if (typeof(T) == typeof(Guid))
        {
            return (T[][])(object)(Guid[][])[
                [],
                [Guid.NewGuid()],
                ExampleData.Guids().ToArray(),
            ];
        }
        throw new NotSupportedException($"Type {typeof(T)} is not supported.");
    }
}
