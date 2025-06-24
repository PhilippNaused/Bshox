using System.Reflection;
using System.Runtime.Versioning;
using Bshox;
using Bshox.Generator;
using Bshox.Meta;
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
    public Task BshoxMeta()
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

    private static Task Validate(Assembly assembly)
    {
        var API = assembly.Decompile();
        return Validate(GetFramework(assembly), API);
    }

    private static string GetFramework(Assembly assembly)
    {
#pragma warning disable CA2263 // Prefer generic overload when type is known
        return ((TargetFrameworkAttribute)assembly.GetCustomAttribute(typeof(TargetFrameworkAttribute))!).FrameworkDisplayName!;
#pragma warning restore CA2263 // Prefer generic overload when type is known
    }

    private static Task Validate(string frameworkName, string publicApi)
    {
        return Validation.Validate(publicApi, "cs", frameworkName);
    }
}
