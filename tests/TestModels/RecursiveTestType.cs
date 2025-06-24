using System.Diagnostics.CodeAnalysis;
using Bshox.Attributes;

namespace TestModels;

[ExcludeFromCodeCoverage]
[BshoxSerializer(typeof(RecursiveTestType))]
public partial class RecursiveTestTypeSerializer;

[ExcludeFromCodeCoverage]
[BshoxContract(ImplicitMembers = true)]
public record RecursiveTestType
{
    public RecursiveTestType? Value1 { get; set; }
}
