```

BenchmarkDotNet v0.15.6, Windows 11 (10.0.26200.7171)
Intel Core i7-14700KF 3.40GHz, 1 CPU, 28 logical and 20 physical cores
.NET SDK 10.0.100
  [Host] : .NET 10.0.0 (10.0.0, 10.0.25.52411), X64 RyuJIT x86-64-v3
  Dry    : .NET 10.0.0 (10.0.0, 10.0.25.52411), X64 RyuJIT x86-64-v3

Job=Dry  IterationCount=1  LaunchCount=25
RunStrategy=ColdStart  UnrollFactor=1  WarmupCount=1

```
| Method           | Count |       Mean |     Error | Ratio |  Allocated | Alloc Ratio |
|------------------|-------|-----------:|----------:|------:|-----------:|------------:|
| Bshox            | 1     |   4.810 ms | 0.1136 ms |  1.00 |    3.05 KB |        1.00 |
| System.Text.Json | 1     |  20.992 ms | 0.3864 ms |  4.37 |    9.23 KB |        3.03 |
| MessagePack      | 1     |  22.918 ms | 0.4274 ms |  4.77 |    4.38 KB |        1.44 |
| protobuf-net     | 1     |  11.669 ms | 0.2692 ms |  2.43 |    8.51 KB |        2.79 |
| Google.Protobuf  | 1     |   6.429 ms | 0.2031 ms |  1.34 |     4.3 KB |        1.41 |
|                  |       |            |           |       |            |             |
| Bshox            | 1000  |  11.736 ms | 0.1965 ms |  1.00 | 2953.62 KB |        1.00 |
| System.Text.Json | 1000  | 112.548 ms | 0.7849 ms |  9.59 | 9191.55 KB |        3.11 |
| MessagePack      | 1000  |  34.969 ms | 0.4063 ms |  2.98 |  4363.3 KB |        1.48 |
| protobuf-net     | 1000  |  41.326 ms | 0.7025 ms |  3.52 | 12758.4 KB |        4.32 |
| Google.Protobuf  | 1000  |  47.302 ms | 0.4407 ms |  4.03 | 4220.64 KB |        1.43 |
