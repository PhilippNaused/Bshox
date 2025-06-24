using System.Collections.Immutable;
using System.Text.Json.SourceGeneration;
using BenchmarkDotNet.Attributes;
using Bshox.Generator;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;

namespace Generator.Benchmark;

[Config(typeof(ColdConfig))]
public class GenerateCold : Generate;

[MemoryDiagnoser]
[Config(typeof(MediumConfig))]
public class Generate
{
    private const string BaseCode = """
                                    using Bshox.Attributes;

                                    namespace TestModels;

                                    [BshoxContract(ImplicitMembers = true)]
                                    public record TestType2
                                    {
                                        public int Value1 { get; set; }
                                        public string Value2 { get; set; }
                                    }
                                    """;

    private const string BshoxCode = $$"""
                                       using System.Collections.Generic;
                                       {{BaseCode}}

                                       [BshoxSerializer(typeof(List<TestType2>))]
                                       public partial class MyBshoxSerializer;
                                       """;

    private const string JsonCode = $$"""
                                      using System.Collections.Generic;
                                      using System.Text.Json.Serialization;

                                      {{BaseCode}}

                                      [JsonSerializable(typeof(List<TestType2>))]
                                      public partial class MyJsonContext : JsonSerializerContext;
                                      """;

    private static readonly CSharpParseOptions parseOptions = new(LanguageVersion.CSharp12);

    private readonly CSharpGeneratorDriver baseDriver = CSharpGeneratorDriver.Create([], null, parseOptions);

    private readonly CSharpGeneratorDriver bshoxDriver = CSharpGeneratorDriver.Create([new BshoxGenerator().AsSourceGenerator()], null, parseOptions);
    private readonly CSharpGeneratorDriver jsonDriver = CSharpGeneratorDriver.Create([new JsonSourceGenerator().AsSourceGenerator()], null, parseOptions);

    private readonly CSharpCompilationOptions options = new CSharpCompilationOptions(OutputKind.DynamicallyLinkedLibrary).WithConcurrentBuild(false).WithDeterministic(true);

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
    public (List<SyntaxTree>, ImmutableArray<Diagnostic>) BshoxGenerator() => Build(BshoxCode, bshoxDriver);

    [Benchmark]
    public (List<SyntaxTree>, ImmutableArray<Diagnostic>) JsonGenerator() => Build(JsonCode, jsonDriver);
}
