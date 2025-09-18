```

BenchmarkDotNet v0.15.3, Windows 11 (10.0.26100.6584/24H2/2024Update/HudsonValley)
Intel Core i7-14700KF 3.40GHz, 1 CPU, 28 logical and 20 physical cores
.NET SDK 10.0.100-rc.1.25451.107
  [Host]    : .NET 10.0.0 (10.0.0-rc.1.25451.107, 10.0.25.45207), X64 RyuJIT x86-64-v3
  MediumRun : .NET 10.0.0 (10.0.0-rc.1.25451.107, 10.0.25.45207), X64 RyuJIT x86-64-v3

Job=MediumRun  MaxRelativeError=0.01  IterationCount=15
LaunchCount=2  WarmupCount=10

```
| Method | TieredCompilation | Segmented |       Mean |    Error | Allocated |
|--------|-------------------|-----------|-----------:|---------:|----------:|
| Bshox  | 0                 | False     | 1,317.1 ns |  7.89 ns |   4.16 KB |
| Bshox  | 1                 | False     |   879.4 ns | 14.43 ns |   4.16 KB |
| Bshox  | 0                 | True      | 1,538.2 ns | 20.74 ns |   4.21 KB |
| Bshox  | 1                 | True      | 1,053.6 ns | 15.21 ns |   4.21 KB |
