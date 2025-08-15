```

BenchmarkDotNet v0.15.2, Windows 11 (10.0.26100.4652/24H2/2024Update/HudsonValley)
Unknown processor
.NET SDK 10.0.100-preview.7.25380.108
  [Host]     : .NET 10.0.0 (10.0.25.38108), X64 RyuJIT AVX2
  Net100-x64 : .NET 10.0.0 (10.0.25.38108), X64 RyuJIT AVX2
  Net48-x64  : .NET Framework 4.8.1 (4.8.9310.0), X64 RyuJIT VectorSize=256
  Net48-x86  : .NET Framework 4.8.1 (4.8.9310.0), X86 LegacyJIT
  Net80-x64  : .NET 8.0.19 (8.0.1925.36514), X64 RyuJIT AVX2
  Net90-x64  : .NET 9.0.8 (9.0.825.36511), X64 RyuJIT AVX2

EnvironmentVariables=DOTNET_TieredPGO=0  Server=True

```
| Method      | Job        |     Mean |     Error | Ratio | Allocated | Alloc Ratio |
|-------------|------------|---------:|----------:|------:|----------:|------------:|
| Deserialize | Net100-x64 | 1.558 μs | 0.0110 μs |  1.04 |   4.16 KB |        1.00 |
| Deserialize | Net48-x64  | 3.612 μs | 0.0100 μs |  2.42 |    4.2 KB |        1.01 |
| Deserialize | Net48-x86  | 3.508 μs | 0.0062 μs |  2.35 |   3.87 KB |        0.93 |
| Deserialize | Net80-x64  | 1.495 μs | 0.0048 μs |  1.00 |   4.16 KB |        1.00 |
| Deserialize | Net90-x64  | 1.613 μs | 0.0159 μs |  1.08 |   4.16 KB |        1.00 |
|             |            |          |           |       |           |             |
| Serialize   | Net100-x64 | 1.317 μs | 0.0091 μs |  0.73 |      3 KB |        1.00 |
| Serialize   | Net48-x64  | 2.701 μs | 0.0051 μs |  1.51 |   3.01 KB |        1.00 |
| Serialize   | Net48-x86  | 3.508 μs | 0.0107 μs |  1.96 |   2.97 KB |        0.99 |
| Serialize   | Net80-x64  | 1.794 μs | 0.0119 μs |  1.00 |      3 KB |        1.00 |
| Serialize   | Net90-x64  | 1.407 μs | 0.0158 μs |  0.78 |      3 KB |        1.00 |
