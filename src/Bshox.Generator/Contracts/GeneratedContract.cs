using System.Collections.Immutable;
using System.Diagnostics.CodeAnalysis;
using Bshox.Generator.Data;
using Bshox.Generator.Extensions;
using Microsoft.CodeAnalysis;

namespace Bshox.Generator.Contracts;

internal static class GeneratedContract
{
    public static bool TryCreate(ITypeSymbol symbol, IGeneratorContext context, IContractResolver resolver, [NotNullWhen(true)] out ContractInfo? contract)
    {
        if (!symbol.TryParseBshoxContractAttribute(context.KnownSymbols, out var parameters))
        {
            contract = null;
            return false;
        }
        var members = GetMembers(parameters, context, (INamedTypeSymbol)symbol);
        string escapeFullName = ContractHelper.EscapeFullName(symbol);
        var generator = new ContractGenerator(parameters, members, $"{escapeFullName}__BshoxContract");
        var dependencies = members.Select(member => member.ContractDemand).ToImmutableArray();
        contract = resolver.CreateGenerated(type: symbol,
            staticDependencies: false, // source generated contracts always resolve dependencies lazily since they can have circular dependencies
            dependencies: dependencies,
            generator: generator,
            initializer: $"new {generator.GeneratedTypeName}()");
        return true;
    }

    private static List<MemberInfo> GetMembers(ContractParameters parameters, IGeneratorContext context, INamedTypeSymbol type)
    {
        List<MemberInfo> members;

        var allMembers = type.GetAllMembers();

        if (parameters.ImplicitMembers)
        {
            members = allMembers // iterate includes parent type
                .Where(static x => x is (IFieldSymbol or IPropertySymbol) and { IsStatic: false, IsImplicitlyDeclared: false, CanBeReferencedByName: true })
                .Reverse()
                .DistinctBy(static x => x.Name) // remove duplicate name(new)
                .Reverse()
                .Where(static x => x.DeclaredAccessibility is Accessibility.Public)
                .Where(static x =>
                {
                    if (x is IPropertySymbol p)
                    {
                        if (p.GetMethod == null)
                            return false;
                        if (p.IsIndexer)
                            return false;
                    }
                    return true;
                })
                .Select(x => new MemberInfo(x, parameters, context))
                .ToList();
        }
        else
        {
            members = allMembers
                .Where(x => x.GetAttribute(context.KnownSymbols.BshoxMemberAttribute) is not null)
                .Select(x => new MemberInfo(x, parameters, context))
                .ToList();
        }
        return members;
    }
}
