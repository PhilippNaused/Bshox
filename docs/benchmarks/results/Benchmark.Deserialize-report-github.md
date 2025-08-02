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
| Bshox            |  1.507 μs | 0.0039 μs |  1.00 |    0.01 |   4.16 KB |        1.00 |
| System.Text.Json | 44.361 μs | 0.2593 μs | 29.43 |    0.28 |  15.45 KB |        3.72 |
| MessagePack      |  4.527 μs | 0.0328 μs |  3.00 |    0.03 |   4.16 KB |        1.00 |
| protobuf-net     | 11.081 μs | 0.0550 μs |  7.35 |    0.06 |   4.29 KB |        1.03 |
| Google.Protobuf  |  8.031 μs | 0.0230 μs |  5.33 |    0.03 |   15.5 KB |        3.73 |
