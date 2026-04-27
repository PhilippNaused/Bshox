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

internal record struct SerializableTypeInfo(ITypeSymbol Type, ITypeSymbol? Surrogate);

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
        var typeInfos = ParseBshoxSerializableAttribute();
        RequestedTypes = [.. typeInfos.Select(i => i.Type)];
        ContractResolver = new ContractResolver(this);
        foreach (var info in typeInfos.Where(i => i.Surrogate is not null))
        {
            if (SurrogateContract.TryGetFromSurrogate(info, this, ContractResolver, out var surrogate))
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
    /// The types that were explicitly requested in the <see cref="BshoxSerializableAttribute"/> attribute.
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
        var attributeDataList = ClassSymbol.GetAttributes().Where(a => SymbolEqualityComparer.Default.Equals(a.AttributeClass, KnownSymbols.BshoxDefaultContractAttribute)).ToList();

        foreach (var attributeData in attributeDataList)
        {
            Debug.Assert(attributeData.ConstructorArguments.Length == 2, "attributeData.ConstructorArguments.Length == 2");
            ITypeSymbol containingType = (ITypeSymbol)attributeData.ConstructorArguments[0].Value!;
            string symbolName = (string)attributeData.ConstructorArguments[1].Value!;
            var location = attributeData.ApplicationSyntaxReference?.GetLocation();
            if (ContractResolver.TryGetContractDemand(containingType, symbolName, location, out var demand))
            {
                Debug.Assert(demand.Value.ContractSymbol is not null, "demand.Value.ContractSymbol is not null");
                if (demand.Value.Type.IsUnresolvedGeneric())
                {
                    this.NotImplemented(location, "Types with unresolved generic parameters cannot be used in the default contract attribute.", demand.Value.ContractSymbol);
                    HasErrors = true;
                    continue;
                }

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

    private List<SerializableTypeInfo> ParseBshoxSerializableAttribute()
    {
        var dataList = ClassSymbol.GetAttributes(KnownSymbols.BshoxSerializableAttribute);
        dataList = dataList.AddRange(ClassSymbol.GetGenericAttributes(KnownSymbols.BshoxSerializableAttribute1));
        if (dataList.IsDefaultOrEmpty)
        {
            throw new DiagnosticException($"Type '{ClassSymbol.ToDisplayString()}' is missing a '{KnownSymbols.BshoxSerializableAttribute.Name}' attribute.", ClassSymbol);
        }

        var list = new List<SerializableTypeInfo>();

        foreach (var data in dataList)
        {
            ITypeSymbol? type = null;
            if (data.AttributeClass!.EqualsUnboundGenericType(KnownSymbols.BshoxSerializableAttribute1))
            {
                type = data.AttributeClass?.TypeArguments.Single();
            }
            else if (data.AttributeClass!.EqualsUnboundGenericType(KnownSymbols.BshoxSerializableAttribute))
            {
                type = data.ConstructorArguments.Single().Value as ITypeSymbol;
            }
            if (type is null)
            {
                throw new DiagnosticException($"Unable to determine the type argument for '{KnownSymbols.BshoxSerializableAttribute.Name}' attribute.", data.ApplicationSyntaxReference?.GetLocation());
            }

            var info = new SerializableTypeInfo(type, null);

            if (data.NamedArguments.TryGet(nameof(BshoxSerializableAttribute.Surrogate), out var typedConstant))
            {
                info.Surrogate = typedConstant.Kind switch
                {
                    TypedConstantKind.Type => (ITypeSymbol)typedConstant.Value!,
                    _ => throw new DiagnosticException($"Invalid argument type '{typedConstant.Kind}' for '{nameof(BshoxSerializableAttribute.Surrogate)}' in '{KnownSymbols.BshoxSerializableAttribute.Name}' attribute.", data.ApplicationSyntaxReference?.GetLocation()),
                };
            }

            list.Add(info);
        }

        return list;
    }
}
