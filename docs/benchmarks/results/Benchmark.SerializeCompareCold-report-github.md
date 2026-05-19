```

BenchmarkDotNet v0.15.8, Windows 11 (10.0.26200.8457/25H2/2025Update/HudsonValley2)
Intel Core i7-14700KF 3.40GHz, 1 CPU, 28 logical and 20 physical cores
.NET SDK 10.0.300
  [Host] : .NET 10.0.8 (10.0.8, 10.0.826.23019), X64 RyuJIT x86-64-v3
  Dry    : .NET 10.0.8 (10.0.8, 10.0.826.23019), X64 RyuJIT x86-64-v3

Job=Dry  IterationCount=1  LaunchCount=25
RunStrategy=ColdStart  UnrollFactor=1  WarmupCount=1

```
| Method           | Count |       Mean |     Error | Ratio | Allocated | Alloc Ratio |
|------------------|-------|-----------:|----------:|------:|----------:|------------:|
| Bshox            | 1     |   4.922 ms | 0.1171 ms |  1.00 |         - |          NA |
| System.Text.Json | 1     |  19.268 ms | 0.3004 ms |  3.92 |     136 B |          NA |
| MessagePack      | 1     |  24.785 ms | 0.3636 ms |  5.04 |         - |          NA |
| protobuf-net     | 1     |  15.892 ms | 0.2407 ms |  3.23 |      80 B |          NA |
| Google.Protobuf  | 1     |   7.298 ms | 0.2060 ms |  1.48 |      56 B |          NA |
|                  |       |            |           |       |           |             |
| Bshox            | 1000  |  12.089 ms | 0.3119 ms |  1.00 |         - |          NA |
| System.Text.Json | 1000  | 109.017 ms | 0.6227 ms |  9.03 |     136 B |          NA |
| MessagePack      | 1000  |  35.471 ms | 0.3656 ms |  2.94 |         - |          NA |
| protobuf-net     | 1000  |  68.011 ms | 0.3650 ms |  5.63 |   80000 B |          NA |
| Google.Protobuf  | 1000  |  44.680 ms | 0.3561 ms |  3.70 |   56000 B |          NA |
