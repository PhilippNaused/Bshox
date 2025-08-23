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
| Bshox            |  4.189 ms | 0.1070 ms |  1.00 |      3 KB |        1.00 |
| System.Text.Json | 21.416 ms | 0.3080 ms |  5.12 |   9.23 KB |        3.08 |
| MessagePack      | 22.559 ms | 0.2760 ms |  5.39 |   4.38 KB |        1.46 |
| protobuf-net     | 10.603 ms | 0.2408 ms |  2.53 |  16.56 KB |        5.52 |
| Google.Protobuf  |  6.109 ms | 0.1817 ms |  1.46 |   4.32 KB |        1.44 |
