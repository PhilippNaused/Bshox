using Bshox.Attributes;

namespace TestModels;

[BshoxSerializable<RecursiveTestType>]
public partial class RecursiveTestTypeSerializer;

[BshoxContract(ImplicitMembers = true)]
public record RecursiveTestType
{
    public RecursiveTestType? Value1 { get; set; }
}
