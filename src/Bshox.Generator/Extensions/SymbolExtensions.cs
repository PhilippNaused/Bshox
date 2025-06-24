using System.Collections.Immutable;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Text;
using Bshox.Generator.Data;
using Microsoft.CodeAnalysis;

namespace Bshox.Generator.Extensions;

internal static class SymbolExtensions
{
    public static readonly SymbolDisplayFormat FullyQualifiedFormatNG = SymbolDisplayFormat.FullyQualifiedFormat.WithGlobalNamespaceStyle(SymbolDisplayGlobalNamespaceStyle.Omitted);
    public static readonly SymbolDisplayFormat FullyQualifiedFormat = SymbolDisplayFormat.FullyQualifiedFormat.AddMiscellaneousOptions(SymbolDisplayMiscellaneousOptions.ExpandValueTuple);

    public static bool ContainsAttributeImpl(this ISymbol symbol, INamedTypeSymbol attribute)
    {
        return symbol.GetAttributes().Any(x =>
        {
            if (x.AttributeClass == null)
                return false;
            if (x.AttributeClass.EqualsUnboundGenericType(attribute))
                return true;
            foreach (var item in x.AttributeClass.GetAllBaseTypes())
            {
                if (item.EqualsUnboundGenericType(attribute))
                {
                    return true;
                }
            }
            return false;
        });
    }

    public static AttributeData? GetAttribute(this ISymbol symbol, INamedTypeSymbol attribute)
    {
        return symbol.GetAttributes().FirstOrDefault(x => SymbolEqualityComparer.Default.Equals(x.AttributeClass, attribute));
    }

    public static AttributeData? GetGenericAttribute(this ISymbol symbol, INamedTypeSymbol attribute)
    {
        Debug.Assert(attribute.IsGenericType, "attribute.IsGenericType");
        Debug.Assert(attribute.IsUnboundGenericType, "attribute.IsUnboundGenericType");
        return symbol.GetAttributes().FirstOrDefault(x => x.AttributeClass?.EqualsUnboundGenericType(attribute) ?? false);
    }

    public static string ToXmlCommentString(this ITypeSymbol symbol)
    {
        if (symbol is IArrayTypeSymbol array)
        {
            return array.ElementType.ToXmlCommentString() + "[]";
        }

        if (symbol is INamedTypeSymbol { IsGenericType: true } namedTypeSymbol)
        {
            if (namedTypeSymbol is { IsTupleType: true, TupleElements.Length: > 0 })
            {
                var types = namedTypeSymbol.TupleElements.Select(x => x.Type).ToList();
                var sb = new StringBuilder().Append('(');
                for (int i = 0; i < types.Count; i++)
                {
                    _ = sb.Append(ToXmlCommentString(types[i]));
                    if (i < types.Count - 1)
                        _ = sb.Append(", ");
                }
                return sb.Append(')').ToString();
            }
            else
            {
                string displayString = namedTypeSymbol.ConstructUnboundGenericType().ToDisplayString(NullableFlowState.None, FullyQualifiedFormat);
                displayString = displayString.Replace('<', '{').Replace('>', '}');
                var typeParameters = namedTypeSymbol.TypeParameters.Select(x => x.Name).ToImmutableArray();
                var typeArguments = namedTypeSymbol.TypeArguments.Select(ToXmlCommentString).ToImmutableArray();
                var sb = new StringBuilder().AppendFormat("<see cref=\"{0}\" />", displayString);
                if (typeParameters.Length > 0)
                    _ = sb.Append(" (");
                for (int i = 0; i < typeParameters.Length; i++)
                {
                    _ = sb.AppendFormat("<c>{0}</c> is {1}", typeParameters[i], typeArguments[i]);
                    if (i < typeParameters.Length - 1)
                        _ = sb.Append(", ");
                }
                if (typeParameters.Length > 0)
                    _ = sb.Append(')');
                return sb.ToString();
            }
        }
        else
        {
            string displayString = symbol.ToDisplayString(NullableFlowState.None, FullyQualifiedFormat);
            displayString = displayString.Replace('<', '{').Replace('>', '}');
            return $"<see cref=\"{displayString}\" />";
        }
    }

    /// <summary>
    /// <see href="https://github.com/dotnet/runtime/blob/dff1d8467845fd93c517f89dc81598e5fd17c270/src/libraries/Common/src/SourceGenerators/TypeModelHelper.cs#L13"/>
    /// </summary>
    public static List<ITypeSymbol>? GetAllTypeArgumentsInScope(this INamedTypeSymbol type)
    {
        if (!type.IsGenericType)
        {
            return null;
        }

        List<ITypeSymbol>? args = null;
        TraverseContainingTypes(type);
        return args;

        void TraverseContainingTypes(INamedTypeSymbol current)
        {
            if (current.ContainingType is { } parent)
            {
                TraverseContainingTypes(parent);
            }

            if (!current.TypeArguments.IsEmpty)
            {
                (args ??= []).AddRange(current.TypeArguments);
            }
        }
    }

    public static bool TryParseBshoxMemberAttribute(this ISymbol symbol, KnownTypeSymbols knownSymbols, out uint key)
    {
        var data = symbol.GetAttribute(knownSymbols.BshoxMemberAttribute);
        if (data is null)
        {
            key = 0;
            return false;
        }
        if (data.ConstructorArguments.Length != 1)
        {
            key = 0;
            return false;
        }
        key = (uint)data.ConstructorArguments[0].Value!;
        return true;
    }

    public static bool TryParseBshoxContractAttribute(this ISymbol symbol, KnownTypeSymbols knownSymbols, out ContractParameters parameters)
    {
        var data = symbol.GetImplAttribute(knownSymbols.BshoxContractAttribute);
        if (data is null)
        {
            parameters = default;
            return false;
        }
        parameters = ParseContractParameters(data);
        return true;
    }

    public static bool TryParseBshoxSurrogateAttribute(this ISymbol symbol, IGeneratorContext context, out ContractParameters parameters, [NotNullWhen(true)] out ITypeSymbol? typeSymbol)
    {
        typeSymbol = null;
        parameters = default;

        var genericAttribute = symbol.GetGenericAttribute(context.KnownSymbols.BshoxSurrogateAttribute1);
        if (genericAttribute is null)
        {
            context.ReportDiagnostic(Diagnostics.SurrogateTypeMustHaveAttribute, symbol, symbol);
            return false;
        }
        // the type argument is the first argument of the generic attribute
        typeSymbol = genericAttribute.AttributeClass?.TypeArguments[0];
        if (typeSymbol is null)
        {
            context.InternalError(genericAttribute.ApplicationSyntaxReference?.GetLocation(), "Invalid argument type");
            return false;
        }
        parameters = ParseContractParameters(genericAttribute);
        return true;

    }

    private static ContractParameters ParseContractParameters(AttributeData data)
    {
        ContractParameters parameters = default;
        if (data.NamedArguments.TryGetAs(nameof(ContractParameters.ImplicitDefaultValues), out bool implicitDefaultValues))
        {
            parameters.ImplicitDefaultValues = implicitDefaultValues;
        }
        if (data.NamedArguments.TryGetAs(nameof(ContractParameters.ImplicitMembers), out bool implicitMembers))
        {
            parameters.ImplicitMembers = implicitMembers;
        }
        return parameters;
    }

    public static bool TryParseDefaultValueAttribute(this ISymbol symbol, IGeneratorContext context, [NotNullWhen(true)] out TypedConstant? value)
    {
        var data = symbol.GetAttribute(context.KnownSymbols.DefaultValueAttribute);
        if (data is null)
        {
            value = null;
            return false;
        }
        if (data.ConstructorArguments.Length != 1)
        {
            var location = data.ApplicationSyntaxReference?.GetSyntax().GetLocation() ?? symbol.Locations.FirstOrDefault();
            context.ReportDiagnostic(Diagnostics.DefaultValueMustHave1Argument, location, symbol);
            value = null;
            return false;
        }
        value = data.ConstructorArguments[0];
        return true;
    }

    public static AttributeData? GetImplAttribute(this ISymbol symbol, INamedTypeSymbol implAttribute)
    {
        return symbol.GetAttributes().FirstOrDefault(x =>
        {
            if (x.AttributeClass == null)
                return false;
            if (x.AttributeClass.EqualsUnboundGenericType(implAttribute))
                return true;

            return x.AttributeClass.GetAllBaseTypes().Any(item => item.EqualsUnboundGenericType(implAttribute));
        });
    }

    public static IEnumerable<ISymbol> GetAllMembers(this INamedTypeSymbol symbol, bool withoutOverride = true)
    {
        // Iterate Parent -> Derived
        if (symbol.BaseType != null)
        {
            foreach (var item in symbol.BaseType.GetAllMembers())
            {
                // override item already iterated in parent type
                if (!withoutOverride || !item.IsOverride)
                {
                    yield return item;
                }
            }
        }

        foreach (var item in symbol.GetMembers())
        {
            if (!withoutOverride || !item.IsOverride)
            {
                yield return item;
            }
        }
    }

    public static IEnumerable<ISymbol> GetParentMembers(this INamedTypeSymbol symbol)
    {
        // Iterate Parent -> Derived
        if (symbol.BaseType != null)
        {
            foreach (var item in symbol.BaseType.GetAllMembers())
            {
                // override item already iterated in parent type
                if (!item.IsOverride)
                {
                    yield return item;
                }
            }
        }
    }

    public static IEnumerable<INamedTypeSymbol> GetAllBaseTypes(this INamedTypeSymbol symbol)
    {
        var t = symbol.BaseType;
        while (t != null)
        {
            yield return t;
            t = t.BaseType;
        }
    }

    public static string FullyQualifiedToString(this ISymbol symbol)
    {
        return symbol.ToDisplayString(FullyQualifiedFormat);
    }

    public static string FullyQualifiedToStringNG(this ISymbol symbol)
    {
        return symbol.ToDisplayString(FullyQualifiedFormatNG);
    }

    public static bool EqualsUnboundGenericType(this INamedTypeSymbol left, INamedTypeSymbol right)
    {
        var l = left.IsGenericType ? left.ConstructUnboundGenericType() : left;
        var r = right.IsGenericType ? right.ConstructUnboundGenericType() : right;
        return SymbolEqualityComparer.Default.Equals(l, r);
    }
}
