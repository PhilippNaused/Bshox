using System.Reflection;
using System.Text.RegularExpressions;

namespace Bshox.Generator;

internal static class Constants
{
    private static readonly AssemblyName GeneratorAssemblyName = typeof(Constants).Assembly.GetName();
    // This is not readonly so it can be overwritten during tests to ensure the generated code is version independent.
    public static Version GeneratorVersion = GeneratorAssemblyName.Version!;
    private static readonly string GeneratorName = GeneratorAssemblyName.Name!;
    public static readonly Regex InvalidPathChars = new($"[{Regex.Escape(new string(Path.GetInvalidFileNameChars()))} ]", RegexOptions.Compiled);
    public static string GeneratedCodeAttributeText => $"""[global::System.CodeDom.Compiler.GeneratedCodeAttribute("{GeneratorName}", "{GeneratorVersion}")]""";
}
