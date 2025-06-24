using Microsoft.CodeAnalysis;

namespace Bshox.Generator.Contracts;

internal readonly record struct ContractDemand
{
    public ITypeSymbol Type { get; }
    public ISymbol? ContractSymbol { get; }

    private ContractDemand(ITypeSymbol type, ISymbol? contractSymbol)
    {
        Type = type;
        ContractSymbol = contractSymbol;
    }

    public static ContractDemand DefaultForType(ITypeSymbol type)
    {
        return new ContractDemand(type, null);
    }

    public static ContractDemand ForSpecificContract(ITypeSymbol type, ISymbol symbol)
    {
        if (symbol is null)
        {
            throw new ArgumentNullException(nameof(symbol));
        }
        return new ContractDemand(type, symbol);
    }

    /// <inheritdoc />
    public bool Equals(ContractDemand other)
    {
        return SymbolEqualityComparer.Default.Equals(Type, other.Type) &&
               SymbolEqualityComparer.Default.Equals(ContractSymbol, other.ContractSymbol);
    }

    /// <inheritdoc />
    public override int GetHashCode()
    {
        return SymbolEqualityComparer.Default.GetHashCode(Type) ^ SymbolEqualityComparer.Default.GetHashCode(ContractSymbol);
    }
}
