using System.Diagnostics.CodeAnalysis;
using Bshox.Attributes;

namespace TestModels;

[ExcludeFromCodeCoverage]
[BshoxSerializable<TestType1>]
[BshoxSerializable<TestType1[]>]
[BshoxSerializable<List<TestType1>>]
public partial class Serializer1;

[ExcludeFromCodeCoverage]
[BshoxContract(ImplicitMembers = true, ImplicitDefaultValues = true)]
public record TestType1
{
    public short Value1 { get; init; }
    public float Value2 { get; init; }
    public Guid Value3 { get; init; }
}
