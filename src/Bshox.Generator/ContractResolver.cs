using System.Collections.Immutable;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using Bshox.Generator.Contracts;
using Bshox.Generator.Data;
using Bshox.Generator.Extensions;
using Microsoft.CodeAnalysis;

namespace Bshox.Generator;

internal sealed class ContractResolver(IGeneratorContext context) : IContractResolver
{
    private readonly Dictionary<ContractDemand, ContractInfo> _resolvedContracts = [];

    private readonly HashSet<string> _usedNames = [];

    private string GetUniqueName(ITypeSymbol type)
    {
        string name = $"c_{ContractHelper.GetContractPropertyName(type)}";
        if (_usedNames.Add(name))
        {
            return name;
        }
        for (int i = 1; ; i++)
        {
            string newName = $"{name}_{i}";
            if (_usedNames.Add(newName))
            {
                return newName;
            }
        }
    }

    public void SetDefault(ContractInfo contract)
    {
        _resolvedContracts[ContractDemand.DefaultForType(contract.Type)] = contract;
    }

    public ContractInfo CreateGenerated(ITypeSymbol type, bool staticDependencies, ImmutableArray<ContractDemand> dependencies, IContractGenerator generator, string initializer)
    {
        return new ContractInfo(type, GetUniqueName(type))
        {
            Generator = generator,
            Dependencies = dependencies,
            StaticDependencies = staticDependencies,
            InitializeStatementFormat = initializer
        };
    }

    public bool TryGetContractDemand(ITypeSymbol containingType, string symbolName, Location? location, [NotNullWhen(true)] out ContractDemand? demand)
    {
        if (containingType is INamedTypeSymbol { IsUnboundGenericType: true })
        {
            // TODO: add support for unbound generics
            throw new NotImplementedException("Unbound generics are not supported");
        }

        // TODO: check visibility
        var symbols = containingType.GetMembers(symbolName).Where(s => s.IsStatic).ToImmutableArray();
        demand = null;
        if (symbols.Length != 1)
        {
            context.ReportDiagnostic(Diagnostics.ContractSymbolNotUnique, location, containingType, symbolName);
            return false;
        }
        var symbol = symbols.Single();
        if (symbol is IPropertySymbol or IFieldSymbol)
        {
            var contractType = symbol switch
            {
                IPropertySymbol p => p.Type,
                IFieldSymbol f => f.Type,
                _ => throw new InvalidOperationException() // Dead code
            };
            if (!TryGetContractType(contractType, symbol.Locations.FirstOrDefault() ?? location, out var type))
            {
                return false;
            }
            demand = ContractDemand.ForSpecificContract(type, symbol);
            return true;
        }

        if (symbol is IMethodSymbol method)
        {
            var contractType = method.ReturnType;
            if (!TryGetContractType(contractType, symbol.Locations.FirstOrDefault() ?? location, out var type))
            {
                return false;
            }
            demand = ContractDemand.ForSpecificContract(type, symbol);
            return true;
        }

        context.InternalError(location, $"Symbol kind '{symbol.Kind}' is not supported", symbol);
        return false;
    }

    /// <summary>
    /// Tries to get type T from BshoxContract{T}
    /// </summary>
    private bool TryGetContractType(ITypeSymbol contractType, Location? location, [NotNullWhen(true)] out ITypeSymbol? serializedType)
    {
        // TODO: try to check the base type(s)
        if (contractType is not INamedTypeSymbol namedType || !namedType.EqualsUnboundGenericType(context.KnownSymbols.BshoxContract))
        {
            // TODO: add a diagnostic
            context.ReportDiagnostic(Diagnostics._internalError, location, contractType);
            serializedType = null;
            return false;
        }
        Debug.Assert(namedType.TypeArguments.Length == 1, "namedType.TypeArguments.Length == 1");
        serializedType = namedType.TypeArguments.Single();
        return true;
    }

    public bool TryResolveContract(ContractDemand demand, [NotNullWhen(true)] out ContractInfo? contract)
    {
        if (_resolvedContracts.TryGetValue(demand, out contract))
        {
            return true;
        }

        if (demand.ContractSymbol is { } symbol)
        {
            if (TryMakeContractFromSymbol(demand.Type, symbol, out contract))
            {
                _resolvedContracts[demand] = contract;
                return true;
            }
            return false;
        }

        if (TryFindContractForTypeInternal(demand.Type, out contract))
        {
            _resolvedContracts[demand] = contract;
            return true;
        }

        return false;
    }

    /// <param name="type">The type that will be serialized</param>
    /// <param name="symbol">The member symbol that returns the contract</param>
    /// <param name="contract">The contract that was resolved</param>
    private bool TryMakeContractFromSymbol(ITypeSymbol type, ISymbol symbol, [NotNullWhen(true)] out ContractInfo? contract)
    {
        contract = null;
        if (symbol is IPropertySymbol or IFieldSymbol)
        {
            contract = new ContractInfo(type, GetUniqueName(type))
            {
                Dependencies = [],
                InitializeStatementFormat = $"{symbol.ContainingType.FullyQualifiedToString()}.{symbol.Name}"
            };
            var demand = ContractDemand.ForSpecificContract(type, symbol);
            _resolvedContracts[demand] = contract;
            return true;
        }
        if (symbol is IMethodSymbol method)
        {
            if (!method.TypeParameters.IsDefaultOrEmpty) // TODO: remove this limitation
            {
                throw new NotImplementedException("Generic methods are not supported");
            }
            ContractDemand[] dependencies = method.Parameters.Length == 0 ? [] : new ContractDemand[method.Parameters.Length];

            for (int i = 0; i < method.Parameters.Length; i++)
            {
                var parameter = method.Parameters[i];
                if (TryGetContractType(parameter.Type, parameter.Locations.FirstOrDefault(), out var parameterType))
                {
                    // TODO: support non-default contract types
                    dependencies[i] = ContractDemand.DefaultForType(parameterType);
                }
                else
                {
                    // TODO: report diagnostic
                }
            }

            contract = new ContractInfo(type, GetUniqueName(type))
            {
                Dependencies = [.. dependencies],
                StaticDependencies = true,
                InitializeStatementFormat = $"{method.ContainingType.FullyQualifiedToString()}.{method.Name}({(dependencies.Length == 0 ? null : "$0")})"
            };
            var demand = ContractDemand.ForSpecificContract(type, symbol);
            _resolvedContracts[demand] = contract;
            return true;
        }
        context.InternalError(symbol, $"Symbol kind '{symbol.Kind}' is not supported");
        return false;
    }

    private bool TryFindContractForTypeInternal(ITypeSymbol type, [NotNullWhen(true)] out ContractInfo? contract)
    {
        contract = null;
        // failed to resolve the type
        if (type is IErrorTypeSymbol)
        {
            return false;
        }
        // unbound generic => not supported
        if (type is INamedTypeSymbol { IsUnboundGenericType: true } unboundSymbol)
        {
            context.ReportDiagnostic(Diagnostics.TypeNotSerializable, unboundSymbol, unboundSymbol.ToDisplayString(SymbolDisplayFormat.CSharpErrorMessageFormat));
            return false;
        }
        // built-in primitive
        if (KnownTypeInfo.GetKnownTypeInfo(type, context.KnownSymbols) is { } knownTypeInfo)
        {
            contract = CreatePrimitiveContract(type, knownTypeInfo);
            return true;
        }
        // enum
        if (type is INamedTypeSymbol { EnumUnderlyingType: { } enumUnderlyingType } enumType)
        {
            // TODO: remove this check?
            var enumTypeInfo = KnownTypeInfo.GetKnownTypeInfo(enumUnderlyingType, context.KnownSymbols);
            if (enumTypeInfo is null)
            {
                context.InternalError(enumType, $"Enum underlying type {enumUnderlyingType} is not a well known type.");
                return false;
            }
            contract = CreateEnumContract(enumType, enumUnderlyingType);
            return true;
        }
        // array
        if (type is IArrayTypeSymbol arrayType)
        {
            contract = CreateArrayContract(arrayType);
            return true;
        }
        if (type is INamedTypeSymbol namedType)
        {
            // custom contract with [BshoxContract] (or derived)
            if (namedType.ContainsAttributeImpl(context.KnownSymbols.BshoxContractAttribute))
            {
                if (GeneratedContract.TryCreate(namedType, context, this, out var contractInfo))
                {
                    contract = contractInfo;
                    return true;
                }
                context.InternalError(namedType, $"Failed to create contract for type '{namedType}'");
            }
            // TODO: surrogates

            // built-in generic
            if (TryCreateKnownGenericContract(namedType, context.KnownSymbols, out var contractInfo2))
            {
                contract = contractInfo2;
                return true;
            }
        }

        context.ReportDiagnostic(Diagnostics.TypeNotSerializable, type, type.ToDisplayString(SymbolDisplayFormat.CSharpErrorMessageFormat));
        return false;
    }

    private ContractInfo CreatePrimitiveContract(ITypeSymbol type, KnownTypeInfo knownTypeInfo)
    {
        return new ContractInfo(type, GetUniqueName(type))
        {
            InlineData = knownTypeInfo.InlineData,
            InitializeStatementFormat = $"bsx::DefaultContracts.{knownTypeInfo.Name}",
            Dependencies = []
        };
    }

    private ContractInfo CreateEnumContract(INamedTypeSymbol enumType, INamedTypeSymbol enumUnderlyingType)
    {
        return new ContractInfo(enumType, GetUniqueName(enumType))
        {
            // TODO: add inline info
            Dependencies = [ContractDemand.DefaultForType(enumUnderlyingType)],
            StaticDependencies = true,
            InitializeStatementFormat = $"bsx::DefaultContracts.Enum<{enumType.FullyQualifiedToString()}, {enumUnderlyingType.FullyQualifiedToString()}>($0)"
        };
    }

    private ContractInfo CreateArrayContract(IArrayTypeSymbol arrayType)
    {
        ITypeSymbol elementType = arrayType.ElementType;
        return new ContractInfo(arrayType, GetUniqueName(arrayType))
        {
            Dependencies = [ContractDemand.DefaultForType(elementType)],
            StaticDependencies = true,
            InitializeStatementFormat = $"bsx::DefaultContracts.Array<{elementType.FullyQualifiedToString()}>($0)"
        };
    }

    private ContractInfo CreateKnownGenericContract(INamedTypeSymbol type, string contractNameBase)
    {
        Debug.Assert(type.IsGenericType, "type.IsGenericType");
        Debug.Assert(!string.IsNullOrWhiteSpace(contractNameBase), "!string.IsNullOrWhiteSpace(contractNameBase)");
        Debug.Assert(!type.IsUnboundGenericType, "!type.IsUnboundGenericType");
        ImmutableArray<ITypeSymbol> parameters = type.TypeArguments;
        Debug.Assert(!parameters.IsDefaultOrEmpty, "!parameters.IsDefaultOrEmpty");
        var genericParameterList = string.Join(", ", parameters.Select(x => x.FullyQualifiedToString()));
        return new ContractInfo(type, GetUniqueName(type))
        {
            Dependencies = [.. parameters.Select(ContractDemand.DefaultForType)],
            StaticDependencies = true, // The only dependencies are the type arguments, so the dependencies are never circular and can be resolved statically
            InitializeStatementFormat = $"bsx::DefaultContracts.{contractNameBase}<{genericParameterList}>($0)"
        };
    }

    private bool TryCreateKnownGenericContract(INamedTypeSymbol namedType, KnownTypeSymbols knownSymbols, [NotNullWhen(true)] out ContractInfo? contractInfo)
    {
        contractInfo = null;
        if (namedType is { IsGenericType: false })
        {
            return false;
        }
        if (namedType is { IsTupleType: true })
        {
            contractInfo = CreateKnownGenericContract(namedType, "ValueTuple");
            return true;
        }
        var unbound = namedType.ConstructUnboundGenericType();
        Debug.Assert(knownSymbols.List.IsUnboundGenericType, "knownSymbols.List.IsUnboundGenericType");
        if (SymbolEqualityComparer.Default.Equals(unbound, knownSymbols.List))
        {
            Debug.Assert(namedType.TypeArguments.Length == 1, "namedType.TypeArguments.Length == 1");
            contractInfo = CreateKnownGenericContract(namedType, "List");
            return true;
        }
        if (SymbolEqualityComparer.Default.Equals(unbound, knownSymbols.Dictionary))
        {
            Debug.Assert(namedType.TypeArguments.Length == 2, "namedType.TypeArguments.Length == 2");
            contractInfo = CreateKnownGenericContract(namedType, "Dictionary");
            return true;
        }
        // TODO: add generated surrogates
        return false;
    }

    public ContractInfo ResolveContract(ContractDemand demand)
    {
        if (TryResolveContract(demand, out var contract))
        {
            return contract;
        }
        throw new DiagnosticException($"Failed to resolve contract for type '{demand.Type}'", demand.Type);
    }
}
