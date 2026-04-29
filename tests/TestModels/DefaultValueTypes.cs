using System.ComponentModel;
using Bshox.Attributes;

namespace TestModels;

[BshoxSerializable<DefaultValueType1>]
[BshoxSerializable<DefaultValueType2>]
[BshoxSerializable<DefaultValueType3>]
public partial class DefaultValueTypeSerializer;

[BshoxContract(ImplicitMembers = true)]
public record DefaultValueType1
{
    [DefaultValue(-4)]
    public int Value1 { get; set; }

    [DefaultValue(3.14)]
    public float Value2 { get; set; }

    [DefaultValue("Hello, World!")] // TODO: this should cause a warning!
    public string Value3 { get; set; } = "Hello, World!";

    [DefaultValue(null)] // TODO: this should cause a warning!
    public string? Value4 { get; set; }

    public string? Value5 { get; set; }

    // TODO: support explicit default values for non-primitive types like Guid, DateTime, TimeSpan, etc. and report an error if the default value is not valid for the member type
    //[DefaultValue((typeof(Guid)), "00000000-0000-0000-0000-000000000000")]
    //public Guid Value6 { get; set; }
}

[BshoxContract(ImplicitMembers = true)]
public record DefaultValueType2
{
    [DefaultValue("")] // TODO: this should cause a warning!
    public string Value { get; set; } = "";
}

[BshoxContract(ImplicitMembers = true)]
public record DefaultValueType3
{
    [DefaultValue(null)] // TODO: this should cause a warning!
    public string? Value { get; set; }
}
