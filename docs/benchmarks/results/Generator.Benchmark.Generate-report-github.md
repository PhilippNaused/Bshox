```

BenchmarkDotNet v0.15.2, Windows 11 (10.0.26100.4946/24H2/2024Update/HudsonValley)
Unknown processor
.NET SDK 10.0.100-preview.7.25380.108
  [Host]    : .NET 10.0.0 (10.0.25.38108), X64 RyuJIT AVX2
  MediumRun : .NET 10.0.0 (10.0.25.38108), X64 RyuJIT AVX2

Job=MediumRun  IterationCount=15  LaunchCount=2
WarmupCount=20

```
| Method         |       Mean |    Error | Ratio | RatioSD |  Allocated | Alloc Ratio |
|----------------|-----------:|---------:|------:|--------:|-----------:|------------:|
| Base           |   224.1 μs |  3.90 μs |  1.00 |    0.04 |   93.22 KB |        1.00 |
| BshoxGenerator | 1,731.8 μs | 57.17 μs |  7.73 |    0.43 |     476 KB |        5.11 |
| JsonGenerator  | 3,907.4 μs | 60.23 μs | 17.45 |    0.60 | 1189.61 KB |       12.76 |
