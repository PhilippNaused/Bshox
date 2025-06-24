using Bshox.Generator.Helpers;

namespace Bshox.Generator.Contracts;

internal interface IContractGenerator
{
    bool TryGenerate(ContractInfo contract, SerializerInfo serializer);
    void WriteXmlDocRemarks(SourceWriter code, IContractResolver resolver);
    string GeneratedTypeName { get; }
}
