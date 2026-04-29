```

BenchmarkDotNet v0.15.8, Windows 11 (10.0.26200.8246/25H2/2025Update/HudsonValley2)
Intel Core i7-14700KF 3.40GHz, 1 CPU, 28 logical and 20 physical cores
.NET SDK 10.0.203
  [Host]    : .NET 10.0.7 (10.0.7, 10.0.726.21808), X64 RyuJIT x86-64-v3
  MediumRun : .NET 10.0.7 (10.0.7, 10.0.726.21808), X64 RyuJIT x86-64-v3

Job=MediumRun  MaxRelativeError=0.01  IterationCount=15
LaunchCount=2  WarmupCount=10

```
| Method           | Count |            Mean |        Error | Ratio |   Allocated | Alloc Ratio |
|------------------|-------|----------------:|-------------:|------:|------------:|------------:|
| Bshox            | 1     |        675.4 ns |      1.85 ns |  1.00 |     3.05 KB |        1.00 |
| System.Text.Json | 1     |     19,782.5 ns |    257.34 ns | 29.29 |     9.23 KB |        3.03 |
| MessagePack      | 1     |      1,834.1 ns |      4.01 ns |  2.72 |     4.38 KB |        1.44 |
| protobuf-net     | 1     |      6,413.4 ns |     87.75 ns |  9.50 |     8.51 KB |        2.79 |
| Google.Protobuf  | 1     |      5,407.8 ns |     54.97 ns |  8.01 |      4.3 KB |        1.41 |
|                  |       |                 |              |       |             |             |
| Bshox            | 1000  |    962,705.4 ns |  6,906.30 ns |  1.00 |  2954.04 KB |        1.00 |
| System.Text.Json | 1000  | 23,020,852.2 ns | 48,277.02 ns | 23.92 |  9191.67 KB |        3.11 |
| MessagePack      | 1000  |  2,379,493.3 ns | 51,523.38 ns |  2.47 |  4363.35 KB |        1.48 |
| protobuf-net     | 1000  |  7,601,506.2 ns | 39,772.91 ns |  7.90 | 12753.83 KB |        4.32 |
| Google.Protobuf  | 1000  |  7,710,430.1 ns | 94,269.96 ns |  8.01 |  4221.82 KB |        1.43 |
