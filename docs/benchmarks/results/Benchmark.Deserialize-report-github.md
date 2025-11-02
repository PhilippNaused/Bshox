```

BenchmarkDotNet v0.15.5, Windows 11 (10.0.26200.6901)
Intel Core i7-14700KF 3.40GHz, 1 CPU, 28 logical and 20 physical cores
.NET SDK 10.0.100-rc.2.25502.107
  [Host]    : .NET 10.0.0 (10.0.0-rc.2.25502.107, 10.0.25.50307), X64 RyuJIT x86-64-v3
  MediumRun : .NET 10.0.0 (10.0.0-rc.2.25502.107, 10.0.25.50307), X64 RyuJIT x86-64-v3

Job=MediumRun  MaxRelativeError=0.01  IterationCount=15
LaunchCount=2  WarmupCount=10

```
| Method | TieredCompilation | Segmented |       Mean |    Error | Allocated |
|--------|-------------------|-----------|-----------:|---------:|----------:|
| Bshox  | 0                 | False     | 1,470.2 ns | 13.16 ns |   4.16 KB |
| Bshox  | 1                 | False     |   933.5 ns |  6.03 ns |   4.16 KB |
| Bshox  | 0                 | True      | 1,598.7 ns |  7.90 ns |   4.21 KB |
| Bshox  | 1                 | True      | 1,097.9 ns | 14.12 ns |   4.21 KB |
