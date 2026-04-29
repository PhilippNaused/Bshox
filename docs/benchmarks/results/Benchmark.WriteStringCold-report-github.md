```

BenchmarkDotNet v0.15.8, Windows 11 (10.0.26200.8246/25H2/2025Update/HudsonValley2)
Intel Core i7-14700KF 3.40GHz, 1 CPU, 28 logical and 20 physical cores
.NET SDK 10.0.203
  [Host] : .NET 10.0.7 (10.0.7, 10.0.726.21808), X64 RyuJIT x86-64-v3
  Dry    : .NET 10.0.7 (10.0.7, 10.0.726.21808), X64 RyuJIT x86-64-v3

Job=Dry  IterationCount=1  LaunchCount=25
RunStrategy=ColdStart  UnrollFactor=1  WarmupCount=1

```
| Method  | Length |     Mean |     Error |
|---------|--------|---------:|----------:|
| Unicode | 10     | 1.618 μs | 0.0466 μs |
| Ascii   | 10     | 1.623 μs | 0.0514 μs |
| Unicode | 100    | 1.818 μs | 0.0602 μs |
| Ascii   | 100    | 1.752 μs | 0.0793 μs |
| Unicode | 1000   | 2.983 μs | 0.0758 μs |
| Ascii   | 1000   | 2.028 μs | 0.0895 μs |
