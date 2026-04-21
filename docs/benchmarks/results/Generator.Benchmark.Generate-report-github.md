```

BenchmarkDotNet v0.15.8, Windows 11 (10.0.26200.8246/25H2/2025Update/HudsonValley2)
Intel Core i7-14700KF 3.40GHz, 1 CPU, 28 logical and 20 physical cores
.NET SDK 10.0.202
  [Host]    : .NET 10.0.6 (10.0.6, 10.0.626.17701), X64 RyuJIT x86-64-v3
  MediumRun : .NET 10.0.6 (10.0.6, 10.0.626.17701), X64 RyuJIT x86-64-v3

Job=MediumRun  IterationCount=15  LaunchCount=2
WarmupCount=20

```
| Method         |       Mean |     Error | Ratio | RatioSD |  Allocated | Alloc Ratio |
|----------------|-----------:|----------:|------:|--------:|-----------:|------------:|
| Base           |   220.9 μs |   2.47 μs |  1.00 |    0.02 |   92.95 KB |        1.00 |
| BshoxGenerator | 2,729.2 μs | 122.60 μs | 12.36 |    0.84 |  660.77 KB |        7.11 |
| JsonGenerator  | 4,649.8 μs |  95.79 μs | 21.06 |    0.73 | 1315.69 KB |       14.16 |
