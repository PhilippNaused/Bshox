namespace Bshox.Generator.Contracts;

internal readonly struct InlineContractData(string serializeFormat, string deserializeString, BshoxCode encoding)
{
    public readonly string SerializeFormat = serializeFormat;
    public readonly string DeserializeString = deserializeString;
    public readonly BshoxCode Encoding = encoding;
}
