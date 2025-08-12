using Benchmark.Models;
using BenchmarkDotNet.Attributes;
using Bshox;
using Bshox.Utils;

namespace Benchmark;

[MemoryDiagnoser]
[Config(typeof(MediumConfig))]
public class Parser
{
    internal readonly byte[] bytes;
    internal readonly Forecast data;
    internal readonly string Text;
    internal readonly BshoxValue Value;

    public Parser()
    {
        data = Forecast.GetRandom();
        bytes = ForecastSerializer.Forecast.Serialize(in data);
        Value = ForecastSerializer.Forecast.ToBshoxValue(in data);
        Text = Value.ToString();
    }

    [GlobalSetup]
    public void Setup()
    {

    }

    [Benchmark]
    public BshoxValue Parse()
    {
        return BshoxTextParser.Parse(Text);
    }

    [Benchmark]
    public string ToText()
    {
        return Value.ToString();
    }

    [Benchmark]
    public BshoxValue FromBytes()
    {
        var reader = new BshoxReader(bytes.AsMemory());
        return BshoxValue.Read(ref reader, ForecastSerializer.Forecast.Encoding);
    }
}
