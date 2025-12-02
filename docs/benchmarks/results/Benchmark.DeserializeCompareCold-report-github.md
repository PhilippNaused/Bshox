```

BenchmarkDotNet v0.15.8, Windows 11 (10.0.26200.7171/25H2/2025Update/HudsonValley2)
Intel Core i7-14700KF 3.40GHz, 1 CPU, 28 logical and 20 physical cores
.NET SDK 10.0.100
  [Host] : .NET 10.0.0 (10.0.0, 10.0.25.52411), X64 RyuJIT x86-64-v3
  Dry    : .NET 10.0.0 (10.0.0, 10.0.25.52411), X64 RyuJIT x86-64-v3

Job=Dry  IterationCount=1  LaunchCount=25
RunStrategy=ColdStart  UnrollFactor=1  WarmupCount=1

```
| Method           |      Mean |     Error | Ratio | Allocated | Alloc Ratio |
|------------------|----------:|----------:|------:|----------:|------------:|
| Bshox            |  1.925 ms | 0.0761 ms |  1.00 |   4.16 KB |        1.00 |
| System.Text.Json |  7.109 ms | 0.2238 ms |  3.70 |  15.45 KB |        3.72 |
| MessagePack      |  4.282 ms | 0.1342 ms |  2.23 |   4.16 KB |        1.00 |
| protobuf-net     | 22.128 ms | 0.2274 ms | 11.52 |   4.29 KB |        1.03 |
| Google.Protobuf  |  2.748 ms | 0.0558 ms |  1.43 |   15.5 KB |        3.73 |
