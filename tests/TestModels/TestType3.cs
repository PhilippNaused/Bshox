using System.Diagnostics.CodeAnalysis;
using Bshox.Attributes;

namespace TestModels;

[ExcludeFromCodeCoverage]
[BshoxSerializer(typeof(TestType3))]
public partial class Serializer3;

[ExcludeFromCodeCoverage]
[BshoxContract(ImplicitMembers = true, ImplicitDefaultValues = true)]
public record TestType3
{
    public TestType1? Value1 { get; set; }
    public TestType2? Value2 { get; set; }
}
