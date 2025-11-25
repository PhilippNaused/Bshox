```

BenchmarkDotNet v0.15.6, Windows 11 (10.0.26200.7171)
Intel Core i7-14700KF 3.40GHz, 1 CPU, 28 logical and 20 physical cores
.NET SDK 10.0.100
  [Host] : .NET 10.0.0 (10.0.0, 10.0.25.52411), X64 RyuJIT x86-64-v3
  Dry    : .NET 10.0.0 (10.0.0, 10.0.25.52411), X64 RyuJIT x86-64-v3

Job=Dry  IterationCount=1  LaunchCount=25
RunStrategy=ColdStart  UnrollFactor=1  WarmupCount=1

```
| Method | Segmented |     Mean |     Error | Allocated |
|--------|-----------|---------:|----------:|----------:|
| Bshox  | False     | 2.022 ms | 0.0815 ms |   4.16 KB |
| Bshox  | True      | 2.377 ms | 0.0592 ms |   4.21 KB |
