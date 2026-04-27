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
| Unicode | 10     | 1.716 μs | 0.0848 μs |
| Ascii   | 10     | 1.681 μs | 0.0868 μs |
| Unicode | 100    | 1.875 μs | 0.0869 μs |
| Ascii   | 100    | 1.775 μs | 0.0871 μs |
| Unicode | 1000   | 3.064 μs | 0.1113 μs |
| Ascii   | 1000   | 2.059 μs | 0.0864 μs |
