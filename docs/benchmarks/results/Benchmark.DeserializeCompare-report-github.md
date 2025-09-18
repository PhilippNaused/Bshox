```

BenchmarkDotNet v0.15.3, Windows 11 (10.0.26100.6584/24H2/2024Update/HudsonValley)
Intel Core i7-14700KF 3.40GHz, 1 CPU, 28 logical and 20 physical cores
.NET SDK 10.0.100-rc.1.25451.107
  [Host]    : .NET 10.0.0 (10.0.0-rc.1.25451.107, 10.0.25.45207), X64 RyuJIT x86-64-v3
  MediumRun : .NET 10.0.0 (10.0.0-rc.1.25451.107, 10.0.25.45207), X64 RyuJIT x86-64-v3

Job=MediumRun  IterationCount=15  LaunchCount=2
WarmupCount=10

```
| Method           |        Mean |    Error | Ratio | Allocated | Alloc Ratio |
|------------------|------------:|---------:|------:|----------:|------------:|
| Bshox            |    870.7 ns | 11.41 ns |  1.00 |   4.16 KB |        1.00 |
| System.Text.Json | 27,735.1 ns | 52.07 ns | 31.87 |  15.45 KB |        3.72 |
| MessagePack      |  2,699.7 ns | 40.37 ns |  3.10 |   4.16 KB |        1.00 |
| protobuf-net     |  8,157.5 ns | 40.38 ns |  9.37 |   4.29 KB |        1.03 |
| Google.Protobuf  |  3,844.3 ns | 19.68 ns |  4.42 |   15.5 KB |        3.73 |
