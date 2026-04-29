using Bshox.Attributes;

namespace TestModels;

[BshoxSerializable<TestType3>]
public partial class Serializer3;

[BshoxContract(ImplicitMembers = true, ImplicitDefaultValues = true)]
public record TestType3
{
    public TestType1? Value1 { get; set; }
    public TestType2? Value2 { get; set; }
}
