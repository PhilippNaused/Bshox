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
| WriteByte | X64      | .NET 8.0           | 0.7410 ns | 0.0039 ns |  2.21 |     564 B |
| Write1    | X64      | .NET 8.0           | 0.8146 ns | 0.0062 ns |  2.43 |     657 B |
| WriteAny  | X64      | .NET 8.0           | 1.4249 ns | 0.0118 ns |  4.25 |     631 B |
| WriteByte | X64      | .NET 9.0           | 0.3384 ns | 0.0015 ns |  1.01 |     472 B |
| Write1    | X64      | .NET 9.0           | 0.4980 ns | 0.0003 ns |  1.49 |     556 B |
| WriteAny  | X64      | .NET 9.0           | 1.7205 ns | 0.0216 ns |  5.13 |     554 B |
| WriteByte | X64      | .NET 10.0          | 0.3351 ns | 0.0013 ns |  1.00 |     460 B |
| Write1    | X64      | .NET 10.0          | 0.5049 ns | 0.0040 ns |  1.51 |     562 B |
| WriteAny  | X64      | .NET 10.0          | 1.1300 ns | 0.0036 ns |  3.37 |     567 B |
| WriteByte | X64      | .NET Framework 4.8 | 1.3223 ns | 0.0015 ns |  3.95 |     536 B |
| Write1    | X64      | .NET Framework 4.8 | 1.8551 ns | 0.0019 ns |  5.54 |     541 B |
| WriteAny  | X64      | .NET Framework 4.8 | 2.4207 ns | 0.0046 ns |  7.22 |     541 B |
| WriteByte | X86      | .NET Framework 4.8 | 2.0387 ns | 0.0035 ns |  6.08 |        NA |
| Write1    | X86      | .NET Framework 4.8 | 1.8747 ns | 0.0084 ns |  5.59 |        NA |
| WriteAny  | X86      | .NET Framework 4.8 | 2.3716 ns | 0.0046 ns |  7.08 |        NA |
