using BenchmarkDotNet.Attributes;
using Bshox;
using Bshox.TestUtils;

namespace Benchmark;

[Config(typeof(FrameworksConfig))]
public class WriteStringFrameworks : WriteString;

[Config(typeof(ColdConfig))]
public class WriteStringCold : WriteString;

/// <summary>
/// Benchmarks for BshoxWriter.WriteString
/// </summary>
[DisassemblyDiagnoser(printSource: true)]
[Config(typeof(BaseConfig))]
public class WriteString
{
    private readonly string[] _stringsUnicode = new string[Count];
    private readonly string[] _stringsAscii = new string[Count];
    private readonly FixedBufferWriter fixedBufferWriter = new(Count * 1000 * 4);

    [GlobalSetup]
    public void Setup()
    {
        var rand = new Random(42);
        for (int i = 0; i < _stringsUnicode.Length; i++)
        {
            _stringsUnicode[i] = rand.NextString(Length);
            _stringsAscii[i] = GetAsciiString(rand, Length);
        }
    }

    private static string GetAsciiString(Random random, int length)
    {
        char[] chars = new char[length];
        for (int i = 0; i < chars.Length; i++)
        {
            chars[i] = (char)random.Next(0, 128);
        }
        return new string(chars);
    }

    public const int Count = 1000;

    [Params(10, 100, 1000)]
    public int Length { get; set; }

    [Benchmark(OperationsPerInvoke = Count)]
    public int Unicode()
    {
        var writer = new BshoxWriter(fixedBufferWriter);
        for (int i = 0; i < Count; i++)
        {
            writer.WriteString(_stringsUnicode[i]);
        }
        fixedBufferWriter.Reset();
        return writer.UnflushedBytes;
    }

    [Benchmark(OperationsPerInvoke = Count)]
    public int Ascii()
    {
        var writer = new BshoxWriter(fixedBufferWriter);
        for (int i = 0; i < Count; i++)
        {
            writer.WriteString(_stringsAscii[i]);
        }
        fixedBufferWriter.Reset();
        return writer.UnflushedBytes;
    }
}
