using System.Collections.Immutable;
using System.Diagnostics.CodeAnalysis;
using Bshox.Generator.Contracts;
using Microsoft.CodeAnalysis;

namespace Bshox.Generator;

/// <summary>
/// Resolves <see cref="ContractInfo"/> for a given <see cref="ContractDemand"/>
/// </summary>
internal interface IContractResolver
{
    bool TryResolveContract(ContractDemand demand, [NotNullWhen(true)] out ContractInfo? contract);
    ContractInfo ResolveContract(ContractDemand demand);
    void SetDefault(ContractInfo contract);
    ContractInfo CreateGenerated(ITypeSymbol type, bool staticDependencies, ImmutableArray<ContractDemand> dependencies, IContractGenerator generator, string initializer);
    bool TryGetContractDemand(ITypeSymbol containingType, string symbolName, Location? location, [NotNullWhen(true)] out ContractDemand? demand);
}
