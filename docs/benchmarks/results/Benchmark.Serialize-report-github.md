```

BenchmarkDotNet v0.15.8, Windows 11 (10.0.26200.8246/25H2/2025Update/HudsonValley2)
Intel Core i7-14700KF 3.40GHz, 1 CPU, 28 logical and 20 physical cores
.NET SDK 10.0.202
  [Host]    : .NET 10.0.6 (10.0.6, 10.0.626.17701), X64 RyuJIT x86-64-v3
  MediumRun : .NET 10.0.6 (10.0.6, 10.0.626.17701), X64 RyuJIT x86-64-v3

Job=MediumRun  MaxRelativeError=0.01  IterationCount=15
LaunchCount=2  WarmupCount=10

```
| Method | Count |         Mean |       Error |  Allocated |
|--------|-------|-------------:|------------:|-----------:|
| Bshox  | 1     |     727.4 ns |    16.29 ns |    3.05 KB |
| Bshox  | 1000  | 956,017.6 ns | 8,758.41 ns | 2953.83 KB |
