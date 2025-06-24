using System.Buffers;
using Benchmark.Models;
using BenchmarkDotNet.Attributes;
using Bshox;

namespace Benchmark;

[MemoryDiagnoser]
[Config(typeof(FrameworksConfig))]
public class Frameworks
{
    private ReadOnlySequence<byte> _bshoxData;
    private Forecast data = null!;

    [GlobalSetup]
    public void Setup()
    {
        data = Forecast.GetRandom();
        _bshoxData = new ReadOnlySequence<byte>(Serialize());
    }

    [Benchmark]
    public Forecast Deserialize()
    {
        return ForecastSerializer.Forecast.Deserialize(_bshoxData);
    }

    [Benchmark]
    public byte[] Serialize()
    {
        return ForecastSerializer.Forecast.Serialize(in data);
    }
}
