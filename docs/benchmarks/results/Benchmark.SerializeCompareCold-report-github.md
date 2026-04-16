```

BenchmarkDotNet v0.15.8, Windows 11 (10.0.26200.8246/25H2/2025Update/HudsonValley2)
Intel Core i7-14700KF 3.40GHz, 1 CPU, 28 logical and 20 physical cores
.NET SDK 10.0.202
  [Host] : .NET 10.0.6 (10.0.6, 10.0.626.17701), X64 RyuJIT x86-64-v3
  Dry    : .NET 10.0.6 (10.0.6, 10.0.626.17701), X64 RyuJIT x86-64-v3

Job=Dry  IterationCount=1  LaunchCount=25
RunStrategy=ColdStart  UnrollFactor=1  WarmupCount=1

```
| Method           | Count |       Mean |     Error | Ratio |   Allocated | Alloc Ratio |
|------------------|-------|-----------:|----------:|------:|------------:|------------:|
| Bshox            | 1     |   5.553 ms | 0.1229 ms |  1.00 |     3.05 KB |        1.00 |
| System.Text.Json | 1     |  20.368 ms | 0.9776 ms |  3.67 |     9.23 KB |        3.03 |
| MessagePack      | 1     |  25.018 ms | 0.4125 ms |  4.51 |     4.38 KB |        1.44 |
| protobuf-net     | 1     |  13.050 ms | 0.2382 ms |  2.35 |     8.51 KB |        2.79 |
| Google.Protobuf  | 1     |   7.321 ms | 0.0946 ms |  1.32 |      4.3 KB |        1.41 |
|                  |       |            |           |       |             |             |
| Bshox            | 1000  |  13.125 ms | 0.3352 ms |  1.00 |  2953.62 KB |        1.00 |
| System.Text.Json | 1000  | 111.411 ms | 0.6290 ms |  8.50 |  9191.55 KB |        3.11 |
| MessagePack      | 1000  |  38.013 ms | 0.6760 ms |  2.90 |   4363.3 KB |        1.48 |
| protobuf-net     | 1000  |  42.922 ms | 0.6584 ms |  3.27 | 12758.09 KB |        4.32 |
| Google.Protobuf  | 1000  |  49.097 ms | 0.4293 ms |  3.74 |  4220.64 KB |        1.43 |
