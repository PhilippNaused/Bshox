using System.Collections.Immutable;
using System.Diagnostics;
using Microsoft.CodeAnalysis;

namespace Bshox.Generator.Contracts;

internal sealed record ContractInfo
{
    public ContractInfo(ITypeSymbol type, string variableName)
    {
        Type = type;
        VariableName = variableName;
        PropertyName = ContractHelper.GetContractPropertyName(Type);
    }

    public IContractGenerator? Generator { get; init; }

    public string VariableFullName => $"{SerializerInfo.Alias}.{VariableName}";

    /// <summary>
    /// The name of the variable that the serializer should use for this contract.
    /// <i>Should</i> be unique
    /// </summary>
    public string VariableName { get; }

    /// <summary>
    /// The name of the public property that the serializer should use for this contract.
    /// Is <i>probably</i> unique.
    /// </summary>
    public string PropertyName { get; }

    /// <summary>
    /// <see langword="true"/> if the contract was explicitly requested.
    /// </summary>
    public bool Explicit { get; set; } // TODO: make this a flag in the serializer instance instead!

    /// <summary>
    /// The type that this contract is representing
    /// </summary>
    public ITypeSymbol Type { get; }

    /// <summary>
    /// <see langword="true"/> if the dependencies of this contract are statically resolved.
    /// <see langword="false"/> if the dependencies are lazily resolved (or there are no dependencies).
    /// </summary>
    public bool StaticDependencies { get; init; }

    public InlineContractData? InlineData { get; init; }

    public required ImmutableArray<ContractDemand> Dependencies { get; init; }

    /// <summary>
    /// The format string for the C# initialization statement of the contract.
    /// The substring <c>$0</c> will be replaced with the variable names of the dependencies.
    /// </summary>
    public required string InitializeStatementFormat { get; init; }

    public string GetDefinition(IContractResolver resolver)
    {
        if (InitializeStatementFormat.Contains("$0"))
        {
            if (Dependencies.IsDefaultOrEmpty)
            {
                string message = $"Format string '{InitializeStatementFormat}' is invalid when there are no dependencies";
                Debug.Fail(message);
                throw new InvalidOperationException(message);
            }
            if (!StaticDependencies)
            {
                string message = $"Format string '{InitializeStatementFormat}' is invalid when dependencies are not statically resolved";
                Debug.Fail(message);
                throw new InvalidOperationException(message);
            }
        }
        var contracts = Dependencies.Select(resolver.ResolveContract).ToList();
        var contractDefinitions = string.Join(", ", contracts.Select(x => x.VariableName));
        return InitializeStatementFormat.Replace("$0", contractDefinitions);
    }
}
