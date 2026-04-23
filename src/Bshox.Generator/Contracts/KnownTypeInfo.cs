using Bshox.Generator.Extensions;
using Microsoft.CodeAnalysis;

namespace Bshox.Generator.Contracts;

internal readonly partial struct KnownTypeInfo // TODO: rename
{
    /// <summary>
    /// The name of the Property in Bshox.DefaultContracts that corresponds to this type. Used for generating code that references the default contract for this type, e.g. <c>Bshox.DefaultContracts.{Name}</c>.
    /// </summary>
    public string Name { get; }
    public InlineContractData? InlineData { get; }

    private KnownTypeInfo(string name, string serializeFormat, string deserializeString, BshoxCode encoding)
    {
        Name = name;
        InlineData = new InlineContractData(serializeFormat, deserializeString, encoding);
    }

    private KnownTypeInfo(string name)
    {
        Name = name;
    }

    static KnownTypeInfo()
    {
        const int maxTupleArity = 8; // System.ValueTuple had 0-8 generic parameters.
        // we don't care about the non-generic System.ValueTuple.
        for (int i = 1; i <= maxTupleArity; i++)
        {
            // `i` is the number of generic parameters, so we need `i-1` commas in the name.
            KnownTypesByName.Add($"System.ValueTuple<{new string(',', i - 1)}>", new KnownTypeInfo("ValueTuple"));
        }
        _ = typeof(ValueTuple<,,,,,,,>); // ValueTuple`8
    }

    internal static readonly Dictionary<string, KnownTypeInfo> KnownTypesByName = GetDefaults();

    public static KnownTypeInfo? GetKnownTypeInfo(ITypeSymbol type)
    {
        if (type is IArrayTypeSymbol { ElementType.SpecialType: SpecialType.System_Byte })
            return KnownTypesByName["byte[]"];

        if (type is INamedTypeSymbol namedType)
        {
            if (namedType.IsGenericType)
            {
                namedType = namedType.ConstructUnboundGenericType();
            }
            var fullName = namedType.FullyQualifiedToStringNG();
            if (KnownTypesByName.TryGetValue(fullName, out var knownType))
            {
                return knownType;
            }
        }

        return null;
    }
}
