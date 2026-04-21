using System.Diagnostics.CodeAnalysis;
using Bshox.Attributes;

namespace TestModels;

[ExcludeFromCodeCoverage]
[BshoxSerializable<RecursiveTestType>]
public partial class RecursiveTestTypeSerializer;

[ExcludeFromCodeCoverage]
[BshoxContract(ImplicitMembers = true)]
public record RecursiveTestType
{
    public RecursiveTestType? Value1 { get; set; }
}
