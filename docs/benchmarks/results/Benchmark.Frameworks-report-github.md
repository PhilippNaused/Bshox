```

BenchmarkDotNet v0.15.8, Windows 11 (10.0.26200.8246/25H2/2025Update/HudsonValley2)
Intel Core i7-14700KF 3.40GHz, 1 CPU, 28 logical and 20 physical cores
.NET SDK 10.0.203
  [Host]      : .NET 10.0.7 (10.0.7, 10.0.726.21808), X64 RyuJIT x86-64-v3
  net8.0-x64  : .NET 8.0.26 (8.0.26, 8.0.2626.16921), X64 RyuJIT x86-64-v3
  net10.0-x64 : .NET 10.0.7 (10.0.7, 10.0.726.21808), X64 RyuJIT x86-64-v3
  net48-x64   : .NET Framework 4.8.1 (4.8.9325.0), X64 RyuJIT VectorSize=256
  net48-x86   : .NET Framework 4.8.1 (4.8.9325.0), X86 LegacyJIT


```
| Method      | Platform | Runtime            |       Mean |    Error | Ratio |
|-------------|----------|--------------------|-----------:|---------:|------:|
| Deserialize | X64      | .NET 8.0           | 1,080.2 ns | 11.18 ns |  1.10 |
| Deserialize | X64      | .NET 10.0          |   979.9 ns | 16.23 ns |  1.00 |
| Deserialize | X64      | .NET Framework 4.8 | 3,826.6 ns | 18.58 ns |  3.91 |
| Deserialize | X86      | .NET Framework 4.8 | 3,615.4 ns | 13.83 ns |  3.69 |
|             |          |                    |            |          |       |
| Serialize   | X64      | .NET 8.0           |   767.1 ns |  3.97 ns |  1.05 |
| Serialize   | X64      | .NET 10.0          |   732.4 ns |  5.66 ns |  1.00 |
| Serialize   | X64      | .NET Framework 4.8 | 3,392.0 ns | 14.01 ns |  4.63 |
| Serialize   | X86      | .NET Framework 4.8 | 4,840.9 ns | 24.80 ns |  6.61 |
