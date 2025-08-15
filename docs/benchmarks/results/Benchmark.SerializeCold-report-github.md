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
| Bshox            |  4.222 ms | 0.0790 ms |  1.00 |      3 KB |        1.00 |
| System.Text.Json | 22.109 ms | 0.1480 ms |  5.24 |   9.23 KB |        3.08 |
| MessagePack      | 23.135 ms | 0.1710 ms |  5.48 |   4.38 KB |        1.46 |
| protobuf-net     | 10.631 ms | 0.1339 ms |  2.52 |  16.56 KB |        5.52 |
| Google.Protobuf  |  6.282 ms | 0.0788 ms |  1.49 |   4.32 KB |        1.44 |
