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
| Deserialize | X64      | .NET 8.0           | 1,141.1 ns | 20.30 ns |  1.00 |
| Deserialize | X64      | .NET 9.0           |   876.6 ns | 17.36 ns |  0.77 |
| Deserialize | X64      | .NET 10.0          |   947.0 ns |  7.16 ns |  0.83 |
| Deserialize | X64      | .NET Framework 4.8 | 3,769.0 ns | 15.53 ns |  3.30 |
| Deserialize | X86      | .NET Framework 4.8 | 3,606.0 ns |  8.74 ns |  3.16 |
|             |          |                    |            |          |       |
| Serialize   | X64      | .NET 8.0           |   766.6 ns | 12.64 ns |  1.00 |
| Serialize   | X64      | .NET 9.0           |   711.3 ns |  5.90 ns |  0.93 |
| Serialize   | X64      | .NET 10.0          |   708.0 ns |  3.16 ns |  0.92 |
| Serialize   | X64      | .NET Framework 4.8 | 3,156.6 ns | 12.94 ns |  4.12 |
| Serialize   | X86      | .NET Framework 4.8 | 4,745.4 ns | 19.20 ns |  6.19 |
