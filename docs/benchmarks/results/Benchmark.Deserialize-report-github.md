```

BenchmarkDotNet v0.15.2, Windows 11 (10.0.26100.4946/24H2/2024Update/HudsonValley)
Unknown processor
.NET SDK 10.0.100-rc.1.25451.107
  [Host]    : .NET 10.0.0 (10.0.25.45207), X64 RyuJIT AVX2
  MediumRun : .NET 10.0.0 (10.0.25.45207), X64 RyuJIT AVX2

Job=MediumRun  MaxRelativeError=0.01  IterationCount=15
LaunchCount=2  WarmupCount=10

```
| Method | TieredCompilation | Segmented |       Mean |    Error | Allocated |
|--------|-------------------|-----------|-----------:|---------:|----------:|
| Bshox  | 0                 | False     | 1,362.8 ns |  9.33 ns |   4.16 KB |
| Bshox  | 1                 | False     |   871.4 ns |  6.90 ns |   4.16 KB |
| Bshox  | 0                 | True      | 1,557.1 ns | 10.49 ns |   4.21 KB |
| Bshox  | 1                 | True      | 1,037.2 ns |  8.26 ns |   4.21 KB |
