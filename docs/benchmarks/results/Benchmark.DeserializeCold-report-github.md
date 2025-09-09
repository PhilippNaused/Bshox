```

BenchmarkDotNet v0.15.2, Windows 11 (10.0.26100.4946/24H2/2024Update/HudsonValley)
Unknown processor
.NET SDK 10.0.100-rc.1.25451.107
  [Host] : .NET 10.0.0 (10.0.25.45207), X64 RyuJIT AVX2
  Dry    : .NET 10.0.0 (10.0.25.45207), X64 RyuJIT AVX2

Job=Dry  IterationCount=1  LaunchCount=25
RunStrategy=ColdStart  UnrollFactor=1  WarmupCount=1

```
| Method | Segmented |     Mean |     Error | Allocated |
|--------|-----------|---------:|----------:|----------:|
| Bshox  | False     | 2.008 ms | 0.1138 ms |   4.16 KB |
| Bshox  | True      | 2.356 ms | 0.1228 ms |   4.21 KB |
