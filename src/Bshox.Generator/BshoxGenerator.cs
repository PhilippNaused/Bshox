using Bshox.Attributes;
using Bshox.Generator.Data;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace Bshox.Generator;

[Generator(LanguageNames.CSharp)]
public class BshoxGenerator : IIncrementalGenerator
{
    public void Initialize(IncrementalGeneratorInitializationContext context)
    {
        var knownTypeSymbols = context.CompilationProvider.Select(static (compilation, _) => new KnownTypeSymbols(compilation));

        var symbolsAndSettings = context.ParseOptionsProvider
            .Combine(knownTypeSymbols)
            .Select(static (tuple, _) =>
            {
                var csOptions = (CSharpParseOptions)tuple.Left;
                var langVersion = csOptions.LanguageVersion;
                return (Symbols: tuple.Right, LangVersion: langVersion);
            });

        var bshoxSerializerClasses = context.SyntaxProvider.ForAttributeWithMetadataName(
            typeof(BshoxSerializerAttribute).FullName!,
            static (node, _) => node is ClassDeclarationSyntax,
            static (context, _) => (ClassDeclarationSyntax)context.TargetNode)
            .SelectWithSymbol(context.CompilationProvider)
            .Combine(symbolsAndSettings);

        context.RegisterSourceOutput(bshoxSerializerClasses, (ctx, tuple) =>
        {
            (KnownTypeSymbols knownTypes, LanguageVersion langVersion) = tuple.Right;
            (ClassDeclarationSyntax classDeclaration, INamedTypeSymbol symbol) = tuple.Left;
            if (langVersion < LanguageVersion.CSharp12)
            {
                ctx.ReportDiagnostic(Diagnostic.Create(Diagnostics.LangVersionMustBe12OrHigher, null, langVersion.MapSpecifiedToEffectiveVersion().ToDisplayString()));
                return;
            }

            Process(ctx, knownTypes, classDeclaration, symbol);
        });
    }

    private protected virtual void Process(SourceProductionContext context, KnownTypeSymbols knownTypes, ClassDeclarationSyntax classDeclaration, INamedTypeSymbol symbol)
    {
        try
        {
            var serializer = new SerializerInfo(symbol, knownTypes, context);
            if (!serializer.HasErrors)
                SerializerGenerator.Generate(serializer, classDeclaration, context.CancellationToken);
        }
        catch (DiagnosticException ex)
        {
            // Debug.Fail(ex.Message);
            context.ReportDiagnostic(ex.Diagnostic);
        }
    }
}

internal static class ProviderExtensions
{
    private static (T Syntax, INamedTypeSymbol? Symbol) GetSymbol<T>((T Syntax, Compilation Compilation) tuple, CancellationToken ct) where T : TypeDeclarationSyntax
    {
        var syntax = tuple.Syntax;
        return (Syntax: syntax, Symbol: tuple.Compilation.GetSemanticModel(syntax.SyntaxTree).GetDeclaredSymbol(syntax, ct));
    }

    public static IncrementalValuesProvider<(T Syntax, INamedTypeSymbol Symbol)> SelectWithSymbol<T>(this IncrementalValuesProvider<T> syntaxProvider, IncrementalValueProvider<Compilation> compilation) where T : TypeDeclarationSyntax
    {
        return syntaxProvider
            .Combine(compilation)
            .Select(GetSymbol)
            .Where(x => x.Symbol is not null)!;
    }
}
