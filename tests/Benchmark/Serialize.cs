using System.Text.Json;
using Benchmark.Models;
using BenchmarkDotNet.Attributes;
using Bshox;
using Google.Protobuf;
using MessagePack;
using ProtoBuf.Meta;

namespace Benchmark;

[Config(typeof(ColdConfig))]
public class SerializeCold : Serialize;

[MemoryDiagnoser]
[Config(typeof(MediumConfig))]
//[Config(typeof(FrameworksConfig))]
public class Serialize
{
    private readonly TypeModel protoSerializer = Forecast.GetProtoModel();
    private Forecast data = null!;
    private Forecast2 data2 = null!;

    [GlobalSetup]
    public void Setup()
    {
        // doesn't work: MessagePack.FormatterNotRegisteredException : System.DateTime[] is not registered in resolver: Benchmark.Models.MyMessagePackResolver
        // MessagePackSerializer.DefaultOptions = MessagePackSerializer.DefaultOptions.WithResolver(MyMessagePackResolver.Instance);

        data = Forecast.GetRandom();
        data2 = Forecast.GetRandom2();
    }

    [Benchmark(Baseline = true)]
    public byte[] Bshox()
    {
        return ForecastSerializer.Forecast.Serialize(in data);
    }

    [Benchmark(Description = "System.Text.Json")]
    public byte[] Json()
    {
        return JsonSerializer.SerializeToUtf8Bytes(data, ForecastJsonContext.Default.Forecast);
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
