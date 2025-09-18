```

BenchmarkDotNet v0.15.3, Windows 11 (10.0.26100.6584/24H2/2024Update/HudsonValley)
Intel Core i7-14700KF 3.40GHz, 1 CPU, 28 logical and 20 physical cores
.NET SDK 10.0.100-rc.1.25451.107
  [Host] : .NET 10.0.0 (10.0.0-rc.1.25451.107, 10.0.25.45207), X64 RyuJIT x86-64-v3
  Dry    : .NET 10.0.0 (10.0.0-rc.1.25451.107, 10.0.25.45207), X64 RyuJIT x86-64-v3

Job=Dry  IterationCount=1  LaunchCount=25
RunStrategy=ColdStart  UnrollFactor=1  WarmupCount=1

```
| Method           |      Mean |     Error | Ratio | Allocated | Alloc Ratio |
|------------------|----------:|----------:|------:|----------:|------------:|
| Bshox            |  1.951 ms | 0.0512 ms |  1.00 |   4.16 KB |        1.00 |
| System.Text.Json |  7.348 ms | 0.2001 ms |  3.77 |  15.45 KB |        3.72 |
| MessagePack      |  4.414 ms | 0.0656 ms |  2.27 |   4.16 KB |        1.00 |
| protobuf-net     | 22.740 ms | 0.3038 ms | 11.67 |   4.29 KB |        1.03 |
| Google.Protobuf  |  2.888 ms | 0.1190 ms |  1.48 |   15.5 KB |        3.73 |
