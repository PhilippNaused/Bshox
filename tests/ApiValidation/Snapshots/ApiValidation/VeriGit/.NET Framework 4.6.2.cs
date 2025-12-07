// VeriGit, PublicKeyToken=71dcaf280189db03
// Platform: AnyCPU (64-bit preferred)
// Runtime: v4.0.30319
// Reference: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// Reference: System, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// Reference: TUnit.Core, Version=1.5.0.0, Culture=neutral, PublicKeyToken=b8d4030011dbd70c
[assembly: System.Reflection.AssemblyCompany("Philipp Naused")]
[assembly: System.Reflection.AssemblyCopyright("© Philipp Naused")]
[assembly: System.Reflection.AssemblyDescription("High performance binary serialization for C#")]
[assembly: System.Reflection.AssemblyMetadata("RepositoryUrl", "https://github.com/PhilippNaused/Bshox")]
[assembly: System.Reflection.AssemblyProduct("Bshox")]
[assembly: System.Reflection.AssemblyTitle("VeriGit")]
[assembly: System.Resources.NeutralResourcesLanguage("en-US")]
[assembly: System.Runtime.CompilerServices.CompilationRelaxations(8)]
[assembly: System.Runtime.CompilerServices.RuntimeCompatibility(WrapNonExceptionThrows = true)]
[assembly: System.Runtime.Versioning.TargetFramework(".NETFramework,Version=v4.6.2", FrameworkDisplayName = ".NET Framework 4.6.2")]
[module: System.Runtime.CompilerServices.RefSafetyRules(11)]
namespace VeriGit
{
    public static class Validation
    {
        public static System.Threading.Tasks.Task Validate(string actual, string extension = "txt", string? targetName = null, [System.Runtime.CompilerServices.CallerFilePath] string callerFilePath = "");
    }
    public sealed class ValidationFailedException : System.Exception
    {
        public ValidationFailedException(string message, string filePath, string? actualText, string? diffText);
        public string? ActualText { get; }
        public string? DiffText { get; }
        public string FilePath { get; }
    }
}
