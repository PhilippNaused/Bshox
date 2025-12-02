```

BenchmarkDotNet v0.15.8, Windows 11 (10.0.26200.7171/25H2/2025Update/HudsonValley2)
Intel Core i7-14700KF 3.40GHz, 1 CPU, 28 logical and 20 physical cores
.NET SDK 10.0.100
  [Host]    : .NET 10.0.0 (10.0.0, 10.0.25.52411), X64 RyuJIT x86-64-v3
  MediumRun : .NET 10.0.0 (10.0.0, 10.0.25.52411), X64 RyuJIT x86-64-v3

Job=MediumRun  MaxRelativeError=0.01  IterationCount=15
LaunchCount=2  WarmupCount=10

```
| Method           |        Mean |     Error | Ratio | Allocated | Alloc Ratio |
|------------------|------------:|----------:|------:|----------:|------------:|
| Bshox            |    960.0 ns |  22.83 ns |  1.00 |   4.16 KB |        1.00 |
| System.Text.Json | 27,333.9 ns | 123.00 ns | 28.51 |  15.45 KB |        3.72 |
| MessagePack      |  2,635.5 ns |  75.92 ns |  2.75 |   4.16 KB |        1.00 |
| protobuf-net     |  7,651.4 ns |  35.23 ns |  7.98 |   4.29 KB |        1.03 |
| Google.Protobuf  |  4,015.4 ns |  22.77 ns |  4.19 |   15.5 KB |        3.73 |
