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
| Method  | Platform | Runtime            | Length |         Mean |      Error | Ratio | Code Size |
|---------|----------|--------------------|--------|-------------:|-----------:|------:|----------:|
| Unicode | X64      | .NET 8.0           | 10     |    12.759 ns |  0.0868 ns |  1.30 |   2,628 B |
| Unicode | X64      | .NET 9.0           | 10     |    10.031 ns |  0.0982 ns |  1.02 |   3,077 B |
| Unicode | X64      | .NET 10.0          | 10     |     9.855 ns |  0.1685 ns |  1.00 |   3,850 B |
| Unicode | X64      | .NET Framework 4.8 | 10     |    45.508 ns |  0.1366 ns |  4.62 |   1,256 B |
| Unicode | X86      | .NET Framework 4.8 | 10     |    45.568 ns |  0.1187 ns |  4.63 |        NA |
|         |          |                    |        |              |            |       |           |
| Ascii   | X64      | .NET 8.0           | 10     |     5.375 ns |  0.0104 ns |  1.53 |   2,628 B |
| Ascii   | X64      | .NET 9.0           | 10     |     4.048 ns |  0.0143 ns |  1.15 |   3,049 B |
| Ascii   | X64      | .NET 10.0          | 10     |     3.521 ns |  0.0089 ns |  1.00 |   3,687 B |
| Ascii   | X64      | .NET Framework 4.8 | 10     |    25.951 ns |  0.1826 ns |  7.37 |   1,256 B |
| Ascii   | X86      | .NET Framework 4.8 | 10     |    26.412 ns |  0.1597 ns |  7.50 |        NA |
|         |          |                    |        |              |            |       |           |
| Unicode | X64      | .NET 8.0           | 100    |   101.780 ns |  0.9164 ns |  1.09 |   3,309 B |
| Unicode | X64      | .NET 9.0           | 100    |    90.122 ns |  0.3506 ns |  0.96 |   2,855 B |
| Unicode | X64      | .NET 10.0          | 100    |    93.436 ns |  0.6408 ns |  1.00 |   5,125 B |
| Unicode | X64      | .NET Framework 4.8 | 100    |   327.558 ns |  0.9136 ns |  3.51 |   1,256 B |
| Unicode | X86      | .NET Framework 4.8 | 100    |   354.750 ns |  1.1174 ns |  3.80 |        NA |
|         |          |                    |        |              |            |       |           |
| Ascii   | X64      | .NET 8.0           | 100    |    17.568 ns |  0.0793 ns |  1.75 |   3,312 B |
| Ascii   | X64      | .NET 9.0           | 100    |    11.921 ns |  0.0424 ns |  1.18 |   2,829 B |
| Ascii   | X64      | .NET 10.0          | 100    |    10.061 ns |  0.0484 ns |  1.00 |   4,893 B |
| Ascii   | X64      | .NET Framework 4.8 | 100    |    72.512 ns |  0.9537 ns |  7.21 |   1,256 B |
| Ascii   | X86      | .NET Framework 4.8 | 100    |    74.471 ns |  0.9908 ns |  7.40 |        NA |
|         |          |                    |        |              |            |       |           |
| Unicode | X64      | .NET 8.0           | 1000   | 1,025.389 ns | 11.0055 ns |  1.15 |   3,316 B |
| Unicode | X64      | .NET 9.0           | 1000   |   899.725 ns |  7.6246 ns |  1.00 |   2,867 B |
| Unicode | X64      | .NET 10.0          | 1000   |   895.371 ns |  2.3055 ns |  1.00 |   4,963 B |
| Unicode | X64      | .NET Framework 4.8 | 1000   | 2,367.115 ns |  9.1643 ns |  2.64 |   1,256 B |
| Unicode | X86      | .NET Framework 4.8 | 1000   | 2,544.016 ns |  7.7469 ns |  2.84 |        NA |
|         |          |                    |        |              |            |       |           |
| Ascii   | X64      | .NET 8.0           | 1000   |    70.130 ns |  0.8394 ns |  1.17 |   3,292 B |
| Ascii   | X64      | .NET 9.0           | 1000   |    55.333 ns |  0.4726 ns |  0.92 |   2,838 B |
| Ascii   | X64      | .NET 10.0          | 1000   |    59.962 ns |  1.0138 ns |  1.00 |   4,924 B |
| Ascii   | X64      | .NET Framework 4.8 | 1000   |   352.397 ns |  6.2711 ns |  5.88 |   1,256 B |
| Ascii   | X86      | .NET Framework 4.8 | 1000   |   387.742 ns |  4.8945 ns |  6.47 |        NA |
