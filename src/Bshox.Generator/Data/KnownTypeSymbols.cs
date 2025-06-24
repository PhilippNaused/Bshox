using System.ComponentModel;
using Bshox.Attributes;
using Microsoft.CodeAnalysis;

namespace Bshox.Generator.Data;

#pragma warning disable CA1720 // Identifier contains type name

internal sealed class KnownTypeSymbols(Compilation compilation)
{
    public INamedTypeSymbol BshoxContractAttribute { get; } = GetType(compilation, typeof(BshoxContractAttribute));
    public INamedTypeSymbol BshoxMemberAttribute { get; } = GetType(compilation, typeof(BshoxMemberAttribute));
    public INamedTypeSymbol BshoxSerializerAttribute { get; } = GetType(compilation, typeof(BshoxSerializerAttribute));
    public INamedTypeSymbol BshoxDefaultContractAttribute { get; } = GetType(compilation, typeof(BshoxDefaultContractAttribute));
    public INamedTypeSymbol BshoxSurrogateAttribute1 { get; } = GetType(compilation, typeof(BshoxSurrogateAttribute<>));
    public INamedTypeSymbol BshoxContract { get; } = GetTypeByMetadataName(compilation, "Bshox.BshoxContract`1").ConstructUnboundGenericType();

    public INamedTypeSymbol Guid { get; } = GetType(compilation, typeof(Guid));
    public INamedTypeSymbol DateTime { get; } = GetType(compilation, typeof(DateTime));
    public INamedTypeSymbol TimeSpan { get; } = GetType(compilation, typeof(TimeSpan));

    public INamedTypeSymbol DefaultValueAttribute { get; } = GetType(compilation, typeof(DefaultValueAttribute));

    public INamedTypeSymbol List { get; } = GetType(compilation, typeof(List<>));

    public INamedTypeSymbol Dictionary { get; } = GetType(compilation, typeof(Dictionary<,>));

    private static INamedTypeSymbol GetTypeByMetadataName(Compilation compilation, string metadataName)
    {
        return compilation.GetTypeByMetadataName(metadataName) ?? throw new InvalidOperationException($"Type {metadataName} is not found in compilation.");
    }

    private static INamedTypeSymbol GetType(Compilation compilation, Type type)
    {
        var symbol = GetTypeByMetadataName(compilation, type.FullName!);
        // check if type is unbound generic type
        if (type is { IsGenericType: true, IsGenericTypeDefinition: true })
        {
            symbol = symbol.ConstructUnboundGenericType();
        }
        return symbol;
    }
}
