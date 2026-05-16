```

BenchmarkDotNet v0.15.8, Windows 11 (10.0.26200.8457/25H2/2025Update/HudsonValley2)
Intel Core i7-14700KF 3.40GHz, 1 CPU, 28 logical and 20 physical cores
.NET SDK 10.0.300
  [Host]    : .NET 10.0.8 (10.0.8, 10.0.826.23019), X64 RyuJIT x86-64-v3
  MediumRun : .NET 10.0.8 (10.0.8, 10.0.826.23019), X64 RyuJIT x86-64-v3

Job=MediumRun  MaxRelativeError=0.01  IterationCount=15
LaunchCount=2  WarmupCount=10

```
| Method | Count |         Mean |       Error |  Allocated |
|--------|-------|-------------:|------------:|-----------:|
| Bshox  | 1     |     623.6 ns |     5.30 ns |    3.05 KB |
| Bshox  | 1000  | 793,228.8 ns | 6,216.70 ns | 2953.81 KB |
