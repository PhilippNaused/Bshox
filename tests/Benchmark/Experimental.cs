using BenchmarkDotNet.Attributes;
using Bshox;
using Bshox.TestUtils;

namespace Benchmark;

[MemoryDiagnoser]
[Config(typeof(BaseConfig))]
//[DisassemblyDiagnoser(printSource: true)]
public class Experimental
{
    private const int Count = 1_000;

    private readonly uint[] array = new uint[Count];
    private readonly FixedBufferWriter buffer = new(new byte[BshoxOptions.BufferSizeDefault]);

    public Experimental()
    {
        var random = new Random(42);
        for (int i = 0; i < Count; i++)
        {
            array[i] = random.NextScaledUInt();
        }
    }

    [Benchmark(OperationsPerInvoke = Count)]
    public void Test1()
    {
        var writer = new BshoxWriter(buffer);
        for (int i = 0; i < Count; i++)
        {
            writer.WriteVarInt32(array[i]);
            writer.WriteByte(17);
        }
        writer.Flush();
        buffer.Reset();
    }
}
