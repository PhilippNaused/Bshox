```

BenchmarkDotNet v0.15.5, Windows 11 (10.0.26200.6901)
Intel Core i7-14700KF 3.40GHz, 1 CPU, 28 logical and 20 physical cores
.NET SDK 10.0.100-rc.2.25502.107
  [Host]    : .NET 10.0.0 (10.0.0-rc.2.25502.107, 10.0.25.50307), X64 RyuJIT x86-64-v3
  MediumRun : .NET 10.0.0 (10.0.0-rc.2.25502.107, 10.0.25.50307), X64 RyuJIT x86-64-v3

Job=MediumRun  IterationCount=15  LaunchCount=2
WarmupCount=10

```
| Method           |        Mean |     Error | Ratio | Allocated | Alloc Ratio |
|------------------|------------:|----------:|------:|----------:|------------:|
| Bshox            |    862.7 ns |   2.88 ns |  1.00 |   4.16 KB |        1.00 |
| System.Text.Json | 27,484.7 ns | 193.72 ns | 31.86 |  15.45 KB |        3.72 |
| MessagePack      |  2,616.6 ns |  24.06 ns |  3.03 |   4.16 KB |        1.00 |
| protobuf-net     |  7,524.3 ns |  58.37 ns |  8.72 |   4.29 KB |        1.03 |
| Google.Protobuf  |  3,899.9 ns |  73.37 ns |  4.52 |   15.5 KB |        3.73 |
