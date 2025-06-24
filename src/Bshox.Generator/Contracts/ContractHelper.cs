using System.Diagnostics;
using System.Text;
using System.Text.RegularExpressions;
using Bshox.Generator.Extensions;
using Microsoft.CodeAnalysis;

namespace Bshox.Generator.Contracts;

internal static class ContractHelper
{
    private static readonly Regex InvalidChars = new("[^a-zA-Z0-9_]", RegexOptions.Compiled);

    public static string EscapeFullName(ISymbol symbol)
    {
        return EscapeFullName(symbol.FullyQualifiedToStringNG());
    }

    public static string EscapeFullName(string fullName)
    {
        string variableName = fullName.Replace('.', '_');
        return InvalidChars.Replace(variableName, match => $"_{(int)match.Value[0]:X2}");
    }

    public static string GetContractPropertyName(ITypeSymbol type)
    {
        Debug.Assert(type is not ITypeParameterSymbol, "type is not ITypeParameterSymbol");
        if (type is ITypeParameterSymbol t)
        {
            throw new NotSupportedException($"Type parameter '{t.Name}' is not supported.");
        }
        string name = GetContractPropertyName2(type);
        if (type.ContainingType is { } parentType)
        {
            return GetContractPropertyName(parentType) + name;
        }
        return name;
    }

    /// <summary>
    /// <see href="https://github.com/dotnet/runtime/blob/dff1d8467845fd93c517f89dc81598e5fd17c270/src/libraries/System.Text.Json/gen/JsonSourceGenerator.Parser.cs#L1654"/>
    /// </summary>
    private static string GetContractPropertyName2(ITypeSymbol type)
    {
        if (type is IArrayTypeSymbol arrayType)
        {
            int rank = arrayType.Rank;
            string suffix = rank == 1 ? "Array" : $"Array{rank}D"; // Array, Array2D, Array3D, ...
            return GetContractPropertyName(arrayType.ElementType) + suffix;
        }

        if (type is not INamedTypeSymbol { IsGenericType: true } namedType)
        {
            return type.Name;
        }

        StringBuilder sb = new();

        string name = namedType.Name;

        _ = sb.Append(name);

        if (namedType.GetAllTypeArgumentsInScope() is { } typeArgsInScope)
        {
            foreach (ITypeSymbol genericArg in typeArgsInScope)
            {
                _ = sb.Append(GetContractPropertyName(genericArg));
            }
        }

        return sb.ToString();
    }
}
