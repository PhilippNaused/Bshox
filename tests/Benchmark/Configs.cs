using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using BenchmarkDotNet.Columns;
using BenchmarkDotNet.Configs;
using BenchmarkDotNet.Environments;
using BenchmarkDotNet.Jobs;
using BenchmarkDotNet.Order;

namespace Benchmark;

public class BaseConfig : ManualConfig
{
    public BaseConfig()
    {
        static string GetMyPath([CallerFilePath] string filePath = "") => filePath; // Get the path of this file
        ArtifactsPath = Path.Combine(GetMyPath(), "../../../docs/benchmarks");
        Orderer = new DefaultOrderer(SummaryOrderPolicy.Declared, jobOrderPolicy: JobOrderPolicy.Numeric);
        _ = HideColumns(StatisticColumn.StdDev);
        _ = HideColumns(StatisticColumn.Median);

        _ = HideColumns(Column.Gen0);
        _ = HideColumns(Column.Gen1);
        _ = HideColumns(Column.Gen2);
        _ = HideColumns(Column.RatioSD);
    }
}

public class FrameworksConfig : BaseConfig
{
    public FrameworksConfig()
    {
        var job = Job.Default.WithPlatform(Platform.X64);
        _ = AddJob(job.WithRuntime(CoreRuntime.Core10_0).WithId("net10.0-x64"));
        _ = AddJob(job.WithRuntime(CoreRuntime.Core90).WithId("net9.0-x64"));
        _ = AddJob(job.WithRuntime(CoreRuntime.Core80).WithId("net8.0-x64").AsBaseline());
        if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
        {
            _ = AddJob(job.WithRuntime(ClrRuntime.Net48).WithId("net48-x64"));
            _ = AddJob(job.WithRuntime(ClrRuntime.Net48).WithPlatform(Platform.X86).WithId("net48-x86"));
        }
        _ = HideColumns(Column.Job);
    }
}

public class ColdConfig : BaseConfig
{
    public ColdConfig()
    {
        _ = AddJob(Job.Dry.WithLaunchCount(25));
    }
}

public class MediumConfig : BaseConfig
{
    public MediumConfig()
    {
        _ = AddJob(Job.MediumRun);
    }
}
