namespace Bshox.Generator.Tests;

public class ContractResolverTests
{
    [Test]
    public async Task CustomContract()
    {
        const string sourceCode = """
                                  using Bshox.Attributes;
                                  namespace TestModels;

                                  [BshoxSerializer(typeof(Type1))]
                                  public partial class Serializer1;

                                  [BshoxContract(ImplicitMembers = true)]
                                  public record Type1
                                  {
                                      public int Value { get; set; }
                                  }
                                  """;
        var generatedOutput = Utils.GetGeneratedOutput(sourceCode, out var diagnostics);

        await Assert.That(diagnostics).IsEmpty();
        await Utils.ValidateOutput(generatedOutput, 2);
    }

    [Test]
    public async Task GenericCustomContract()
    {
        const string sourceCode = """
                                  using Bshox.Attributes;
                                  namespace TestModels;

                                  [BshoxSerializer(typeof(Type1<int>))]
                                  public partial class Serializer1;

                                  [BshoxContract(ImplicitMembers = true)]
                                  public record Type1<T>
                                  {
                                      public T? Value { get; set; }
                                  }
                                  """;
        var generatedOutput = Utils.GetGeneratedOutput(sourceCode, out var diagnostics);

        await Assert.That(diagnostics).IsEmpty();
        await Utils.ValidateOutput(generatedOutput, 2);
    }

    [Test]
    public async Task VariableNameCollision()
    {
        // there are two different types with the same name in different namespaces
        // => possible name collision
        const string sourceCode = """
                                using Bshox.Attributes;

                                namespace TestModels2
                                {
                                    public enum MyEnum;
                                }

                                namespace TestModels
                                {
                                    public enum MyEnum;

                                    [BshoxSerializer(typeof(Type1))]
                                    public partial class Serializer1;

                                    [BshoxContract(ImplicitMembers = true)]
                                    public record Type1
                                    {
                                        public MyEnum Value1 { get; set; }
                                        public TestModels2.MyEnum Value2 { get; set; }
                                    }
                                }
                                """;
        var generatedOutput = Utils.GetGeneratedOutput(sourceCode, out var diagnostics);

        await Utils.RunTest(sourceCode, async (ctx, types, _, symbol) =>
        {
            var info = new SerializerInfo(symbol, types, ctx);
            await Assert.That(info.HasErrors).IsFalse();
            await Assert.That(info.RequestedTypes).HasSingleItem();
        });

        await Assert.That(diagnostics).IsEmpty();
        await Utils.ValidateOutput(generatedOutput, 2);
    }

    [Test]
    public async Task Enum()
    {
        const string sourceCode = """
                                  using Bshox.Attributes;
                                  namespace TestModels;

                                  public enum Enum1
                                  {
                                      Zero = 0,
                                      One = 1,
                                  }

                                  [BshoxSerializer(typeof(Type1))]
                                  public partial class Serializer1;

                                  [BshoxContract(ImplicitMembers = true)]
                                  public record Type1
                                  {
                                      public Enum1 Value { get; set; }
                                  }
                                  """;
        var generatedOutput = Utils.GetGeneratedOutput(sourceCode, out var diagnostics);

        await Assert.That(diagnostics).IsEmpty();
        await Utils.ValidateOutput(generatedOutput, 2);
    }

    public static string[] GetTupleTypes() =>
    [
        "System.ValueTuple<int>",
        "(int, long)",
        "(uint, string, byte)",
        "(int, int, int, int)",
        "(int, int, int, int, int)",
        "(int, int, int, int, int, int)",
        "(int, int, int, int, int, int, int)",
        "(int, int, int, int, int, int, int, int)",
        "(int, int, int, int, int, int, int, int, int)"
    ];

    [Test]
    [MethodDataSource(nameof(GetTupleTypes))]
    public async Task ValueTuple(string type)
    {
        string sourceCode = $"""
                              using Bshox.Attributes;

                              namespace TestModels;

                              [BshoxSerializer(typeof({type}))]
                              public partial class ValueTupleSerializer;
                              """;
        var generatedOutput = Utils.GetGeneratedOutput(sourceCode, out var diagnostics);

        await Assert.That(diagnostics).IsEmpty();
        await Assert.That(generatedOutput).HasCount(1);
    }

    [Test]
    public async Task ValueTuples()
    {
        string types = string.Join(", ", GetTupleTypes().Select(x => $"typeof({x})"));
        string sourceCode = $"""
                              using Bshox.Attributes;

                              namespace TestModels;

                              [BshoxSerializer({types})]
                              public partial class ValueTupleSerializer;
                              """;
        var generatedOutput = Utils.GetGeneratedOutput(sourceCode, out var diagnostics);

        await Assert.That(diagnostics).IsEmpty();
        await Utils.ValidateOutput(generatedOutput, 1);
    }

    [Test]
    public async Task List()
    {
        const string sourceCode = """
                                  using Bshox.Attributes;
                                  using System.Collections.Generic;
                                  namespace TestModels;

                                  [BshoxSerializer(typeof(List<int>))]
                                  public partial class Serializer1;
                                  """;
        var generatedOutput = Utils.GetGeneratedOutput(sourceCode, out var diagnostics);

        await Assert.That(diagnostics).IsEmpty();
        await Assert.That(generatedOutput).IsNotEmpty();
    }

    [Test]
    public async Task Dictionary()
    {
        const string sourceCode = """
                                  using Bshox.Attributes;
                                  using System.Collections.Generic;
                                  namespace TestModels;

                                  [BshoxSerializer(typeof(Dictionary<int, string>))]
                                  public partial class Serializer1;
                                  """;
        var generatedOutput = Utils.GetGeneratedOutput(sourceCode, out var diagnostics);

        await Assert.That(diagnostics).IsEmpty();
        await Assert.That(generatedOutput).IsNotEmpty();
    }

    [Test]
    public async Task Dictionary2()
    {
        const string sourceCode = """
                                  using Bshox.Attributes;
                                  using System.Collections.Generic;
                                  namespace TestModels;

                                  [BshoxSerializer(typeof(Dictionary<string, string>))]
                                  public partial class Serializer1;
                                  """;
        var generatedOutput = Utils.GetGeneratedOutput(sourceCode, out var diagnostics);

        await Assert.That(diagnostics).IsEmpty();
        await Assert.That(generatedOutput).IsNotEmpty();
    }

    [Test]
    public async Task StaticDependencyOrdering()
    {
        const string sourceCode = """
                                  using Bshox.Attributes;
                                  using System.Collections.Generic;

                                  namespace TestModels;

                                  [BshoxSerializer(typeof(TestType2), typeof(List<List<TestType2[]>[]>), typeof(TestType2[]), typeof(List<TestType2>))]
                                  public partial class Serializer2;

                                  [BshoxContract(ImplicitMembers = true)]
                                  public record TestType2
                                  {
                                      public int Value1 { get; set; }
                                      public string? Value2 { get; set; }
                                  }
                                  """;
        var generatedOutput = Utils.GetGeneratedOutput(sourceCode, out var diagnostics);

        await Assert.That(diagnostics).IsEmpty();
        await Utils.ValidateOutput(generatedOutput, 2);
    }
}
