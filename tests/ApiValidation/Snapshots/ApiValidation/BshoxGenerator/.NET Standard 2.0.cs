// Bshox.Generator, PublicKeyToken=71dcaf280189db03
// Platform: AnyCPU (64-bit preferred)
// Runtime: v4.0.30319
// Reference: netstandard, Version=2.0.0.0, Culture=neutral, PublicKeyToken=cc7b13ffcd2ddd51
// Reference: Microsoft.CodeAnalysis, Version=4.9.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35
// Reference: Microsoft.CodeAnalysis.CSharp, Version=4.9.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35
// Reference: System.Collections.Immutable, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a
// Reference: System.Memory, Version=4.0.1.2, Culture=neutral, PublicKeyToken=cc7b13ffcd2ddd51
[assembly: System.Runtime.CompilerServices.CompilationRelaxations(8)]
[assembly: System.Runtime.CompilerServices.RuntimeCompatibility(WrapNonExceptionThrows = true)]
[assembly: System.Runtime.Versioning.TargetFramework(".NETStandard,Version=v2.0", FrameworkDisplayName = ".NET Standard 2.0")]
[assembly: System.Reflection.AssemblyCompany("Philipp Naused")]
[assembly: System.Reflection.AssemblyCopyright("© Philipp Naused")]
[assembly: System.Reflection.AssemblyDescription("High performance binary serialization for C#")]
[assembly: System.Reflection.AssemblyProduct("Bshox")]
[assembly: System.Reflection.AssemblyTitle("Bshox.Generator")]
[assembly: System.Reflection.AssemblyMetadata("RepositoryUrl", "https://github.com/PhilippNaused/Bshox")]
[assembly: System.Resources.NeutralResourcesLanguage("en-US")]
[assembly: System.Runtime.CompilerServices.InternalsVisibleTo("Bshox.Generator.Tests, PublicKey=0024000004800000940000000602000000240000525341310004000001000100dd5aaf9dfcf30dc5f78e30f2ccbd27f7dc88c9a8db26eda6ed229b883fd34edbdcffce799b053db93a3c4d288e976266f67acb9cd2fa5d24c3642e5b8191d53aebe1954a64512f7d9e992eeb779d011e2c25b4b76d8cacde8f0c675c8093a2f4b8eaafbf6ff24e271d502b023c5f5f2afced11ed447be096d332d3f8c4f70fcd")]
[assembly: System.Security.Permissions.SecurityPermission(System.Security.Permissions.SecurityAction.RequestMinimum, SkipVerification = true)]
[module: System.Security.UnverifiableCode]
[module: System.Runtime.CompilerServices.RefSafetyRules(11)]
namespace Bshox
{
    public enum BshoxCode : byte
    {
        VarInt = 0,
        Fixed4 = 1,
        Fixed8 = 2,
        Prefixed = 3,
        Array = 4,
        SubObject = 5
    }
    public static class BshoxConstants
    {
        public const uint MinKey = 1u;
        public const uint MaxKey = 536870911u;
    }
}
namespace Bshox.Attributes
{
    [System.AttributeUsage(System.AttributeTargets.Class | System.AttributeTargets.Struct, AllowMultiple = false, Inherited = false)]
    public class BshoxContractAttribute : System.Attribute
    {
        public BshoxContractAttribute();
        public bool ImplicitDefaultValues { get; set; }
        public bool ImplicitMembers { get; set; }
    }
    [System.AttributeUsage(System.AttributeTargets.Class, AllowMultiple = true, Inherited = false)]
    public sealed class BshoxDefaultContractAttribute : System.Attribute
    {
        public BshoxDefaultContractAttribute(System.Type containingType, string symbolName);
        public System.Type ContainingType { get; }
        public string SymbolName { get; }
    }
    [System.AttributeUsage(System.AttributeTargets.Property | System.AttributeTargets.Field, AllowMultiple = false, Inherited = true)]
    public sealed class BshoxMemberAttribute : System.Attribute
    {
        public BshoxMemberAttribute([System.Diagnostics.CodeAnalysis.ConstantExpected(Min = 1u, Max = 536870911u)] uint key);
        public uint Key { get; }
    }
    [System.AttributeUsage(System.AttributeTargets.Class, AllowMultiple = false, Inherited = false)]
    public sealed class BshoxSerializerAttribute : System.Attribute
    {
        public BshoxSerializerAttribute(params System.Type[] types);
        public System.Type[] Surrogates { get; set; }
        public System.Type[] Types { get; }
    }
    [System.AttributeUsage(System.AttributeTargets.Class | System.AttributeTargets.Struct, AllowMultiple = false, Inherited = false)]
    public sealed class BshoxSurrogateAttribute<T> : Bshox.Attributes.BshoxSurrogateAttribute
    {
        public BshoxSurrogateAttribute();
    }
    [System.AttributeUsage(System.AttributeTargets.Class | System.AttributeTargets.Struct, AllowMultiple = false, Inherited = false)]
    public class BshoxSurrogateAttribute : Bshox.Attributes.BshoxContractAttribute
    {
        public BshoxSurrogateAttribute(System.Type type);
        public System.Type Type { get; }
    }
}
namespace Bshox.Generator
{
    [Microsoft.CodeAnalysis.Generator("C#", new string[] { })]
    public class BshoxGenerator : Microsoft.CodeAnalysis.IIncrementalGenerator
    {
        public BshoxGenerator();
        public void Initialize(Microsoft.CodeAnalysis.IncrementalGeneratorInitializationContext context);
    }
    public static class Diagnostics
    {
        public static readonly Microsoft.CodeAnalysis.DiagnosticDescriptor TypeMustBePartial;
        public static readonly Microsoft.CodeAnalysis.DiagnosticDescriptor TypeMustNotBeNested;
        public static readonly Microsoft.CodeAnalysis.DiagnosticDescriptor SerializerMustHaveAtLeastOneType;
        public static readonly Microsoft.CodeAnalysis.DiagnosticDescriptor LangVersionMustBe12OrHigher;
        public static readonly Microsoft.CodeAnalysis.DiagnosticDescriptor MemberMustHaveExplicitKey;
        public static readonly Microsoft.CodeAnalysis.DiagnosticDescriptor KeyMustBeUnique;
        public static readonly Microsoft.CodeAnalysis.DiagnosticDescriptor ImplicitMemberMustNotHaveKey;
        public static readonly Microsoft.CodeAnalysis.DiagnosticDescriptor KeyMustBeInValidRange;
        public static readonly Microsoft.CodeAnalysis.DiagnosticDescriptor DefaultValueMustHave1Argument;
        public static readonly Microsoft.CodeAnalysis.DiagnosticDescriptor TypeNotSerializable;
        public static readonly Microsoft.CodeAnalysis.DiagnosticDescriptor SurrogateShouldHaveSuffix;
        public static readonly Microsoft.CodeAnalysis.DiagnosticDescriptor SurrogateMustHaveCorrectConstructor;
        public static readonly Microsoft.CodeAnalysis.DiagnosticDescriptor SurrogateMustHaveCorrectConvertMethod;
        public static readonly Microsoft.CodeAnalysis.DiagnosticDescriptor RoslynVersionIsTooOld;
        public static readonly Microsoft.CodeAnalysis.DiagnosticDescriptor DepthLockNotUsedCorrectly;
        public static readonly Microsoft.CodeAnalysis.DiagnosticDescriptor SurrogateTypeMustHaveAttribute;
        public static readonly Microsoft.CodeAnalysis.DiagnosticDescriptor ContractSymbolNotUnique;
    }
    [Microsoft.CodeAnalysis.Diagnostics.DiagnosticAnalyzer("C#", new string[] { })]
    public sealed class UseDepthLockCorrectly : Microsoft.CodeAnalysis.Diagnostics.DiagnosticAnalyzer
    {
        public UseDepthLockCorrectly();
        public override System.Collections.Immutable.ImmutableArray<Microsoft.CodeAnalysis.DiagnosticDescriptor> SupportedDiagnostics { get; }
        public override void Initialize(Microsoft.CodeAnalysis.Diagnostics.AnalysisContext context);
    }
}
