```

BenchmarkDotNet v0.15.4, Windows 11 (10.0.26200.6899)
Intel Core i7-14700KF 3.40GHz, 1 CPU, 28 logical and 20 physical cores
.NET SDK 10.0.100-rc.2.25502.107
  [Host]    : .NET 10.0.0 (10.0.0-rc.2.25502.107, 10.0.25.50307), X64 RyuJIT x86-64-v3
  MediumRun : .NET 10.0.0 (10.0.0-rc.2.25502.107, 10.0.25.50307), X64 RyuJIT x86-64-v3

Job=MediumRun  MaxRelativeError=0.01  IterationCount=15
LaunchCount=2  WarmupCount=10

```
| Method | TieredCompilation | Count |           Mean |        Error |  Allocated |
|--------|-------------------|-------|---------------:|-------------:|-----------:|
| Bshox  | 0                 | 1     |     1,612.1 ns |      7.95 ns |    3.05 KB |
| Bshox  | 1                 | 1     |       727.1 ns |     12.37 ns |    3.05 KB |
| Bshox  | 0                 | 1000  | 1,819,313.5 ns | 30,411.32 ns | 2953.85 KB |
| Bshox  | 1                 | 1000  |   955,231.3 ns | 16,309.00 ns | 2953.82 KB |
