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
| ReadByte | X64      | .NET 8.0           | 0.5504 ns | 0.0013 ns |  1.00 |     378 B |
| Read1    | X64      | .NET 8.0           | 0.8930 ns | 0.0033 ns |  1.63 |     458 B |
| ReadAny  | X64      | .NET 8.0           | 2.5577 ns | 0.0214 ns |  4.66 |     467 B |
| ReadByte | X64      | .NET 9.0           | 0.5520 ns | 0.0016 ns |  1.01 |     357 B |
| Read1    | X64      | .NET 9.0           | 0.7288 ns | 0.0008 ns |  1.33 |     396 B |
| ReadAny  | X64      | .NET 9.0           | 1.9384 ns | 0.0119 ns |  3.53 |     405 B |
| ReadByte | X64      | .NET 10.0          | 0.5491 ns | 0.0013 ns |  1.00 |     358 B |
| Read1    | X64      | .NET 10.0          | 0.6841 ns | 0.0023 ns |  1.25 |     397 B |
| ReadAny  | X64      | .NET 10.0          | 1.9072 ns | 0.0148 ns |  3.47 |     404 B |
| ReadByte | X64      | .NET Framework 4.8 | 0.9275 ns | 0.0013 ns |  1.69 |     669 B |
| Read1    | X64      | .NET Framework 4.8 | 2.5659 ns | 0.0042 ns |  4.67 |     803 B |
| ReadAny  | X64      | .NET Framework 4.8 | 8.5080 ns | 0.0795 ns | 15.49 |     806 B |
| ReadByte | X86      | .NET Framework 4.8 | 2.9054 ns | 0.0225 ns |  5.29 |        NA |
| Read1    | X86      | .NET Framework 4.8 | 2.9902 ns | 0.0369 ns |  5.45 |        NA |
| ReadAny  | X86      | .NET Framework 4.8 | 8.1406 ns | 0.0520 ns | 14.83 |        NA |
