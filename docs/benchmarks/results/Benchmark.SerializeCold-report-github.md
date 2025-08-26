```

BenchmarkDotNet v0.15.2, Windows 11 (10.0.26100.4946/24H2/2024Update/HudsonValley)
Unknown processor
.NET SDK 10.0.100-preview.7.25380.108
  [Host] : .NET 10.0.0 (10.0.25.38108), X64 RyuJIT AVX2
  Dry    : .NET 10.0.0 (10.0.25.38108), X64 RyuJIT AVX2

Job=Dry  IterationCount=1  LaunchCount=25
RunStrategy=ColdStart  UnrollFactor=1  WarmupCount=1

```
| Method | Count |      Mean |     Error |  Allocated |
|--------|-------|----------:|----------:|-----------:|
| Bshox  | 1     |  4.633 ms | 0.1118 ms |    3.05 KB |
| Bshox  | 1000  | 11.782 ms | 0.1819 ms | 2953.61 KB |
