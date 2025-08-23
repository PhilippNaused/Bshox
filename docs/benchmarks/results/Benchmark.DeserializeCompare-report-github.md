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
| Bshox            |    928.4 ns |  6.33 ns |  1.00 |   4.16 KB |        1.00 |
| System.Text.Json | 27,493.9 ns | 57.52 ns | 29.62 |  15.45 KB |        3.72 |
| MessagePack      |  2,629.6 ns | 14.91 ns |  2.83 |   4.16 KB |        1.00 |
| protobuf-net     |  7,852.8 ns | 13.26 ns |  8.46 |   4.29 KB |        1.03 |
| Google.Protobuf  |  4,091.9 ns | 16.53 ns |  4.41 |   15.5 KB |        3.73 |
