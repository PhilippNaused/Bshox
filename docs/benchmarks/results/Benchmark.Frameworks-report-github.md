```

BenchmarkDotNet v0.15.8, Windows 11 (10.0.26200.7171/25H2/2025Update/HudsonValley2)
Intel Core i7-14700KF 3.40GHz, 1 CPU, 28 logical and 20 physical cores
.NET SDK 10.0.100
  [Host]      : .NET 10.0.0 (10.0.0, 10.0.25.52411), X64 RyuJIT x86-64-v3
  net8.0-x64  : .NET 8.0.22 (8.0.22, 8.0.2225.52707), X64 RyuJIT x86-64-v3
  net9.0-x64  : .NET 9.0.11 (9.0.11, 9.0.1125.51716), X64 RyuJIT x86-64-v3
  net10.0-x64 : .NET 10.0.0 (10.0.0, 10.0.25.52411), X64 RyuJIT x86-64-v3
  net48-x64   : .NET Framework 4.8.1 (4.8.9323.0), X64 RyuJIT VectorSize=256
  net48-x86   : .NET Framework 4.8.1 (4.8.9323.0), X86 LegacyJIT


```
| Method      | Platform | Runtime            |       Mean |    Error | Ratio |
|-------------|----------|--------------------|-----------:|---------:|------:|
| Deserialize | X64      | .NET 8.0           | 1,063.4 ns |  6.97 ns |  1.15 |
| Deserialize | X64      | .NET 9.0           |   862.8 ns |  4.86 ns |  0.93 |
| Deserialize | X64      | .NET 10.0          |   925.4 ns |  3.01 ns |  1.00 |
| Deserialize | X64      | .NET Framework 4.8 | 3,816.2 ns | 16.43 ns |  4.12 |
| Deserialize | X86      | .NET Framework 4.8 | 3,563.3 ns |  5.18 ns |  3.85 |
|             |          |                    |            |          |       |
| Serialize   | X64      | .NET 8.0           |   785.3 ns | 12.68 ns |  1.14 |
| Serialize   | X64      | .NET 9.0           |   716.2 ns | 12.61 ns |  1.04 |
| Serialize   | X64      | .NET 10.0          |   691.5 ns |  2.74 ns |  1.00 |
| Serialize   | X64      | .NET Framework 4.8 | 3,148.7 ns | 20.21 ns |  4.55 |
| Serialize   | X86      | .NET Framework 4.8 | 4,648.1 ns | 15.77 ns |  6.72 |
