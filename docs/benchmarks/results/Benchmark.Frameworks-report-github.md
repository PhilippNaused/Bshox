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
| Method      | Platform | Runtime            |       Mean |    Error | Ratio |
|-------------|----------|--------------------|-----------:|---------:|------:|
| Deserialize | X64      | .NET 8.0           |   868.4 ns |  9.65 ns |  1.01 |
| Deserialize | X64      | .NET 9.0           |   810.1 ns |  4.26 ns |  0.94 |
| Deserialize | X64      | .NET 10.0          |   859.3 ns |  4.98 ns |  1.00 |
| Deserialize | X64      | .NET Framework 4.8 | 3,715.7 ns |  7.13 ns |  4.32 |
| Deserialize | X86      | .NET Framework 4.8 | 3,491.6 ns |  7.45 ns |  4.06 |
|             |          |                    |            |          |       |
| Serialize   | X64      | .NET 8.0           |   779.6 ns | 12.46 ns |  1.02 |
| Serialize   | X64      | .NET 9.0           |   740.7 ns |  3.65 ns |  0.97 |
| Serialize   | X64      | .NET 10.0          |   767.2 ns |  5.34 ns |  1.00 |
| Serialize   | X64      | .NET Framework 4.8 | 3,121.3 ns |  6.19 ns |  4.07 |
| Serialize   | X86      | .NET Framework 4.8 | 4,809.0 ns | 25.83 ns |  6.27 |
