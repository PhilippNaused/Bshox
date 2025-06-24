using System.Diagnostics.CodeAnalysis;
using Bshox.Attributes;

namespace TestModels;

[ExcludeFromCodeCoverage]
[BshoxSerializer(typeof(TestType4))]
public partial class Serializer4;

[ExcludeFromCodeCoverage]
[BshoxContract(ImplicitMembers = true, ImplicitDefaultValues = true)]
public sealed record TestType4 : IEquatable<TestType4>
{
#pragma warning disable CA2227 // Collection properties should be read only
    public List<TestType1>? List { get; set; }
#pragma warning restore CA2227 // Collection properties should be read only

#pragma warning disable CS8851 // Record defines 'Equals' but not 'GetHashCode'.
    public bool Equals(TestType4? test)
#pragma warning restore CS8851 // Record defines 'Equals' but not 'GetHashCode'.
    {
        if (test is null)
            return false;
        if (test.List is null)
            return List is null;
        if (List?.SequenceEqual(test.List) ?? false)
            return true;
        return false;
    }
}
