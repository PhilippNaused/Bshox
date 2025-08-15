```

BenchmarkDotNet v0.15.2, Windows 11 (10.0.26100.4652/24H2/2024Update/HudsonValley)
Unknown processor
.NET SDK 10.0.100-preview.7.25380.108
  [Host]    : .NET 10.0.0 (10.0.25.38108), X64 RyuJIT AVX2
  MediumRun : .NET 10.0.0 (10.0.25.38108), X64 RyuJIT AVX2

Job=MediumRun  EnvironmentVariables=DOTNET_TieredPGO=0  Server=True
IterationCount=15  LaunchCount=2  WarmupCount=10

```
| Method           |      Mean |     Error | Ratio | Allocated | Alloc Ratio |
|------------------|----------:|----------:|------:|----------:|------------:|
| Bshox            |  1.564 μs | 0.0066 μs |  1.00 |   4.16 KB |        1.00 |
| System.Text.Json | 37.472 μs | 0.2189 μs | 23.95 |  15.45 KB |        3.72 |
| MessagePack      |  4.489 μs | 0.0368 μs |  2.87 |   4.16 KB |        1.00 |
| protobuf-net     | 10.551 μs | 0.0305 μs |  6.74 |   4.29 KB |        1.03 |
| Google.Protobuf  |  8.261 μs | 0.0572 μs |  5.28 |   15.5 KB |        3.73 |
