```

BenchmarkDotNet v0.15.6, Windows 11 (10.0.26200.7171)
Intel Core i7-14700KF 3.40GHz, 1 CPU, 28 logical and 20 physical cores
.NET SDK 10.0.100
  [Host]    : .NET 10.0.0 (10.0.0, 10.0.25.52411), X64 RyuJIT x86-64-v3
  MediumRun : .NET 10.0.0 (10.0.0, 10.0.25.52411), X64 RyuJIT x86-64-v3

Job=MediumRun  IterationCount=15  LaunchCount=2
WarmupCount=10

```
| Method           |        Mean |     Error | Ratio | Allocated | Alloc Ratio |
|------------------|------------:|----------:|------:|----------:|------------:|
| Bshox            |    872.7 ns |   6.02 ns |  1.00 |   4.16 KB |        1.00 |
| System.Text.Json | 27,506.4 ns | 204.79 ns | 31.52 |  15.45 KB |        3.72 |
| MessagePack      |  2,713.1 ns |  65.66 ns |  3.11 |   4.16 KB |        1.00 |
| protobuf-net     |  7,511.4 ns |  38.85 ns |  8.61 |   4.29 KB |        1.03 |
| Google.Protobuf  |  3,796.9 ns |  32.16 ns |  4.35 |   15.5 KB |        3.73 |
