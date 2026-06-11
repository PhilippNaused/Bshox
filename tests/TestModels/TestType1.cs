using Bshox.Attributes;

namespace TestModels;

[BshoxSerializable<TestType1>]
[BshoxSerializable<TestType1[]>]
[BshoxSerializable<List<TestType1>>]
public partial class Serializer1;

#pragma warning disable CA1036 // Override methods on comparable types

[BshoxContract(ImplicitMembers = true, ImplicitDefaultValues = true)]
public record TestType1 : IComparable<TestType1>
{
    public short Value1 { get; init; }
    public float Value2 { get; init; }
    public Guid Value3 { get; init; }

    public int CompareTo(TestType1? other)
    {
        if (other is null)
            return 1;
        int result = Value1.CompareTo(other.Value1);
        if (result != 0)
            return result;
        result = Value2.CompareTo(other.Value2);
        if (result != 0)
            return result;
        return Value3.CompareTo(other.Value3);
    }
}
