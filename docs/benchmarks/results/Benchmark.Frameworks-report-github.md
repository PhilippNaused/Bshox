```

BenchmarkDotNet v0.15.2, Windows 11 (10.0.26100.4652/24H2/2024Update/HudsonValley)
Unknown processor
.NET SDK 10.0.100-preview.6.25358.103
  [Host]     : .NET 8.0.18 (8.0.1825.31117), X64 RyuJIT AVX2
  Net100-x64 : .NET 10.0.0 (10.0.25.35903), X64 RyuJIT AVX2
  Net48-x64  : .NET Framework 4.8.1 (4.8.9310.0), X64 RyuJIT VectorSize=256
  Net48-x86  : .NET Framework 4.8.1 (4.8.9310.0), X86 LegacyJIT
  Net80-x64  : .NET 8.0.18 (8.0.1825.31117), X64 RyuJIT AVX2
  Net90-x64  : .NET 9.0.7 (9.0.725.31616), X64 RyuJIT AVX2

EnvironmentVariables=DOTNET_TieredPGO=0  Server=True

```
| Method      | Job        |     Mean |     Error | Ratio | Allocated | Alloc Ratio |
|-------------|------------|---------:|----------:|------:|----------:|------------:|
| Deserialize | Net100-x64 | 1.568 μs | 0.0052 μs |  1.04 |   4.16 KB |        1.00 |
| Deserialize | Net48-x64  | 3.718 μs | 0.0103 μs |  2.48 |    4.2 KB |        1.01 |
| Deserialize | Net48-x86  | 3.750 μs | 0.0072 μs |  2.50 |   3.87 KB |        0.93 |
| Deserialize | Net80-x64  | 1.501 μs | 0.0050 μs |  1.00 |   4.16 KB |        1.00 |
| Deserialize | Net90-x64  | 1.633 μs | 0.0110 μs |  1.09 |   4.16 KB |        1.00 |
|             |            |          |           |       |           |             |
| Serialize   | Net100-x64 | 1.371 μs | 0.0068 μs |  0.78 |      3 KB |        1.00 |
| Serialize   | Net48-x64  | 2.862 μs | 0.0103 μs |  1.63 |   3.01 KB |        1.00 |
| Serialize   | Net48-x86  | 3.683 μs | 0.0058 μs |  2.09 |   2.98 KB |        0.99 |
| Serialize   | Net80-x64  | 1.758 μs | 0.0080 μs |  1.00 |      3 KB |        1.00 |
| Serialize   | Net90-x64  | 1.413 μs | 0.0136 μs |  0.80 |      3 KB |        1.00 |
