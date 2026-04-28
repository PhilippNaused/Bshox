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
| Deserialize | X64      | .NET 8.0           |   823.6 ns |  1.36 ns |  1.05 |
| Deserialize | X64      | .NET 9.0           |   717.0 ns |  1.84 ns |  0.92 |
| Deserialize | X64      | .NET 10.0          |   782.2 ns |  1.87 ns |  1.00 |
| Deserialize | X64      | .NET Framework 4.8 | 3,693.6 ns |  3.17 ns |  4.72 |
| Deserialize | X86      | .NET Framework 4.8 | 3,542.1 ns |  6.44 ns |  4.53 |
|             |          |                    |            |          |       |
| Serialize   | X64      | .NET 8.0           |   766.4 ns |  2.17 ns |  1.17 |
| Serialize   | X64      | .NET 9.0           |   678.5 ns |  2.54 ns |  1.04 |
| Serialize   | X64      | .NET 10.0          |   654.4 ns |  3.22 ns |  1.00 |
| Serialize   | X64      | .NET Framework 4.8 | 3,037.1 ns |  5.40 ns |  4.64 |
| Serialize   | X86      | .NET Framework 4.8 | 4,456.5 ns | 11.46 ns |  6.81 |
