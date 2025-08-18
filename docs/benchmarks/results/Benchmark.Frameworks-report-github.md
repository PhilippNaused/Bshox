```

BenchmarkDotNet v0.15.2, Windows 11 (10.0.26100.4946/24H2/2024Update/HudsonValley)
Unknown processor
.NET SDK 10.0.100-preview.7.25380.108
  [Host]      : .NET 10.0.0 (10.0.25.38108), X64 RyuJIT AVX2
  net8.0-x64  : .NET 8.0.19 (8.0.1925.36514), X64 RyuJIT AVX2
  net9.0-x64  : .NET 9.0.8 (9.0.825.36511), X64 RyuJIT AVX2
  net10.0-x64 : .NET 10.0.0 (10.0.25.38108), X64 RyuJIT AVX2
  net48-x64   : .NET Framework 4.8.1 (4.8.9310.0), X64 RyuJIT VectorSize=256
  net48-x86   : .NET Framework 4.8.1 (4.8.9310.0), X86 LegacyJIT

EnvironmentVariables=DOTNET_TieredPGO=0  Server=True

```
| Method      | Platform | Runtime            |     Mean |     Error | Ratio |
|-------------|----------|--------------------|---------:|----------:|------:|
| Deserialize | X64      | .NET 8.0           | 1.532 μs | 0.0066 μs |  1.00 |
| Deserialize | X64      | .NET 9.0           | 1.590 μs | 0.0050 μs |  1.04 |
| Deserialize | X64      | .NET 10.0          | 1.571 μs | 0.0069 μs |  1.03 |
| Deserialize | X64      | .NET Framework 4.8 | 3.859 μs | 0.0163 μs |  2.52 |
| Deserialize | X86      | .NET Framework 4.8 | 3.672 μs | 0.0106 μs |  2.40 |
|             |          |                    |          |           |       |
| Serialize   | X64      | .NET 8.0           | 2.686 μs | 0.0529 μs |  1.00 |
| Serialize   | X64      | .NET 9.0           | 1.385 μs | 0.0077 μs |  0.52 |
| Serialize   | X64      | .NET 10.0          | 1.354 μs | 0.0026 μs |  0.50 |
| Serialize   | X64      | .NET Framework 4.8 | 2.783 μs | 0.0108 μs |  1.04 |
| Serialize   | X86      | .NET Framework 4.8 | 3.666 μs | 0.0105 μs |  1.37 |
