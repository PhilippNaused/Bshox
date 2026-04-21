```

BenchmarkDotNet v0.15.8, Windows 11 (10.0.26200.8246/25H2/2025Update/HudsonValley2)
Intel Core i7-14700KF 3.40GHz, 1 CPU, 28 logical and 20 physical cores
.NET SDK 10.0.202
  [Host]      : .NET 10.0.6 (10.0.6, 10.0.626.17701), X64 RyuJIT x86-64-v3
  net8.0-x64  : .NET 8.0.26 (8.0.26, 8.0.2626.16921), X64 RyuJIT x86-64-v3
  net9.0-x64  : .NET 9.0.15 (9.0.15, 9.0.1526.17522), X64 RyuJIT x86-64-v3
  net10.0-x64 : .NET 10.0.6 (10.0.6, 10.0.626.17701), X64 RyuJIT x86-64-v3
  net48-x64   : .NET Framework 4.8.1 (4.8.9325.0), X64 RyuJIT VectorSize=256
  net48-x86   : .NET Framework 4.8.1 (4.8.9325.0), X86 LegacyJIT


```
| Method      | Platform | Runtime            |       Mean |    Error | Ratio |
|-------------|----------|--------------------|-----------:|---------:|------:|
| Deserialize | X64      | .NET 8.0           | 1,102.1 ns | 12.91 ns |  1.20 |
| Deserialize | X64      | .NET 9.0           |   857.5 ns |  4.65 ns |  0.94 |
| Deserialize | X64      | .NET 10.0          |   916.4 ns |  4.04 ns |  1.00 |
| Deserialize | X64      | .NET Framework 4.8 | 3,739.6 ns |  7.55 ns |  4.08 |
| Deserialize | X86      | .NET Framework 4.8 | 3,653.6 ns |  6.69 ns |  3.99 |
|             |          |                    |            |          |       |
| Serialize   | X64      | .NET 8.0           |   747.2 ns |  1.47 ns |  1.07 |
| Serialize   | X64      | .NET 9.0           |   702.7 ns |  2.90 ns |  1.00 |
| Serialize   | X64      | .NET 10.0          |   700.4 ns |  3.35 ns |  1.00 |
| Serialize   | X64      | .NET Framework 4.8 | 3,017.7 ns |  5.39 ns |  4.31 |
| Serialize   | X86      | .NET Framework 4.8 | 5,287.7 ns | 58.01 ns |  7.55 |
