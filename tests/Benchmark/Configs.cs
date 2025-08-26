using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using BenchmarkDotNet.Columns;
using BenchmarkDotNet.Configs;
using BenchmarkDotNet.Environments;
using BenchmarkDotNet.Jobs;
using BenchmarkDotNet.Order;
using BenchmarkDotNet.Reports;
using BenchmarkDotNet.Running;

namespace Benchmark;

public class BaseConfig : ManualConfig
{
    private static string GetMyPath([CallerFilePath] string filePath = "") => filePath; // Get the path of this file

    public BaseConfig()
    {
        ArtifactsPath = Path.Combine(GetMyPath(), "../../../docs/benchmarks");
        Orderer = new DefaultOrderer(SummaryOrderPolicy.Declared, jobOrderPolicy: JobOrderPolicy.Numeric);
        _ = HideColumns(StatisticColumn.StdDev);
        _ = HideColumns(StatisticColumn.Median);

        _ = HideColumns(Column.Gen0);
        _ = HideColumns(Column.Gen1);
        _ = HideColumns(Column.Gen2);
        _ = HideColumns(Column.RatioSD);
        Options |= ConfigOptions.DisableLogFile;
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

public class Medium2Config : BaseConfig
{
    public Medium2Config()
    {
        var job = Job.MediumRun.WithMaxRelativeError(0.01);
        _ = AddJob(job.WithEnvironmentVariable("DOTNET_TieredCompilation", "0"));
        _ = AddJob(job.WithEnvironmentVariable("DOTNET_TieredCompilation", "1"));
        _ = AddColumn(new TieredCompilationColumn());
        _ = HideColumns(Column.EnvironmentVariables);
    }
}

internal class TieredCompilationColumn : IColumn
{
    private const string EnvName = "DOTNET_TieredCompilation";

    /// <inheritdoc />
    public string GetValue(Summary summary, BenchmarkCase benchmarkCase)
    {
        return benchmarkCase.Job.Environment.EnvironmentVariables.FirstOrDefault(e =>
            string.Equals(e.Key, EnvName, StringComparison.OrdinalIgnoreCase))?.Value ?? "";
    }

    /// <inheritdoc />
    public string GetValue(Summary summary, BenchmarkCase benchmarkCase, SummaryStyle style)
    {
        return GetValue(summary, benchmarkCase);
    }

    /// <inheritdoc />
    public bool IsDefault(Summary summary, BenchmarkCase benchmarkCase)
    {
        string value = GetValue(summary, benchmarkCase);
        return string.IsNullOrEmpty(value) || string.Equals(value, "1");
    }

    /// <inheritdoc />
    public bool IsAvailable(Summary summary)
    {
        return summary.BenchmarksCases.Any(b => !IsDefault(summary, b));
    }

    /// <inheritdoc />
    public string Id => "TieredCompilation";

    /// <inheritdoc />
    public string ColumnName => "TieredCompilation";

    /// <inheritdoc />
    public bool AlwaysShow => false;

    /// <inheritdoc />
    public ColumnCategory Category => ColumnCategory.Job;

    /// <inheritdoc />
    public int PriorityInCategory { get; }

    /// <inheritdoc />
    public bool IsNumeric => false;

    /// <inheritdoc />
    public UnitType UnitType => UnitType.Dimensionless;

    /// <inheritdoc />
    public string Legend => "TieredCompilation";
}
