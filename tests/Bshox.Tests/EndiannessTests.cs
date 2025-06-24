using Bshox.Internals;

namespace Bshox.Tests;

public class EndiannessTests
{
    private static readonly Guid guid1 = Guid.ParseExact("1746c2e8-e16f-4685-a2a6-fb938b547f25", "D");
    private static readonly Guid guid2 = Guid.ParseExact("e8c24617-6fe1-8546-a2a6-fb938b547f25", "D");

    [Test]
    public async Task ReverseGuid1()
    {
        Guid result = EndiannessHelper.Reverse(guid1);
        await Assert.That(result).IsEqualTo(guid2);
    }
}
