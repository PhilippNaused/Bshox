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
| Method    | Platform | Runtime            |      Mean |     Error | Ratio | Code Size |
|-----------|----------|--------------------|----------:|----------:|------:|----------:|
| WriteByte | X64      | .NET 8.0           | 0.7316 ns | 0.0058 ns |  2.21 |     564 B |
| Write1    | X64      | .NET 8.0           | 0.7232 ns | 0.0087 ns |  2.19 |     699 B |
| WriteAny  | X64      | .NET 8.0           | 1.4992 ns | 0.0061 ns |  4.54 |     678 B |
| WriteByte | X64      | .NET 9.0           | 0.3311 ns | 0.0005 ns |  1.00 |     472 B |
| Write1    | X64      | .NET 9.0           | 0.3735 ns | 0.0005 ns |  1.13 |     581 B |
| WriteAny  | X64      | .NET 9.0           | 1.1526 ns | 0.0027 ns |  3.49 |     613 B |
| WriteByte | X64      | .NET 10.0          | 0.3303 ns | 0.0004 ns |  1.00 |     460 B |
| Write1    | X64      | .NET 10.0          | 0.3738 ns | 0.0007 ns |  1.13 |     575 B |
| WriteAny  | X64      | .NET 10.0          | 1.1772 ns | 0.0058 ns |  3.56 |     591 B |
| WriteByte | X64      | .NET Framework 4.8 | 1.6511 ns | 0.0018 ns |  5.00 |     432 B |
| Write1    | X64      | .NET Framework 4.8 | 1.8270 ns | 0.0041 ns |  5.53 |     555 B |
| WriteAny  | X64      | .NET Framework 4.8 | 2.4116 ns | 0.0044 ns |  7.30 |     555 B |
| WriteByte | X86      | .NET Framework 4.8 | 1.8564 ns | 0.0073 ns |  5.62 |        NA |
| Write1    | X86      | .NET Framework 4.8 | 1.8684 ns | 0.0158 ns |  5.66 |        NA |
| WriteAny  | X86      | .NET Framework 4.8 | 2.5936 ns | 0.0152 ns |  7.85 |        NA |
