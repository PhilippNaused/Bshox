using Bshox.Attributes;

namespace TestModels;

[BshoxSerializable(typeof(Dictionary<string, TestType7?>))]
public partial class DictionarySerializer1;

[BshoxSerializable(typeof(List<(string, TestType7?)>))]
public partial class DictionarySerializer2;

[BshoxContract(ImplicitMembers = true, ImplicitDefaultValues = true)]
public record TestType7()
{
    public TestType7(int value1, string? value2) : this()
    {
        Value1 = value1;
        Value2 = value2;
    }

    public int Value1 { get; set; }
    public string? Value2 { get; set; }
}
