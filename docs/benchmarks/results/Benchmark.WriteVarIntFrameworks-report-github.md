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
| Method    | Platform | Runtime            |      Mean |     Error | Ratio |
|-----------|----------|--------------------|----------:|----------:|------:|
| WriteByte | X64      | .NET 8.0           | 0.6619 ns | 0.0030 ns |  1.99 |
| Write1    | X64      | .NET 8.0           | 0.8014 ns | 0.0021 ns |  2.40 |
| Write2    | X64      | .NET 8.0           | 1.0083 ns | 0.0030 ns |  3.03 |
| Write5    | X64      | .NET 8.0           | 1.8181 ns | 0.0024 ns |  5.46 |
| WriteAny  | X64      | .NET 8.0           | 1.3348 ns | 0.0092 ns |  4.01 |
| WriteByte | X64      | .NET 9.0           | 0.3369 ns | 0.0008 ns |  1.01 |
| Write1    | X64      | .NET 9.0           | 0.5008 ns | 0.0020 ns |  1.50 |
| Write2    | X64      | .NET 9.0           | 0.7371 ns | 0.0009 ns |  2.21 |
| Write5    | X64      | .NET 9.0           | 1.6434 ns | 0.0038 ns |  4.93 |
| WriteAny  | X64      | .NET 9.0           | 1.3794 ns | 0.0070 ns |  4.14 |
| WriteByte | X64      | .NET 10.0          | 0.3333 ns | 0.0007 ns |  1.00 |
| Write1    | X64      | .NET 10.0          | 0.4946 ns | 0.0014 ns |  1.48 |
| Write2    | X64      | .NET 10.0          | 0.7161 ns | 0.0040 ns |  2.15 |
| Write5    | X64      | .NET 10.0          | 1.6405 ns | 0.0035 ns |  4.92 |
| WriteAny  | X64      | .NET 10.0          | 1.0421 ns | 0.0018 ns |  3.13 |
| WriteByte | X64      | .NET Framework 4.8 | 1.3083 ns | 0.0024 ns |  3.93 |
| Write1    | X64      | .NET Framework 4.8 | 1.8349 ns | 0.0030 ns |  5.51 |
| Write2    | X64      | .NET Framework 4.8 | 2.0590 ns | 0.0089 ns |  6.18 |
| Write5    | X64      | .NET Framework 4.8 | 2.9375 ns | 0.0042 ns |  8.81 |
| WriteAny  | X64      | .NET Framework 4.8 | 2.2883 ns | 0.0067 ns |  6.87 |
| WriteByte | X86      | .NET Framework 4.8 | 1.7290 ns | 0.0146 ns |  5.19 |
| Write1    | X86      | .NET Framework 4.8 | 2.1669 ns | 0.0077 ns |  6.50 |
| Write2    | X86      | .NET Framework 4.8 | 2.0902 ns | 0.0044 ns |  6.27 |
| Write5    | X86      | .NET Framework 4.8 | 3.0404 ns | 0.0098 ns |  9.12 |
| WriteAny  | X86      | .NET Framework 4.8 | 2.4842 ns | 0.0128 ns |  7.45 |
