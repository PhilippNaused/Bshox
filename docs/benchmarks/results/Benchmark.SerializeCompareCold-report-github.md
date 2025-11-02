```

BenchmarkDotNet v0.15.5, Windows 11 (10.0.26200.6901)
Intel Core i7-14700KF 3.40GHz, 1 CPU, 28 logical and 20 physical cores
.NET SDK 10.0.100-rc.2.25502.107
  [Host] : .NET 10.0.0 (10.0.0-rc.2.25502.107, 10.0.25.50307), X64 RyuJIT x86-64-v3
  Dry    : .NET 10.0.0 (10.0.0-rc.2.25502.107, 10.0.25.50307), X64 RyuJIT x86-64-v3

Job=Dry  IterationCount=1  LaunchCount=25
RunStrategy=ColdStart  UnrollFactor=1  WarmupCount=1

```
| Method           | Count |       Mean |     Error | Ratio |   Allocated | Alloc Ratio |
|------------------|-------|-----------:|----------:|------:|------------:|------------:|
| Bshox            | 1     |   4.693 ms | 0.1221 ms |  1.00 |     3.05 KB |        1.00 |
| System.Text.Json | 1     |  21.190 ms | 0.3258 ms |  4.52 |     9.23 KB |        3.03 |
| MessagePack      | 1     |  22.666 ms | 0.3843 ms |  4.84 |     4.38 KB |        1.44 |
| protobuf-net     | 1     |  11.521 ms | 0.2583 ms |  2.46 |     8.51 KB |        2.79 |
| Google.Protobuf  | 1     |   6.199 ms | 0.1462 ms |  1.32 |      4.3 KB |        1.41 |
|                  |       |            |           |       |             |             |
| Bshox            | 1000  |  11.593 ms | 0.1464 ms |  1.00 |  2953.62 KB |        1.00 |
| System.Text.Json | 1000  | 115.266 ms | 0.8863 ms |  9.95 |  9191.55 KB |        3.11 |
| MessagePack      | 1000  |  35.202 ms | 0.3511 ms |  3.04 |   4363.3 KB |        1.48 |
| protobuf-net     | 1000  |  40.864 ms | 0.4847 ms |  3.53 | 12758.24 KB |        4.32 |
| Google.Protobuf  | 1000  |  48.081 ms | 0.2472 ms |  4.15 |  4220.64 KB |        1.43 |
