using System.ComponentModel;
using Bshox.Attributes;

namespace TestModels;

[BshoxSerializable<TestClass10>]
public partial class Serializer10;

[BshoxContract(ImplicitMembers = true)]
public sealed record TestClass10
{
    public int? Value1 { get; set; }
    [DefaultValue(null)]
    public int? Value2 { get; set; }
    [DefaultValue(42)]
    public int? Value3 { get; set; }
}
