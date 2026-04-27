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
| Serialize   | X64      | .NET 8.0           |  4.071 ns | 0.0305 ns |  1.05 |     570 B |
| Serialize   | X64      | .NET 9.0           |  3.818 ns | 0.0128 ns |  0.98 |     560 B |
| Serialize   | X64      | .NET 10.0          |  3.881 ns | 0.0305 ns |  1.00 |     555 B |
| Serialize   | X64      | .NET Framework 4.8 | 22.119 ns | 0.1658 ns |  5.70 |     152 B |
| Serialize   | X86      | .NET Framework 4.8 | 25.015 ns | 0.0623 ns |  6.45 |        NA |
|             |          |                    |           |           |       |           |
| Deserialize | X64      | .NET 8.0           |  6.801 ns | 0.0352 ns |  1.02 |     806 B |
| Deserialize | X64      | .NET 9.0           |  6.555 ns | 0.0131 ns |  0.99 |     755 B |
| Deserialize | X64      | .NET 10.0          |  6.639 ns | 0.0434 ns |  1.00 |     725 B |
| Deserialize | X64      | .NET Framework 4.8 | 28.032 ns | 0.0900 ns |  4.22 |        NA |
| Deserialize | X86      | .NET Framework 4.8 | 24.297 ns | 0.2025 ns |  3.66 |        NA |
