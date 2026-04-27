```

BenchmarkDotNet v0.15.8, Windows 11 (10.0.26200.8246/25H2/2025Update/HudsonValley2)
Intel Core i7-14700KF 3.40GHz, 1 CPU, 28 logical and 20 physical cores
.NET SDK 10.0.203
  [Host]      : .NET 10.0.7 (10.0.7, 10.0.726.21808), X64 RyuJIT x86-64-v3
  net8.0-x64  : .NET 8.0.26 (8.0.26, 8.0.2626.16921), X64 RyuJIT x86-64-v3
  net9.0-x64  : .NET 9.0.15 (9.0.15, 9.0.1526.17522), X64 RyuJIT x86-64-v3
  net10.0-x64 : .NET 10.0.7 (10.0.7, 10.0.726.21808), X64 RyuJIT x86-64-v3
  net48-x64   : .NET Framework 4.8.1 (4.8.9325.0), X64 RyuJIT VectorSize=256
  net48-x86   : .NET Framework 4.8.1 (4.8.9325.0), X86 LegacyJIT


```
| Method      | Platform | Runtime            |      Mean |     Error | Ratio | Code Size |
|-------------|----------|--------------------|----------:|----------:|------:|----------:|
| Serialize   | X64      | .NET 8.0           |  5.611 ns | 0.0109 ns |  0.73 |   1,485 B |
| Serialize   | X64      | .NET 9.0           |  5.227 ns | 0.0182 ns |  0.68 |   1,419 B |
| Serialize   | X64      | .NET 10.0          |  7.689 ns | 0.0175 ns |  1.00 |   1,435 B |
| Serialize   | X64      | .NET Framework 4.8 | 28.669 ns | 0.1074 ns |  3.73 |     152 B |
| Serialize   | X86      | .NET Framework 4.8 | 39.133 ns | 0.0999 ns |  5.09 |        NA |
|             |          |                    |           |           |       |           |
| Deserialize | X64      | .NET 8.0           |  8.595 ns | 0.0337 ns |  1.10 |   1,737 B |
| Deserialize | X64      | .NET 9.0           |  8.922 ns | 0.0517 ns |  1.14 |   1,623 B |
| Deserialize | X64      | .NET 10.0          |  7.809 ns | 0.0585 ns |  1.00 |   1,614 B |
| Deserialize | X64      | .NET Framework 4.8 | 35.287 ns | 0.2813 ns |  4.52 |        NA |
| Deserialize | X86      | .NET Framework 4.8 | 34.155 ns | 0.2165 ns |  4.37 |        NA |
