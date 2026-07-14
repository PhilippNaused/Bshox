using System.Diagnostics.CodeAnalysis;

namespace Bshox.Generator.Contracts;

internal readonly struct InlineContractData(string serializeFormat, string deserializeString, BshoxEncoding encoding)
{
    /// <summary>
    /// A single C# statement that can be used to serialize a value of the type represented by this contract.
    /// The substring <c>{0}</c> will be replaced with the expression for the value to be serialized.
    /// e.g.:
    /// <code>
    /// writer.WriteVarInt32({0});
    /// </code>
    /// </summary>
    [StringSyntax("C#")]
    public readonly string SerializeFormat = serializeFormat;
    /// <summary>
    /// A single C# expression that can be used to deserialize a value of the type represented by this contract.
    /// e.g.:
    /// <code>
    /// reader.ReadVarInt32()
    /// </code>
    /// </summary>
    [StringSyntax("C#")]
    public readonly string DeserializeString = deserializeString;
    public readonly BshoxEncoding Encoding = encoding;
}
