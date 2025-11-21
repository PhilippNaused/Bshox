```

BenchmarkDotNet v0.15.6, Windows 11 (10.0.26200.7171)
Intel Core i7-14700KF 3.40GHz, 1 CPU, 28 logical and 20 physical cores
.NET SDK 10.0.100
  [Host]    : .NET 10.0.0 (10.0.0, 10.0.25.52411), X64 RyuJIT x86-64-v3
  MediumRun : .NET 10.0.0 (10.0.0, 10.0.25.52411), X64 RyuJIT x86-64-v3

Job=MediumRun  IterationCount=15  LaunchCount=2
WarmupCount=10

```
| Method           | Count |            Mean |        Error | Ratio |   Allocated | Alloc Ratio |
|------------------|-------|----------------:|-------------:|------:|------------:|------------:|
| Bshox            | 1     |        708.9 ns |     15.65 ns |  1.00 |     3.05 KB |        1.00 |
| System.Text.Json | 1     |     19,350.3 ns |     83.49 ns | 27.32 |     9.23 KB |        3.03 |
| MessagePack      | 1     |      1,840.3 ns |      5.48 ns |  2.60 |     4.38 KB |        1.44 |
| protobuf-net     | 1     |      6,551.1 ns |     49.06 ns |  9.25 |     8.51 KB |        2.79 |
| Google.Protobuf  | 1     |      5,496.9 ns |     97.35 ns |  7.76 |      4.3 KB |        1.41 |
|                  |       |                 |              |       |             |             |
| Bshox            | 1000  |  1,000,495.3 ns | 12,916.68 ns |  1.00 |  2954.14 KB |        1.00 |
| System.Text.Json | 1000  | 22,931,161.1 ns | 58,306.81 ns | 22.93 |  9191.71 KB |        3.11 |
| MessagePack      | 1000  |  2,363,190.4 ns | 21,157.00 ns |  2.36 |  4363.35 KB |        1.48 |
| protobuf-net     | 1000  |  7,489,908.4 ns | 67,366.32 ns |  7.49 | 12753.84 KB |        4.32 |
| Google.Protobuf  | 1000  |  7,934,657.0 ns | 60,270.21 ns |  7.93 |  4221.61 KB |        1.43 |
