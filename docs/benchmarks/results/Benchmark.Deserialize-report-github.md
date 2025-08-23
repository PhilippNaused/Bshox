```

BenchmarkDotNet v0.15.2, Windows 11 (10.0.26100.4946/24H2/2024Update/HudsonValley)
Unknown processor
.NET SDK 10.0.100-preview.7.25380.108
  [Host]    : .NET 10.0.0 (10.0.25.38108), X64 RyuJIT AVX2
  MediumRun : .NET 10.0.0 (10.0.25.38108), X64 RyuJIT AVX2

Job=MediumRun  IterationCount=15  LaunchCount=2
WarmupCount=10

```
| Method | TieredCompilation | Segmented |       Mean |    Error | Allocated |
|--------|-------------------|-----------|-----------:|---------:|----------:|
| Bshox  | 0                 | False     | 1,352.2 ns | 11.42 ns |   4.16 KB |
| Bshox  | 1                 | False     |   915.6 ns |  8.19 ns |   4.16 KB |
| Bshox  | 0                 | True      | 1,602.5 ns |  8.19 ns |   4.21 KB |
| Bshox  | 1                 | True      | 1,029.4 ns |  2.68 ns |   4.21 KB |
