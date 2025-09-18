```

BenchmarkDotNet v0.15.3, Windows 11 (10.0.26100.6584/24H2/2024Update/HudsonValley)
Intel Core i7-14700KF 3.40GHz, 1 CPU, 28 logical and 20 physical cores
.NET SDK 10.0.100-rc.1.25451.107
  [Host] : .NET 10.0.0 (10.0.0-rc.1.25451.107, 10.0.25.45207), X64 RyuJIT x86-64-v3
  Dry    : .NET 10.0.0 (10.0.0-rc.1.25451.107, 10.0.25.45207), X64 RyuJIT x86-64-v3

Job=Dry  IterationCount=1  LaunchCount=25
RunStrategy=ColdStart  UnrollFactor=1  WarmupCount=1

```
| Method | Count |      Mean |     Error |  Allocated |
|--------|-------|----------:|----------:|-----------:|
| Bshox  | 1     |  4.838 ms | 0.0840 ms |    3.05 KB |
| Bshox  | 1000  | 11.588 ms | 0.1712 ms | 2953.61 KB |
