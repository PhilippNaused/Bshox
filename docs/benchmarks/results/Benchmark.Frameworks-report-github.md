```

BenchmarkDotNet v0.15.2, Windows 11 (10.0.26100.4946/24H2/2024Update/HudsonValley)
Unknown processor
.NET SDK 10.0.100-rc.1.25451.107
  [Host]      : .NET 10.0.0 (10.0.25.45207), X64 RyuJIT AVX2
  net8.0-x64  : .NET 8.0.20 (8.0.2025.41914), X64 RyuJIT AVX2
  net9.0-x64  : .NET 9.0.9 (9.0.925.41916), X64 RyuJIT AVX2
  net10.0-x64 : .NET 10.0.0 (10.0.25.45207), X64 RyuJIT AVX2
  net48-x64   : .NET Framework 4.8.1 (4.8.9310.0), X64 RyuJIT VectorSize=256
  net48-x86   : .NET Framework 4.8.1 (4.8.9310.0), X86 LegacyJIT


```
| Method      | Platform | Runtime            |       Mean |    Error | Ratio |
|-------------|----------|--------------------|-----------:|---------:|------:|
| Deserialize | X64      | .NET 8.0           | 1,024.2 ns | 15.68 ns |  1.00 |
| Deserialize | X64      | .NET 9.0           |   827.5 ns |  8.87 ns |  0.81 |
| Deserialize | X64      | .NET 10.0          |   866.2 ns |  4.46 ns |  0.85 |
| Deserialize | X64      | .NET Framework 4.8 | 3,628.4 ns | 20.34 ns |  3.54 |
| Deserialize | X86      | .NET Framework 4.8 | 3,532.9 ns | 15.14 ns |  3.45 |
|             |          |                    |            |          |       |
| Serialize   | X64      | .NET 8.0           |   755.8 ns |  7.41 ns |  1.00 |
| Serialize   | X64      | .NET 9.0           |   691.3 ns |  5.05 ns |  0.91 |
| Serialize   | X64      | .NET 10.0          |   715.0 ns |  6.18 ns |  0.95 |
| Serialize   | X64      | .NET Framework 4.8 | 2,965.5 ns | 13.97 ns |  3.92 |
| Serialize   | X86      | .NET Framework 4.8 | 4,329.5 ns | 34.61 ns |  5.73 |
