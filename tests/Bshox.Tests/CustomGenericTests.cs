using TestModels;

namespace Bshox.Tests;

public class CustomGenericTests
{
    [Test]
    public async Task Test1()
    {
        var value = new CustomGenericType<TestType2>
        {
            Value = new TestType2
            {
                Value1 = 1,
            }
        };

        await CustomGenericTypeSerializer.CustomGenericTypeTestType2.TestSerialization(value, "0D08010000");
    }
}
