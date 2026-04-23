namespace Bshox.Generator.Tests;

public class BlockEmptyConstructors
{
    [Test]
    [Arguments("Bshox.BshoxWriter")]
    [Arguments("Bshox.BshoxReader")]
    public async Task EmptyConstructorsCausesError(string typeName)
    {
        string sourceCode = $$"""
                            namespace TestModels;

                            public class Type1
                            {
                                public static void Method1()
                                {
                                    _ = new {{typeName}}();
                                }
                            }
                            """;

        var generatedOutput = Utils.GetGeneratedOutput(sourceCode, out var diagnostics);

        await Assert.That(diagnostics).HasSingleItem();
        var diagnostic = diagnostics.Single();
        await Assert.That(diagnostic.Id).IsEqualTo("CS0619");
        await Assert.That(diagnostic.GetMessage()).EndsWith("' is obsolete: 'Do not use the parameterless constructor.'");
        await Assert.That(generatedOutput).IsEmpty();
    }
}
