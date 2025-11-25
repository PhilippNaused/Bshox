```

BenchmarkDotNet v0.15.6, Windows 11 (10.0.26200.7171)
Intel Core i7-14700KF 3.40GHz, 1 CPU, 28 logical and 20 physical cores
.NET SDK 10.0.100
  [Host]    : .NET 10.0.0 (10.0.0, 10.0.25.52411), X64 RyuJIT x86-64-v3
  MediumRun : .NET 10.0.0 (10.0.0, 10.0.25.52411), X64 RyuJIT x86-64-v3

Job=MediumRun  MaxRelativeError=0.01  IterationCount=15
LaunchCount=2  WarmupCount=10

```
| Method | TieredCompilation | Segmented |       Mean |    Error | Allocated |
|--------|-------------------|-----------|-----------:|---------:|----------:|
| Bshox  | 0                 | False     | 1,377.9 ns |  9.77 ns |   4.16 KB |
| Bshox  | 1                 | False     |   917.1 ns | 27.77 ns |   4.16 KB |
| Bshox  | 0                 | True      | 1,650.9 ns | 40.81 ns |   4.21 KB |
| Bshox  | 1                 | True      | 1,085.7 ns | 27.14 ns |   4.21 KB |
