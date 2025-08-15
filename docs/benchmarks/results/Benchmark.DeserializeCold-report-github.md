```

BenchmarkDotNet v0.15.2, Windows 11 (10.0.26100.4652/24H2/2024Update/HudsonValley)
Unknown processor
.NET SDK 10.0.100-preview.7.25380.108
  [Host] : .NET 10.0.0 (10.0.25.38108), X64 RyuJIT AVX2
  Dry    : .NET 10.0.0 (10.0.25.38108), X64 RyuJIT AVX2

Job=Dry  EnvironmentVariables=DOTNET_TieredPGO=0  Server=True
IterationCount=1  LaunchCount=25  RunStrategy=ColdStart
UnrollFactor=1  WarmupCount=1

```
| Method           |      Mean |     Error | Ratio | Allocated | Alloc Ratio |
|------------------|----------:|----------:|------:|----------:|------------:|
| Bshox            |  1.867 ms | 0.0460 ms |  1.00 |   4.16 KB |        1.00 |
| System.Text.Json |  7.295 ms | 0.0925 ms |  3.91 |  15.45 KB |        3.72 |
| MessagePack      |  4.388 ms | 0.0773 ms |  2.35 |   4.16 KB |        1.00 |
| protobuf-net     | 20.307 ms | 0.1337 ms | 10.89 |   4.29 KB |        1.03 |
| Google.Protobuf  |  2.807 ms | 0.0870 ms |  1.51 |   15.5 KB |        3.73 |
