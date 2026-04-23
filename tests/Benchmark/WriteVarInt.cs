using BenchmarkDotNet.Attributes;
using Bshox;
using Bshox.TestUtils;

#pragma warning disable CA1051 // Do not declare visible instance fields

namespace Benchmark;

[Config(typeof(FrameworksConfig))]
public class WriteVarIntFrameworks : WriteVarInt
{
}

/// <summary>
/// Benchmarks for BshoxWriter.WriteVarInt32
/// </summary>
// [DisassemblyDiagnoser]
[Config(typeof(BaseConfig))]
public class WriteVarInt : VarIntBase
{
    protected readonly FixedBufferWriter buffer = new(new byte[BshoxOptions.BufferSizeDefault]);

    [Benchmark(OperationsPerInvoke = Count, Baseline = true)]
    public int WriteByte()
    {
        var w = new BshoxWriter(buffer);
        for (int i = 0; i < Count; i++)
        {
            w.WriteByte((byte)values1[i]);
        }
        w.Flush();
        buffer.Reset();
        return w.UnflushedBytes;
    }

    [Benchmark(OperationsPerInvoke = Count)]
    public int Write1()
    {
        var w = new BshoxWriter(buffer);
        for (int i = 0; i < Count; i++)
        {
            w.WriteVarInt32(values1[i]);
        }
        w.Flush();
        buffer.Reset();
        return w.UnflushedBytes;
    }

    [Benchmark(OperationsPerInvoke = Count)]
    public int Write2()
    {
        var w = new BshoxWriter(buffer);
        for (int i = 0; i < Count; i++)
        {
            w.WriteVarInt32(values2[i]);
        }
        w.Flush();
        buffer.Reset();
        return w.UnflushedBytes;
    }

    // [Benchmark(OperationsPerInvoke = Count)]
    public int Write3()
    {
        var w = new BshoxWriter(buffer);
        for (int i = 0; i < Count; i++)
        {
            w.WriteVarInt32(values3[i]);
        }
        w.Flush();
        buffer.Reset();
        return w.UnflushedBytes;
    }

    // [Benchmark(OperationsPerInvoke = Count)]
    public int Write4()
    {
        var w = new BshoxWriter(buffer);
        for (int i = 0; i < Count; i++)
        {
            w.WriteVarInt32(values4[i]);
        }
        w.Flush();
        buffer.Reset();
        return w.UnflushedBytes;
    }

    [Benchmark(OperationsPerInvoke = Count)]
    public int Write5()
    {
        var w = new BshoxWriter(buffer);
        for (int i = 0; i < Count; i++)
        {
            w.WriteVarInt32(values5[i]);
        }
        w.Flush();
        buffer.Reset();
        return w.UnflushedBytes;
    }

    [Benchmark(OperationsPerInvoke = Count)]
    public int WriteAny()
    {
        var w = new BshoxWriter(buffer);
        for (int i = 0; i < Count; i++)
        {
            w.WriteVarInt32(valuesX[i]);
        }
        w.Flush();
        buffer.Reset();
        return w.UnflushedBytes;
    }
}
