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
            await Assert.That(json).Count().IsEqualTo(941402);
#else
            await Assert.That(json).Count().IsEqualTo(986354);
#endif
            await Assert.That(bshox).Count().IsEqualTo(300988);
            await Assert.That(messagePack).Count().IsEqualTo(446625);
            await Assert.That(proto).Count().IsEqualTo(426556);
            await Assert.That(google).Count().IsEqualTo(426874);
            await Assert.That(proto).Count().IsEqualTo(426556);

#if NET9_0_OR_GREATER
            // size after when using gzip compression.
            await Assert.That(CompressedSize(json)).IsEqualTo(420399);
            await Assert.That(CompressedSize(bshox)).IsEqualTo(285102);
            await Assert.That(CompressedSize(messagePack)).IsEqualTo(359522);
            await Assert.That(CompressedSize(proto)).IsEqualTo(356002);
            await Assert.That(CompressedSize(google)).IsEqualTo(355984);
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
    private static int CompressedSize(byte[] data)
    {
        var ms = new MemoryStream();
        using (var gzip = new System.IO.Compression.GZipStream(ms, System.IO.Compression.CompressionLevel.Optimal, true))
        {
            gzip.Write(data);
        }
        return (int)ms.Length;
    }
#endif
}
