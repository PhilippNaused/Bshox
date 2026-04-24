using Bshox.Attributes;

namespace TestModels;

[BshoxSerializable<TestType6>]
[BshoxSerializable<(int, TestType6)>]
public partial class Serializer6;

[BshoxContract(ImplicitMembers = true, ImplicitDefaultValues = true)]
public record struct TestType6(int Value1, string? Value2);
