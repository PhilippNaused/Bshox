```

BenchmarkDotNet v0.15.8, Windows 11 (10.0.26200.7171/25H2/2025Update/HudsonValley2)
Intel Core i7-14700KF 3.40GHz, 1 CPU, 28 logical and 20 physical cores
.NET SDK 10.0.100
  [Host]    : .NET 10.0.0 (10.0.0, 10.0.25.52411), X64 RyuJIT x86-64-v3
  MediumRun : .NET 10.0.0 (10.0.0, 10.0.25.52411), X64 RyuJIT x86-64-v3

Job=MediumRun  MaxRelativeError=0.01  IterationCount=15
LaunchCount=2  WarmupCount=10

```
| Method | Segmented |       Mean |    Error | Allocated |
|--------|-----------|-----------:|---------:|----------:|
| Bshox  | False     |   942.1 ns | 15.96 ns |   4.16 KB |
| Bshox  | True      | 1,059.1 ns | 17.67 ns |   4.21 KB |
