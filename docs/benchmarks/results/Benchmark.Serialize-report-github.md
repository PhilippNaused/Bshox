```

BenchmarkDotNet v0.15.2, Windows 11 (10.0.26100.4946/24H2/2024Update/HudsonValley)
Unknown processor
.NET SDK 10.0.100-preview.7.25380.108
  [Host]    : .NET 10.0.0 (10.0.25.38108), X64 RyuJIT AVX2
  MediumRun : .NET 10.0.0 (10.0.25.38108), X64 RyuJIT AVX2

Job=MediumRun  IterationCount=15  LaunchCount=2
WarmupCount=10  Gen0=0.1774

```
| Method | TieredCompilation |       Mean |    Error | Allocated |
|--------|-------------------|-----------:|---------:|----------:|
| Bshox  | 0                 | 1,816.1 ns |  6.19 ns |      3 KB |
| Bshox  | 1                 |   779.7 ns | 10.64 ns |      3 KB |
