```

BenchmarkDotNet v0.15.2, Windows 11 (10.0.26100.4652/24H2/2024Update/HudsonValley)
Unknown processor
.NET SDK 10.0.100-preview.6.25358.103
  [Host]    : .NET 8.0.18 (8.0.1825.31117), X64 RyuJIT AVX2
  MediumRun : .NET 8.0.18 (8.0.1825.31117), X64 RyuJIT AVX2

Job=MediumRun  EnvironmentVariables=DOTNET_TieredPGO=0  Server=True
IterationCount=15  LaunchCount=2  WarmupCount=10

```
| Method           |      Mean |     Error | Ratio | RatioSD | Allocated | Alloc Ratio |
|------------------|----------:|----------:|------:|--------:|----------:|------------:|
| Bshox            |  1.767 μs | 0.0075 μs |  1.00 |    0.01 |      3 KB |        1.00 |
| System.Text.Json | 28.984 μs | 0.0304 μs | 16.41 |    0.11 |   9.23 KB |        3.08 |
| MessagePack      |  3.201 μs | 0.0460 μs |  1.81 |    0.04 |   4.38 KB |        1.46 |
| protobuf-net     | 11.355 μs | 0.0607 μs |  6.43 |    0.06 |  16.56 KB |        5.52 |
| Google.Protobuf  |  8.737 μs | 0.0303 μs |  4.95 |    0.04 |   4.32 KB |        1.44 |
