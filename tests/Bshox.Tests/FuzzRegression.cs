using System.Text;
using Bshox.Utils;

namespace Bshox.Tests;

[NotInParallel]
public class FuzzRegression
{
    [Test]
    public async Task ParseOverTextFlow()
    {
        var text = new string('[', 20000);
        await Assert.That(() => BshoxTextParser.Parse(text)).Throws<BshoxParserException>();
    }

    [Test]
    [MethodDataSource(nameof(AllMetaResourceNames))]
    [Timeout(500)]
    public async Task ParseMeta(string name, CancellationToken token)
    {
        _ = token;
        var resource = GetResource(name);
        await Assert.That(CanParseBinary(resource)).IsTrue();
    }

    [Test]
    [MethodDataSource(nameof(AllTextResourceNames))]
    [Timeout(500)]
    public void ParseText(string name, CancellationToken token)
    {
        _ = token;
        var resource = GetResource(name);
        var text = Encoding.UTF8.GetString(resource);

        try
        {
            _ = BshoxTextParser.Parse(text);
        }
        catch (BshoxParserException) { }
    }

    public static string[] AllMetaResourceNames()
    {
        return GetResourceNames("Bshox.Fuzz.Meta");
    }

    public static string[] AllTextResourceNames()
    {
        return GetResourceNames("Bshox.Fuzz.Text");
    }

    private static string[] GetResourceNames(string filter)
    {
        return [.. GetAllResourceNames().Where(x => x.StartsWith(filter))];
    }

    private static bool CanParseBinary(byte[] array)
    {
        bool success = false;
#if NETCOREAPP
        foreach (BshoxCode code in Enum.GetValues<BshoxCode>())
#else
        foreach (BshoxCode code in Enum.GetValues(typeof(BshoxCode)))
#endif
        {
            var reader = new BshoxReader(array);
            try
            {
                _ = BshoxValue.Read(ref reader, code);
                success = true;
            }
            catch (BshoxException) { } // this is the only valid exception type
        }
        return success;
    }

    private static string[] GetAllResourceNames()
    {
        return typeof(FuzzRegression).Assembly.GetManifestResourceNames();
    }

    private static byte[] GetResource(string name)
    {
        using var stream = typeof(FuzzRegression).Assembly.GetManifestResourceStream(name) ?? throw new InvalidOperationException($"Resource '{name}' not found.");
        var ms = new MemoryStream();
        stream.CopyTo(ms);
        return ms.ToArray();
    }
}
