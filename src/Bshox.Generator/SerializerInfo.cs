using System.Collections.Immutable;
using System.Diagnostics;
using Bshox.Attributes;
using Bshox.Generator.Contracts;
using Bshox.Generator.Data;
using Bshox.Generator.Extensions;
using Bshox.Generator.Helpers;
using Microsoft.CodeAnalysis;

namespace Bshox.Generator;

internal interface IDiagnosticOutput
{
    void ReportDiagnostic(Diagnostic diagnostic);
    void ReportDiagnostic(DiagnosticDescriptor descriptor, Location? location, params object?[] messageArgs);

    /// <summary>
    /// <c>true</c> if an error has occurred and the generation should be aborted.
    /// </summary>
    bool HasErrors { get; }
}

internal interface IGeneratorContext : IDiagnosticOutput
{
    /// <summary>
    /// The collection of well known types.
    /// </summary>
    KnownTypeSymbols KnownSymbols { get; }

    void AddSource(string hintName, SourceWriter source);
}

internal sealed class SerializerInfo : IGeneratorContext
{
    private readonly SourceProductionContext context;

    /// <summary>
    /// A per-file type alias for the generated serializer type.
    /// </summary>
    /// <remarks>
    /// Allows the generated type to be referenced without having a reference to the <see cref="SerializerInfo"/> instance.
    /// </remarks>
    public const string Alias = "_gen_bshox_t";

    public SerializerInfo(INamedTypeSymbol classSymbol, KnownTypeSymbols knownSymbols, SourceProductionContext context)
    {
        ClassSymbol = classSymbol;
        KnownSymbols = knownSymbols;
        this.context = context;
        RequestedTypes = ParseBshoxSerializerAttribute(out var surrogates);
        ContractResolver = new ContractResolver(this);
        foreach (var type in surrogates)
        {
            if (SurrogateContract.TryGetFromSurrogate(type, this, ContractResolver, out var surrogate))
            {
                ContractResolver.SetDefault(surrogate);
            }
            else
            {
                Debug.Assert(HasErrors, "HasErrors");
            }
        }
        if (!HasErrors)
            ParseDefaultContracts();
    }

    public IContractResolver ContractResolver { get; }

    /// <summary>
    /// The symbol of the class that is being generated.
    /// </summary>
    public INamedTypeSymbol ClassSymbol { get; }

    /// <summary>
    /// The collection of well known types.
    /// </summary>
    public KnownTypeSymbols KnownSymbols { get; }

    /// <summary>
    /// The types that were explicitly requested in the <see cref="BshoxSerializerAttribute"/> attribute.
    /// </summary>
    public ImmutableArray<ITypeSymbol> RequestedTypes { get; }

    public void AddSource(string hintName, SourceWriter source) => context.AddSource(hintName, source.ToSourceText());

    public void ReportDiagnostic(Diagnostic diagnostic) => context.ReportDiagnostic(diagnostic);

    public void ReportDiagnostic(DiagnosticDescriptor descriptor, Location? location, params object?[] messageArgs)
    {
        if (descriptor.DefaultSeverity == DiagnosticSeverity.Error)
        {
            HasErrors = true;
        }
#if DEBUG
        var format = descriptor.MessageFormat.ToString();
        int count = messageArgs.Length;
        int expected = format.Split('{').Length - 1;
        Debug.Assert(count == expected, $"Expected {expected} arguments, but got {count}");
#endif
        for (int i = 0; i < messageArgs.Length; i++)
        {
            if (messageArgs[i] is ISymbol symbol)
            {
                messageArgs[i] = symbol.ToDisplayString(SymbolDisplayFormat.CSharpErrorMessageFormat);
            }
        }
        var diagnostic = Diagnostic.Create(descriptor, location, messageArgs);
        context.ReportDiagnostic(diagnostic);
    }

    public bool HasErrors { get; private set; }

    private void ParseDefaultContracts()
    {
        // TODO: move parsing to utility class and support the attribute on other members
        var attributeDatas = ClassSymbol.GetAttributes().Where(a => SymbolEqualityComparer.Default.Equals(a.AttributeClass, KnownSymbols.BshoxDefaultContractAttribute)).ToList();

        foreach (var attributeData in attributeDatas)
        {
            Debug.Assert(attributeData.ConstructorArguments.Length == 2, "attributeData.ConstructorArguments.Length == 2");
            ITypeSymbol containingType = (ITypeSymbol)attributeData.ConstructorArguments[0].Value!;
            string symbolName = (string)attributeData.ConstructorArguments[1].Value!;
            var location = attributeData.ApplicationSyntaxReference?.GetLocation();
            if (ContractResolver.TryGetContractDemand(containingType, symbolName, location, out var demand))
            {
                if (ContractResolver.TryResolveContract(demand.Value, out var contract))
                {
                    ContractResolver.SetDefault(contract);
                }
                else
                {
                    Debug.Assert(HasErrors, "HasErrors");
                }
            }
            else
            {
                Debug.Assert(HasErrors, "HasErrors");
            }
            if (HasErrors)
                return;
        }
    }

    private ImmutableArray<ITypeSymbol> ParseBshoxSerializerAttribute(out ImmutableArray<ITypeSymbol> surrogateTypes)
    {
        var data = ClassSymbol.GetAttribute(KnownSymbols.BshoxSerializerAttribute) ?? throw new DiagnosticException($"Type '{ClassSymbol.ToDisplayString()}' is missing the '{KnownSymbols.BshoxSerializerAttribute.Name}' attribute.", ClassSymbol);

        if (data.NamedArguments.TryGet(nameof(BshoxSerializerAttribute.Surrogates), out var surrogateTypesArg))
        {
            surrogateTypes = surrogateTypesArg.Kind switch
            {
                TypedConstantKind.Array => [.. surrogateTypesArg.Values.Select(x => (ITypeSymbol)x.Value!)],
                TypedConstantKind.Type => [(ITypeSymbol)surrogateTypesArg.Value!],
                _ => throw new DiagnosticException($"Invalid argument type '{surrogateTypesArg.Kind}' for '{nameof(BshoxSerializerAttribute.Surrogates)}' in '{KnownSymbols.BshoxSerializerAttribute.Name}' attribute.", data.ApplicationSyntaxReference?.GetLocation()),
            };
        }
        else
        {
            surrogateTypes = [];
        }

        var typesArg = data.ConstructorArguments[0];
        return typesArg.Kind switch
        {
            TypedConstantKind.Array => [.. typesArg.Values.Select(x => (ITypeSymbol)x.Value!)],
            TypedConstantKind.Type => [(ITypeSymbol)typesArg.Value!],
            _ => throw new DiagnosticException($"Invalid argument type '{typesArg.Kind}' for '{KnownSymbols.BshoxSerializerAttribute.Name}' attribute.", data.ApplicationSyntaxReference?.GetLocation()),
        };
    }
}
