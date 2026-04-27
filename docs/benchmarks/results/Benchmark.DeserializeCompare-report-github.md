```

BenchmarkDotNet v0.15.8, Windows 11 (10.0.26200.8246/25H2/2025Update/HudsonValley2)
Intel Core i7-14700KF 3.40GHz, 1 CPU, 28 logical and 20 physical cores
.NET SDK 10.0.203
  [Host]    : .NET 10.0.7 (10.0.7, 10.0.726.21808), X64 RyuJIT x86-64-v3
  MediumRun : .NET 10.0.7 (10.0.7, 10.0.726.21808), X64 RyuJIT x86-64-v3

Job=MediumRun  MaxRelativeError=0.01  IterationCount=15
LaunchCount=2  WarmupCount=10

```
| Method           |        Mean |     Error | Ratio | Allocated | Alloc Ratio |
|------------------|------------:|----------:|------:|----------:|------------:|
| Bshox            |    832.7 ns |  13.96 ns |  1.00 |   4.16 KB |        1.00 |
| System.Text.Json | 27,582.2 ns | 135.43 ns | 33.14 |  15.45 KB |        3.72 |
| MessagePack      |  2,714.3 ns |  50.83 ns |  3.26 |   4.16 KB |        1.00 |
| protobuf-net     |  7,873.9 ns |  55.70 ns |  9.46 |   4.29 KB |        1.03 |
| Google.Protobuf  |  4,132.6 ns |  49.36 ns |  4.97 |   15.5 KB |        3.73 |
