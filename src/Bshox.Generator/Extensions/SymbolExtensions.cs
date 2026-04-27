using System.Collections.Immutable;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Text;
using Bshox.Generator.Data;
using Microsoft.CodeAnalysis;

namespace Bshox.Generator.Extensions;

#pragma warning disable IDE0051 // Remove unused private members (false positive for extension methods)

internal static class SymbolExtensions
{
    public static readonly SymbolDisplayFormat FullyQualifiedFormat = SymbolDisplayFormat.FullyQualifiedFormat.AddMiscellaneousOptions(SymbolDisplayMiscellaneousOptions.ExpandValueTuple).AddMemberOptions(SymbolDisplayMemberOptions.IncludeContainingType);
    public static readonly SymbolDisplayFormat FullyQualifiedFormatNG = FullyQualifiedFormat.WithGlobalNamespaceStyle(SymbolDisplayGlobalNamespaceStyle.Omitted);
    public static readonly SymbolDisplayFormat FullyQualifiedFormatWithNull = FullyQualifiedFormat.AddMiscellaneousOptions(SymbolDisplayMiscellaneousOptions.IncludeNullableReferenceTypeModifier);

    extension(ISymbol symbol)
    {
        public bool ContainsAttributeImpl(INamedTypeSymbol attribute)
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

        public AttributeData? GetAttribute(INamedTypeSymbol attribute)
        {
            return symbol.GetAttributes().FirstOrDefault(x => SymbolEqualityComparer.Default.Equals(x.AttributeClass, attribute));
        }

        public ImmutableArray<AttributeData> GetAttributes(INamedTypeSymbol attribute)
        {
            return [.. symbol.GetAttributes().Where(x => SymbolEqualityComparer.Default.Equals(x.AttributeClass, attribute))];
        }

        public AttributeData? GetGenericAttribute(INamedTypeSymbol attribute)
        {
            Debug.Assert(attribute.IsGenericType, "attribute.IsGenericType");
            Debug.Assert(attribute.IsUnboundGenericType, "attribute.IsUnboundGenericType");
            return symbol.GetAttributes().FirstOrDefault(x => x.AttributeClass?.EqualsUnboundGenericType(attribute) ?? false);
        }

        public ImmutableArray<AttributeData> GetGenericAttributes(INamedTypeSymbol attribute)
        {
            Debug.Assert(attribute.IsGenericType, "attribute.IsGenericType");
            Debug.Assert(attribute.IsUnboundGenericType, "attribute.IsUnboundGenericType");
            return [.. symbol.GetAttributes().Where(x => x.AttributeClass?.EqualsUnboundGenericType(attribute) ?? false)];
        }

        public bool TryParseBshoxMemberAttribute(KnownTypeSymbols knownSymbols, out uint key)
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

        public bool TryParseBshoxContractAttribute(KnownTypeSymbols knownSymbols, out ContractParameters parameters)
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

        public bool TryParseDefaultValueAttribute(IGeneratorContext context, [NotNullWhen(true)] out TypedConstant? value)
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

        public AttributeData? GetImplAttribute(INamedTypeSymbol implAttribute)
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

        public string FullyQualifiedToString()
        {
            return symbol.ToDisplayString(FullyQualifiedFormat);
        }

        public string FullyQualifiedToStringWithNull()
        {
            return symbol.ToDisplayString(FullyQualifiedFormatWithNull);
        }

        public string FullyQualifiedToStringNG()
        {
            return symbol.ToDisplayString(FullyQualifiedFormatNG);
        }
    }

    extension(ITypeSymbol symbol)
    {
        public string ToXmlCommentString()
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
                    // Constructed generics are not supported in XML doc comments, so we need to use the unbound generic type
                    // See: https://github.com/dotnet/csharplang/discussions/8986
                    string displayString = namedTypeSymbol.ConstructedFrom.ToDisplayString(NullableFlowState.None, FullyQualifiedFormat);
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

        public bool IsNested()
        {
            return symbol.ContainingType is not null;
        }

        public bool IsUnresolvedGeneric()
        {
            if (symbol is INamedTypeSymbol named)
            {
                return named.TypeArguments.Any(IsUnresolvedGeneric);
            }
            if (symbol is ITypeParameterSymbol)
            {
                return true;
            }
            if (symbol is IArrayTypeSymbol array)
            {
                return array.ElementType.IsUnresolvedGeneric();
            }
            return false;
        }
    }

    extension(INamedTypeSymbol symbol)
    {
        /// <summary>
        /// <see href="https://github.com/dotnet/runtime/blob/dff1d8467845fd93c517f89dc81598e5fd17c270/src/libraries/Common/src/SourceGenerators/TypeModelHelper.cs#L13"/>
        /// </summary>
        public List<ITypeSymbol>? GetAllTypeArgumentsInScope()
        {
            if (!symbol.IsGenericType)
            {
                return null;
            }

            List<ITypeSymbol>? args = null;
            TraverseContainingTypes(symbol);
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

        public IEnumerable<ISymbol> GetAllMembers(bool withoutOverride = true)
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

        public IEnumerable<ISymbol> GetParentMembers()
        {
            // Iterate Parent -> Derived
            if (symbol.BaseType is null)
            {
                yield break;
            }
            foreach (var item in symbol.BaseType.GetAllMembers())
            {
                // override item already iterated in parent type
                if (!item.IsOverride)
                {
                    yield return item;
                }
            }
        }

        public IEnumerable<INamedTypeSymbol> GetAllBaseTypes()
        {
            var t = symbol.BaseType;
            while (t != null)
            {
                yield return t;
                t = t.BaseType;
            }
        }

        public bool EqualsUnboundGenericType(INamedTypeSymbol other)
        {
            var l = symbol.IsGenericType ? symbol.ConstructUnboundGenericType() : symbol;
            var r = other.IsGenericType ? other.ConstructUnboundGenericType() : other;
            return SymbolEqualityComparer.Default.Equals(l, r);
        }
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
}
