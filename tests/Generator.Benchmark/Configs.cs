using System.Runtime.CompilerServices;
using BenchmarkDotNet.Columns;
using BenchmarkDotNet.Configs;
using BenchmarkDotNet.Jobs;
using BenchmarkDotNet.Order;

namespace Generator.Benchmark;

public class BaseConfig : ManualConfig
{
    public BaseConfig()
    {
        static string GetMyPath([CallerFilePath] string filePath = "") => filePath; // Get the path of this file
        ArtifactsPath = Path.Combine(GetMyPath(), "../../../docs/benchmarks");
        Orderer = new DefaultOrderer(SummaryOrderPolicy.Declared);
        _ = HideColumns(StatisticColumn.StdDev);
        _ = HideColumns(StatisticColumn.Median);

        _ = HideColumns(Column.Gen0);
        _ = HideColumns(Column.Gen1);
        _ = HideColumns(Column.Gen2);
    }
}

public class ColdConfig : BaseConfig
{
    public ColdConfig()
    {
        _ = AddJob(Job.Dry.WithLaunchCount(10));
    }
}

public class MediumConfig : BaseConfig
{
    public MediumConfig()
    {
        _ = AddJob(Job.MediumRun.WithWarmupCount(20));
    }
}
