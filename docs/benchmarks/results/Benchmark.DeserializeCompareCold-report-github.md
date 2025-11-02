```

BenchmarkDotNet v0.15.5, Windows 11 (10.0.26200.6901)
Intel Core i7-14700KF 3.40GHz, 1 CPU, 28 logical and 20 physical cores
.NET SDK 10.0.100-rc.2.25502.107
  [Host] : .NET 10.0.0 (10.0.0-rc.2.25502.107, 10.0.25.50307), X64 RyuJIT x86-64-v3
  Dry    : .NET 10.0.0 (10.0.0-rc.2.25502.107, 10.0.25.50307), X64 RyuJIT x86-64-v3

Job=Dry  IterationCount=1  LaunchCount=25
RunStrategy=ColdStart  UnrollFactor=1  WarmupCount=1

```
| Method           |      Mean |     Error | Ratio | Allocated | Alloc Ratio |
|------------------|----------:|----------:|------:|----------:|------------:|
| Bshox            |  1.908 ms | 0.0692 ms |  1.00 |   4.16 KB |        1.00 |
| System.Text.Json |  7.036 ms | 0.1850 ms |  3.70 |  15.45 KB |        3.72 |
| MessagePack      |  4.300 ms | 0.1404 ms |  2.26 |   4.16 KB |        1.00 |
| protobuf-net     | 22.333 ms | 0.2214 ms | 11.73 |   4.29 KB |        1.03 |
| Google.Protobuf  |  2.869 ms | 0.1351 ms |  1.51 |   15.5 KB |        3.73 |
