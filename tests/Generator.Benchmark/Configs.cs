using System.IO.Hashing;
using System.Text;
using BenchmarkDotNet.Columns;
using BenchmarkDotNet.Configs;
using BenchmarkDotNet.Jobs;
using BenchmarkDotNet.Order;

namespace Generator.Benchmark;

public class BaseConfig : ManualConfig
{
    public BaseConfig()
    {
        ArtifactsPath = Path.Combine("tests", nameof(Benchmark), "results", Crc32.HashToUInt32(Encoding.UTF8.GetBytes(Environment.MachineName)).ToString());
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
        _ = AddJob(Job.Dry.WithEnvironmentVariable("DOTNET_TieredPGO", "0").WithGcServer(true).WithLaunchCount(10));
    }
}

public class MediumConfig : BaseConfig
{
    public MediumConfig()
    {
        _ = AddJob(Job.MediumRun.WithEnvironmentVariable("DOTNET_TieredPGO", "0").WithGcServer(true));
    }
}
