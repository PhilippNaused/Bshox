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


```
| Method      | Platform | Runtime            |       Mean |    Error | Ratio |
|-------------|----------|--------------------|-----------:|---------:|------:|
| Deserialize | X64      | .NET 8.0           | 1,029.8 ns |  5.31 ns |  1.00 |
| Deserialize | X64      | .NET 9.0           |   846.1 ns |  4.84 ns |  0.82 |
| Deserialize | X64      | .NET 10.0          |   922.6 ns |  3.13 ns |  0.90 |
| Deserialize | X64      | .NET Framework 4.8 | 3,680.0 ns | 22.44 ns |  3.57 |
| Deserialize | X86      | .NET Framework 4.8 | 3,604.8 ns | 16.30 ns |  3.50 |
|             |          |                    |            |          |       |
| Serialize   | X64      | .NET 8.0           | 1,202.9 ns |  5.22 ns |  1.00 |
| Serialize   | X64      | .NET 9.0           |   744.3 ns |  2.14 ns |  0.62 |
| Serialize   | X64      | .NET 10.0          |   771.3 ns | 10.97 ns |  0.64 |
| Serialize   | X64      | .NET Framework 4.8 | 2,753.7 ns | 32.57 ns |  2.29 |
| Serialize   | X86      | .NET Framework 4.8 | 3,487.7 ns | 13.97 ns |  2.90 |
