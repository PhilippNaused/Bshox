```

BenchmarkDotNet v0.15.4, Windows 11 (10.0.26200.6899)
Intel Core i7-14700KF 3.40GHz, 1 CPU, 28 logical and 20 physical cores
.NET SDK 10.0.100-rc.2.25502.107
  [Host]    : .NET 10.0.0 (10.0.0-rc.2.25502.107, 10.0.25.50307), X64 RyuJIT x86-64-v3
  MediumRun : .NET 10.0.0 (10.0.0-rc.2.25502.107, 10.0.25.50307), X64 RyuJIT x86-64-v3

Job=MediumRun  MaxRelativeError=0.01  IterationCount=15
LaunchCount=2  WarmupCount=10

```
| Method | TieredCompilation | Segmented |       Mean |    Error | Allocated |
|--------|-------------------|-----------|-----------:|---------:|----------:|
| Bshox  | 0                 | False     | 1,388.4 ns |  8.76 ns |   4.16 KB |
| Bshox  | 1                 | False     |   890.3 ns | 15.19 ns |   4.16 KB |
| Bshox  | 0                 | True      | 1,563.7 ns | 14.01 ns |   4.21 KB |
| Bshox  | 1                 | True      | 1,066.5 ns | 16.74 ns |   4.21 KB |
