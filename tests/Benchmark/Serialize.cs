using Benchmark.Models;
using BenchmarkDotNet.Attributes;
using Bshox;
using Bshox.TestUtils;

namespace Benchmark;

[Config(typeof(ColdConfig))]
public class SerializeCold : Serialize;

[MemoryDiagnoser]
[Config(typeof(MediumConfig))]
public class Serialize
{
    private Forecast[] data = null!;
    private FixedBufferWriter buffer = null!;

    [Params(1, 1000)]
    public int Count { get; set; } = 1;

    [GlobalSetup]
    public void Setup()
    {
        data = new Forecast[Count];
        for (int i = 0; i < Count; i++)
        {
            data[i] = Forecast.GetRandom();
        }
        buffer = new FixedBufferWriter(new byte[Math.Max(Count * 1024 * 5, BshoxOptions.BufferSizeDefault)]);
    }

    [Benchmark]
    public object Bshox()
    {
        buffer.Reset();
        ForecastSerializer.ForecastArray.Serialize(buffer, in data);
        return buffer;
    }
}
