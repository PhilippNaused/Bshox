namespace Bshox.Generator.Tests;

public class ContractResolverTests
{
    [Test]
    public async Task CustomContract()
    {
        const string sourceCode = """
                                  using Bshox.Attributes;
                                  namespace TestModels;

                                  [BshoxSerializable<Type1>]
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

                                  [BshoxSerializable<Type1<int>>]
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

                                    [BshoxSerializable<Type1>]
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

                                  [BshoxSerializable<Type1>]
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
        "(string, string?)",
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

                              [BshoxSerializable(typeof({type}))]
                              public partial class ValueTupleSerializer;
                              """;
        var generatedOutput = Utils.GetGeneratedOutput(sourceCode, out var diagnostics);

        await Assert.That(diagnostics).IsEmpty();
        await Assert.That(generatedOutput).Count().IsEqualTo(1);
    }

    [Test]
    public async Task ValueTuples()
    {
        string types = string.Join(", ", GetTupleTypes().Select(type => $"BshoxSerializable(typeof({type}))"));
        string sourceCode = $"""
                              using Bshox.Attributes;

                              namespace TestModels;

                              [{types}]
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

                                  [BshoxSerializable<List<int>>]
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

                                  [BshoxSerializable<Dictionary<int, string>>]
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

                                  [BshoxSerializable<Dictionary<string, string>>]
                                  public partial class Serializer1;
                                  """;
        var generatedOutput = Utils.GetGeneratedOutput(sourceCode, out var diagnostics);

        await Assert.That(diagnostics).IsEmpty();
        await Assert.That(generatedOutput).IsNotEmpty();
    }

    [Test]
    [Arguments("System.Collections.Generic.IDictionary<string, string>")]
    [Arguments("System.Collections.Generic.IList<long>")]
    [Arguments("System.Collections.Concurrent.ConcurrentDictionary<uint, byte>")]
    public async Task Surrogate(string type)
    {
        string sourceCode = $"""
                          using Bshox.Attributes;
                          namespace TestModels;

                          [BshoxSerializable<{type}>]
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

                                  [BshoxSerializable<TestType2>]
                                  [BshoxSerializable<List<List<TestType2[]>[]>>]
                                  [BshoxSerializable<TestType2[]>]
                                  [BshoxSerializable<List<TestType2>>]
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
