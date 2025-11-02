```

BenchmarkDotNet v0.15.5, Windows 11 (10.0.26200.6901)
Intel Core i7-14700KF 3.40GHz, 1 CPU, 28 logical and 20 physical cores
.NET SDK 10.0.100-rc.2.25502.107
  [Host] : .NET 10.0.0 (10.0.0-rc.2.25502.107, 10.0.25.50307), X64 RyuJIT x86-64-v3
  Dry    : .NET 10.0.0 (10.0.0-rc.2.25502.107, 10.0.25.50307), X64 RyuJIT x86-64-v3

Job=Dry  IterationCount=1  LaunchCount=10
RunStrategy=ColdStart  UnrollFactor=1  WarmupCount=1

```
| Method         |     Mean |    Error | Ratio | RatioSD | Allocated | Alloc Ratio |
|----------------|---------:|---------:|------:|--------:|----------:|------------:|
| Base           | 349.4 ms |  4.84 ms |  1.00 |    0.01 |   1.33 MB |        1.00 |
| BshoxGenerator | 716.9 ms | 10.51 ms |  2.05 |    0.03 |   7.43 MB |        5.58 |
| JsonGenerator  | 699.0 ms |  5.73 ms |  2.00 |    0.02 |   8.87 MB |        6.66 |
