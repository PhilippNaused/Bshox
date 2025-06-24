using TestModels;

namespace Bshox.Tests;

public class CustomContractTests1
{
    [Test]
    public async Task Test1()
    {
        await Assert.That(CustomContracts1.Int32).IsSameReferenceAs(CustomIntContract.Instance);
        await CustomContracts1.Int32.TestSerialization(0x24, "00000024");
    }

    [Test]
    public async Task Test2()
    {
        await Assert.That(CustomContracts2.Int32).IsSameReferenceAs(DefaultContracts.Int32Z);
        await CustomContracts2.Int32.TestSerialization(0, "00");
        await CustomContracts2.Int32.TestSerialization(-1, "01");
        await CustomContracts2.Int32.TestSerialization(1, "02");
    }
}
