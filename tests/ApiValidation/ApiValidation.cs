using System.Reflection;
using System.Runtime.Versioning;
using Bshox;
using Bshox.Generator;
using Bshox.Utils;
using ICSharpCode.Decompiler.Metadata;
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
        var (identifier, version) = ParseTargetFramework(attribute?.FrameworkName ?? string.Empty);
        return GetFtm(identifier, version);
    }

    private static string? GetFtm(TargetFrameworkIdentifier type, Version version)
    {
        // get framework moniker like "net48", "net6.0", "netstandard2.0", etc.
        return type switch
        {
            TargetFrameworkIdentifier.NETFramework => version.Build > 0
                ? $"net{version.Major}{version.Minor}{version.Build}" // e.g. net462
                : $"net{version.Major}{version.Minor}", // e.g. net48
            TargetFrameworkIdentifier.NETCoreApp => $"netcoreapp{version.Major}.{version.Minor}", // e.g. netcoreapp3.1
            TargetFrameworkIdentifier.NETStandard => $"netstandard{version.Major}.{version.Minor}", // e.g. netstandard2.0
            TargetFrameworkIdentifier.NET => $"net{version.Major}.{version.Minor}", // e.g. net8.0
            _ => "unknown",
        };
    }

    private static Task Validate(string frameworkName, string publicApi)
    {
        return Validation.Validate(publicApi, "cs", frameworkName);
    }

    private static readonly Version ZeroVersion = new(0, 0, 0, 0);

    /// <summary>
    /// <see href="https://github.com/icsharpcode/ILSpy/blob/1cfc5e740b6b8f1d0e424161d808e3bc94ee5276/ICSharpCode.Decompiler/Metadata/UniversalAssemblyResolver.cs#L157"/>
    /// </summary>
    private static (TargetFrameworkIdentifier, Version) ParseTargetFramework(string targetFramework)
    {
        if (string.IsNullOrEmpty(targetFramework))
            return (TargetFrameworkIdentifier.NETFramework, ZeroVersion);
        string[] tokens = targetFramework.Split(',');
        var identifier = tokens[0].Trim().ToUpperInvariant() switch
        {
            ".NETCOREAPP" => TargetFrameworkIdentifier.NETCoreApp,
            ".NETFRAMEWORK" => TargetFrameworkIdentifier.NETFramework,
            ".NETSTANDARD" => TargetFrameworkIdentifier.NETStandard,
            _ => throw new NotSupportedException($"Unsupported target framework: {tokens[0].Trim()}"),
        };
        Version? version = null;

        for (int i = 1; i < tokens.Length; i++)
        {
            var pair = tokens[i].Trim().Split('=');

            if (pair.Length != 2)
                continue;

            switch (pair[0].Trim().ToUpperInvariant())
            {
                case "VERSION":
                    var versionString = pair[1].TrimStart('v', 'V', ' ', '\t');

                    if (!Version.TryParse(versionString, out version))
                    {
                        version = null;
                    }
                    else
                    {
                        version = new Version(version.Major, version.Minor, version.Build < 0 ? 0 : version.Build);
                    }
                    // .NET 5 or greater still use ".NETCOREAPP" as TargetFrameworkAttribute value...
                    if (version?.Major >= 5 && identifier == TargetFrameworkIdentifier.NETCoreApp)
                        identifier = TargetFrameworkIdentifier.NET;
                    break;
            }
        }

        return (identifier, version ?? ZeroVersion);
    }
}
