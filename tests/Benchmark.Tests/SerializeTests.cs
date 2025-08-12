#if NETCOREAPP
using Benchmark.Models;
using Bshox.Utils;
#endif

namespace Benchmark.Tests;

public sealed class SerializeTests : Serialize
{
    [Test]
    public async Task Regression()
    {
        Setup();

        byte[] bshox = Bshox();
        byte[] json = Json();
        byte[] messagePack = MessagePack();
        byte[] proto = ProtoBufNet();
        byte[] google = GoogleProtobuf();
        using (Assert.Multiple())
        {
#if NETCOREAPP // netfx uses less compact json
            await Assert.That(json).HasCount(9421);
#else
            await Assert.That(json).HasCount(9865);
#endif
            await Assert.That(bshox).HasCount(3011);
            await Assert.That(messagePack).HasCount(4463);
            await Assert.That(proto).HasCount(4273);
            await Assert.That(google).HasCount(4273);
            await Assert.That(proto).HasCount(google.Length);

#if NETCOREAPP
            float jsonLength = json.Length;
            await Assert.That(bshox.Length / jsonLength).IsEqualTo(0.319605142f);
            await Assert.That(messagePack.Length / jsonLength).IsEqualTo(0.473728895f);
            await Assert.That(proto.Length / jsonLength).IsEqualTo(0.453561187f);
            await Assert.That(google.Length / jsonLength).IsEqualTo(0.453561187f);

            // relative size reduction when using Brotli compression
            await Assert.That(Compressibility(json)).IsEqualTo(0.522768259f);
            await Assert.That(Compressibility(bshox)).IsEqualTo(0.0172700286f);
            await Assert.That(Compressibility(messagePack)).IsEqualTo(0.154380441f);
            await Assert.That(Compressibility(proto)).IsEqualTo(0.112567306f);
            await Assert.That(Compressibility(google)).IsEqualTo(0.109992981f);
#endif
        }
    }

#if NETCOREAPP
    [Test]
    public Task BshoxText()
    {
        Setup();
        var bshox = ForecastSerializer.Forecast.ToBshoxString(Forecast.GetRandom());
        return VeriGit.Validation.Validate(bshox.Replace("\r\n", "\n"));
    }

    private static async Task<float> Compressibility(byte[] data)
    {
        var dest = new byte[System.IO.Compression.BrotliEncoder.GetMaxCompressedLength(data.Length)];
        var success = System.IO.Compression.BrotliEncoder.TryCompress(data, dest, out int size2);
        await Assert.That(success).IsTrue();
        return 1 - (float)size2 / data.Length;
    }
#endif
}
