using System.Collections.Immutable;
using Bshox.Generator.Extensions;
using Microsoft.CodeAnalysis.CSharp;

namespace Bshox.Generator.Tests;

public class DiagnosticTests
{
    [Test]
    public async Task Test1()
    {
        const string sourceCode = """
                                  using Bshox.Attributes;
                                  namespace TestModels;

                                  [BshoxSerializer(typeof(Type1))]
                                  public partial class Serializer1;

                                  [BshoxContract(ImplicitMembers = true)]
                                  public record Type1
                                  {
                                      public int Value { get; set; }
                                  }
                                  """;

        await Utils.RunTest(sourceCode, async (ctx, types, @class, symbol) =>
        {
            await Assert.That(types).IsNotNull();
            await Assert.That(@class).IsNotNull();
            await Assert.That(symbol).IsNotNull();
            await Assert.That(symbol.FullyQualifiedToStringNG()).IsEqualTo("TestModels.Serializer1");
        });

        var generatedOutput = Utils.GetGeneratedOutput(sourceCode, out var diagnostics);

        await Assert.That(diagnostics).IsEmpty();
        await Assert.That(generatedOutput).IsNotEmpty();
    }

    [Test]
    public async Task Test2()
    {
        const string sourceCode = """
                                  using Bshox.Attributes;
                                  namespace TestModels;

                                  [BshoxSerializer(typeof(Type1))]
                                  public partial class Serializer1;

                                  [BshoxContract]
                                  public record Type1
                                  {
                                      [BshoxMember(1)]
                                      public int Value { get; set; }
                                  }
                                  """;
        var generatedOutput = Utils.GetGeneratedOutput(sourceCode, out var diagnostics);

        await Assert.That(diagnostics).IsEmpty();
        await Assert.That(generatedOutput).IsNotEmpty();
    }

    [Test]
    public async Task SerializerMustBePartial()
    {
        const string sourceCode = """
                                  using Bshox.Attributes;
                                  namespace TestModels;

                                  [BshoxSerializer(typeof(Type1))]
                                  public class Serializer1;

                                  [BshoxContract]
                                  public record Type1
                                  {
                                      [BshoxMember(1)]
                                      public int Value { get; set; }
                                  }
                                  """;

        var generatedOutput = Utils.GetGeneratedOutput(sourceCode, out var diagnostics);

        await Assert.That(diagnostics).HasSingleItem();
        var diagnostic = diagnostics.Single();
        await diagnostic.AssertEqual(Diagnostics.TypeMustBePartial, "Type 'Serializer1' must be partial to use [BshoxSerializerAttribute]");
        await Assert.That(generatedOutput).IsEmpty();
    }

    [Test]
    public async Task TypeCanBeNested()
    {
        const string sourceCode = """
                                  using Bshox.Attributes;
                                  namespace TestModels;

                                  [BshoxSerializer(typeof(Type2.Type1))]
                                  public partial class Serializer1;

                                  public class Type2
                                  {
                                      [BshoxContract]
                                      public record Type1
                                      {
                                          [BshoxMember(1)]
                                          public int Value { get; set; }
                                      }
                                  }
                                  """;

        var generatedOutput = Utils.GetGeneratedOutput(sourceCode, out var diagnostics);

        await Assert.That(diagnostics).IsEmpty();
        await Assert.That(generatedOutput).IsNotEmpty();
    }

    [Test]
    public async Task SerializerMustHaveAtLeastOneType()
    {
        const string sourceCode = """
                                  using Bshox.Attributes;
                                  namespace TestModels;

                                  [BshoxSerializer]
                                  public partial class Serializer1;
                                  """;

        var generatedOutput = Utils.GetGeneratedOutput(sourceCode, out var diagnostics);

        await Assert.That(diagnostics).HasSingleItem();
        var diagnostic = diagnostics.Single();
        await diagnostic.AssertEqual(Diagnostics.SerializerMustHaveAtLeastOneType, "The generated type 'TestModels.Serializer1' must have at least one serializable type");
        await Assert.That(generatedOutput).IsEmpty();
    }

    [Test]
    public async Task TypeNotSerializable_MissingAttribute()
    {
        const string sourceCode = """
                                  using Bshox.Attributes;
                                  namespace TestModels;

                                  [BshoxSerializer(typeof(Type1))]
                                  public partial class Serializer1;

                                  public record struct Type1(int Value);
                                  """;

        var generatedOutput = Utils.GetGeneratedOutput(sourceCode, out var diagnostics);

        await Assert.That(diagnostics).HasSingleItem();
        var diagnostic = diagnostics.Single();
        await diagnostic.AssertEqual(Diagnostics.TypeNotSerializable, "Type 'TestModels.Type1' is not serializable");
        await Assert.That(generatedOutput).IsEmpty();
    }

    [Test]
    public async Task TypeNotSerializable_UnboundGeneric()
    {
        const string sourceCode = """
                                  using Bshox.Attributes;
                                  namespace TestModels;

                                  [BshoxSerializer(typeof(Type1<>))]
                                  public partial class Serializer1;

                                  public record struct Type1<T>(int Value);
                                  """;

        var generatedOutput = Utils.GetGeneratedOutput(sourceCode, out var diagnostics);

        await Assert.That(diagnostics).HasSingleItem();
        var diagnostic = diagnostics.Single();
        await diagnostic.AssertEqual(Diagnostics.TypeNotSerializable, "Type 'TestModels.Type1<>' is not serializable");
        await Assert.That(generatedOutput).IsEmpty();
    }

    public static IEnumerable<LanguageVersion> GetLanguageVersions()
    {
#if NETFRAMEWORK
        var all = Enum.GetNames(typeof(LanguageVersion)).Select(static name => (LanguageVersion)Enum.Parse(typeof(LanguageVersion), name));
#else
        var all = Enum.GetValues<LanguageVersion>();
#endif
        // C# 1 doesn't support partial classes, so we get a different error
        return all.Where(version => version > LanguageVersion.CSharp1).Select(version => version.MapSpecifiedToEffectiveVersion());
    }

    [Test]
    [MethodDataSource(nameof(GetLanguageVersions))]
    public async Task LangVersionMustBe12OrHigher(LanguageVersion version)
    {
        const string sourceCode = """
                                  using Bshox.Attributes;
                                  namespace TestModels
                                  {
                                      [BshoxSerializer(typeof(Type1))]
                                      public partial class Serializer1 { }

                                      [BshoxContract]
                                      public class Type1
                                      {
                                          [BshoxMember(1)]
                                          public int Value;
                                      }
                                  }
                                  """;

        var generatedOutput = Utils.GetGeneratedOutput(sourceCode, out var diagnostics, null, new CSharpParseOptions(version));

        if (version >= LanguageVersion.CSharp12)
        {
            await Assert.That(diagnostics).IsEmpty();
            await Assert.That(generatedOutput).IsNotEmpty();
            return;
        }

        await Assert.That(diagnostics).HasSingleItem();
        var diagnostic = diagnostics.Single();
        await diagnostic.AssertEqual(Diagnostics.LangVersionMustBe12OrHigher, $"Bshox code generation is not available in C# {version.ToDisplayString()}. Please use C# 12.0 or later.");
        await Assert.That(generatedOutput).IsEmpty();
    }

    [Test]
    public async Task KeyMustBeUnique()
    {
        const string sourceCode = """
                                  using Bshox.Attributes;
                                  namespace TestModels;

                                  [BshoxSerializer(typeof(Type1))]
                                  public partial class Serializer1;

                                  [BshoxContract]
                                  public record Type1
                                  {
                                      [BshoxMember(1)]
                                      public int Value1 { get; set; }

                                      [BshoxMember(1)]
                                      public int Value2 { get; set; }
                                  }
                                  """;
        var generatedOutput = Utils.GetGeneratedOutput(sourceCode, out var diagnostics);

        await Assert.That(diagnostics).Count().EqualTo(2);
        diagnostics = diagnostics.OrderBy(d => d.Location.SourceSpan.Start).ToImmutableArray();
        for (int i = 0; i < diagnostics.Length; i++)
        {
            await diagnostics[i].AssertEqual(Diagnostics.KeyMustBeUnique, $"Member 'Value{i + 1}' has a duplicate key '1'");
        }
        await Assert.That(generatedOutput).IsEmpty();
    }

    [Test]
    public async Task ImplicitMemberMustNotHaveKey()
    {
        const string sourceCode = """
                                  using Bshox.Attributes;
                                  namespace TestModels;

                                  [BshoxSerializer(typeof(Type1))]
                                  public partial class Serializer1;

                                  [BshoxContract(ImplicitMembers = true)]
                                  public record Type1
                                  {
                                      [BshoxMember(1)]
                                      public int Value { get; set; }
                                  }
                                  """;
        var generatedOutput = Utils.GetGeneratedOutput(sourceCode, out var diagnostics);

        await Assert.That(diagnostics).HasSingleItem();
        var diagnostic = diagnostics.Single();
        await diagnostic.AssertEqual(Diagnostics.ImplicitMemberMustNotHaveKey, "Member 'TestModels.Type1.Value' must not have a [BshoxMember] attribute when using implicit members");
        await Assert.That(generatedOutput).IsEmpty();
    }

    [Test]
    [Arguments(0u, false)]
    [Arguments(1u, true)]
    [Arguments(uint.MaxValue >> 3, true)]
    [Arguments((uint.MaxValue >> 3) + 1, false)]
    [Arguments(uint.MaxValue, false)]
    public async Task KeyMustBeInValidRange(uint key, bool valid)
    {
        string sourceCode = $$"""
                              using Bshox.Attributes;
                              namespace TestModels;

                              [BshoxSerializer(typeof(Type1))]
                              public partial class Serializer1;

                              [BshoxContract]
                              public record Type1
                              {
                                  [BshoxMember({{key}})]
                                  public int Value { get; set; }
                              }
                              """;
        var generatedOutput = Utils.GetGeneratedOutput(sourceCode, out var diagnostics);

        if (valid)
        {
            await Assert.That(diagnostics).IsEmpty();
            await Assert.That(generatedOutput).IsNotEmpty();
            return;
        }
        await Assert.That(diagnostics).HasSingleItem();
        var diagnostic = diagnostics.Single();
        await diagnostic.AssertEqual(Diagnostics.KeyMustBeInValidRange, $"Member 'TestModels.Type1.Value' has an invalid key '{key}'. Keys must be values between 1 and 536870911.");
        await Assert.That(generatedOutput).IsEmpty();
    }

    [Test]
    public async Task DefaultValueMustHave1Argument()
    {
        const string sourceCode = """
                                  using System.ComponentModel;
                                  using Bshox.Attributes;
                                  namespace TestModels;

                                  [BshoxSerializer(typeof(Type1))]
                                  public partial class Serializer1;

                                  [BshoxContract]
                                  public record Type1
                                  {
                                      [BshoxMember(1)]
                                      [DefaultValue(typeof(int), "42")]
                                      public int Value { get; set; }
                                  }
                                  """;
        var generatedOutput = Utils.GetGeneratedOutput(sourceCode, out var diagnostics);

        await Assert.That(diagnostics).HasSingleItem();
        var diagnostic = diagnostics.Single();
        await diagnostic.AssertEqual(Diagnostics.DefaultValueMustHave1Argument, "The DefaultValueAttribute on 'TestModels.Type1.Value' must have exactly one constructor argument");
        await Assert.That(generatedOutput).IsNotEmpty();
    }
}
