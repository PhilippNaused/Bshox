```

BenchmarkDotNet v0.15.2, Windows 11 (10.0.26100.4946/24H2/2024Update/HudsonValley)
Unknown processor
.NET SDK 10.0.100-preview.7.25380.108
  [Host] : .NET 10.0.0 (10.0.25.38108), X64 RyuJIT AVX2
  Dry    : .NET 10.0.0 (10.0.25.38108), X64 RyuJIT AVX2

Job=Dry  IterationCount=1  LaunchCount=25
RunStrategy=ColdStart  UnrollFactor=1  WarmupCount=1

```
| Method           | Count |       Mean |     Error | Ratio |   Allocated | Alloc Ratio |
|------------------|-------|-----------:|----------:|------:|------------:|------------:|
| Bshox            | 1     |   4.294 ms | 0.0941 ms |  1.00 |        3 KB |        1.00 |
| System.Text.Json | 1     |  20.898 ms | 0.3143 ms |  4.87 |     9.23 KB |        3.08 |
| MessagePack      | 1     |  21.856 ms | 0.2016 ms |  5.09 |     4.38 KB |        1.46 |
| protobuf-net     | 1     |  11.279 ms | 0.2275 ms |  2.63 |     8.51 KB |        2.84 |
| Google.Protobuf  | 1     |   6.181 ms | 0.1689 ms |  1.44 |      4.3 KB |        1.43 |
|                  |       |            |           |       |             |             |
| Bshox            | 1000  |  12.882 ms | 0.5822 ms |  1.00 |  2939.21 KB |        1.00 |
| System.Text.Json | 1000  | 112.334 ms | 1.2081 ms |  8.75 |  9191.55 KB |        3.13 |
| MessagePack      | 1000  |  34.534 ms | 0.1830 ms |  2.69 |   4363.3 KB |        1.48 |
| protobuf-net     | 1000  |  40.850 ms | 0.1942 ms |  3.18 | 12758.01 KB |        4.34 |
| Google.Protobuf  | 1000  |  48.645 ms | 0.2751 ms |  3.79 |  4220.64 KB |        1.44 |
