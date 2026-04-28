```

BenchmarkDotNet v0.15.8, Windows 11 (10.0.26200.8246/25H2/2025Update/HudsonValley2)
Intel Core i7-14700KF 3.40GHz, 1 CPU, 28 logical and 20 physical cores
.NET SDK 10.0.203
  [Host] : .NET 10.0.7 (10.0.7, 10.0.726.21808), X64 RyuJIT x86-64-v3
  Dry    : .NET 10.0.7 (10.0.7, 10.0.726.21808), X64 RyuJIT x86-64-v3

Job=Dry  IterationCount=1  LaunchCount=25
RunStrategy=ColdStart  UnrollFactor=1  WarmupCount=1

```
| Method           | Count |       Mean |     Error | Ratio |   Allocated | Alloc Ratio |
|------------------|-------|-----------:|----------:|------:|------------:|------------:|
| Bshox            | 1     |   5.530 ms | 0.1412 ms |  1.00 |     3.05 KB |        1.00 |
| System.Text.Json | 1     |  19.525 ms | 0.2467 ms |  3.53 |     9.23 KB |        3.03 |
| MessagePack      | 1     |  24.513 ms | 0.2848 ms |  4.44 |     4.38 KB |        1.44 |
| protobuf-net     | 1     |  12.930 ms | 0.2269 ms |  2.34 |     8.51 KB |        2.79 |
| Google.Protobuf  | 1     |   7.216 ms | 0.1125 ms |  1.31 |      4.3 KB |        1.41 |
|                  |       |            |           |       |             |             |
| Bshox            | 1000  |  12.989 ms | 0.2090 ms |  1.00 |  2953.62 KB |        1.00 |
| System.Text.Json | 1000  | 110.666 ms | 0.9367 ms |  8.52 |  9191.55 KB |        3.11 |
| MessagePack      | 1000  |  37.174 ms | 0.3696 ms |  2.86 |   4363.3 KB |        1.48 |
| protobuf-net     | 1000  |  41.843 ms | 0.5607 ms |  3.22 | 12757.89 KB |        4.32 |
| Google.Protobuf  | 1000  |  48.414 ms | 0.2999 ms |  3.73 |  4220.64 KB |        1.43 |
