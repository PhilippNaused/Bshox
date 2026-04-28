```

BenchmarkDotNet v0.15.8, Windows 11 (10.0.26200.8246/25H2/2025Update/HudsonValley2)
Intel Core i7-14700KF 3.40GHz, 1 CPU, 28 logical and 20 physical cores
.NET SDK 10.0.203
  [Host]    : .NET 10.0.7 (10.0.7, 10.0.726.21808), X64 RyuJIT x86-64-v3
  MediumRun : .NET 10.0.7 (10.0.7, 10.0.726.21808), X64 RyuJIT x86-64-v3

Job=MediumRun  MaxRelativeError=0.01  IterationCount=15
LaunchCount=2  WarmupCount=10

```
| Method | Count |         Mean |       Error |  Allocated |
|--------|-------|-------------:|------------:|-----------:|
| Bshox  | 1     |     703.3 ns |     1.83 ns |    3.05 KB |
| Bshox  | 1000  | 908,815.1 ns | 4,970.74 ns | 2953.84 KB |
