using System.Text.RegularExpressions;
using Bshox.Generator.Data;
using Bshox.Generator.Extensions;
using Bshox.Generator.Helpers;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace Bshox.Generator.Tests;

public class CustomContractTests
{
    [Test]
    public async Task DefaultContractsFromProperty()
    {
        const string sourceCode = """
                                  using Bshox;
                                  using Bshox.Attributes;
                                  namespace TestModels;

                                  [BshoxSerializable<int>]
                                  [BshoxDefaultContract(typeof(DefaultContracts), nameof(DefaultContracts.Int32Z))]
                                  public partial class CustomContracts1;
                                  """;
        var generatedOutput = Utils.GetGeneratedOutput(sourceCode, out var diagnostics);

        await Assert.That(diagnostics).IsEmpty();
        await Utils.ValidateOutput(generatedOutput, 1);
    }

    [Test]
    public async Task CustomContractFromProperty()
    {
        const string sourceCode = """
                                  using Bshox;
                                  using Bshox.Attributes;
                                  namespace TestModels;

                                  internal class Test1
                                  {
                                      public static BshoxContract<int> Contract1 => null!;
                                  }

                                  [BshoxSerializable<int>]
                                  [BshoxDefaultContract(typeof(Test1), "Contract1")]
                                  public partial class CustomContracts1;
                                  """;
        var generatedOutput = Utils.GetGeneratedOutput(sourceCode, out var diagnostics);

        await Assert.That(diagnostics).IsEmpty();
        await Utils.ValidateOutput(generatedOutput, 1);
    }

    [Test]
    public async Task CustomContractFromMethod()
    {
        const string sourceCode = """
                                  using Bshox;
                                  using Bshox.Attributes;
                                  namespace TestModels;

                                  internal class Test1
                                  {
                                      public static BshoxContract<int> Contract1() => null!;
                                  }

                                  [BshoxSerializable<int>]
                                  [BshoxDefaultContract(typeof(Test1), "Contract1")]
                                  public partial class CustomContracts1;
                                  """;
        var generatedOutput = Utils.GetGeneratedOutput(sourceCode, out var diagnostics);

        await Assert.That(diagnostics).IsEmpty();
        await Utils.ValidateOutput(generatedOutput, 1);
    }

    [Test]
    public async Task CustomContractFromMethodWithParameter()
    {
        const string sourceCode = """
                                  using Bshox;
                                  using Bshox.Attributes;
                                  namespace TestModels;

                                  internal class Test1
                                  {
                                      public static BshoxContract<int> Contract1(BshoxContract<long> longContract) => null!;
                                  }

                                  [BshoxSerializable<int>]
                                  [BshoxDefaultContract(typeof(Test1), "Contract1")]
                                  public partial class CustomContracts1;
                                  """;
        var generatedOutput = Utils.GetGeneratedOutput(sourceCode, out var diagnostics);

        await Assert.That(diagnostics).IsEmpty();
        await Utils.ValidateOutput(generatedOutput, 1);
    }

    [Test]
    public async Task CustomGenericContractFromMethod()
    {
        const string sourceCode = """
                                  using Bshox;
                                  using Bshox.Attributes;
                                  namespace TestModels;

                                  internal class Test1<T>
                                  {
                                      public static BshoxContract<T> Contract1() => null!;
                                  }

                                  [BshoxSerializable<int>]
                                  [BshoxDefaultContract(typeof(Test1<int>), "Contract1")]
                                  public partial class CustomContracts1;
                                  """;
        var generatedOutput = Utils.GetGeneratedOutput(sourceCode, out var diagnostics);

        await Assert.That(diagnostics).IsEmpty();
        await Utils.ValidateOutput(generatedOutput, 1);
    }

    [Test]
    public async Task CustomGenericContractFromMethodWithParameter()
    {
        const string sourceCode = """
                                  using Bshox;
                                  using Bshox.Attributes;
                                  namespace TestModels;

                                  internal class Test1<T>
                                  {
                                      public static BshoxContract<T> Contract1(BshoxContract<long> longContract) => null!;
                                  }

                                  [BshoxSerializable<int>]
                                  [BshoxDefaultContract(typeof(Test1<int>), "Contract1")]
                                  public partial class CustomContracts1;
                                  """;
        var generatedOutput = Utils.GetGeneratedOutput(sourceCode, out var diagnostics);

        await Assert.That(diagnostics).IsEmpty();
        await Utils.ValidateOutput(generatedOutput, 1);
    }

    [Test]
    public async Task CustomGenericContractFromMethodWithParameter2()
    {
        const string sourceCode = """
                                  using Bshox;
                                  using Bshox.Attributes;
                                  namespace TestModels;

                                  internal class Test1<T>
                                  {
                                      public static BshoxContract<T> Contract1(BshoxContract<T> tContract) => null!;
                                  }

                                  [BshoxSerializable<int>]
                                  [BshoxDefaultContract(typeof(Test1<int>), "Contract1")]
                                  public partial class CustomContracts1;
                                  """;
        var generatedOutput = Utils.GetGeneratedOutput(sourceCode, out var diagnostics);

        await Assert.That(diagnostics).Count().IsEqualTo(1);
        await Assert.That(diagnostics.Single().GetMessage()).IsEqualTo("Internal Error: The static type dependencies are cyclical: int");
        await Assert.That(generatedOutput).IsEmpty();
    }

    [Test]
    [Explicit] // not implemented yet
    public async Task CustomContractFromGenericMethod()
    {
        const string sourceCode = """
                                  using Bshox;
                                  using Bshox.Attributes;
                                  namespace TestModels;

                                  internal class Test1
                                  {
                                      public static BshoxContract<T> Contract1<T>() => null!;
                                  }

                                  [BshoxSerializable<int>]
                                  [BshoxDefaultContract(typeof(Test1), "Contract1")]
                                  public partial class CustomContracts1;
                                  """;
        var generatedOutput = Utils.GetGeneratedOutput(sourceCode, out var diagnostics);

        await Assert.That(diagnostics).IsEmpty();
        await Utils.ValidateOutput(generatedOutput, 1);
    }

    [Test]
    public async Task ResolveGenericContract()
    {
        const string sourceCode = """
                                  using Bshox;
                                  using Bshox.Attributes;
                                  using System.Collections.Generic;
                                  namespace TestModels;

                                  internal class Test1
                                  {
                                      public static BshoxContract<Dictionary<uint,T>> Contract1<T>(BshoxContract<T> t) => null!;
                                  }

                                  [BshoxSerializable<int>]
                                  [BshoxDefaultContract(typeof(Test1), "Contract1")]
                                  public partial class CustomContracts1;
                                  """;
        await Utils.RunTest(sourceCode, Process);
        return;

        static async Task Process(SourceProductionContext ctx, KnownTypeSymbols types, ClassDeclarationSyntax @class, INamedTypeSymbol symbol)
        {
            var type = types.Compilation.GetTypeByMetadataName("TestModels.Test1")!;
            await Assert.That(type).IsNotNull();
            var method = (IMethodSymbol)type.GetMembers("Contract1").Single();
            await Assert.That(method).IsNotNull();
            var typeParas = method.TypeParameters;
            var paras = method.Parameters;
            await Assert.That(typeParas).Count().IsEqualTo(paras.Length);
            var full = method.FullyQualifiedToStringNG();
            var rx = Regex.Replace(full, @"\b\w+\b", match =>
            {
                for (int i = 0; i < typeParas.Length; i++)
                {
                    if (typeParas[i].Name.Equals(match.Value, StringComparison.Ordinal))
                    {
                        return $"{{{i}}}";
                    }
                }
                return match.Value;
            }, RegexOptions.None);

            await Assert.That(rx).IsEqualTo("TestModels.Test1.Contract1<{0}>");

            var retType = (INamedTypeSymbol)method.ReturnType;
            await Assert.That(retType.FullyQualifiedToStringNG()).IsEqualTo("Bshox.BshoxContract<System.Collections.Generic.Dictionary<uint, T>>");

            var tContext = new MockContext(types);
            var resolver = new ContractResolver(tContext);
            resolver.TryGetContractType(retType, null, out ITypeSymbol? typeSymbol);
            await Assert.That(typeSymbol).IsNotNull();
            var s2 = (INamedTypeSymbol)typeSymbol!;
            await Assert.That(s2.FullyQualifiedToStringNG()).IsEqualTo("System.Collections.Generic.Dictionary<uint, T>");
            await Assert.That(s2.ConstructUnboundGenericType().FullyQualifiedToStringNG()).IsEqualTo("System.Collections.Generic.Dictionary<,>");
        }
    }

    [Test]
    public async Task CustomContractGenericProperty()
    {
        const string sourceCode = """
                                  using Bshox;
                                  using Bshox.Attributes;
                                  using System.Collections.Generic;
                                  namespace TestModels;

                                  internal class Test1<T>
                                  {
                                      public static BshoxContract<List<T>> Contract1 => null!;
                                  }

                                  [BshoxSerializable<List<int>>]
                                  [BshoxDefaultContract(typeof(Test1<int>), "Contract1")]
                                  public partial class CustomContracts1;
                                  """;
        var generatedOutput = Utils.GetGeneratedOutput(sourceCode, out var diagnostics);

        await Assert.That(diagnostics).IsEmpty();
        await Utils.ValidateOutput(generatedOutput, 1);
    }

    [Test]
    [Explicit] // not implemented yet
    public async Task CustomContractUnboundGeneric()
    {
        const string sourceCode = """
                                  using Bshox;
                                  using Bshox.Attributes;
                                  using System.Collections.Generic;
                                  namespace TestModels;

                                  internal class Test1<T>
                                  {
                                      public static BshoxContract<List<T>> Contract1 => null!;
                                  }

                                  [BshoxSerializable<List<int>>]
                                  [BshoxDefaultContract(typeof(Test1<>), "Contract1")]
                                  public partial class CustomContracts1;
                                  """;
        var generatedOutput = Utils.GetGeneratedOutput(sourceCode, out var diagnostics);

        await Assert.That(diagnostics).IsEmpty();
        await Utils.ValidateOutput(generatedOutput, 1);
    }

    [Test]
    public async Task CustomContractFromField()
    {
        const string sourceCode = """
                                  using Bshox;
                                  using Bshox.Attributes;
                                  namespace TestModels;

                                  internal class Test1
                                  {
                                      public static BshoxContract<int> Contract1 = null!;
                                  }

                                  [BshoxSerializable<int>]
                                  [BshoxDefaultContract(typeof(Test1), "Contract1")]
                                  public partial class CustomContracts1;
                                  """;
        var generatedOutput = Utils.GetGeneratedOutput(sourceCode, out var diagnostics);

        await Assert.That(diagnostics).IsEmpty();
        await Utils.ValidateOutput(generatedOutput, 1);
    }

    [Test]
    public async Task CustomContractSymbolNotStatic()
    {
        const string sourceCode = """
                                  using Bshox;
                                  using Bshox.Attributes;
                                  namespace TestModels;

                                  internal class Test1
                                  {
                                      public BshoxContract<int> Contract1 => null!;
                                  }

                                  [BshoxSerializable<int>]
                                  [BshoxDefaultContract(typeof(Test1), "Contract1")]
                                  public partial class CustomContracts1;
                                  """;
        var generatedOutput = Utils.GetGeneratedOutput(sourceCode, out var diagnostics);

        await Assert.That(diagnostics).HasSingleItem();
        await Assert.That(generatedOutput).IsEmpty();
        await diagnostics.Single().AssertEqual(Diagnostics.ContractSymbolNotUnique, "Type 'TestModels.Test1' must have exactly one static member called 'Contract1'");
    }
}

internal sealed class MockContext(KnownTypeSymbols knownSymbols) : IGeneratorContext
{
    /// <inheritdoc />
    public void ReportDiagnostic(Diagnostic diagnostic)
    {
        Assert.Fail(diagnostic.GetMessage());
    }

    /// <inheritdoc />
    public void ReportDiagnostic(DiagnosticDescriptor descriptor, Location? location, params object?[] messageArgs)
    {
        Assert.Fail(string.Format(descriptor.MessageFormat.ToString(), messageArgs));
    }

    /// <inheritdoc />
    public bool HasErrors => false;

    /// <inheritdoc />
    public KnownTypeSymbols KnownSymbols => knownSymbols;

    /// <inheritdoc />
    public void AddSource(string hintName, SourceWriter source)
    {
        throw new NotImplementedException();
    }
}
