using System.Diagnostics;
using Bshox.Generator.Extensions;
using Bshox.Generator.Helpers;
using Microsoft.CodeAnalysis;

namespace Bshox.Generator.Contracts;

internal sealed class SurrogateGenerator(INamedTypeSymbol surrogateType, string generatedTypeName) : IContractGenerator
{
    /// <inheritdoc />
    public string GeneratedTypeName => generatedTypeName;

    /// <inheritdoc />
    public bool TryGenerate(ContractInfo contract, SerializerInfo serializer)
    {
        if (!serializer.ContractResolver.TryResolveContract(contract.Dependencies.Single(), out var innerContract))
        {
            return false;
        }
        var code = new SourceWriter();

        // TODO: remove?
        Debug.Assert(innerContract.Generator is not null, "innerContract.Generator is not null"); // the inner contract should be generated

        code.DeclareAlias(serializer);
        code.WriteLine();

        var ns = serializer.ClassSymbol.ContainingNamespace;
        if (!ns.IsGlobalNamespace)
        {
            code.WriteLine($"namespace {ns};");
            code.WriteLine();
        }

        string serializerClassName = serializer.ClassSymbol.ToDisplayString(NullableFlowState.None, SymbolDisplayFormat.MinimallyQualifiedFormat);
        code.WriteLine($"partial class {serializerClassName}");
        code.OpenScope();
        {
            string contractClassName = GeneratedTypeName;
            string typeName = contract.Type.ToDisplayString(NullableFlowState.None, SymbolExtensions.FullyQualifiedFormat);
            string surrogateFullName = surrogateType.ToDisplayString(NullableFlowState.None, SymbolExtensions.FullyQualifiedFormat);

            code.WriteComment($"This contract is using {surrogateType} as a surrogate for {contract.Type}");

            string innerContractFullName = innerContract.VariableFullName;

            code.WriteLine(Constants.GeneratedCodeAttributeText);
            code.WriteLine($$"""
                             private sealed class {{contractClassName}} : bsx::BshoxContract<{{typeName}}>
                             {
                                 internal {{contractClassName}}() : base(bsx::BshoxCode.SubObject)
                                 {
                                 }

                                 public override void Serialize(ref bsx::BshoxWriter writer, scoped ref readonly {{typeName}} value)
                                 {
                                     {{surrogateFullName}} surrogate = new {{surrogateFullName}}(value);
                                     {{innerContractFullName}}.Serialize(ref writer, in surrogate);
                                 }

                                 public override void Deserialize(ref bsx::BshoxReader reader, out {{typeName}} value)
                                 {
                                     {{innerContractFullName}}.Deserialize(ref reader, out {{surrogateFullName}} surrogate);
                                     value = surrogate.Convert();
                                 }
                             }
                             """);
        }
        code.CloseScope(); // partial class {serializerClassName}

        string fullType = serializer.ClassSymbol.FullyQualifiedToStringNG()
            .Replace('<', '(')
            .Replace('>', ')');
        string fileName = Constants.InvalidPathChars.Replace($"{fullType}.{contract.VariableName}.g.cs", string.Empty);

        serializer.AddSource(fileName, code);
        return true;
    }

    /// <inheritdoc />
    public void WriteXmlDocRemarks(SourceWriter code, IContractResolver resolver)
    {
        code.WriteLine($"/// This contract is using {surrogateType.ToXmlCommentString()} as a surrogate.<br/>");
        var contract = resolver.ResolveContract(ContractDemand.DefaultForType(surrogateType));
        Debug.Assert(contract.Generator is not null, "contract.Generator is not null");
        if (contract.Generator is { } generator)
        {
            generator.WriteXmlDocRemarks(code, resolver);
        }
    }
}
