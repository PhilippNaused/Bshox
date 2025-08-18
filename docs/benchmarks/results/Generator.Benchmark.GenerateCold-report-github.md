```

BenchmarkDotNet v0.15.2, Windows 11 (10.0.26100.4946/24H2/2024Update/HudsonValley)
Unknown processor
.NET SDK 10.0.100-preview.7.25380.108
  [Host] : .NET 10.0.0 (10.0.25.38108), X64 RyuJIT AVX2
  Dry    : .NET 10.0.0 (10.0.25.38108), X64 RyuJIT AVX2

Job=Dry  EnvironmentVariables=DOTNET_TieredPGO=0  Server=True
IterationCount=1  LaunchCount=10  RunStrategy=ColdStart
UnrollFactor=1  WarmupCount=1

```
| Method         |     Mean |   Error | Ratio | RatioSD | Allocated | Alloc Ratio |
|----------------|---------:|--------:|------:|--------:|----------:|------------:|
| Base           | 358.7 ms | 5.76 ms |  1.00 |    0.01 |   1.33 MB |        1.00 |
| BshoxGenerator | 663.9 ms | 3.39 ms |  1.85 |    0.02 |   3.62 MB |        2.72 |
| JsonGenerator  | 713.2 ms | 3.19 ms |  1.99 |    0.02 |   8.87 MB |        6.66 |
