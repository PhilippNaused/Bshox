```

BenchmarkDotNet v0.15.2, Windows 11 (10.0.26100.4946/24H2/2024Update/HudsonValley)
Unknown processor
.NET SDK 10.0.100-preview.7.25380.108
  [Host]    : .NET 10.0.0 (10.0.25.38108), X64 RyuJIT AVX2
  MediumRun : .NET 10.0.0 (10.0.25.38108), X64 RyuJIT AVX2

Job=MediumRun  IterationCount=15  LaunchCount=2
WarmupCount=10

```
| Method           | Count |            Mean |        Error | Ratio |   Allocated | Alloc Ratio |
|------------------|-------|----------------:|-------------:|------:|------------:|------------:|
| Bshox            | 1     |        722.4 ns |      4.95 ns |  1.00 |     3.05 KB |        1.00 |
| System.Text.Json | 1     |     20,562.0 ns |     80.37 ns | 28.47 |     9.23 KB |        3.03 |
| MessagePack      | 1     |      1,934.8 ns |     15.86 ns |  2.68 |     4.38 KB |        1.44 |
| protobuf-net     | 1     |      6,716.0 ns |     65.02 ns |  9.30 |     8.51 KB |        2.79 |
| Google.Protobuf  | 1     |      5,819.4 ns |     62.27 ns |  8.06 |      4.3 KB |        1.41 |
|                  |       |                 |              |       |             |             |
| Bshox            | 1000  |  1,144,456.1 ns |  5,505.76 ns |  1.00 |  2954.12 KB |        1.00 |
| System.Text.Json | 1000  | 23,609,642.5 ns | 50,203.74 ns | 20.63 |  9191.71 KB |        3.11 |
| MessagePack      | 1000  |  2,463,629.5 ns | 48,840.65 ns |  2.15 |  4363.35 KB |        1.48 |
| protobuf-net     | 1000  |  7,802,579.1 ns | 95,870.57 ns |  6.82 | 12753.72 KB |        4.32 |
| Google.Protobuf  | 1000  |  7,871,928.8 ns | 68,733.84 ns |  6.88 |  4222.03 KB |        1.43 |
