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
| Bshox            | 1     |   4.341 ms | 0.0914 ms |  1.00 |     3.05 KB |        1.00 |
| System.Text.Json | 1     |  21.054 ms | 0.5241 ms |  4.85 |     9.23 KB |        3.03 |
| MessagePack      | 1     |  21.913 ms | 0.1540 ms |  5.05 |     4.38 KB |        1.44 |
| protobuf-net     | 1     |  11.373 ms | 0.1697 ms |  2.62 |     8.51 KB |        2.79 |
| Google.Protobuf  | 1     |   6.175 ms | 0.1140 ms |  1.42 |      4.3 KB |        1.41 |
|                  |       |            |           |       |             |             |
| Bshox            | 1000  |  11.547 ms | 0.1117 ms |  1.00 |  2953.62 KB |        1.00 |
| System.Text.Json | 1000  | 112.310 ms | 0.3178 ms |  9.73 |  9191.55 KB |        3.11 |
| MessagePack      | 1000  |  34.758 ms | 0.2562 ms |  3.01 |   4363.3 KB |        1.48 |
| protobuf-net     | 1000  |  40.931 ms | 0.1945 ms |  3.55 | 12758.32 KB |        4.32 |
| Google.Protobuf  | 1000  |  49.002 ms | 0.2909 ms |  4.24 |  4220.64 KB |        1.43 |
