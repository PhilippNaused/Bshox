using System.Text.Json;
using Benchmark.Models;
using BenchmarkDotNet.Attributes;
using Bshox;
using Bshox.TestUtils;
using Google.Protobuf;
using MessagePack;
using ProtoBuf.Meta;

namespace Benchmark;

[Config(typeof(ColdConfig))]
public class SerializeCompareCold : SerializeCompare;

[MemoryDiagnoser]
[Config(typeof(MediumConfig))]
public class SerializeCompare
{
    private readonly TypeModel protoSerializer = Forecast.GetProtoModel();
    private Forecast[] data = null!;
    private Forecast2Array data2 = null!;
    private FixedBufferWriter buffer = null!;

    [GlobalSetup]
    public void Setup() => Setup(new Random());

    internal void Setup(Random random)
    {
        // doesn't work: MessagePack.FormatterNotRegisteredException : System.DateTime[] is not registered in resolver: Benchmark.Models.MyMessagePackResolver
        // MessagePackSerializer.DefaultOptions = MessagePackSerializer.DefaultOptions.WithResolver(MyMessagePackResolver.Instance);
        data = new Forecast[Count];
        data2 = new Forecast2Array
        {
            Items = { Capacity = Count }
        };
        for (int i = 0; i < Count; i++)
        {
            data[i] = Forecast.GetRandom(random: random);
            data2.Items.Add(Forecast.GetRandom2(random: random));
        }
        buffer = new FixedBufferWriter(new byte[Math.Max(Count * 1024 * 10, BshoxOptions.BufferSizeDefault)]);
    }

    [Params(1, 1000)]
    public int Count { get; set; } = 1;

    [Benchmark(Baseline = true)]
    public object Bshox()
    {
        buffer.Reset();
        ForecastSerializer.ForecastArray.Serialize(buffer, in data);
        return buffer;
    }

    [Benchmark(Description = "System.Text.Json")]
    public object Json()
    {
        buffer.Reset();
        using var writer = new Utf8JsonWriter(buffer);
        JsonSerializer.Serialize(writer, data, ForecastJsonContext.Default.ForecastArray);
        return buffer;
    }

    [Benchmark]
    public object MessagePack()
    {
        buffer.Reset();
        MessagePackSerializer.Serialize(buffer, data);
        return buffer;
    }

    [Benchmark(Description = "protobuf-net")]
    public object ProtoBufNet()
    {
        buffer.Reset();
        _ = protoSerializer.Serialize(buffer, data);
        return buffer;
    }

    [Benchmark(Description = "Google.Protobuf")]
    public object GoogleProtobuf()
    {
        buffer.Reset();
        data2.WriteTo(buffer);
        return buffer;
    }
}
