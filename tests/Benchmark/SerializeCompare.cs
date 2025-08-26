using System.Text.Json;
using Benchmark.Models;
using BenchmarkDotNet.Attributes;
using Bshox;
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

    [GlobalSetup]
    public void Setup()
    {
        var random = new Random(42);
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
    }

    [Params(1, 1000)]
    public int Count { get; set; } = 1;

    [Benchmark(Baseline = true)]
    public byte[] Bshox()
    {
        return ForecastSerializer.ForecastArray.Serialize(in data);
    }

    [Benchmark(Description = "System.Text.Json")]
    public byte[] Json()
    {
        return JsonSerializer.SerializeToUtf8Bytes(data, ForecastJsonContext.Default.ForecastArray);
    }

    [Benchmark]
    public byte[] MessagePack()
    {
        return MessagePackSerializer.Serialize(data);
    }

    [Benchmark(Description = "protobuf-net")]
    public byte[] ProtoBufNet()
    {
        var stream = new MemoryStream();
        _ = protoSerializer.Serialize(stream, data);
        return stream.ToArray();
    }

    [Benchmark(Description = "Google.Protobuf")]
    public byte[] GoogleProtobuf()
    {
        return data2.ToByteArray();
    }
}
