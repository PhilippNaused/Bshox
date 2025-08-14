using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using Bshox.Attributes;

namespace TestModels;

[ExcludeFromCodeCoverage]
[BshoxSerializer(typeof(DefaultValueType1))]
public partial class DefaultValueTypeSerializer;

[ExcludeFromCodeCoverage]
[BshoxContract(ImplicitMembers = true)]
public record DefaultValueType1
{
    [DefaultValue(-4)]
    public int Value1 { get; set; }

    [DefaultValue(3.14)]
    public float Value2 { get; set; }

    [DefaultValue("Hello, World!")]
    public string Value3 { get; set; } = "Hello, World!";

    [DefaultValue(null)] // TODO: this should cause an error!
    public string? Value4 { get; set; }

    public string? Value5 { get; set; }
}
