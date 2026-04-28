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
| Bshox            | 1     |   5.455 ms | 0.1102 ms |  1.00 |     3.05 KB |        1.00 |
| System.Text.Json | 1     |  19.375 ms | 0.2424 ms |  3.55 |     9.23 KB |        3.03 |
| MessagePack      | 1     |  24.603 ms | 0.8815 ms |  4.51 |     4.38 KB |        1.44 |
| protobuf-net     | 1     |  12.812 ms | 0.1107 ms |  2.35 |     8.51 KB |        2.79 |
| Google.Protobuf  | 1     |   7.243 ms | 0.1748 ms |  1.33 |      4.3 KB |        1.41 |
|                  |       |            |           |       |             |             |
| Bshox            | 1000  |  12.935 ms | 0.2489 ms |  1.00 |  2953.62 KB |        1.00 |
| System.Text.Json | 1000  | 110.536 ms | 0.7352 ms |  8.55 |  9191.55 KB |        3.11 |
| MessagePack      | 1000  |  37.399 ms | 0.4924 ms |  2.89 |   4363.3 KB |        1.48 |
| protobuf-net     | 1000  |  42.492 ms | 0.8527 ms |  3.29 | 12758.01 KB |        4.32 |
| Google.Protobuf  | 1000  |  48.817 ms | 0.4050 ms |  3.78 |  4220.64 KB |        1.43 |
