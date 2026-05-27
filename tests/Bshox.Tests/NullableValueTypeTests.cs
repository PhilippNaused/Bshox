using TestModels;
using TUnit.Assertions.Exceptions;

namespace Bshox.Tests;

public class NullableValueTypeTests
{
    [Test]
    public async Task SerializeTestClass10()
    {
        var value = new TestClass10
        {
            Value1 = null, // implicit default value
            Value2 = null, // explicit default value
            Value3 = 42
        };

        await Serializer10.TestClass10.TestSerialization(value, "00");

        value = new TestClass10
        {
            Value1 = 1,
            Value2 = 2,
            Value3 = 3
        };

        await Serializer10.TestClass10.TestSerialization(value);

        value = new TestClass10
        {
            Value1 = null,
            Value2 = null,
            Value3 = null // this will cause the roundtrip to fail because the default is 42, but null cannot be serialized, so it will be deserialized as 42.
        };

        await Assert.ThrowsAsync<AssertionException>(async () => await Serializer10.TestClass10.TestSerialization(value));
    }

    [Test]
    public async Task RoundTripContract()
    {
        var contract = DefaultContracts.Nullable(DefaultContracts.Int32);
        await contract.TestSerialization(0, "00");
        await contract.TestSerialization(42, "2A");
        await Assert.ThrowsAsync<ArgumentNullException>(async () => await contract.TestSerialization(null));
    }
}
