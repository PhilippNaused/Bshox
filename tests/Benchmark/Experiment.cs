using BenchmarkDotNet.Attributes;
using Bshox;
using Bshox.Contracts;
using Bshox.TestUtils;

namespace Benchmark;

//[MemoryDiagnoser]
[Config(typeof(MediumConfig))]
public class Experiment
{
    private readonly BshoxContract<float[]> c1;
    private float[] data = null!;

    public Experiment()
    {
        c1 = new ArrayContract<float>(DefaultContracts.Single);
    }

    [Params(1, 100, 10_000)]
    public int Size { get; set; }

    [GlobalSetup]
    public void Setup()
    {
        data = new Random(42).NextArray(Size, float.MinValue, float.MaxValue);
    }

    [Benchmark]
    public byte[] Normal()
    {
        return c1.Serialize(in data);
    }
}
