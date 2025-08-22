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
| Bshox            |    767.7 ns |   5.43 ns |  1.00 |      3 KB |        1.00 |
| System.Text.Json | 20,353.7 ns |  54.04 ns | 26.52 |   9.23 KB |        3.08 |
| MessagePack      |  1,891.7 ns |  31.91 ns |  2.46 |   4.38 KB |        1.46 |
| protobuf-net     |  6,846.1 ns |  35.24 ns |  8.92 |  16.56 KB |        5.52 |
| Google.Protobuf  |  5,058.1 ns | 103.45 ns |  6.59 |   4.32 KB |        1.44 |
