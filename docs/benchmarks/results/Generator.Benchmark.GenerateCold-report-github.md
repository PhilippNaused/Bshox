```

BenchmarkDotNet v0.15.8, Windows 11 (10.0.26200.8246/25H2/2025Update/HudsonValley2)
Intel Core i7-14700KF 3.40GHz, 1 CPU, 28 logical and 20 physical cores
.NET SDK 10.0.202
  [Host] : .NET 10.0.6 (10.0.6, 10.0.626.17701), X64 RyuJIT x86-64-v3
  Dry    : .NET 10.0.6 (10.0.6, 10.0.626.17701), X64 RyuJIT x86-64-v3

Job=Dry  IterationCount=1  LaunchCount=10
RunStrategy=ColdStart  UnrollFactor=1  WarmupCount=1

```
| Method         |     Mean |   Error | Ratio | RatioSD | Allocated | Alloc Ratio |
|----------------|---------:|--------:|------:|--------:|----------:|------------:|
| Base           | 420.8 ms | 4.40 ms |  1.00 |    0.01 |   1.34 MB |        1.00 |
| BshoxGenerator | 862.9 ms | 6.71 ms |  2.05 |    0.02 |   7.43 MB |        5.55 |
| JsonGenerator  | 830.6 ms | 5.57 ms |  1.97 |    0.02 |   8.89 MB |        6.64 |
