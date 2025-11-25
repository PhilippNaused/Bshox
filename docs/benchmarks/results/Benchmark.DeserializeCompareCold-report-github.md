```

BenchmarkDotNet v0.15.6, Windows 11 (10.0.26200.7171)
Intel Core i7-14700KF 3.40GHz, 1 CPU, 28 logical and 20 physical cores
.NET SDK 10.0.100
  [Host] : .NET 10.0.0 (10.0.0, 10.0.25.52411), X64 RyuJIT x86-64-v3
  Dry    : .NET 10.0.0 (10.0.0, 10.0.25.52411), X64 RyuJIT x86-64-v3

Job=Dry  IterationCount=1  LaunchCount=25
RunStrategy=ColdStart  UnrollFactor=1  WarmupCount=1

```
| Method           |      Mean |     Error | Ratio | Allocated | Alloc Ratio |
|------------------|----------:|----------:|------:|----------:|------------:|
| Bshox            |  1.931 ms | 0.0327 ms |  1.00 |   4.16 KB |        1.00 |
| System.Text.Json |  7.173 ms | 0.0691 ms |  3.72 |  15.45 KB |        3.72 |
| MessagePack      |  4.355 ms | 0.0679 ms |  2.26 |   4.16 KB |        1.00 |
| protobuf-net     | 22.535 ms | 0.2502 ms | 11.67 |   4.29 KB |        1.03 |
| Google.Protobuf  |  2.849 ms | 0.0399 ms |  1.48 |   15.5 KB |        3.73 |
