```

BenchmarkDotNet v0.15.8, Windows 11 (10.0.26200.8457/25H2/2025Update/HudsonValley2)
Intel Core i7-14700KF 3.40GHz, 1 CPU, 28 logical and 20 physical cores
.NET SDK 10.0.300
  [Host]    : .NET 10.0.8 (10.0.8, 10.0.826.23019), X64 RyuJIT x86-64-v3
  MediumRun : .NET 10.0.8 (10.0.8, 10.0.826.23019), X64 RyuJIT x86-64-v3

Job=MediumRun  MaxRelativeError=0.01  IterationCount=15
LaunchCount=2  WarmupCount=10

```
| Method           | Count |            Mean |         Error | Ratio | Allocated | Alloc Ratio |
|------------------|-------|----------------:|--------------:|------:|----------:|------------:|
| Bshox            | 1     |        481.3 ns |       0.63 ns |  1.00 |         - |          NA |
| System.Text.Json | 1     |     19,004.8 ns |      60.72 ns | 39.49 |     136 B |          NA |
| MessagePack      | 1     |      1,671.2 ns |       2.39 ns |  3.47 |         - |          NA |
| protobuf-net     | 1     |     11,175.7 ns |      96.74 ns | 23.22 |      80 B |          NA |
| Google.Protobuf  | 1     |      4,606.4 ns |      35.57 ns |  9.57 |      56 B |          NA |
|                  |       |                 |               |       |           |             |
| Bshox            | 1000  |    553,241.8 ns |   3,393.69 ns |  1.00 |         - |          NA |
| System.Text.Json | 1000  | 21,339,070.2 ns |  76,849.66 ns | 38.57 |     136 B |          NA |
| MessagePack      | 1000  |  1,846,193.3 ns |  11,356.58 ns |  3.34 |         - |          NA |
| protobuf-net     | 1000  | 12,507,706.8 ns | 247,372.57 ns | 22.61 |   80000 B |          NA |
| Google.Protobuf  | 1000  |  6,515,626.5 ns |  50,473.22 ns | 11.78 |   56000 B |          NA |
