```

BenchmarkDotNet v0.15.2, Windows 11 (10.0.26100.4946/24H2/2024Update/HudsonValley)
Unknown processor
.NET SDK 10.0.100-preview.7.25380.108
  [Host]    : .NET 10.0.0 (10.0.25.38108), X64 RyuJIT AVX2
  MediumRun : .NET 10.0.0 (10.0.25.38108), X64 RyuJIT AVX2

Job=MediumRun  MaxRelativeError=0.01  IterationCount=15
LaunchCount=2  WarmupCount=10

```
| Method | TieredCompilation | Count |           Mean |        Error |  Allocated |
|--------|-------------------|-------|---------------:|-------------:|-----------:|
| Bshox  | 0                 | 1     |     1,661.5 ns |      6.03 ns |    3.05 KB |
| Bshox  | 1                 | 1     |       704.6 ns |      2.18 ns |    3.05 KB |
| Bshox  | 0                 | 1000  | 1,795,819.9 ns |  4,129.18 ns | 2953.85 KB |
| Bshox  | 1                 | 1000  | 1,048,190.9 ns | 14,286.66 ns | 2953.83 KB |
