```

BenchmarkDotNet v0.15.4, Windows 11 (10.0.26200.6899)
Intel Core i7-14700KF 3.40GHz, 1 CPU, 28 logical and 20 physical cores
.NET SDK 10.0.100-rc.2.25502.107
  [Host]    : .NET 10.0.0 (10.0.0-rc.2.25502.107, 10.0.25.50307), X64 RyuJIT x86-64-v3
  MediumRun : .NET 10.0.0 (10.0.0-rc.2.25502.107, 10.0.25.50307), X64 RyuJIT x86-64-v3

Job=MediumRun  IterationCount=15  LaunchCount=2
WarmupCount=10

```
| Method           |        Mean |     Error | Ratio | Allocated | Alloc Ratio |
|------------------|------------:|----------:|------:|----------:|------------:|
| Bshox            |    885.8 ns |  12.44 ns |  1.00 |   4.16 KB |        1.00 |
| System.Text.Json | 27,350.6 ns | 173.98 ns | 30.89 |  15.45 KB |        3.72 |
| MessagePack      |  2,543.7 ns |  10.23 ns |  2.87 |   4.16 KB |        1.00 |
| protobuf-net     |  7,523.8 ns |  24.10 ns |  8.50 |   4.29 KB |        1.03 |
| Google.Protobuf  |  4,085.2 ns |  44.66 ns |  4.61 |   15.5 KB |        3.73 |
