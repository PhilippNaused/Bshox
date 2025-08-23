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
| Bshox            |  1.912 ms | 0.0395 ms |  1.00 |   4.16 KB |        1.00 |
| System.Text.Json |  7.212 ms | 0.0957 ms |  3.77 |  15.45 KB |        3.72 |
| MessagePack      |  4.385 ms | 0.0989 ms |  2.30 |   4.16 KB |        1.00 |
| protobuf-net     | 22.709 ms | 0.7486 ms | 11.88 |   4.29 KB |        1.03 |
| Google.Protobuf  |  2.810 ms | 0.0665 ms |  1.47 |   15.5 KB |        3.73 |
