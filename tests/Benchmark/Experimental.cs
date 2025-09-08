using BenchmarkDotNet.Attributes;
using Bshox;
using Bshox.Internals;
using Bshox.TestUtils;

namespace Benchmark;

[MemoryDiagnoser]
[Config(typeof(BaseConfig))]
//[DisassemblyDiagnoser(printSource: true)]
public class Experimental
{
    private const int Count = 1_000;

    private readonly uint[] array = new uint[Count];
    private readonly PooledByteBufferWriter buffer = new();

    public Experimental()
    {
        var random = new Random();
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
        buffer.Dispose();
    }
}
