namespace Bshox.Generator.Tests;

public class CustomContractTests
{
    [Test]
    public async Task DefaultContractsFromProperty()
    {
        const string sourceCode = """
                                  using Bshox;
                                  using Bshox.Attributes;
                                  namespace TestModels;

                                  [BshoxSerializer(typeof(int))]
                                  [BshoxDefaultContract(typeof(DefaultContracts), nameof(DefaultContracts.Int32Z))]
                                  public partial class CustomContracts1;
                                  """;
        var generatedOutput = Utils.GetGeneratedOutput(sourceCode, out var diagnostics);

        await Assert.That(diagnostics).IsEmpty();
        await Utils.ValidateOutput(generatedOutput, 1);
    }

    [Test]
    public async Task CustomContractFromProperty()
    {
        const string sourceCode = """
                                  using Bshox;
                                  using Bshox.Attributes;
                                  namespace TestModels;

                                  internal class Test1
                                  {
                                      public static BshoxContract<int> Contract1 => null;
                                  }

                                  [BshoxSerializer(typeof(int))]
                                  [BshoxDefaultContract(typeof(Test1), "Contract1")]
                                  public partial class CustomContracts1;
                                  """;
        var generatedOutput = Utils.GetGeneratedOutput(sourceCode, out var diagnostics);

        await Assert.That(diagnostics).IsEmpty();
        await Utils.ValidateOutput(generatedOutput, 1);
    }

    [Test]
    public async Task CustomContractFromMethod()
    {
        const string sourceCode = """
                                  using Bshox;
                                  using Bshox.Attributes;
                                  namespace TestModels;

                                  internal class Test1
                                  {
                                      public static BshoxContract<int> Contract1() => null;
                                  }

                                  [BshoxSerializer(typeof(int))]
                                  [BshoxDefaultContract(typeof(Test1), "Contract1")]
                                  public partial class CustomContracts1;
                                  """;
        var generatedOutput = Utils.GetGeneratedOutput(sourceCode, out var diagnostics);

        await Assert.That(diagnostics).IsEmpty();
        await Utils.ValidateOutput(generatedOutput, 1);
    }

    [Test]
    public async Task CustomContractFromMethodWithParameter()
    {
        const string sourceCode = """
                                  using Bshox;
                                  using Bshox.Attributes;
                                  namespace TestModels;

                                  internal class Test1
                                  {
                                      public static BshoxContract<int> Contract1(BshoxContract<long> longContract) => null;
                                  }

                                  [BshoxSerializer(typeof(int))]
                                  [BshoxDefaultContract(typeof(Test1), "Contract1")]
                                  public partial class CustomContracts1;
                                  """;
        var generatedOutput = Utils.GetGeneratedOutput(sourceCode, out var diagnostics);

        await Assert.That(diagnostics).IsEmpty();
        await Utils.ValidateOutput(generatedOutput, 1);
    }

    [Test]
    public async Task CustomGenericContractFromMethod()
    {
        const string sourceCode = """
                                  using Bshox;
                                  using Bshox.Attributes;
                                  namespace TestModels;

                                  internal class Test1<T>
                                  {
                                      public static BshoxContract<T> Contract1() => null;
                                  }

                                  [BshoxSerializer(typeof(int))]
                                  [BshoxDefaultContract(typeof(Test1<int>), "Contract1")]
                                  public partial class CustomContracts1;
                                  """;
        var generatedOutput = Utils.GetGeneratedOutput(sourceCode, out var diagnostics);

        await Assert.That(diagnostics).IsEmpty();
        await Utils.ValidateOutput(generatedOutput, 1);
    }

    [Test]
    public async Task CustomGenericContractFromMethodWithParameter()
    {
        const string sourceCode = """
                                  using Bshox;
                                  using Bshox.Attributes;
                                  namespace TestModels;

                                  internal class Test1<T>
                                  {
                                      public static BshoxContract<T> Contract1(BshoxContract<long> longContract) => null;
                                  }

                                  [BshoxSerializer(typeof(int))]
                                  [BshoxDefaultContract(typeof(Test1<int>), "Contract1")]
                                  public partial class CustomContracts1;
                                  """;
        var generatedOutput = Utils.GetGeneratedOutput(sourceCode, out var diagnostics);

        await Assert.That(diagnostics).IsEmpty();
        await Utils.ValidateOutput(generatedOutput, 1);
    }

    [Test]
    public async Task CustomGenericContractFromMethodWithParameter2()
    {
        const string sourceCode = """
                                  using Bshox;
                                  using Bshox.Attributes;
                                  namespace TestModels;

                                  internal class Test1<T>
                                  {
                                      public static BshoxContract<T> Contract1(BshoxContract<T> tContract) => null;
                                  }

                                  [BshoxSerializer(typeof(int))]
                                  [BshoxDefaultContract(typeof(Test1<int>), "Contract1")]
                                  public partial class CustomContracts1;
                                  """;
        var generatedOutput = Utils.GetGeneratedOutput(sourceCode, out var diagnostics);

        await Assert.That(diagnostics).HasCount().EqualToOne();
        await Assert.That(diagnostics.Single().GetMessage()).IsEqualTo("Internal Error: The static type dependencies are cyclical: int");
        await Assert.That(generatedOutput).IsEmpty();
    }

    [Test]
    [Explicit] // not implemented yet
    public async Task CustomContractFromGenericMethod()
    {
        const string sourceCode = """
                                  using Bshox;
                                  using Bshox.Attributes;
                                  namespace TestModels;

                                  internal class Test1
                                  {
                                      public static BshoxContract<T> Contract1<T>() => null;
                                  }

                                  [BshoxSerializer(typeof(int))]
                                  [BshoxDefaultContract(typeof(Test1), "Contract1")]
                                  public partial class CustomContracts1;
                                  """;
        var generatedOutput = Utils.GetGeneratedOutput(sourceCode, out var diagnostics);

        await Assert.That(diagnostics).IsEmpty();
        await Utils.ValidateOutput(generatedOutput, 1);
    }

    [Test]
    public async Task CustomContractGenericProperty()
    {
        const string sourceCode = """
                                  using Bshox;
                                  using Bshox.Attributes;
                                  using System.Collections.Generic;
                                  namespace TestModels;

                                  internal class Test1<T>
                                  {
                                      public static BshoxContract<List<T>> Contract1 => null;
                                  }

                                  [BshoxSerializer(typeof(List<int>))]
                                  [BshoxDefaultContract(typeof(Test1<int>), "Contract1")]
                                  public partial class CustomContracts1;
                                  """;
        var generatedOutput = Utils.GetGeneratedOutput(sourceCode, out var diagnostics);

        await Assert.That(diagnostics).IsEmpty();
        await Utils.ValidateOutput(generatedOutput, 1);
    }

    [Test]
    [Explicit] // not implemented yet
    public async Task CustomContractUnboundGeneric()
    {
        const string sourceCode = """
                                  using Bshox;
                                  using Bshox.Attributes;
                                  using System.Collections.Generic;
                                  namespace TestModels;

                                  internal class Test1<T>
                                  {
                                      public static BshoxContract<List<T>> Contract1 => null;
                                  }

                                  [BshoxSerializer(typeof(List<int>))]
                                  [BshoxDefaultContract(typeof(Test1<>), "Contract1")]
                                  public partial class CustomContracts1;
                                  """;
        var generatedOutput = Utils.GetGeneratedOutput(sourceCode, out var diagnostics);

        await Assert.That(diagnostics).IsEmpty();
        await Utils.ValidateOutput(generatedOutput, 1);
    }

    [Test]
    public async Task CustomContractFromField()
    {
        const string sourceCode = """
                                  using Bshox;
                                  using Bshox.Attributes;
                                  namespace TestModels;

                                  internal class Test1
                                  {
                                      public static BshoxContract<int> Contract1 = null;
                                  }

                                  [BshoxSerializer(typeof(int))]
                                  [BshoxDefaultContract(typeof(Test1), "Contract1")]
                                  public partial class CustomContracts1;
                                  """;
        var generatedOutput = Utils.GetGeneratedOutput(sourceCode, out var diagnostics);

        await Assert.That(diagnostics).IsEmpty();
        await Utils.ValidateOutput(generatedOutput, 1);
    }

    [Test]
    public async Task CustomContractSymbolNotStatic()
    {
        const string sourceCode = """
                                  using Bshox;
                                  using Bshox.Attributes;
                                  namespace TestModels;

                                  internal class Test1
                                  {
                                      public BshoxContract<int> Contract1 => null;
                                  }

                                  [BshoxSerializer(typeof(int))]
                                  [BshoxDefaultContract(typeof(Test1), "Contract1")]
                                  public partial class CustomContracts1;
                                  """;
        var generatedOutput = Utils.GetGeneratedOutput(sourceCode, out var diagnostics);

        await Assert.That(diagnostics).HasSingleItem();
        await Assert.That(generatedOutput).IsEmpty();
        await diagnostics.Single().AssertEqual(Diagnostics.ContractSymbolNotUnique, "Type 'TestModels.Test1' must have exactly one static member called 'Contract1'");
    }
}
