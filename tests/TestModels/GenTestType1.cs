using Bshox.Attributes;

namespace TestModels;

[BshoxContract(ImplicitMembers = true)]
public sealed record GenTestType1<T, T2>
{
    public T? Value1 { get; set; }
    public GenTestType1<T2, int>? Value2 { get; set; }
}

[BshoxSerializable<TestType2>]
[BshoxSerializable(typeof(CustomGenericType<TestType2?>))]
public partial class CustomGenericTypeSerializer;

[BshoxContract(ImplicitMembers = true)]
public sealed record CustomGenericType<T>
{
    public T? Value { get; set; }
}
