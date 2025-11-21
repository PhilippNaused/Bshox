```

BenchmarkDotNet v0.15.6, Windows 11 (10.0.26200.7171)
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
| Deserialize | X64      | .NET 8.0           | 1,061.6 ns |  6.81 ns |  1.00 |
| Deserialize | X64      | .NET 9.0           |   842.2 ns |  4.87 ns |  0.79 |
| Deserialize | X64      | .NET 10.0          |   879.0 ns |  4.47 ns |  0.83 |
| Deserialize | X64      | .NET Framework 4.8 | 3,681.6 ns |  6.01 ns |  3.47 |
| Deserialize | X86      | .NET Framework 4.8 | 3,628.9 ns | 48.26 ns |  3.42 |
|             |          |                    |            |          |       |
| Serialize   | X64      | .NET 8.0           |   783.4 ns |  7.67 ns |  1.00 |
| Serialize   | X64      | .NET 9.0           |   709.1 ns |  6.49 ns |  0.91 |
| Serialize   | X64      | .NET 10.0          |   722.8 ns |  3.57 ns |  0.92 |
| Serialize   | X64      | .NET Framework 4.8 | 2,843.9 ns | 13.54 ns |  3.63 |
| Serialize   | X86      | .NET Framework 4.8 | 4,216.7 ns | 21.34 ns |  5.38 |
