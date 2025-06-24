namespace Bshox.Generator.Tests;

public class DepthLockNotUsedCorrectlyTests
{
    [Test]
    public async Task BadRead1()
    {
        const string sourceCode = """
            using Bshox;
            namespace TestModels;

            public static class Type1
            {
                public static string Method1(ref BshoxReader reader)
                {
                    var _ = reader.DepthLock();
                    return reader.ReadString();
                }
            }
            """;
        _ = Utils.GetGeneratedOutput(sourceCode, out var diagnostics);

        await Assert.That(diagnostics).HasSingleItem();
        var diagnostic = diagnostics.Single();
        await diagnostic.AssertEqual(Diagnostics.DepthLockNotUsedCorrectly, Diagnostics.DepthLockNotUsedCorrectly.MessageFormat.ToString());
    }

    [Test]
    public async Task BadWrite1()
    {
        const string sourceCode = """
                                  using Bshox;
                                  namespace TestModels;

                                  public static class Type1
                                  {
                                      public static void Method1(ref BshoxWriter writer)
                                      {
                                          var _ = writer.DepthLock();
                                          writer.WriteString("Hello, World!");
                                      }
                                  }
                                  """;
        _ = Utils.GetGeneratedOutput(sourceCode, out var diagnostics);

        await Assert.That(diagnostics).HasSingleItem();
        var diagnostic = diagnostics.Single();
        await diagnostic.AssertEqual(Diagnostics.DepthLockNotUsedCorrectly, Diagnostics.DepthLockNotUsedCorrectly.MessageFormat.ToString());
    }

    [Test]
    public async Task Bad2()
    {
        const string sourceCode = """
            using Bshox;
            namespace TestModels;

            public static class Type1
            {
                public static string Method1(ref BshoxReader reader)
                {
                    var scope = reader.DepthLock();
                    using (scope)
                    {
                        return reader.ReadString();
                    }
                }
            }
            """;
        _ = Utils.GetGeneratedOutput(sourceCode, out var diagnostics);

        await Assert.That(diagnostics).HasSingleItem();
        var diagnostic = diagnostics.Single();
        await diagnostic.AssertEqual(Diagnostics.DepthLockNotUsedCorrectly, Diagnostics.DepthLockNotUsedCorrectly.MessageFormat.ToString());
    }

    [Test]
    public async Task Bad3()
    {
        const string sourceCode = """
            using Bshox;
            namespace TestModels;

            public static class Type1
            {
                public static string Method1(ref BshoxReader reader)
                {
                    using var scope = reader.DepthLock();
                    scope.Dispose();
                    return reader.ReadString();
                }
            }
            """;
        _ = Utils.GetGeneratedOutput(sourceCode, out var diagnostics);

        await Assert.That(diagnostics).HasSingleItem();
        var diagnostic = diagnostics.Single();
        await diagnostic.AssertEqual(Diagnostics.DepthLockNotUsedCorrectly, Diagnostics.DepthLockNotUsedCorrectly.MessageFormat.ToString());
    }

    [Test]
    public async Task Good1()
    {
        const string sourceCode = """
            using Bshox;
            namespace TestModels;

            public static class Type1
            {
                public static string Method1(ref BshoxReader reader)
                {
                    using var _ = reader.DepthLock();
                    return reader.ReadString();
                }
            }
            """;
        _ = Utils.GetGeneratedOutput(sourceCode, out var diagnostics);

        await Assert.That(diagnostics).IsEmpty();
    }

    [Test]
    public async Task Good2()
    {
        const string sourceCode = """
            using Bshox;
            namespace TestModels;

            public static class Type1
            {
                public static string Method1(ref BshoxReader reader)
                {
                    using (var _ = reader.DepthLock())
                    {
                        return reader.ReadString();
                    }
                }
            }
            """;
        _ = Utils.GetGeneratedOutput(sourceCode, out var diagnostics);

        await Assert.That(diagnostics).IsEmpty();
    }

    [Test]
    public async Task Good3()
    {
        const string sourceCode = """
            using Bshox;
            namespace TestModels;

            public static class Type1
            {
                public static string Method1(ref BshoxReader reader)
                {
                    using (reader.DepthLock())
                    {
                        return reader.ReadString();
                    }
                }
            }
            """;
        _ = Utils.GetGeneratedOutput(sourceCode, out var diagnostics);

        await Assert.That(diagnostics).IsEmpty();
    }
}
