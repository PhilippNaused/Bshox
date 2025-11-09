using System.Reflection;
using System.Runtime.Versioning;
using Bshox;
using Bshox.Generator;
using Bshox.Utils;
using VeriGit;

namespace ApiValidation;

internal class ApiValidation
{
    [Test]
    public Task Bshox()
    {
        var assembly = typeof(BshoxWriter).Assembly;
        return Validate(assembly);
    }

    [Test]
    public Task BshoxUtils()
    {
        var assembly = typeof(BshoxTextParser).Assembly;
        return Validate(assembly);
    }

    [Test]
    public Task BshoxGenerator()
    {
        var assembly = typeof(BshoxGenerator).Assembly;
        return Validate(assembly);
    }

    [Test]
    public Task VeriGit()
    {
        var assembly = typeof(Validation).Assembly;
        return Validate(assembly);
    }

    private static Task Validate(Assembly assembly)
    {
        var API = assembly.Decompile();
        return Validate(GetFramework(assembly) ?? "unknown", API);
    }

    private static string? GetFramework(Assembly assembly)
    {
        TargetFrameworkAttribute? attribute = assembly.GetCustomAttribute<TargetFrameworkAttribute>();
        return attribute?.FrameworkDisplayName;
    }

    private static Task Validate(string frameworkName, string publicApi)
    {
        return Validation.Validate(publicApi, "cs", frameworkName);
    }
}
