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
| Method   | Platform | Runtime            |      Mean |     Error | Ratio | Code Size |
|----------|----------|--------------------|----------:|----------:|------:|----------:|
| ReadByte | X64      | .NET 8.0           | 0.4540 ns | 0.0025 ns |  1.21 |        NA |
| Read1    | X64      | .NET 8.0           | 0.7454 ns | 0.0044 ns |  1.98 |        NA |
| ReadAny  | X64      | .NET 8.0           | 2.3653 ns | 0.0328 ns |  6.29 |        NA |
| ReadByte | X64      | .NET 9.0           | 0.3761 ns | 0.0015 ns |  1.00 |        NA |
| Read1    | X64      | .NET 9.0           | 0.6395 ns | 0.0049 ns |  1.70 |        NA |
| ReadAny  | X64      | .NET 9.0           | 1.4751 ns | 0.0135 ns |  3.92 |        NA |
| ReadByte | X64      | .NET 10.0          | 0.3762 ns | 0.0012 ns |  1.00 |        NA |
| Read1    | X64      | .NET 10.0          | 0.6339 ns | 0.0045 ns |  1.69 |        NA |
| ReadAny  | X64      | .NET 10.0          | 1.7255 ns | 0.0102 ns |  4.59 |        NA |
| ReadByte | X64      | .NET Framework 4.8 | 0.9467 ns | 0.0023 ns |  2.52 |        NA |
| Read1    | X64      | .NET Framework 4.8 | 2.5936 ns | 0.0063 ns |  6.90 |     758 B |
| ReadAny  | X64      | .NET Framework 4.8 | 5.7435 ns | 0.0157 ns | 15.27 |     761 B |
| ReadByte | X86      | .NET Framework 4.8 | 1.8542 ns | 0.0128 ns |  4.93 |        NA |
| Read1    | X86      | .NET Framework 4.8 | 2.6631 ns | 0.0259 ns |  7.08 |        NA |
| ReadAny  | X86      | .NET Framework 4.8 | 7.2406 ns | 0.0526 ns | 19.25 |        NA |
