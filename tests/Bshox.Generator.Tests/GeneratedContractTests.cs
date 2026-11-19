namespace Bshox.Generator.Tests;

public class GeneratedContractTests
{
    [Test]
    [Arguments(true)]
    [Arguments(false)]
    public async Task NoMembers(bool implicitMembers)
    {
        string sourceCode = $$"""
                            using Bshox.Attributes;
                            namespace TestModels;

                            [BshoxSerializable<Type1>]
                            public partial class Serializer1;

                            [BshoxContract(ImplicitMembers = {{implicitMembers.ToString().ToLower()}})]
                            public record Type1
                            {

                            }
                            """;
        var generatedOutput = Utils.GetGeneratedOutput(sourceCode, out var diagnostics);

        await Assert.That(diagnostics).IsEmpty(); // TODO: fix. There should be a warning here
        await Assert.That(generatedOutput).IsNotEmpty();
    }

    [Test]
    public async Task RecursiveType()
    {
        const string sourceCode = """
                                  using Bshox.Attributes;
                                  namespace TestModels;

                                  [BshoxSerializable<RecursiveTestType>]
                                  public partial class RecursiveTestTypeSerializer;

                                  [BshoxContract(ImplicitMembers = true)]
                                  public class RecursiveTestType
                                  {
                                      public RecursiveTestType? Value1 { get; set; }
                                  }
                                  """;
        var generatedOutput = Utils.GetGeneratedOutput(sourceCode, out var diagnostics);

        await Assert.That(diagnostics).IsEmpty();
        await Utils.ValidateOutput(generatedOutput, 2);
    }

    [Test]
    [Arguments("public", true)]
    [Arguments("internal", true)]
    [Arguments("protected internal", true)]
    [Arguments("private", false)]
    [Arguments("protected", false)]
    [Arguments("private protected", false)]
    public async Task MemberAccess(string access, bool valid)
    {
        string sourceCode = $$"""
                            using Bshox.Attributes;
                            namespace TestModels;

                            [BshoxSerializable<Type1>]
                            public partial class Serializer1;

                            [BshoxContract(ImplicitMembers = false)]
                            public record Type1
                            {
                                [BshoxMember(1)]
                                {{access}} int Value { get; set; }
                            }
                            """;
        var generatedOutput = Utils.GetGeneratedOutput(sourceCode, out var diagnostics);

        if (valid)
        {
            await Assert.That(diagnostics).IsEmpty();
            await Assert.That(generatedOutput).IsNotEmpty();
        }
        else
        {
            await Assert.That(diagnostics).HasSingleItem();
            await diagnostics.Single().AssertEqual(Diagnostics.MemberMustBeAccessibleAndWritable, "Member 'TestModels.Type1.Value' must be an accessible instance field or an accessible instance property with a getter and setter");
            await Assert.That(generatedOutput).IsEmpty();
        }
    }

    [Test]
    [Arguments("public int Value { get; }")]
    [Arguments("public readonly int Value;")]
    [Arguments("public int Value { private get; set; }")]
    [Arguments("public int Value { get; private set; }")]
    public async Task MemberMustBeReadableAndWritable(string member)
    {
        string sourceCode = $$"""
                            using Bshox.Attributes;
                            namespace TestModels;

                            [BshoxSerializable<Type1>]
                            public partial class Serializer1;

                            [BshoxContract(ImplicitMembers = true)]
                            public class Type1
                            {
                                {{member}}
                            }
                            """;
        var generatedOutput = Utils.GetGeneratedOutput(sourceCode, out var diagnostics);

        await Assert.That(diagnostics).HasSingleItem();
        await diagnostics.Single().AssertEqual(Diagnostics.MemberMustBeAccessibleAndWritable, "Member 'TestModels.Type1.Value' must be an accessible instance field or an accessible instance property with a getter and setter");
        await Assert.That(generatedOutput).IsEmpty();
    }

    [Test]
    public async Task KeywordMemberName()
    {
        const string sourceCode = """
                                  using Bshox.Attributes;
                                  namespace TestModels;

                                  [BshoxSerializable<Type1>]
                                  public partial class Serializer1;

                                  [BshoxContract(ImplicitMembers = true)]
                                  public class Type1
                                  {
                                      public int @class { get; set; }
                                  }
                                  """;
        var generatedOutput = Utils.GetGeneratedOutput(sourceCode, out var diagnostics);

        await Assert.That(diagnostics).IsEmpty();
        await Assert.That(generatedOutput).IsNotEmpty();
    }

    [Test]
    public async Task NullableValueTypes()
    {
        const string sourceCode = """
                                  using System.ComponentModel;
                                  using Bshox.Attributes;
                                  namespace TestModels;

                                  [BshoxSerializable<TestType1>]
                                  public partial class Serializer1;

                                  [BshoxContract(ImplicitMembers = true)]
                                  public record TestType1
                                  {
                                      public int? Value1 { get; set; }

                                      [DefaultValue(42)]
                                      public int? Value2 { get; set; }

                                      [DefaultValue(null)]
                                      public int? Value3 { get; set; }
                                  }
                                  """;
        var generatedOutput = Utils.GetGeneratedOutput(sourceCode, out var diagnostics);

        await Assert.That(diagnostics).IsEmpty();
        await Utils.ValidateOutput(generatedOutput, 2);
    }

    [Test]
    public async Task DefaultValues()
    {
        const string sourceCode = """
        using System.ComponentModel;
        using Bshox.Attributes;
        namespace TestModels;

        public enum MyEnum
        {
            EnumValue1
        }

        [BshoxSerializable<TestType1>]
        public partial class Serializer1;

        [BshoxContract(ImplicitMembers = true)]
        public record TestType1
        {
            [DefaultValue(42)]
            public int Value1 { get; set; }

            [DefaultValue(MyEnum.EnumValue1)]
            public MyEnum Value2 { get; set; }

            [DefaultValue(null)]
            public MyEnum? Value3 { get; set; }

            [DefaultValue(null)]
            public string? Value4 { get; set; }

            [DefaultValue("Hello, World!")]
            public string? Value5 { get; set; }

            [DefaultValue(-3.14)]
            public decimal Value6 { get; set; }
        }
        """;
        var generatedOutput = Utils.GetGeneratedOutput(sourceCode, out var diagnostics);

        await Assert.That(diagnostics).IsEmpty();
        await Utils.ValidateOutput(generatedOutput, 2);
    }
}
