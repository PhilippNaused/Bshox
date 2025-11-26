using BenchmarkDotNet.Attributes;
using Bshox;
using Bshox.Contracts;
using Bshox.Internals;
using Bshox.TestUtils;

namespace Benchmark;

//[MemoryDiagnoser]
[Config(typeof(MediumConfig))]
public class Experiment
{
    private static readonly BshoxContract<float[]> c1 = new ArrayContract<float>(DefaultContracts.Single);
    private float[] data = null!;
    private BshoxOptions options = null!;
    private readonly PooledByteBufferWriter buffer = new();

    [Params(100, 10_000)]
    public int Size { get; set; }

    [Params(true, false)]
    public bool LittleEndian { get; set; }

    [GlobalSetup]
    public void Setup()
    {
        data = new Random(42).NextArray(Size, float.MinValue, float.MaxValue);
        options = new() { LittleEndian = LittleEndian };
    }

    [Benchmark]
    public void Normal()
    {
        c1.Serialize(buffer, in data, options);
        buffer.Dispose();
    }
}
