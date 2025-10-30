extern alias bsx;
using System.Collections.Immutable;
using System.ComponentModel;
using Bshox.Generator.Data;
using Bshox.Generator.Helpers;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Diagnostics;
using VeriGit;
using Assembly = System.Reflection.Assembly;

namespace Bshox.Generator.Tests;

public static class Utils
{

#pragma warning disable CA1810 // Initialize reference type static fields inline
    static Utils()
    {
        SourceWriter.disableComments = true;
        Constants.GeneratorVersion = new Version(0, 0, 0, 0);
    }
#pragma warning restore CA1810 // Initialize reference type static fields inline

    private static readonly List<MetadataReference> s_References = GetReferences();

    public static CSharpCompilation GetCompilation(string sourceCode, CSharpCompilationOptions? options = null, CSharpParseOptions? options2 = null)
    {
        var syntaxTree = CSharpSyntaxTree.ParseText(sourceCode, options2, cancellationToken: TestContext.Current?.Execution.CancellationToken ?? CancellationToken.None);

        //TestContext.WriteLine($"Assemblies:\n{string.Join(",\n", s_References.Select(r => r.Display).OrderBy(n => n))}");

        var nullable = NullableContextOptions.Enable;
        if (options2?.LanguageVersion < LanguageVersion.CSharp8)
            nullable = NullableContextOptions.Disable;

        var compilation = CSharpCompilation.Create("SourceGeneratorTests",
            [syntaxTree],
            s_References,
            options ?? new CSharpCompilationOptions(OutputKind.DynamicallyLinkedLibrary, nullableContextOptions: nullable));

        return compilation;
    }

    private static List<MetadataReference> GetReferences()
    {
        Type[] types =
        [
            typeof(object),
            typeof(DefaultValueAttribute),
            typeof(bsx::Bshox.Attributes.BshoxContractAttribute) // Use alias to avoid conflict with Bshox.Generator
        ];

        var assemblies = types
            .Select(t => t.Assembly)
#if NETCOREAPP
            .Append(Assembly.Load("System.Collections, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"))
            .Append(Assembly.Load("System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"))
#else
            .Append(Assembly.Load("netstandard, Version=2.0.0.0, Culture=neutral, PublicKeyToken=cc7b13ffcd2ddd51"))
            .Append(Assembly.Load("System.ValueTuple, Version=4.0.0.0, Culture=neutral, PublicKeyToken=cc7b13ffcd2ddd51"))
#endif
            .Select(a => a.Location)
            .Distinct(StringComparer.OrdinalIgnoreCase)
            .OrderBy(path => path)
            .Select(MetadataReference (path) => MetadataReference.CreateFromFile(path))
            .ToList();
        return assemblies;
    }

    public static List<string> GetGeneratedOutput(string sourceCode, out ImmutableArray<Diagnostic> diagnostics, CSharpCompilationOptions? options = null, CSharpParseOptions? options2 = null, IIncrementalGenerator? generator = null)
    {
        var compilation = GetCompilation(sourceCode, options, options2);
        // Source Generator to test
        generator ??= new BshoxGenerator();
        var analyzer = new UseDepthLockCorrectly();
        var x = compilation.WithAnalyzers([analyzer], new CompilationWithAnalyzersOptions(null, null, concurrentAnalysis: false, logAnalyzerExecutionTime: false, reportSuppressedDiagnostics: true, null));

        _ = CSharpGeneratorDriver.Create([generator.AsSourceGenerator()], null, options2)
            .RunGeneratorsAndUpdateCompilation(compilation,
                out var outputCompilation,
                out diagnostics, TestContext.Current?.Execution.CancellationToken ?? CancellationToken.None);
        diagnostics = [.. diagnostics, .. outputCompilation.GetDiagnostics(TestContext.Current?.Execution.CancellationToken ?? CancellationToken.None), .. x.GetAllDiagnosticsAsync().Result];

        diagnostics = [.. diagnostics.Distinct()];

        return outputCompilation.SyntaxTrees.Skip(1).Select(tree => tree.ToString()).ToList();
    }

    public static string ToHex(this byte[] bytes)
    {
#if NET6_0_OR_GREATER
        return Convert.ToHexString(bytes);
#else
        return BitConverter.ToString(bytes).Replace("-", "");
#endif
    }

    public static async Task AssertEqual(this Diagnostic actual, DiagnosticDescriptor expected, string? expectedMessage)
    {
        await Assert.That(actual.Descriptor).IsEqualTo(expected).Because(GetMessage());
        if (expectedMessage is not null)
        {
            await Assert.That(actual.GetMessage()).IsEqualTo(expectedMessage);
        }
        return;

        string GetMessage() => $"\nExpected {expected.Id}: {expected.Description}\nBut got: {actual.Id}: {actual.Descriptor.Description} {actual.GetMessage()}";
    }

    public static async Task ValidateOutput(List<string> generatedOutput, int expectedCount)
    {
        await Assert.That(generatedOutput).IsNotNull().And.IsNotEmpty();
        await Assert.That(generatedOutput).HasCount(expectedCount);
        using (Assert.Multiple())
        {
            for (int i = 0; i < expectedCount; i++)
            {
                string actualCode = generatedOutput[i];
                try
                {
                    await Validation.Validate(actualCode, "cs", $"{i}");
                }
                catch (ValidationFailedException e)
                {
                    Assert.Fail(e.Message);
                }
            }
        }
    }

    internal static async Task RunTest(string sourceCode, ProcessAsync process)
    {
        var generator = new TestGen(process);
        _ = GetGeneratedOutput(sourceCode, out var diagnostics, generator: generator);
        await Assert.That(diagnostics).IsEmpty();
    }

    internal delegate Task ProcessAsync(SourceProductionContext ctx, KnownTypeSymbols types, ClassDeclarationSyntax @class, INamedTypeSymbol symbol);

    private sealed class TestGen(ProcessAsync action) : BshoxGenerator
    {
        private protected override void Process(SourceProductionContext context, KnownTypeSymbols knownTypes, ClassDeclarationSyntax classDeclaration, INamedTypeSymbol symbol)
        {
            action(context, knownTypes, classDeclaration, symbol).GetAwaiter().GetResult();
        }
    }
}
