using Bshox.Generator.Helpers;

namespace Bshox.Generator.Extensions;

internal static class SourceWriterExtension
{
    public static void DeclareAlias(this SourceWriter code, SerializerInfo type)
    {
        code.WriteLine($"using {SerializerInfo.Alias} = {type.ClassSymbol.FullyQualifiedToString()};");
    }
}
