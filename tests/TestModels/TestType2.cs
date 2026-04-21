using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using Bshox.Attributes;

namespace TestModels;

[ExcludeFromCodeCoverage]
[BshoxSerializable<TestType2>]
[BshoxSerializable<TestType2[]>]
[BshoxSerializable<List<TestType2>>]
[BshoxSerializable<List<List<TestType2[]>[]>>]
public partial class Serializer2;

[ExcludeFromCodeCoverage]
[BshoxContract(ImplicitMembers = true)]
public record TestType2
{
    [DefaultValue(0)]
    public int Value1 { get; set; }

    [DefaultValue(null)]
    public string? Value2 { get; set; }
}
