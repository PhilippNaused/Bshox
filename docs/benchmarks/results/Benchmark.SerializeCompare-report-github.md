```

BenchmarkDotNet v0.15.2, Windows 11 (10.0.26100.4946/24H2/2024Update/HudsonValley)
Unknown processor
.NET SDK 10.0.100-preview.7.25380.108
  [Host]    : .NET 10.0.0 (10.0.25.38108), X64 RyuJIT AVX2
  MediumRun : .NET 10.0.0 (10.0.25.38108), X64 RyuJIT AVX2

Job=MediumRun  IterationCount=15  LaunchCount=2
WarmupCount=10

```
| Method           |        Mean |     Error | Ratio | Allocated | Alloc Ratio |
|------------------|------------:|----------:|------:|----------:|------------:|
| Bshox            |    694.8 ns |   5.15 ns |  1.00 |      3 KB |        1.00 |
| System.Text.Json | 19,638.2 ns | 121.45 ns | 28.27 |   9.23 KB |        3.08 |
| MessagePack      |  1,838.0 ns |  27.42 ns |  2.65 |   4.38 KB |        1.46 |
| protobuf-net     |  6,605.3 ns |  38.15 ns |  9.51 |  16.56 KB |        5.52 |
| Google.Protobuf  |  4,788.2 ns |  49.94 ns |  6.89 |   4.32 KB |        1.44 |
