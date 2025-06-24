extern alias bsx;
using System.Text.Encodings.Web;
using System.Text.Json.Serialization;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;

namespace Generator.Benchmark;

public static class Utils
{
    private static readonly string[] excludedKeyTokens =
    [
        "b8d4030011dbd70c", // TUnit
        "5c492ec4f3eccde3", // ReSharper
        "6fe0a02d2036aa1d", // testcentric
        "50cebf1cceb9d05e", // Mono
    ];

    private static readonly string[] excludedNames =
    [
        "System.Text.Json.SourceGeneration",
        "Bshox.Generator",
    ];

    private static List<MetadataReference>? s_References;

    public static CSharpCompilation GetCompilation(string sourceCode, CSharpCompilationOptions? options = null, CSharpParseOptions? options2 = null)
    {
        var syntaxTree = CSharpSyntaxTree.ParseText(sourceCode, options2);
        s_References ??= GetReferences();

        // Console.WriteLine($"Assemblies:\n{string.Join(",\n", s_References.Select(r => r.Display).OrderBy(n => n))}");

        var compilation = CSharpCompilation.Create("SourceGeneratorTests",
            [syntaxTree],
            s_References,
            options ?? new CSharpCompilationOptions(OutputKind.DynamicallyLinkedLibrary));

        return compilation;
    }

    private static List<MetadataReference> GetReferences()
    {
        var assemblies = AppDomain.CurrentDomain.GetAssemblies()
                    .Where(assembly => !assembly.IsDynamic)
                    .Append(typeof(bsx::Bshox.Attributes.BshoxContractAttribute).Assembly) // force Bshox to be included. Use alias to avoid conflict with Bshox.Generator
                    .Append(typeof(JsonSerializableAttribute).Assembly) // force System.Text.Json to be included
                    .Append(typeof(JavaScriptEncoder).Assembly) // force System.Text.Encodings.Web to be included
                    .Where(a =>
                    {
                        var token = a.GetName().GetPublicKeyToken()?.ToHex();
                        return !excludedKeyTokens.Contains(token, StringComparer.OrdinalIgnoreCase);
                    })
                    .Where(a =>
                    {
                        var name = a.GetName().Name;
                        return !excludedNames.Contains(name, StringComparer.OrdinalIgnoreCase);
                    })
                    .Select(assembly => MetadataReference.CreateFromFile(assembly.Location) as MetadataReference)
                    .ToList();
        return assemblies;
    }

    public static string ToHex(this byte[] bytes)
    {
#if NET6_0_OR_GREATER
        return Convert.ToHexString(bytes);
#else
        return BitConverter.ToString(bytes).Replace("-", "");
#endif
    }
}
