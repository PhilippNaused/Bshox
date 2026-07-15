using System.Collections.Immutable;
using System.Diagnostics.CodeAnalysis;
using System.Text.Json.SourceGeneration;
using BenchmarkDotNet.Attributes;
using Bshox.Generator;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;

namespace Generator.Benchmark;

#pragma warning disable CA1051 // Do not declare visible instance fields
public class GenerateBase
{
    [StringSyntax("C#")]
    protected const string BaseCode = """
                                    using Bshox.Attributes;

                                    namespace TestModels;

                                    [BshoxContract(ImplicitMembers = true)]
                                    public record TestType2
                                    {
                                        public int Value1 { get; set; }
                                        public string Value2 { get; set; }
                                    }
                                    """;

    [StringSyntax("C#")]
    protected const string BshoxEncoding = $$"""
                                        using System.Collections.Generic;
                                        {{BaseCode}}

                                        [BshoxSerializable<List<TestType2>>]
                                        public partial class MyBshoxSerializer;
                                        """;

    [StringSyntax("C#")]
    protected const string JsonCode = $$"""
                                        using System.Collections.Generic;
                                        using System.Text.Json.Serialization;

                                        {{BaseCode}}

                                        [JsonSerializable(typeof(List<TestType2>))]
                                        public partial class MyJsonContext : JsonSerializerContext;
                                        """;

    protected static readonly CSharpParseOptions parseOptions = new(LanguageVersion.Latest);
    protected readonly CSharpGeneratorDriver baseDriver = CSharpGeneratorDriver.Create([], null, parseOptions);

    protected readonly CSharpGeneratorDriver bshoxDriver = CSharpGeneratorDriver.Create([new BshoxGenerator().AsSourceGenerator()], null, parseOptions);
    protected readonly CSharpGeneratorDriver jsonDriver = CSharpGeneratorDriver.Create([new JsonSourceGenerator().AsSourceGenerator()], null, parseOptions);

    protected readonly CSharpCompilationOptions options = new CSharpCompilationOptions(OutputKind.DynamicallyLinkedLibrary).WithConcurrentBuild(false).WithDeterministic(true);
}

[Config(typeof(ColdConfig))]
public class GenerateCold : Generate;

[MemoryDiagnoser]
[Config(typeof(MediumConfig))]
public class Generate : GenerateBase
{
    private (List<SyntaxTree>, ImmutableArray<Diagnostic>) Build(string code, CSharpGeneratorDriver driver)
    {
        var compilation = Utils.GetCompilation(code, options, parseOptions);
        _ = driver.RunGeneratorsAndUpdateCompilation(compilation, out var outputCompilation, out var diags);
        diags = diags.AddRange(outputCompilation.GetDiagnostics()).Distinct().ToImmutableArray();
        return (outputCompilation.SyntaxTrees.ToList(), diags);
    }

    [Benchmark(Baseline = true)]
    public (List<SyntaxTree>, ImmutableArray<Diagnostic>) Base() => Build(BaseCode, baseDriver);

    [Benchmark]
    public (List<SyntaxTree>, ImmutableArray<Diagnostic>) BshoxGenerator() => Build(BshoxEncoding, bshoxDriver);

    [Benchmark]
    public (List<SyntaxTree>, ImmutableArray<Diagnostic>) JsonGenerator() => Build(JsonCode, jsonDriver);
}

[MemoryDiagnoser]
[Config(typeof(MediumConfig))]
public class GenerateIncremental : GenerateBase
{
    private CSharpCompilation BaseCompilation;
    private CSharpCompilation JsonCompilation;
    private CSharpCompilation BshoxCompilation;

    public GenerateIncremental()
    {
        BaseCompilation = Create(BaseCode);
        JsonCompilation = Create(JsonCode);
        BshoxCompilation = Create(BshoxEncoding);
    }

    private CSharpCompilation Create(string code) => Utils.GetCompilation(code, options, parseOptions);

    private (IEnumerable<SyntaxTree>, ImmutableArray<Diagnostic>) Build(string code, ref CSharpCompilation compilation, CSharpGeneratorDriver driver)
    {
        var newSyntaxTree = CSharpSyntaxTree.ParseText(code, parseOptions);
        compilation = compilation.ReplaceSyntaxTree(compilation.SyntaxTrees.Single(), newSyntaxTree);
        _ = driver.RunGeneratorsAndUpdateCompilation(compilation, out var outputCompilation, out var diags);
        diags = [.. diags.AddRange(outputCompilation.GetDiagnostics()).Distinct()];
        return (outputCompilation.SyntaxTrees, diags);
    }

    private static readonly Random rand = new();

    /// <summary>
    /// Adds random comments to the code to force a change in the syntax tree.
    /// </summary>
    private static string UpdateCode(string code)
    {
        return $"""
                // {rand.Next()}
                {code}
                // {rand.Next()}
                """;
    }

    [Benchmark(Baseline = true)]
    public (IEnumerable<SyntaxTree>, ImmutableArray<Diagnostic>) Base() => Build(UpdateCode(BaseCode), ref BaseCompilation, baseDriver);

    [Benchmark]
    public (IEnumerable<SyntaxTree>, ImmutableArray<Diagnostic>) Json() => Build(UpdateCode(JsonCode), ref JsonCompilation, jsonDriver);

    [Benchmark]
    public (IEnumerable<SyntaxTree>, ImmutableArray<Diagnostic>) Bshox() => Build(UpdateCode(BshoxEncoding), ref BshoxCompilation, bshoxDriver);
}
