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
    private Forecast data = null!;

    [GlobalSetup]
    public void Setup()
    {
        data = Forecast.GetRandom();
    }

    [Benchmark]
    public byte[] Bshox()
    {
        return ForecastSerializer.Forecast.Serialize(in data);
    }
}
