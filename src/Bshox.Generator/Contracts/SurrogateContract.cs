using System.Diagnostics.CodeAnalysis;
using Bshox.Generator.Extensions;
using Microsoft.CodeAnalysis;

namespace Bshox.Generator.Contracts;

internal static class SurrogateContract
{
    public static bool TryGetFromSurrogate(ITypeSymbol surrogateType, IGeneratorContext context, IContractResolver resolver, [NotNullWhen(true)] out ContractInfo? contract)
    {
        contract = null;
        if (surrogateType is not INamedTypeSymbol namedSurrogateType)
        {
            //TODO: add a diagnostic
            context.InternalError(surrogateType, $"{surrogateType} is not a valid type for a surrogate");
            return false;
        }

        if (!namedSurrogateType.TryParseBshoxSurrogateAttribute(context, out _, out var implementationType))
        {
            return false;
        }

        // check if the surrogate type has the correct constructor
        var ctor = namedSurrogateType.InstanceConstructors.FirstOrDefault(c => c.Parameters.Length == 1
                                                                               && SymbolEqualityComparer.Default.Equals(c.Parameters[0].Type, implementationType)
                                                                               && c.DeclaredAccessibility == Accessibility.Public);

        if (ctor is null)
        {
            context.ReportDiagnostic(Diagnostics.SurrogateMustHaveCorrectConstructor, namedSurrogateType, namedSurrogateType, implementationType);
            return false;
        }

        // check if the surrogate type has the correct Convert method
        // must be public, not static, not an operator, return the implementation type, and be named "Convert"
        var convertMethod = namedSurrogateType.GetMembers("Convert").OfType<IMethodSymbol>().FirstOrDefault(m =>
            m is { IsStatic: false, Parameters.IsEmpty: true, DeclaredAccessibility: Accessibility.Public }
            && SymbolEqualityComparer.Default.Equals(m.ReturnType, implementationType));

        if (convertMethod is null)
        {
            context.ReportDiagnostic(Diagnostics.SurrogateMustHaveCorrectConvertMethod, namedSurrogateType, namedSurrogateType, implementationType);
            return false;
        }

        // only warn if the surrogate type is in source
        if (namedSurrogateType.Locations.All(l => l.IsInSource))
        {
            if (!namedSurrogateType.Name.EndsWith("Surrogate", StringComparison.InvariantCulture))
            {
                context.ReportDiagnostic(Diagnostics.SurrogateShouldHaveSuffix, namedSurrogateType, namedSurrogateType.Name);
            }
        }

        string escapeFullName = ContractHelper.EscapeFullName(namedSurrogateType);
        var generator = new SurrogateGenerator(namedSurrogateType, $"{escapeFullName}__SurrogateContract");

        contract = resolver.CreateGenerated(type: implementationType,
            staticDependencies: true,
            dependencies: [ContractDemand.DefaultForType(surrogateType)],
            generator: generator,
            initializer: $"new {generator.GeneratedTypeName}()");
        return true;
    }
}
