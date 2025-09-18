using Microsoft.CodeAnalysis;

namespace Bshox.Generator;

#pragma warning disable CA1724 // Type names should not match namespaces

public static class Diagnostics
{
    private const string Prefix = "BSHOX";
    private const string Category = "Bshox";

    // TODO: fix these
#pragma warning disable RS1007 // Provide localizable arguments to diagnostic descriptor constructor
#pragma warning disable RS1015 // Provide non-null 'helpLinkUri' value to diagnostic descriptor constructor
#pragma warning disable RS1028 // Provide non-null 'customTags' value to diagnostic descriptor constructor

    internal static readonly DiagnosticDescriptor _internalError = new(
        id: $"{Prefix}999",
        title: "Bshox.Generator encountered an internal error",
        messageFormat: "Internal Error: {0}: {1}",
        category: Category,
        defaultSeverity: DiagnosticSeverity.Error,
        isEnabledByDefault: true);

    public static readonly DiagnosticDescriptor TypeMustBePartial = new(
        id: $"{Prefix}001",
        title: "Type must be partial",
        messageFormat: "Type '{0}' must be partial to use [{1}]",
        category: Category,
        defaultSeverity: DiagnosticSeverity.Error,
        isEnabledByDefault: true);

    public static readonly DiagnosticDescriptor TypeMustNotBeNested = new(
        id: $"{Prefix}002",
        title: "Type must not be nested",
        messageFormat: "Type '{0}' must not be nested to use [{1}]",
        category: Category,
        defaultSeverity: DiagnosticSeverity.Error,
        isEnabledByDefault: true);

    public static readonly DiagnosticDescriptor SerializerMustHaveAtLeastOneType = new(
        id: $"{Prefix}003",
        title: "A generated Bshox serializer must have at least one serializable type",
        messageFormat: "The generated type '{0}' must have at least one serializable type",
        category: Category,
        defaultSeverity: DiagnosticSeverity.Error,
        isEnabledByDefault: true);

    public static readonly DiagnosticDescriptor LangVersionMustBe12OrHigher = new(
        id: $"{Prefix}004",
        title: "Bshox requires C# 12 or later",
        messageFormat: "Bshox code generation is not available in C# {0}. Please use C# 12.0 or later.",
        category: Category,
        defaultSeverity: DiagnosticSeverity.Error,
        isEnabledByDefault: true);

    public static readonly DiagnosticDescriptor MemberMustHaveExplicitKey = new(
        id: $"{Prefix}005",
        title: "Bshox serializable members must have an explicit key",
        messageFormat: "Member '{0}' must have a [BshoxMember] attribute with an explicit key",
        category: Category,
        defaultSeverity: DiagnosticSeverity.Error,
        isEnabledByDefault: true);

    public static readonly DiagnosticDescriptor KeyMustBeUnique = new(
        id: $"{Prefix}006",
        title: "Bshox serializable members must have unique keys",
        messageFormat: "Member '{0}' has a duplicate key '{1}'",
        category: Category,
        defaultSeverity: DiagnosticSeverity.Error,
        isEnabledByDefault: true);

    public static readonly DiagnosticDescriptor ImplicitMemberMustNotHaveKey = new(
        id: $"{Prefix}007",
        title: "Implicit Bshox members must not have a key",
        messageFormat: "Member '{0}' must not have a [BshoxMember] attribute when using implicit members",
        category: Category,
        defaultSeverity: DiagnosticSeverity.Error,
        isEnabledByDefault: true);

    public static readonly DiagnosticDescriptor KeyMustBeInValidRange = new(
        id: $"{Prefix}008",
        title: "Bshox serializable members must have a valid key",
        messageFormat: $"Member '{{0}}' has an invalid key '{{1}}'. Keys must be values between {BshoxConstants.MinKey} and {BshoxConstants.MaxKey}.",
        category: Category,
        defaultSeverity: DiagnosticSeverity.Error,
        isEnabledByDefault: true);

    public static readonly DiagnosticDescriptor DefaultValueMustHave1Argument = new(
        id: $"{Prefix}009",
        title: "DefaultValueAttribute must have exactly one constructor argument",
        messageFormat: "The DefaultValueAttribute on '{0}' must have exactly one constructor argument",
        category: Category,
        defaultSeverity: DiagnosticSeverity.Error,
        isEnabledByDefault: true);

    public static readonly DiagnosticDescriptor TypeNotSerializable = new(
        id: $"{Prefix}010",
        title: "Type is not serializable",
        messageFormat: "Type '{0}' is not serializable",
        category: Category,
        defaultSeverity: DiagnosticSeverity.Error,
        isEnabledByDefault: true);

    public static readonly DiagnosticDescriptor SurrogateShouldHaveSuffix = new(
        id: $"{Prefix}011",
        title: "Surrogate types should have a 'Surrogate' suffix",
        messageFormat: "The surrogate type '{0}' should have the 'Surrogate' suffix",
        category: Category,
        defaultSeverity: DiagnosticSeverity.Warning,
        isEnabledByDefault: true);

    public static readonly DiagnosticDescriptor SurrogateMustHaveCorrectConstructor = new(
        id: $"{Prefix}012",
        title: "Surrogate types must have the required constructor",
        messageFormat: "The surrogate type '{0}' must have a public constructor that takes a '{1}'",
        category: Category,
        defaultSeverity: DiagnosticSeverity.Error,
        isEnabledByDefault: true);

    public static readonly DiagnosticDescriptor SurrogateMustHaveCorrectConvertMethod = new(
        id: $"{Prefix}013",
        title: "Surrogate types must have the required 'Convert' method",
        messageFormat: "The surrogate type '{0}' must have a public method with signature 'public {1} Convert()'",
        category: Category,
        defaultSeverity: DiagnosticSeverity.Error,
        isEnabledByDefault: true);

    public static readonly DiagnosticDescriptor RoslynVersionIsTooOld = new(
        id: $"{Prefix}014",
        title: "The current compiler version is too low",
        messageFormat: "The Bshox source generator has been disabled because the current compiler version ({0}) is too low. Bshox requires at least Roslyn {1}.",
        category: Category,
        defaultSeverity: DiagnosticSeverity.Error,
        isEnabledByDefault: true);

    public static readonly DiagnosticDescriptor DepthLockNotUsedCorrectly = new(
        id: $"{Prefix}015",
        title: "Dispose the return value of the DepthLock() method correctly",
        messageFormat: "DepthLock() should only be called in a using statement or using declaration without assigning it to a named variable",
        category: Category,
        defaultSeverity: DiagnosticSeverity.Warning,
        isEnabledByDefault: true);

    public static readonly DiagnosticDescriptor SurrogateTypeMustHaveAttribute = new(
        id: $"{Prefix}016",
        title: "Surrogate types must have the [BshoxSurrogate<T>] attribute",
        messageFormat: "The surrogate type '{0}' must have the [BshoxSurrogate<T>] attribute",
        category: Category,
        defaultSeverity: DiagnosticSeverity.Error,
        isEnabledByDefault: true);

    public static readonly DiagnosticDescriptor ContractSymbolNotUnique = new(
        id: $"{Prefix}017",
        title: "The specified symbol for the contract is not unique",
        messageFormat: "Type '{0}' must have exactly one static member called '{1}'",
        category: Category,
        defaultSeverity: DiagnosticSeverity.Error,
        isEnabledByDefault: true);

    internal static void InternalError(this IDiagnosticOutput diagnostics, Location? location, string arg0, object? arg1 = null)
    {
        diagnostics.ReportDiagnostic(_internalError, location, arg0, arg1);
    }

    internal static void InternalError(this IDiagnosticOutput diagnostics, ISymbol symbol, string arg0, object? arg1 = null)
    {
        Location? location = symbol.Locations.FirstOrDefault();
        diagnostics.ReportDiagnostic(_internalError, location, arg0, arg1);
    }

    internal static void ReportDiagnostic(this IDiagnosticOutput diagnostics, DiagnosticDescriptor descriptor, SyntaxToken token, params object?[] messageArgs)
    {
        diagnostics.ReportDiagnostic(descriptor, token.GetLocation(), messageArgs);
    }

    internal static void ReportDiagnostic(this IDiagnosticOutput diagnostics, DiagnosticDescriptor descriptor, ISymbol symbol, params object?[] messageArgs)
    {
        Location? location = symbol.Locations.FirstOrDefault();
        diagnostics.ReportDiagnostic(descriptor, location, messageArgs);
    }
}
