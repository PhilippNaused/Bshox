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
| Bshox            |  1.435 μs | 0.0136 μs |  1.00 |      3 KB |        1.00 |
| System.Text.Json | 29.589 μs | 0.0595 μs | 20.63 |   9.23 KB |        3.08 |
| MessagePack      |  3.188 μs | 0.0127 μs |  2.22 |   4.38 KB |        1.46 |
| protobuf-net     | 11.138 μs | 0.0752 μs |  7.76 |  16.56 KB |        5.52 |
| Google.Protobuf  |  8.655 μs | 0.0334 μs |  6.03 |   4.32 KB |        1.44 |
