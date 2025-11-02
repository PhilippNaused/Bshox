```

BenchmarkDotNet v0.15.5, Windows 11 (10.0.26200.6901)
Intel Core i7-14700KF 3.40GHz, 1 CPU, 28 logical and 20 physical cores
.NET SDK 10.0.100-rc.2.25502.107
  [Host]      : .NET 10.0.0 (10.0.0-rc.2.25502.107, 10.0.25.50307), X64 RyuJIT x86-64-v3
  net8.0-x64  : .NET 8.0.21 (8.0.21, 8.0.2125.47513), X64 RyuJIT x86-64-v3
  net9.0-x64  : .NET 9.0.10 (9.0.10, 9.0.1025.47515), X64 RyuJIT x86-64-v3
  net10.0-x64 : .NET 10.0.0 (10.0.0-rc.2.25502.107, 10.0.25.50307), X64 RyuJIT x86-64-v3
  net48-x64   : .NET Framework 4.8.1 (4.8.9221.0), X64 RyuJIT VectorSize=256
  net48-x86   : .NET Framework 4.8.1 (4.8.9221.0), X86 LegacyJIT


```
| Method      | Platform | Runtime            |       Mean |    Error | Ratio |
|-------------|----------|--------------------|-----------:|---------:|------:|
| Deserialize | X64      | .NET 8.0           | 1,015.8 ns | 11.74 ns |  1.00 |
| Deserialize | X64      | .NET 9.0           |   841.1 ns |  3.28 ns |  0.83 |
| Deserialize | X64      | .NET 10.0          |   871.9 ns |  4.06 ns |  0.86 |
| Deserialize | X64      | .NET Framework 4.8 | 3,589.5 ns |  9.93 ns |  3.53 |
| Deserialize | X86      | .NET Framework 4.8 | 3,553.1 ns |  9.28 ns |  3.50 |
|             |          |                    |            |          |       |
| Serialize   | X64      | .NET 8.0           |   752.9 ns |  5.40 ns |  1.00 |
| Serialize   | X64      | .NET 9.0           |   695.7 ns |  3.60 ns |  0.92 |
| Serialize   | X64      | .NET 10.0          |   723.8 ns | 11.56 ns |  0.96 |
| Serialize   | X64      | .NET Framework 4.8 | 2,829.2 ns | 31.13 ns |  3.76 |
| Serialize   | X86      | .NET Framework 4.8 | 4,320.6 ns | 23.46 ns |  5.74 |
