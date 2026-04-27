using BenchmarkDotNet.Attributes;
using Bshox;
using Bshox.TestUtils;

namespace Benchmark;

[Config(typeof(FrameworksConfig))]
public class GuidContractFrameworks : GuidContract;

[Config(typeof(ColdConfig))]
public class GuidContractCold : GuidContract;

/// <summary>
/// Benchmarks for DefaultContracts.Guid
/// </summary>
[DisassemblyDiagnoser(printSource: true, maxDepth: 3)]
[Config(typeof(BaseConfig))]
public class GuidContract
{
    private readonly Guid _guid = Guid.NewGuid();
    private readonly FixedBufferWriter fixedBufferWriter = new();
    private readonly BshoxContract<Guid> contract = DefaultContracts.Guid;
    private readonly byte[] buffer = new byte[17];

    public GuidContract()
    {
        var writer = new BshoxWriter(fixedBufferWriter);
        contract.Serialize(ref writer, in _guid);
        fixedBufferWriter.GetSpan(17).Slice(0, 17).CopyTo(buffer);
        fixedBufferWriter.Reset();
    }

    [Benchmark]
    public int Serialize()
    {
        var writer = new BshoxWriter(fixedBufferWriter);
        contract.Serialize(ref writer, in _guid);
        fixedBufferWriter.Reset();
        return writer.UnflushedBytes;
    }

    [Benchmark]
    public Guid Deserialize()
    {
        var reader = new BshoxReader(buffer);
        contract.Deserialize(ref reader, out Guid guid);
        return guid;
    }
}
