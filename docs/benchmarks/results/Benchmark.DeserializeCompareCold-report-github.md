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
| Bshox            |  1.882 ms | 0.0413 ms |  1.00 |   4.16 KB |        1.00 |
| System.Text.Json |  7.062 ms | 0.0960 ms |  3.75 |  15.45 KB |        3.72 |
| MessagePack      |  4.319 ms | 0.1346 ms |  2.30 |   4.16 KB |        1.00 |
| protobuf-net     | 22.125 ms | 0.2788 ms | 11.76 |   4.29 KB |        1.03 |
| Google.Protobuf  |  2.760 ms | 0.0808 ms |  1.47 |   15.5 KB |        3.73 |
