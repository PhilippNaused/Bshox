using Bshox.Generator.Data;
using Microsoft.CodeAnalysis;

namespace Bshox.Generator.Contracts;

internal readonly partial struct KnownTypeInfo // TODO: rename
{
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

    public static KnownTypeInfo? GetKnownTypeInfo(ITypeSymbol type, KnownTypeSymbols knownSymbols)
    {
        if (type.SpecialType is SpecialType.None)
        {
            if (type is IArrayTypeSymbol { ElementType.SpecialType: SpecialType.System_Byte })
                return ByteArray;

            if (SymbolEqualityComparer.Default.Equals(type, knownSymbols.Guid))
                return Guid;

            if (SymbolEqualityComparer.Default.Equals(type, knownSymbols.TimeSpan))
                return TimeSpan;

            return null;
        }

        return type.SpecialType switch
        {
            SpecialType.System_Boolean => Boolean,

            SpecialType.System_Byte => Byte,
            SpecialType.System_SByte => SByte,

            SpecialType.System_Int16 => Int16,
            SpecialType.System_UInt16 => UInt16,
            SpecialType.System_Char => Char,

            SpecialType.System_Int32 => Int32,
            SpecialType.System_UInt32 => UInt32,

            SpecialType.System_Int64 => Int64,
            SpecialType.System_UInt64 => UInt64,

            SpecialType.System_Single => Single,
            SpecialType.System_Double => Double,

            SpecialType.System_String => String,

            SpecialType.System_DateTime => DateTime, // No idea why this one is a "special" type but TimeSpan is not.

            _ => null
        };
    }
}
