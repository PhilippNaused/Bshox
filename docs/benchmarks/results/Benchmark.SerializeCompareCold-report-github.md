```

BenchmarkDotNet v0.15.3, Windows 11 (10.0.26100.6584/24H2/2024Update/HudsonValley)
Intel Core i7-14700KF 3.40GHz, 1 CPU, 28 logical and 20 physical cores
.NET SDK 10.0.100-rc.1.25451.107
  [Host] : .NET 10.0.0 (10.0.0-rc.1.25451.107, 10.0.25.45207), X64 RyuJIT x86-64-v3
  Dry    : .NET 10.0.0 (10.0.0-rc.1.25451.107, 10.0.25.45207), X64 RyuJIT x86-64-v3

Job=Dry  IterationCount=1  LaunchCount=25
RunStrategy=ColdStart  UnrollFactor=1  WarmupCount=1

```
| Method           | Count |       Mean |     Error | Ratio |  Allocated | Alloc Ratio |
|------------------|-------|-----------:|----------:|------:|-----------:|------------:|
| Bshox            | 1     |   4.630 ms | 0.1557 ms |  1.00 |    3.05 KB |        1.00 |
| System.Text.Json | 1     |  22.290 ms | 0.3447 ms |  4.82 |    9.23 KB |        3.03 |
| MessagePack      | 1     |  23.235 ms | 0.3050 ms |  5.03 |    4.38 KB |        1.44 |
| protobuf-net     | 1     |  11.832 ms | 0.3814 ms |  2.56 |    8.51 KB |        2.79 |
| Google.Protobuf  | 1     |   6.440 ms | 0.1599 ms |  1.39 |     4.3 KB |        1.41 |
|                  |       |            |           |       |            |             |
| Bshox            | 1000  |  11.499 ms | 0.1568 ms |  1.00 | 2953.62 KB |        1.00 |
| System.Text.Json | 1000  | 114.141 ms | 0.5619 ms |  9.93 | 9191.55 KB |        3.11 |
| MessagePack      | 1000  |  35.880 ms | 0.3386 ms |  3.12 |  4363.3 KB |        1.48 |
| protobuf-net     | 1000  |  41.832 ms | 0.2924 ms |  3.64 | 12758.2 KB |        4.32 |
| Google.Protobuf  | 1000  |  48.522 ms | 0.3270 ms |  4.22 | 4220.64 KB |        1.43 |
