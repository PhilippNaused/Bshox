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
        Orderer = new DefaultOrderer(SummaryOrderPolicy.Declared);
        _ = HideColumns(StatisticColumn.StdDev);
        _ = HideColumns(StatisticColumn.Median);

        _ = HideColumns(Column.Gen0);
        _ = HideColumns(Column.Gen1);
        _ = HideColumns(Column.Gen2);
    }
}

public class FrameworksConfig : BaseConfig
{
    public FrameworksConfig()
    {
        _ = AddJob(Job.Default.WithEnvironmentVariable("DOTNET_TieredPGO", "0").WithGcServer(true).WithPlatform(Platform.X64).WithRuntime(CoreRuntime.Core10_0).WithId("Net100-x64"));
        _ = AddJob(Job.Default.WithEnvironmentVariable("DOTNET_TieredPGO", "0").WithGcServer(true).WithPlatform(Platform.X64).WithRuntime(CoreRuntime.Core90).WithId("Net90-x64"));
        _ = AddJob(Job.Default.WithEnvironmentVariable("DOTNET_TieredPGO", "0").WithGcServer(true).WithPlatform(Platform.X64).WithRuntime(CoreRuntime.Core80).WithId("Net80-x64").AsBaseline());
        if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
        {
            _ = AddJob(Job.Default.WithEnvironmentVariable("DOTNET_TieredPGO", "0").WithGcServer(true).WithPlatform(Platform.X64).WithRuntime(ClrRuntime.Net48).WithId("Net48-x64"));
            _ = AddJob(Job.Default.WithEnvironmentVariable("DOTNET_TieredPGO", "0").WithGcServer(true).WithPlatform(Platform.X86).WithRuntime(ClrRuntime.Net48).WithId("Net48-x86"));
        }
        _ = HideColumns(Column.Platform);
        _ = HideColumns(Column.Runtime);
    }
}

public class ColdConfig : BaseConfig
{
    public ColdConfig()
    {
        _ = AddJob(Job.Dry.WithEnvironmentVariable("DOTNET_TieredPGO", "0").WithGcServer(true).WithLaunchCount(25));
    }
}

public class MediumConfig : BaseConfig
{
    public MediumConfig()
    {
        _ = AddJob(Job.MediumRun.WithEnvironmentVariable("DOTNET_TieredPGO", "0").WithGcServer(true));
    }
}
