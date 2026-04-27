```

BenchmarkDotNet v0.15.8, Windows 11 (10.0.26200.8246/25H2/2025Update/HudsonValley2)
Intel Core i7-14700KF 3.40GHz, 1 CPU, 28 logical and 20 physical cores
.NET SDK 10.0.203
  [Host] : .NET 10.0.7 (10.0.7, 10.0.726.21808), X64 RyuJIT x86-64-v3
  Dry    : .NET 10.0.7 (10.0.7, 10.0.726.21808), X64 RyuJIT x86-64-v3

Job=Dry  IterationCount=1  LaunchCount=25
RunStrategy=ColdStart  UnrollFactor=1  WarmupCount=1

```
| Method    |     Mean |     Error | Ratio |
|-----------|---------:|----------:|------:|
| WriteByte | 1.475 μs | 0.0709 μs |  1.00 |
| Write1    | 1.504 μs | 0.0776 μs |  1.02 |
| WriteAny  | 1.512 μs | 0.0588 μs |  1.03 |
