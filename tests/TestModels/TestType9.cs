using Bshox.Attributes;

namespace TestModels;

[BshoxSerializable<TestClass9A.TestType9>]
public partial class Serializer9;

#pragma warning disable CA1034 // Nested types should not be visible

public static class TestClass9A
{
    [BshoxContract(ImplicitMembers = true)]
    public sealed record TestType9
    {
        public int Value { get; set; }
    }
}
