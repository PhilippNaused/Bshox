```

BenchmarkDotNet v0.15.4, Windows 11 (10.0.26200.6899)
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
| Deserialize | X64      | .NET 8.0           | 1,040.3 ns | 20.66 ns |  1.00 |
| Deserialize | X64      | .NET 9.0           |   831.0 ns |  4.31 ns |  0.80 |
| Deserialize | X64      | .NET 10.0          |   864.5 ns |  9.71 ns |  0.83 |
| Deserialize | X64      | .NET Framework 4.8 | 3,619.0 ns | 10.05 ns |  3.48 |
| Deserialize | X86      | .NET Framework 4.8 | 3,486.5 ns | 25.57 ns |  3.35 |
|             |          |                    |            |          |       |
| Serialize   | X64      | .NET 8.0           |   783.6 ns | 14.08 ns |  1.00 |
| Serialize   | X64      | .NET 9.0           |   696.5 ns |  4.77 ns |  0.89 |
| Serialize   | X64      | .NET 10.0          |   708.9 ns |  2.42 ns |  0.90 |
| Serialize   | X64      | .NET Framework 4.8 | 2,868.9 ns | 35.27 ns |  3.66 |
| Serialize   | X86      | .NET Framework 4.8 | 4,248.7 ns | 19.68 ns |  5.42 |
