using System.Diagnostics.CodeAnalysis;
using Bshox.Attributes;

namespace TestModels;

[ExcludeFromCodeCoverage]
[BshoxSerializable<TestType6>]
[BshoxSerializable<(int, TestType6)>]
public partial class Serializer6;

[ExcludeFromCodeCoverage]
[BshoxContract(ImplicitMembers = true, ImplicitDefaultValues = true)]
public record struct TestType6(int Value1, string? Value2);
