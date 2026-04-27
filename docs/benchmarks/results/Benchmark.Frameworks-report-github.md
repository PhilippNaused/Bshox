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
| Deserialize | X64      | .NET 8.0           | 1,015.4 ns |  9.78 ns |  1.13 |
| Deserialize | X64      | .NET 9.0           |   930.8 ns |  7.38 ns |  1.03 |
| Deserialize | X64      | .NET 10.0          |   902.2 ns | 11.00 ns |  1.00 |
| Deserialize | X64      | .NET Framework 4.8 | 3,822.7 ns | 14.30 ns |  4.24 |
| Deserialize | X86      | .NET Framework 4.8 | 3,670.1 ns | 14.68 ns |  4.07 |
|             |          |                    |            |          |       |
| Serialize   | X64      | .NET 8.0           |   782.0 ns |  7.66 ns |  1.06 |
| Serialize   | X64      | .NET 9.0           |   702.6 ns |  4.42 ns |  0.96 |
| Serialize   | X64      | .NET 10.0          |   735.0 ns |  5.88 ns |  1.00 |
| Serialize   | X64      | .NET Framework 4.8 | 3,177.9 ns | 20.04 ns |  4.32 |
| Serialize   | X86      | .NET Framework 4.8 | 4,901.0 ns | 26.34 ns |  6.67 |
