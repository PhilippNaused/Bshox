```

BenchmarkDotNet v0.15.8, Windows 11 (10.0.26200.8246/25H2/2025Update/HudsonValley2)
Intel Core i7-14700KF 3.40GHz, 1 CPU, 28 logical and 20 physical cores
.NET SDK 10.0.202
  [Host] : .NET 10.0.6 (10.0.6, 10.0.626.17701), X64 RyuJIT x86-64-v3
  Dry    : .NET 10.0.6 (10.0.6, 10.0.626.17701), X64 RyuJIT x86-64-v3

Job=Dry  IterationCount=1  LaunchCount=25
RunStrategy=ColdStart  UnrollFactor=1  WarmupCount=1

```
| Method | Count |      Mean |     Error |  Allocated |
|--------|-------|----------:|----------:|-----------:|
| Bshox  | 1     |  5.858 ms | 0.1469 ms |    3.05 KB |
| Bshox  | 1000  | 13.136 ms | 0.1105 ms | 2953.61 KB |
