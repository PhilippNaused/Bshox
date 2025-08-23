using System.Buffers;
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
public class DeserializeCompareCold : DeserializeCompare;

[MemoryDiagnoser]
[Config(typeof(MediumConfig))]
public class DeserializeCompare
{
    private readonly TypeModel protoSerializer = Forecast.GetProtoModel();
    internal ReadOnlySequence<byte> _bshoxData;
    internal ReadOnlySequence<byte> _jsonData;
    internal ReadOnlySequence<byte> _messageData;
    internal ReadOnlySequence<byte> _protoData;
    internal ReadOnlySequence<byte> _googleData;

    internal Forecast data = null!;
    internal Forecast2 data2 = null!;

    //[Params(true, false)]
    public bool Segmented { get; set; }

    [GlobalSetup]
    public void Setup()
    {
        // doesn't work: MessagePack.FormatterNotRegisteredException : System.DateTime[] is not registered in resolver: Benchmark.Models.MyMessagePackResolver
        // MessagePackSerializer.DefaultOptions = MessagePackSerializer.DefaultOptions.WithResolver(MyMessagePackResolver.Instance);

        MemoryStream stream;
        data = Forecast.GetRandom();
        data2 = Forecast.GetRandom2();

        stream = new();
        JsonSerializer.Serialize(stream, data, ForecastJsonContext.Default.Forecast);
        _jsonData = Get(stream.ToArray());

        stream = new();
        _ = protoSerializer.Serialize(stream, data);
        _protoData = Get(stream.ToArray());

        stream = new();
        ForecastSerializer.Forecast.Serialize(stream, in data);
        _bshoxData = Get(stream.ToArray());

        stream = new();
        MessagePackSerializer.Serialize(stream, data);
        _messageData = Get(stream.ToArray());

        _googleData = Get(data2.ToByteArray());
    }

    private ReadOnlySequence<byte> Get(byte[] bytes)
    {
        if (bytes.Length < 256)
        {
            throw new ArgumentException("Data must be at least 256 bytes long for segmentation tests.", nameof(bytes));
        }
        return Segmented ? SequenceSegmenter.MakeSegmentedSequence(bytes, 128) : new ReadOnlySequence<byte>(bytes);
    }

    [Benchmark(Baseline = true)]
    public Forecast Bshox()
    {
        return ForecastSerializer.Forecast.Deserialize(_bshoxData);
    }

    [Benchmark(Description = "System.Text.Json")]
    public Forecast? Json()
    {
        var reader = new Utf8JsonReader(_jsonData);
        return JsonSerializer.Deserialize(ref reader, ForecastJsonContext.Default.Forecast);
    }

    [Benchmark]
    public Forecast MessagePack()
    {
        return MessagePackSerializer.Deserialize<Forecast>(in _messageData);
    }

    [Benchmark(Description = "protobuf-net")]
    public Forecast ProtoBuf()
    {
        return protoSerializer.Deserialize<Forecast>(_protoData);
    }

    [Benchmark(Description = "Google.Protobuf")]
    public Forecast2 Google()
    {
        return Forecast2.Parser.ParseFrom(_googleData);
    }
}
