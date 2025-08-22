```

BenchmarkDotNet v0.15.2, Windows 11 (10.0.26100.4946/24H2/2024Update/HudsonValley)
Unknown processor
.NET SDK 10.0.100-preview.7.25380.108
  [Host]    : .NET 10.0.0 (10.0.25.38108), X64 RyuJIT AVX2
  MediumRun : .NET 10.0.0 (10.0.25.38108), X64 RyuJIT AVX2

Job=MediumRun  IterationCount=15  LaunchCount=2
WarmupCount=10

```
| Method           |        Mean |    Error | Ratio | Allocated | Alloc Ratio |
|------------------|------------:|---------:|------:|----------:|------------:|
| Bshox            |    934.1 ns |  8.38 ns |  1.00 |   4.16 KB |        1.00 |
| System.Text.Json | 27,557.2 ns | 52.27 ns | 29.51 |  15.45 KB |        3.72 |
| MessagePack      |  2,725.7 ns | 56.56 ns |  2.92 |   4.16 KB |        1.00 |
| protobuf-net     |  7,873.7 ns | 50.30 ns |  8.43 |   4.29 KB |        1.03 |
| Google.Protobuf  |  4,003.7 ns | 78.16 ns |  4.29 |   15.5 KB |        3.73 |
