using System.ComponentModel;
using Bshox.Attributes;

namespace TestModels;

[BshoxSerializable<TestClass11>]
public partial class Serializer11;

[BshoxContract(ImplicitMembers = true)]
public sealed record TestClass11
{
    //[DefaultValue(0)] // uncommenting this causes Bshox015: Required members cannot have default values
    public required int Value1 { get; set; }

    [DefaultValue(0)]
    public int Value2 { get; set; }
}
