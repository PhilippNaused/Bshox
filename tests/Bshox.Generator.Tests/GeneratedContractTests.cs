namespace Bshox.Generator.Tests;

public class GeneratedContractTests
{
    [Test]
    public async Task NoMembers1()
    {
        const string sourceCode = """
                                  using Bshox.Attributes;
                                  namespace TestModels;

                                  [BshoxSerializer(typeof(Type1))]
                                  public partial class Serializer1;

                                  [BshoxContract(ImplicitMembers = true)]
                                  public record Type1
                                  {

                                  }
                                  """;
        var generatedOutput = Utils.GetGeneratedOutput(sourceCode, out var diagnostics);

        await Assert.That(diagnostics).IsEmpty();
        await Assert.That(generatedOutput).IsNotEmpty();
    }

    [Test]
    public async Task RecursiveType()
    {
        const string sourceCode = """
                                  using Bshox.Attributes;
                                  namespace TestModels;

                                  [BshoxSerializer(typeof(RecursiveTestType))]
                                  public partial class RecursiveTestTypeSerializer;

                                  [BshoxContract(ImplicitMembers = true)]
                                  public class RecursiveTestType
                                  {
                                      public RecursiveTestType Value1 { get; set; }
                                  }
                                  """;
        var generatedOutput = Utils.GetGeneratedOutput(sourceCode, out var diagnostics);

        await Assert.That(diagnostics).IsEmpty();
        await Utils.ValidateOutput(generatedOutput, 2);
    }

    [Test]
    public async Task NoMembers2()
    {
        const string sourceCode = """
                                  using Bshox.Attributes;
                                  namespace TestModels;

                                  [BshoxSerializer(typeof(Type1))]
                                  public partial class Serializer1;

                                  [BshoxContract(ImplicitMembers = false)]
                                  public record Type1
                                  {

                                  }
                                  """;
        var generatedOutput = Utils.GetGeneratedOutput(sourceCode, out var diagnostics);

        await Assert.That(diagnostics).IsEmpty();
        await Assert.That(generatedOutput).IsNotEmpty();
    }

    [Test]
    [Arguments("public")]
    [Arguments("internal")]
    [Arguments("protected internal")]
    public async Task GoodMemberAccess(string access)
    {
        string sourceCode = $$"""
                            using Bshox.Attributes;
                            namespace TestModels;

                            [BshoxSerializer(typeof(Type1))]
                            public partial class Serializer1;

                            [BshoxContract(ImplicitMembers = false)]
                            public record Type1
                            {
                                [BshoxMember(1)]
                                {{access}} int Value { get; set; }
                            }
                            """;
        var generatedOutput = Utils.GetGeneratedOutput(sourceCode, out var diagnostics);

        await Assert.That(diagnostics).IsEmpty();
        await Assert.That(generatedOutput).IsNotEmpty();
    }

    [Test]
    [Arguments("private")]
    [Arguments("protected")]
    [Arguments("private protected")]
    public async Task BadMemberAccess(string access)
    {
        string sourceCode = $$"""
                            using Bshox.Attributes;
                            namespace TestModels;

                            [BshoxSerializer(typeof(Type1))]
                            public partial class Serializer1;

                            [BshoxContract(ImplicitMembers = false)]
                            public record Type1
                            {
                                [BshoxMember(1)]
                                {{access}} int Value { get; set; }
                            }
                            """;
        var generatedOutput = Utils.GetGeneratedOutput(sourceCode, out var diagnostics);

        await Assert.That(diagnostics.Select(d => d.Id)).All().Satisfy(x => x.IsEqualTo("CS0122"));
        await Assert.That(generatedOutput).IsNotEmpty(); // TODO: fix this
    }
}
