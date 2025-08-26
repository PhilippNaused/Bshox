using Benchmark.Models;
using BenchmarkDotNet.Attributes;
using Bshox;

namespace Benchmark;

[Config(typeof(ColdConfig))]
public class SerializeCold : Serialize;

[MemoryDiagnoser]
[Config(typeof(Medium2Config))]
public class Serialize
{
    private Forecast[] data = null!;

    [Params(1, 1000)]
    public int Count { get; set; } = 1;

    [GlobalSetup]
    public void Setup()
    {
        var random = new Random(42);
        data = new Forecast[Count];
        for (int i = 0; i < Count; i++)
        {
            data[i] = Forecast.GetRandom(random: random);
        }
    }

    [Benchmark]
    public byte[] Bshox()
    {
        return ForecastSerializer.ForecastArray.Serialize(in data);
    }
}
