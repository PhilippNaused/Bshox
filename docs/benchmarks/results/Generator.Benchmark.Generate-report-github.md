```

BenchmarkDotNet v0.15.5, Windows 11 (10.0.26200.6901)
Intel Core i7-14700KF 3.40GHz, 1 CPU, 28 logical and 20 physical cores
.NET SDK 10.0.100-rc.2.25502.107
  [Host]    : .NET 10.0.0 (10.0.0-rc.2.25502.107, 10.0.25.50307), X64 RyuJIT x86-64-v3
  MediumRun : .NET 10.0.0 (10.0.0-rc.2.25502.107, 10.0.25.50307), X64 RyuJIT x86-64-v3

Job=MediumRun  IterationCount=15  LaunchCount=2
WarmupCount=20

```
| Method         |       Mean |     Error | Ratio | RatioSD |  Allocated | Alloc Ratio |
|----------------|-----------:|----------:|------:|--------:|-----------:|------------:|
| Base           |   225.4 μs |   2.91 μs |  1.00 |    0.03 |   92.16 KB |        1.00 |
| BshoxGenerator | 2,818.2 μs | 123.99 μs | 12.51 |    0.84 |   644.3 KB |        6.99 |
| JsonGenerator  | 4,341.7 μs | 516.29 μs | 19.27 |    3.39 | 1191.02 KB |       12.92 |
