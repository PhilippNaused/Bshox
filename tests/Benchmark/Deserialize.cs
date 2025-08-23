using System.Buffers;
using Benchmark.Models;
using BenchmarkDotNet.Attributes;
using Bshox;
using Bshox.TestUtils;

namespace Benchmark;

[Config(typeof(ColdConfig))]
public class DeserializeCold : Deserialize;

[MemoryDiagnoser]
[Config(typeof(Medium2Config))]
public class Deserialize
{
    internal ReadOnlySequence<byte> _bshoxData;
    internal Forecast data = null!;

    [Params(true, false)]
    public bool Segmented { get; set; }

    [GlobalSetup]
    public void Setup()
    {
        data = Forecast.GetRandom();
        _bshoxData = Get(ForecastSerializer.Forecast.Serialize(in data));
    }

    private ReadOnlySequence<byte> Get(byte[] bytes)
    {
        if (bytes.Length < 256)
        {
            throw new ArgumentException("Data must be at least 256 bytes long for segmentation tests.", nameof(bytes));
        }
        return Segmented ? SequenceSegmenter.MakeSegmentedSequence(bytes, 128) : new ReadOnlySequence<byte>(bytes);
    }

    [Benchmark]
    public Forecast Bshox()
    {
        return ForecastSerializer.Forecast.Deserialize(_bshoxData);
    }
}
