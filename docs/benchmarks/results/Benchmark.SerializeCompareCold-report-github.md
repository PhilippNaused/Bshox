```

BenchmarkDotNet v0.15.4, Windows 11 (10.0.26200.6899)
Intel Core i7-14700KF 3.40GHz, 1 CPU, 28 logical and 20 physical cores
.NET SDK 10.0.100-rc.2.25502.107
  [Host] : .NET 10.0.0 (10.0.0-rc.2.25502.107, 10.0.25.50307), X64 RyuJIT x86-64-v3
  Dry    : .NET 10.0.0 (10.0.0-rc.2.25502.107, 10.0.25.50307), X64 RyuJIT x86-64-v3

Job=Dry  IterationCount=1  LaunchCount=25
RunStrategy=ColdStart  UnrollFactor=1  WarmupCount=1

```
| Method           | Count |       Mean |     Error | Ratio |   Allocated | Alloc Ratio |
|------------------|-------|-----------:|----------:|------:|------------:|------------:|
| Bshox            | 1     |   4.580 ms | 0.1403 ms |  1.00 |     3.05 KB |        1.00 |
| System.Text.Json | 1     |  21.072 ms | 0.9444 ms |  4.61 |     9.23 KB |        3.03 |
| MessagePack      | 1     |  22.163 ms | 0.4412 ms |  4.85 |     4.38 KB |        1.44 |
| protobuf-net     | 1     |  11.518 ms | 0.2288 ms |  2.52 |     8.51 KB |        2.79 |
| Google.Protobuf  | 1     |   6.077 ms | 0.1712 ms |  1.33 |      4.3 KB |        1.41 |
|                  |       |            |           |       |             |             |
| Bshox            | 1000  |  11.540 ms | 0.2158 ms |  1.00 |  2953.62 KB |        1.00 |
| System.Text.Json | 1000  | 113.516 ms | 0.6232 ms |  9.84 |  9191.55 KB |        3.11 |
| MessagePack      | 1000  |  34.513 ms | 0.3115 ms |  2.99 |   4363.3 KB |        1.48 |
| protobuf-net     | 1000  |  40.824 ms | 0.3975 ms |  3.54 | 12758.01 KB |        4.32 |
| Google.Protobuf  | 1000  |  47.955 ms | 0.4183 ms |  4.16 |  4220.64 KB |        1.43 |
