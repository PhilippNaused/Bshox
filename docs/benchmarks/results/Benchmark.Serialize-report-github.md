```

BenchmarkDotNet v0.15.2, Windows 11 (10.0.26100.4946/24H2/2024Update/HudsonValley)
Unknown processor
.NET SDK 10.0.100-rc.1.25451.107
  [Host]    : .NET 10.0.0 (10.0.25.45207), X64 RyuJIT AVX2
  MediumRun : .NET 10.0.0 (10.0.25.45207), X64 RyuJIT AVX2

Job=MediumRun  MaxRelativeError=0.01  IterationCount=15
LaunchCount=2  WarmupCount=10

```
| Method | TieredCompilation | Count |           Mean |        Error |  Allocated |
|--------|-------------------|-------|---------------:|-------------:|-----------:|
| Bshox  | 0                 | 1     |     1,648.8 ns |     15.46 ns |    3.05 KB |
| Bshox  | 1                 | 1     |       717.6 ns |      5.54 ns |    3.05 KB |
| Bshox  | 0                 | 1000  | 1,792,687.3 ns | 12,985.92 ns | 2953.82 KB |
| Bshox  | 1                 | 1000  |   943,382.6 ns | 21,473.74 ns | 2953.82 KB |
