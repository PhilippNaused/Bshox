```

BenchmarkDotNet v0.15.8, Windows 11 (10.0.26200.8246/25H2/2025Update/HudsonValley2)
Intel Core i7-14700KF 3.40GHz, 1 CPU, 28 logical and 20 physical cores
.NET SDK 10.0.203
  [Host]    : .NET 10.0.7 (10.0.7, 10.0.726.21808), X64 RyuJIT x86-64-v3
  MediumRun : .NET 10.0.7 (10.0.7, 10.0.726.21808), X64 RyuJIT x86-64-v3

Job=MediumRun  MaxRelativeError=0.01  IterationCount=15
LaunchCount=2  WarmupCount=10

```
| Method           | Count |            Mean |         Error | Ratio |   Allocated | Alloc Ratio |
|------------------|-------|----------------:|--------------:|------:|------------:|------------:|
| Bshox            | 1     |        700.1 ns |       1.32 ns |  1.00 |     3.05 KB |        1.00 |
| System.Text.Json | 1     |     19,869.9 ns |     264.14 ns | 28.38 |     9.23 KB |        3.03 |
| MessagePack      | 1     |      1,829.5 ns |       5.92 ns |  2.61 |     4.38 KB |        1.44 |
| protobuf-net     | 1     |      6,348.2 ns |      26.56 ns |  9.07 |     8.51 KB |        2.79 |
| Google.Protobuf  | 1     |      5,532.2 ns |     101.65 ns |  7.90 |      4.3 KB |        1.41 |
|                  |       |                 |               |       |             |             |
| Bshox            | 1000  |    992,596.1 ns |  12,043.77 ns |  1.00 |  2954.13 KB |        1.00 |
| System.Text.Json | 1000  | 23,338,282.6 ns | 230,479.01 ns | 23.52 |  9191.71 KB |        3.11 |
| MessagePack      | 1000  |  2,435,213.1 ns | 115,156.53 ns |  2.45 |  4363.35 KB |        1.48 |
| protobuf-net     | 1000  |  7,692,921.4 ns | 121,740.33 ns |  7.75 | 12753.57 KB |        4.32 |
| Google.Protobuf  | 1000  |  7,738,862.3 ns |  71,255.75 ns |  7.80 |  4221.44 KB |        1.43 |
