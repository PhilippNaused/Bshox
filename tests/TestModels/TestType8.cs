using System.Diagnostics.CodeAnalysis;
using Bshox.Attributes;

namespace TestModels;

[ExcludeFromCodeCoverage]
[BshoxSerializer(typeof(TestType8), typeof(TestType8B))]
public partial class Serializer8;

[ExcludeFromCodeCoverage]
[BshoxContract(ImplicitMembers = true, ImplicitDefaultValues = true)]
public record TestType8
{
    public short Value1 { get; set; }
    public ushort Value2 { get; set; }
    public int Value3 { get; set; }
    public uint Value4 { get; set; }
    public long Value5 { get; set; }
    public ulong Value6 { get; set; }
    public string? Value7 { get; set; }
    public bool Value8 { get; set; }
    public Guid Value9 { get; set; }
    public float Value10 { get; set; }
    public double Value11 { get; set; }
    public byte Value12 { get; set; }
    public sbyte Value13 { get; set; }
    public char Value14 { get; set; }
    public TimeSpan Value15 { get; set; }
    public DateTime Value16 { get; set; }
}

#pragma warning disable CS8851 // Record defines 'Equals' but not 'GetHashCode'.

[ExcludeFromCodeCoverage]
[BshoxContract(ImplicitMembers = true, ImplicitDefaultValues = true)]
public sealed record TestType8B
{
    public byte[]? Value1 { get; set; }

    // This is a workaround for the fact that byte[] is a reference type and the default implementation of Equals does not compare the contents of the array.
    public bool Equals(TestType8B? other)
    {
        if (other is null)
            return false;
        if (ReferenceEquals(this, other))
            return true;
        if (Value1 == other.Value1)
            return true;
        if (Value1 is null || other.Value1 is null)
            return false;
        return Value1.SequenceEqual(other.Value1);
    }
}
