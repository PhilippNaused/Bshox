```

BenchmarkDotNet v0.15.2, Windows 11 (10.0.26100.4946/24H2/2024Update/HudsonValley)
Unknown processor
.NET SDK 10.0.100-preview.7.25380.108
  [Host] : .NET 10.0.0 (10.0.25.38108), X64 RyuJIT AVX2
  Dry    : .NET 10.0.0 (10.0.25.38108), X64 RyuJIT AVX2

Job=Dry  IterationCount=1  LaunchCount=25
RunStrategy=ColdStart  UnrollFactor=1  WarmupCount=1

```
| Method           |      Mean |     Error | Ratio | Allocated | Alloc Ratio |
|------------------|----------:|----------:|------:|----------:|------------:|
| Bshox            |  1.993 ms | 0.0544 ms |  1.00 |   4.16 KB |        1.00 |
| System.Text.Json |  7.485 ms | 0.2047 ms |  3.76 |  15.45 KB |        3.72 |
| MessagePack      |  4.495 ms | 0.0535 ms |  2.26 |   4.16 KB |        1.00 |
| protobuf-net     | 22.990 ms | 0.2243 ms | 11.55 |   4.29 KB |        1.03 |
| Google.Protobuf  |  2.903 ms | 0.0499 ms |  1.46 |   15.5 KB |        3.73 |
