```

BenchmarkDotNet v0.15.4, Windows 11 (10.0.26200.6899)
Intel Core i7-14700KF 3.40GHz, 1 CPU, 28 logical and 20 physical cores
.NET SDK 10.0.100-rc.2.25502.107
  [Host] : .NET 10.0.0 (10.0.0-rc.2.25502.107, 10.0.25.50307), X64 RyuJIT x86-64-v3
  Dry    : .NET 10.0.0 (10.0.0-rc.2.25502.107, 10.0.25.50307), X64 RyuJIT x86-64-v3

Job=Dry  IterationCount=1  LaunchCount=25
RunStrategy=ColdStart  UnrollFactor=1  WarmupCount=1

```
| Method           |      Mean |     Error | Ratio | Allocated | Alloc Ratio |
|------------------|----------:|----------:|------:|----------:|------------:|
| Bshox            |  1.933 ms | 0.0981 ms |  1.00 |   4.16 KB |        1.00 |
| System.Text.Json |  6.946 ms | 0.1047 ms |  3.61 |  15.45 KB |        3.72 |
| MessagePack      |  4.318 ms | 0.0651 ms |  2.24 |   4.16 KB |        1.00 |
| protobuf-net     | 22.069 ms | 0.1545 ms | 11.46 |   4.29 KB |        1.03 |
| Google.Protobuf  |  2.780 ms | 0.0950 ms |  1.44 |   15.5 KB |        3.73 |
