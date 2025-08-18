namespace Bshox.Generator.Tests;

public class GeneratedContractTests
{
    [Test]
    [Arguments(true)]
    [Arguments(false)]
    public async Task NoMembers(bool implicitMembers)
    {
        string sourceCode = $$"""
                            using Bshox.Attributes;
                            namespace TestModels;

                            [BshoxSerializer(typeof(Type1))]
                            public partial class Serializer1;

                            [BshoxContract(ImplicitMembers = {{implicitMembers.ToString().ToLower()}})]
                            public record Type1
                            {

                            }
                            """;
        var generatedOutput = Utils.GetGeneratedOutput(sourceCode, out var diagnostics);

        await Assert.That(diagnostics).IsEmpty(); // TODO: fix. There should be a warning here
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
    [Arguments("public", true)]
    [Arguments("internal", true)]
    [Arguments("protected internal", true)]
    [Arguments("private", false)]
    [Arguments("protected", false)]
    [Arguments("private protected", false)]
    public async Task MemberAccess(string access, bool valid)
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

        if (valid)
        {
            await Assert.That(diagnostics).IsEmpty();
            await Assert.That(generatedOutput).IsNotEmpty();
        }
        else
        {
            await Assert.That(diagnostics.Select(d => d.Id)).All().Satisfy(x => x.IsEqualTo("CS0122"));
            await Assert.That(generatedOutput).IsNotEmpty(); // TODO: fix this
        }
    }
}
