using BenchmarkDotNet.Attributes;
using Bshox;

#pragma warning disable CA1051 // Do not declare visible instance fields

namespace Benchmark;

[Config(typeof(FrameworksConfig))]
public class ReadVarIntFrameworks : ReadVarInt;

/// <summary>
/// Benchmarks for BshoxReader.ReadVarInt32
/// </summary>
[DisassemblyDiagnoser(printSource: true)]
[Config(typeof(BaseConfig))]
public class ReadVarInt : VarIntBase
{
    private readonly ReadOnlyMemory<byte> buffer1;
    private readonly ReadOnlyMemory<byte> buffer2;
    private readonly ReadOnlyMemory<byte> buffer3;
    private readonly ReadOnlyMemory<byte> buffer4;
    private readonly ReadOnlyMemory<byte> buffer5;
    private readonly ReadOnlyMemory<byte> bufferX;
    public ReadVarInt() : base()
    {
        // skip the first 2 bytes to avoid the length prefix
        // The length prefix (1000) has 2 bytes
        const int prefixLength = 2;
        buffer1 = DefaultContracts.Array(DefaultContracts.UInt32).Serialize(values1).AsMemory().Slice(prefixLength);
        buffer2 = DefaultContracts.Array(DefaultContracts.UInt32).Serialize(values2).AsMemory().Slice(prefixLength);
        buffer3 = DefaultContracts.Array(DefaultContracts.UInt32).Serialize(values3).AsMemory().Slice(prefixLength);
        buffer4 = DefaultContracts.Array(DefaultContracts.UInt32).Serialize(values4).AsMemory().Slice(prefixLength);
        buffer5 = DefaultContracts.Array(DefaultContracts.UInt32).Serialize(values5).AsMemory().Slice(prefixLength);
        bufferX = DefaultContracts.Array(DefaultContracts.UInt32).Serialize(valuesX).AsMemory().Slice(prefixLength);
    }

    [Benchmark(OperationsPerInvoke = Count, Baseline = true)]
    public long ReadByte()
    {
        var r = new BshoxReader(buffer1);
        for (int i = 0; i < Count; i++)
        {
            _ = r.ReadByte();
        }
        return r.Consumed;
    }

    [Benchmark(OperationsPerInvoke = Count)]
    public long Read1()
    {
        var r = new BshoxReader(buffer1);
        for (int i = 0; i < Count; i++)
        {
            _ = r.ReadVarInt32();
        }
        return r.Consumed;
    }

    // [Benchmark(OperationsPerInvoke = Count)]
    public long Read2()
    {
        var r = new BshoxReader(buffer2);
        for (int i = 0; i < Count; i++)
        {
            _ = r.ReadVarInt32();
        }
        return r.Consumed;
    }

    //[Benchmark(OperationsPerInvoke = Count)]
    public long Read3()
    {
        var r = new BshoxReader(buffer3);
        for (int i = 0; i < Count; i++)
        {
            _ = r.ReadVarInt32();
        }
        return r.Consumed;
    }

    //[Benchmark(OperationsPerInvoke = Count)]
    public long Read4()
    {
        var r = new BshoxReader(buffer4);
        for (int i = 0; i < Count; i++)
        {
            _ = r.ReadVarInt32();
        }
        return r.Consumed;
    }

    // [Benchmark(OperationsPerInvoke = Count)]
    public long Read5()
    {
        var r = new BshoxReader(buffer5);
        for (int i = 0; i < Count; i++)
        {
            _ = r.ReadVarInt32();
        }
        return r.Consumed;
    }

    [Benchmark(OperationsPerInvoke = Count)]
    public long ReadAny()
    {
        var r = new BshoxReader(bufferX);
        for (int i = 0; i < Count; i++)
        {
            _ = r.ReadVarInt32();
        }
        return r.Consumed;
    }
}
