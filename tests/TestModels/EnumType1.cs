using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using Bshox.Attributes;

namespace TestModels;

[ExcludeFromCodeCoverage]
[BshoxSerializer(typeof(EnumType1), typeof(TestEnum1), typeof(List<TestEnum1>))]
public partial class EnumSerializer1;

public enum TestEnum1 : int
{
    None = 0,
    Value1 = 1,
    Value2 = 2,
    Value3 = 3
}

#pragma warning disable CA1028 // Enum Storage should be Int32

public enum TestEnum2 : ulong
{
    None = 0,
    Value1 = 1,
    Value2 = 2,
    Value3 = 3,
    ValueX = ulong.MaxValue
}

[ExcludeFromCodeCoverage]
[BshoxContract(ImplicitMembers = true)]
public sealed record EnumType1
{
    [DefaultValue(TestEnum1.None)]
    public TestEnum1 Value { get; set; }

    [DefaultValue(TestEnum2.None)]
    public TestEnum2 Value2 { get; set; }
}
