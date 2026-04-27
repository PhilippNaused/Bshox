```

BenchmarkDotNet v0.15.8, Windows 11 (10.0.26200.8246/25H2/2025Update/HudsonValley2)
Intel Core i7-14700KF 3.40GHz, 1 CPU, 28 logical and 20 physical cores
.NET SDK 10.0.203
  [Host] : .NET 10.0.7 (10.0.7, 10.0.726.21808), X64 RyuJIT x86-64-v3
  Dry    : .NET 10.0.7 (10.0.7, 10.0.726.21808), X64 RyuJIT x86-64-v3

Job=Dry  IterationCount=1  LaunchCount=25
RunStrategy=ColdStart  UnrollFactor=1  WarmupCount=1

```
| Method           |      Mean |     Error | Ratio | Allocated | Alloc Ratio |
|------------------|----------:|----------:|------:|----------:|------------:|
| Bshox            |  2.293 ms | 0.0583 ms |  1.00 |   4.16 KB |        1.00 |
| System.Text.Json |  6.089 ms | 0.0802 ms |  2.66 |  15.45 KB |        3.72 |
| MessagePack      |  4.995 ms | 0.0892 ms |  2.18 |   4.16 KB |        1.00 |
| protobuf-net     | 24.605 ms | 0.0909 ms | 10.74 |   4.29 KB |        1.03 |
| Google.Protobuf  |  3.213 ms | 0.0827 ms |  1.40 |   15.5 KB |        3.73 |
