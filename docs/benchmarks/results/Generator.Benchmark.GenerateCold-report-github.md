```

BenchmarkDotNet v0.15.2, Windows 11 (10.0.26100.4946/24H2/2024Update/HudsonValley)
Unknown processor
.NET SDK 10.0.100-preview.7.25380.108
  [Host] : .NET 10.0.0 (10.0.25.38108), X64 RyuJIT AVX2
  Dry    : .NET 10.0.0 (10.0.25.38108), X64 RyuJIT AVX2

Job=Dry  IterationCount=1  LaunchCount=10  
RunStrategy=ColdStart  UnrollFactor=1  WarmupCount=1  

```
| Method         | Mean     | Error   | Ratio | RatioSD | Allocated | Alloc Ratio |
|--------------- |---------:|--------:|------:|--------:|----------:|------------:|
| Base           | 382.9 ms | 8.62 ms |  1.00 |    0.02 |   1.33 MB |        1.00 |
| BshoxGenerator | 700.2 ms | 7.45 ms |  1.83 |    0.03 |   3.62 MB |        2.72 |
| JsonGenerator  | 753.7 ms | 3.21 ms |  1.97 |    0.03 |   8.87 MB |        6.66 |
