```

BenchmarkDotNet v0.15.2, Windows 11 (10.0.26100.4946/24H2/2024Update/HudsonValley)
Unknown processor
.NET SDK 10.0.100-preview.7.25380.108
  [Host]    : .NET 10.0.0 (10.0.25.38108), X64 RyuJIT AVX2
  MediumRun : .NET 10.0.0 (10.0.25.38108), X64 RyuJIT AVX2

Job=MediumRun  IterationCount=15  LaunchCount=2
WarmupCount=10

```
| Method           | Count |            Mean |         Error | Ratio |   Allocated | Alloc Ratio |
|------------------|-------|----------------:|--------------:|------:|------------:|------------:|
| Bshox            | 1     |        702.4 ns |       2.06 ns |  1.00 |        3 KB |        1.00 |
| System.Text.Json | 1     |     20,178.8 ns |     300.68 ns | 28.73 |     9.23 KB |        3.08 |
| MessagePack      | 1     |      1,912.8 ns |      28.40 ns |  2.72 |     4.38 KB |        1.46 |
| protobuf-net     | 1     |      6,870.5 ns |     151.92 ns |  9.78 |     8.51 KB |        2.84 |
| Google.Protobuf  | 1     |      5,591.0 ns |      56.19 ns |  7.96 |      4.3 KB |        1.43 |
|                  |       |                 |               |       |             |             |
| Bshox            | 1000  |  1,381,635.1 ns |  36,747.02 ns |  1.00 |  2940.33 KB |        1.00 |
| System.Text.Json | 1000  | 23,521,259.2 ns |  55,917.64 ns | 17.05 |  9191.71 KB |        3.13 |
| MessagePack      | 1000  |  2,457,730.1 ns |  39,652.74 ns |  1.78 |  4363.35 KB |        1.48 |
| protobuf-net     | 1000  |  7,756,232.8 ns |  29,487.49 ns |  5.62 | 12753.63 KB |        4.34 |
| Google.Protobuf  | 1000  |  7,520,700.6 ns | 112,113.85 ns |  5.45 |  4221.79 KB |        1.44 |
