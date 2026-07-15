using System.Text;
using Bshox.TestUtils;
using Bshox.Utils;

namespace Bshox.Tests;

[NotInParallel]
[Arguments(true)]
[Arguments(false)]
public class FuzzRegression(bool segmented)
{
    private const int Timeout = 500;

    [Test]
    public async Task ParseOverTextFlow()
    {
        var text = new string('[', 20000);
        await Assert.That(() => BshoxTextParser.Parse(text)).Throws<BshoxParserException>();
    }

    [Test]
    [MethodDataSource(nameof(AllMetaResourceNames))]
    [Timeout(Timeout)]
    public async Task ParseMeta(string name, CancellationToken token)
    {
        _ = token; // TODO: use the token to cancel the test (somehow)
        var resource = GetResource(name);
        await Assert.That(CanParseBinary(resource, skip: false)).IsTrue();
    }

    [Test]
    [MethodDataSource(nameof(AllMetaResourceNames))]
    [Timeout(Timeout)]
    public async Task SkipMeta(string name, CancellationToken token)
    {
        _ = token; // TODO: use the token to cancel the test (somehow)
        var resource = GetResource(name);
        await Assert.That(CanParseBinary(resource, skip: true)).IsTrue();
    }

    [Test]
    [MethodDataSource(nameof(AllTextResourceNames))]
    [Timeout(Timeout)]
    public void ParseText(string name, CancellationToken token)
    {
        _ = token; // TODO: use the token to cancel the test (somehow)
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

    private bool CanParseBinary(byte[] array, bool skip)
    {
        bool success = false;
#if NETCOREAPP
        foreach (BshoxEncoding code in Enum.GetValues<BshoxEncoding>())
#else
        foreach (BshoxEncoding code in Enum.GetValues(typeof(BshoxEncoding)))
#endif
        {
            BshoxReader reader;
            if (segmented)
            {
                var seg = SequenceSegmenter.MakeSegmentedSequence(array, 1);
                reader = new BshoxReader(seg);
            }
            else
            {
                reader = new BshoxReader(array);
            }
            try
            {
                if (skip)
                {
                    reader.SkipValue(code);
                }
                else
                {
                    _ = BshoxValue.Read(ref reader, code);
                }
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
