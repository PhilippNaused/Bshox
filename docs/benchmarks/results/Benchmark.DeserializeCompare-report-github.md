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
| Bshox            |    787.4 ns |   3.88 ns |  1.00 |   4.16 KB |        1.00 |
| System.Text.Json | 27,072.2 ns | 175.35 ns | 34.38 |  15.45 KB |        3.72 |
| MessagePack      |  2,541.1 ns |  31.82 ns |  3.23 |   4.16 KB |        1.00 |
| protobuf-net     |  7,749.4 ns |  53.62 ns |  9.84 |   4.29 KB |        1.03 |
| Google.Protobuf  |  3,981.1 ns |  27.91 ns |  5.06 |   15.5 KB |        3.73 |
