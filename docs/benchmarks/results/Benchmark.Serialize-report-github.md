```

BenchmarkDotNet v0.15.6, Windows 11 (10.0.26200.7171)
Intel Core i7-14700KF 3.40GHz, 1 CPU, 28 logical and 20 physical cores
.NET SDK 10.0.100
  [Host]    : .NET 10.0.0 (10.0.0, 10.0.25.52411), X64 RyuJIT x86-64-v3
  MediumRun : .NET 10.0.0 (10.0.0, 10.0.25.52411), X64 RyuJIT x86-64-v3

Job=MediumRun  MaxRelativeError=0.01  IterationCount=15
LaunchCount=2  WarmupCount=10

```
| Method | TieredCompilation | Count |           Mean |        Error |  Allocated |
|--------|-------------------|-------|---------------:|-------------:|-----------:|
| Bshox  | 0                 | 1     |     1,650.1 ns |     11.25 ns |    3.05 KB |
| Bshox  | 1                 | 1     |       754.3 ns |     29.21 ns |    3.05 KB |
| Bshox  | 0                 | 1000  | 1,778,051.5 ns |  8,525.88 ns | 2953.83 KB |
| Bshox  | 1                 | 1000  |   963,059.8 ns | 13,388.36 ns | 2953.86 KB |
