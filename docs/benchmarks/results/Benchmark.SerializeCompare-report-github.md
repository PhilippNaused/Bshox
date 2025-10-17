```

BenchmarkDotNet v0.15.4, Windows 11 (10.0.26200.6899)
Intel Core i7-14700KF 3.40GHz, 1 CPU, 28 logical and 20 physical cores
.NET SDK 10.0.100-rc.2.25502.107
  [Host]    : .NET 10.0.0 (10.0.0-rc.2.25502.107, 10.0.25.50307), X64 RyuJIT x86-64-v3
  MediumRun : .NET 10.0.0 (10.0.0-rc.2.25502.107, 10.0.25.50307), X64 RyuJIT x86-64-v3

Job=MediumRun  IterationCount=15  LaunchCount=2
WarmupCount=10

```
| Method           | Count |            Mean |        Error | Ratio |   Allocated | Alloc Ratio |
|------------------|-------|----------------:|-------------:|------:|------------:|------------:|
| Bshox            | 1     |        727.7 ns |     15.13 ns |  1.00 |     3.05 KB |        1.00 |
| System.Text.Json | 1     |     20,045.2 ns |     71.24 ns | 27.57 |     9.23 KB |        3.03 |
| MessagePack      | 1     |      1,941.6 ns |     21.27 ns |  2.67 |     4.38 KB |        1.44 |
| protobuf-net     | 1     |      6,628.0 ns |     84.15 ns |  9.12 |     8.51 KB |        2.79 |
| Google.Protobuf  | 1     |      5,583.6 ns |     39.63 ns |  7.68 |      4.3 KB |        1.41 |
|                  |       |                 |              |       |             |             |
| Bshox            | 1000  |  1,032,625.7 ns | 14,461.18 ns |  1.00 |  2954.12 KB |        1.00 |
| System.Text.Json | 1000  | 23,619,883.4 ns | 93,259.13 ns | 22.88 |  9191.71 KB |        3.11 |
| MessagePack      | 1000  |  2,444,274.8 ns | 30,474.14 ns |  2.37 |  4363.35 KB |        1.48 |
| protobuf-net     | 1000  |  7,889,676.7 ns | 86,220.48 ns |  7.64 | 12753.63 KB |        4.32 |
| Google.Protobuf  | 1000  |  7,852,422.3 ns | 93,054.14 ns |  7.61 |   4221.6 KB |        1.43 |
