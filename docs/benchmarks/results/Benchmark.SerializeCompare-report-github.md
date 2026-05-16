```

BenchmarkDotNet v0.15.8, Windows 11 (10.0.26200.8457/25H2/2025Update/HudsonValley2)
Intel Core i7-14700KF 3.40GHz, 1 CPU, 28 logical and 20 physical cores
.NET SDK 10.0.300
  [Host]    : .NET 10.0.8 (10.0.8, 10.0.826.23019), X64 RyuJIT x86-64-v3
  MediumRun : .NET 10.0.8 (10.0.8, 10.0.826.23019), X64 RyuJIT x86-64-v3

Job=MediumRun  MaxRelativeError=0.01  IterationCount=15
LaunchCount=2  WarmupCount=10

```
| Method           | Count |          Mean |       Error | Ratio |   Allocated | Alloc Ratio |
|------------------|-------|--------------:|------------:|------:|------------:|------------:|
| Bshox            | 1     |      1.100 μs |   0.0094 μs |  1.00 |     3.05 KB |        1.00 |
| System.Text.Json | 1     |     37.649 μs |   0.3518 μs | 34.23 |     9.19 KB |        3.02 |
| MessagePack      | 1     |      3.177 μs |   0.0425 μs |  2.89 |     4.38 KB |        1.44 |
| protobuf-net     | 1     |     11.799 μs |   0.2988 μs | 10.73 |     8.46 KB |        2.78 |
| Google.Protobuf  | 1     |      8.308 μs |   0.0180 μs |  7.55 |     4.33 KB |        1.42 |
|                  |       |               |             |       |             |             |
| Bshox            | 1000  |  1,374.218 μs |  57.5616 μs |  1.00 |  2953.98 KB |        1.00 |
| System.Text.Json | 1000  | 42,469.801 μs | 131.8419 μs | 31.03 |  9191.97 KB |        3.11 |
| MessagePack      | 1000  |  4,108.296 μs |  59.3696 μs |  3.00 |  4362.35 KB |        1.48 |
| protobuf-net     | 1000  | 13,122.760 μs |  63.3785 μs |  9.59 | 12786.73 KB |        4.33 |
| Google.Protobuf  | 1000  | 10,840.126 μs |  40.1525 μs |  7.92 |  4221.06 KB |        1.43 |
