```

BenchmarkDotNet v0.15.5, Windows 11 (10.0.26200.6901)
Intel Core i7-14700KF 3.40GHz, 1 CPU, 28 logical and 20 physical cores
.NET SDK 10.0.100-rc.2.25502.107
  [Host]    : .NET 10.0.0 (10.0.0-rc.2.25502.107, 10.0.25.50307), X64 RyuJIT x86-64-v3
  MediumRun : .NET 10.0.0 (10.0.0-rc.2.25502.107, 10.0.25.50307), X64 RyuJIT x86-64-v3

Job=MediumRun  MaxRelativeError=0.01  IterationCount=15
LaunchCount=2  WarmupCount=10

```
| Method | TieredCompilation | Count |           Mean |        Error |  Allocated |
|--------|-------------------|-------|---------------:|-------------:|-----------:|
| Bshox  | 0                 | 1     |     1,624.0 ns |      6.28 ns |    3.05 KB |
| Bshox  | 1                 | 1     |       710.4 ns |      3.59 ns |    3.05 KB |
| Bshox  | 0                 | 1000  | 1,801,103.2 ns | 13,593.37 ns | 2953.84 KB |
| Bshox  | 1                 | 1000  |   941,940.2 ns |  4,101.68 ns | 2953.83 KB |
