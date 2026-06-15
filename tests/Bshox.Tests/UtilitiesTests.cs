using System.Numerics;

namespace Bshox.Tests;

public class UtilitiesTests
{
    [Test]
    [GenerateGenericTest(typeof(int))]
    [GenerateGenericTest(typeof(bool))]
    [GenerateGenericTest(typeof(Complex))]
    [GenerateGenericTest(typeof(DateTimeKind))]
    [GenerateGenericTest(typeof(TimeSpan))]
    [GenerateGenericTest(typeof(DateTimeOffset))]
    [GenerateGenericTest(typeof(Guid))]
    [GenerateGenericTest(typeof(DateTime))]
    [GenerateGenericTest(typeof(decimal))]
    [GenerateGenericTest(typeof(ValueTuple<int, Guid>))]
    [GenerateGenericTest(typeof(KeyValuePair<int, DateTimeOffset>))]
    public async Task CheckHasNoReference<T>()
    {
        var actual = Internals.Utils<T>.IsReferenceOrContainsReferences;
        await Assert.That(actual).IsFalse();
    }

    [Test]
    [GenerateGenericTest(typeof(Type))]
    [GenerateGenericTest(typeof(BigInteger))]
    [GenerateGenericTest(typeof(Uri))]
    [GenerateGenericTest(typeof(Version))]
    [GenerateGenericTest(typeof(string))]
    [GenerateGenericTest(typeof(ValueTuple<Type, Guid>))]
    [GenerateGenericTest(typeof(Tuple<int, Guid>))]
    public async Task CheckHasReference<T>()
    {
        var actual = Internals.Utils<T>.IsReferenceOrContainsReferences;
        await Assert.That(actual).IsTrue();
    }
}
