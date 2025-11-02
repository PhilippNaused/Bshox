```

BenchmarkDotNet v0.15.5, Windows 11 (10.0.26200.6901)
Intel Core i7-14700KF 3.40GHz, 1 CPU, 28 logical and 20 physical cores
.NET SDK 10.0.100-rc.2.25502.107
  [Host]    : .NET 10.0.0 (10.0.0-rc.2.25502.107, 10.0.25.50307), X64 RyuJIT x86-64-v3
  MediumRun : .NET 10.0.0 (10.0.0-rc.2.25502.107, 10.0.25.50307), X64 RyuJIT x86-64-v3

Job=MediumRun  IterationCount=15  LaunchCount=2
WarmupCount=10

```
| Method           | Count |            Mean |        Error | Ratio |   Allocated | Alloc Ratio |
|------------------|-------|----------------:|-------------:|------:|------------:|------------:|
| Bshox            | 1     |        714.3 ns |      2.77 ns |  1.00 |     3.05 KB |        1.00 |
| System.Text.Json | 1     |     20,052.3 ns |    155.17 ns | 28.08 |     9.23 KB |        3.03 |
| MessagePack      | 1     |      1,914.0 ns |     13.67 ns |  2.68 |     4.38 KB |        1.44 |
| protobuf-net     | 1     |      6,613.7 ns |     20.78 ns |  9.26 |     8.51 KB |        2.79 |
| Google.Protobuf  | 1     |      5,490.4 ns |    200.12 ns |  7.69 |      4.3 KB |        1.41 |
|                  |       |                 |              |       |             |             |
| Bshox            | 1000  |  1,007,390.1 ns |  5,500.10 ns |  1.00 |  2954.04 KB |        1.00 |
| System.Text.Json | 1000  | 23,520,004.1 ns | 64,825.62 ns | 23.35 |  9191.71 KB |        3.11 |
| MessagePack      | 1000  |  2,462,286.8 ns | 72,481.44 ns |  2.44 |  4363.35 KB |        1.48 |
| protobuf-net     | 1000  |  7,570,466.3 ns | 58,116.70 ns |  7.52 | 12753.84 KB |        4.32 |
| Google.Protobuf  | 1000  |  8,090,416.1 ns | 57,090.99 ns |  8.03 |  4221.61 KB |        1.43 |
