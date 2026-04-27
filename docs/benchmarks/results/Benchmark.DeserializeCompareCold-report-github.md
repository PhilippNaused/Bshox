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
| Bshox            |  2.247 ms | 0.0753 ms |  1.00 |   4.16 KB |        1.00 |
| System.Text.Json |  6.039 ms | 0.1567 ms |  2.69 |  15.45 KB |        3.72 |
| MessagePack      |  4.907 ms | 0.0999 ms |  2.19 |   4.16 KB |        1.00 |
| protobuf-net     | 24.514 ms | 0.1951 ms | 10.93 |   4.29 KB |        1.03 |
| Google.Protobuf  |  3.269 ms | 0.1135 ms |  1.46 |   15.5 KB |        3.73 |
