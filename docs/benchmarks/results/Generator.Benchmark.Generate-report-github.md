```

BenchmarkDotNet v0.15.2, Windows 11 (10.0.26100.4946/24H2/2024Update/HudsonValley)
Unknown processor
.NET SDK 10.0.100-preview.7.25380.108
  [Host]    : .NET 10.0.0 (10.0.25.38108), X64 RyuJIT AVX2
  MediumRun : .NET 10.0.0 (10.0.25.38108), X64 RyuJIT AVX2

Job=MediumRun  EnvironmentVariables=DOTNET_TieredPGO=0  Server=True
IterationCount=15  LaunchCount=2  WarmupCount=10

```
| Method         |       Mean |    Error | Ratio | RatioSD | Allocated | Alloc Ratio |
|----------------|-----------:|---------:|------:|--------:|----------:|------------:|
| Base           |   266.7 μs |  1.69 μs |  1.00 |    0.01 |  94.27 KB |        1.00 |
| BshoxGenerator | 1,749.7 μs |  6.88 μs |  6.56 |    0.07 | 468.03 KB |        4.96 |
| JsonGenerator  | 4,133.7 μs | 31.39 μs | 15.50 |    0.22 | 1142.3 KB |       12.12 |
