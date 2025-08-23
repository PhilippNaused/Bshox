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
| Bshox            |    758.1 ns |  3.85 ns |  1.00 |      3 KB |        1.00 |
| System.Text.Json | 20,209.3 ns | 66.95 ns | 26.66 |   9.23 KB |        3.08 |
| MessagePack      |  1,924.6 ns | 19.23 ns |  2.54 |   4.38 KB |        1.46 |
| protobuf-net     |  6,924.9 ns | 76.91 ns |  9.14 |  16.56 KB |        5.52 |
| Google.Protobuf  |  5,015.2 ns | 55.42 ns |  6.62 |   4.32 KB |        1.44 |
