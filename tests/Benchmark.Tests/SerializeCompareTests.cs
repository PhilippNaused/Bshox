using Benchmark.Models;
using Bshox.Utils;

namespace Benchmark.Tests;

public sealed class SerializeCompareTests : SerializeCompare
{
    [Test]
    public async Task Regression()
    {
        Count = 100;
        Setup();

        byte[] bshox = Bshox();
        byte[] json = Json();
        byte[] messagePack = MessagePack();
        byte[] proto = ProtoBufNet();
        byte[] google = GoogleProtobuf();
        using (Assert.Multiple())
        {
#if NETCOREAPP // netfx uses less compact json
            await Assert.That(json).Count().IsEqualTo(941_402);
#else
            await Assert.That(json).Count().IsEqualTo(986_354);
#endif
            await Assert.That(bshox).Count().IsEqualTo(300_988);
            await Assert.That(messagePack).Count().IsEqualTo(446_625);
            await Assert.That(proto).Count().IsEqualTo(426_556);
            await Assert.That(google).Count().IsEqualTo(426_874);
            await Assert.That(proto).Count().IsEqualTo(426_556);

#if NET9_0_OR_GREATER // older frameworks give different compression ratios
            // Compression ratio after GZip compression (bigger is better)
            await Assert.That(CompressionRatio(json)).IsEqualTo(44.7);
            await Assert.That(CompressionRatio(bshox)).IsEqualTo(94.7);
            await Assert.That(CompressionRatio(messagePack)).IsEqualTo(80.5);
            await Assert.That(CompressionRatio(proto)).IsEqualTo(83.5);
            await Assert.That(CompressionRatio(google)).IsEqualTo(83.4);
#endif
        }
    }

    [Test]
    public Task BshoxText()
    {
        var bshox = ForecastSerializer.Forecast.ToBshoxString(Forecast.GetRandom());
        return VeriGit.Validation.Validate(bshox.Replace("\r\n", "\n"));
    }

#if NET9_0_OR_GREATER
    private static double CompressionRatio(byte[] data)
    {
        var ms = new MemoryStream();
        using (var gzip = new System.IO.Compression.GZipStream(ms, System.IO.Compression.CompressionLevel.Optimal, true))
        {
            gzip.Write(data);
        }
        return Math.Round((double)ms.Length / data.Length * 100, 1);
    }
#endif
}
