```

BenchmarkDotNet v0.15.8, Windows 11 (10.0.26200.8246/25H2/2025Update/HudsonValley2)
Intel Core i7-14700KF 3.40GHz, 1 CPU, 28 logical and 20 physical cores
.NET SDK 10.0.202
  [Host]    : .NET 10.0.6 (10.0.6, 10.0.626.17701), X64 RyuJIT x86-64-v3
  MediumRun : .NET 10.0.6 (10.0.6, 10.0.626.17701), X64 RyuJIT x86-64-v3

Job=MediumRun  MaxRelativeError=0.01  IterationCount=15
LaunchCount=2  WarmupCount=10

```
| Method           | Count |            Mean |         Error | Ratio |   Allocated | Alloc Ratio |
|------------------|-------|----------------:|--------------:|------:|------------:|------------:|
| Bshox            | 1     |        738.9 ns |       6.49 ns |  1.00 |     3.05 KB |        1.00 |
| System.Text.Json | 1     |     20,828.4 ns |     431.18 ns | 28.19 |     9.23 KB |        3.03 |
| MessagePack      | 1     |      1,956.9 ns |      18.68 ns |  2.65 |     4.38 KB |        1.44 |
| protobuf-net     | 1     |      6,481.6 ns |     100.62 ns |  8.77 |     8.51 KB |        2.79 |
| Google.Protobuf  | 1     |      5,422.4 ns |     118.55 ns |  7.34 |      4.3 KB |        1.41 |
|                  |       |                 |               |       |             |             |
| Bshox            | 1000  |    999,930.7 ns |   5,551.34 ns |  1.00 |  2954.12 KB |        1.00 |
| System.Text.Json | 1000  | 23,547,740.7 ns | 170,246.37 ns | 23.55 |  9191.71 KB |        3.11 |
| MessagePack      | 1000  |  2,426,014.1 ns |  19,926.01 ns |  2.43 |  4363.35 KB |        1.48 |
| protobuf-net     | 1000  |  7,852,404.7 ns |  66,646.95 ns |  7.85 | 12753.93 KB |        4.32 |
| Google.Protobuf  | 1000  |  7,949,365.2 ns |  49,592.10 ns |  7.95 |  4221.95 KB |        1.43 |
