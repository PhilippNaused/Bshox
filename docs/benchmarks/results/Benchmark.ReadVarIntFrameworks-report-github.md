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
| ReadByte | X64      | .NET 8.0           | 0.5629 ns | 0.0055 ns |  1.03 |     378 B |
| Read1    | X64      | .NET 8.0           | 0.9224 ns | 0.0043 ns |  1.68 |     458 B |
| ReadAny  | X64      | .NET 8.0           | 2.4481 ns | 0.0091 ns |  4.46 |     467 B |
| ReadByte | X64      | .NET 9.0           | 0.5598 ns | 0.0022 ns |  1.02 |     357 B |
| Read1    | X64      | .NET 9.0           | 0.7736 ns | 0.0048 ns |  1.41 |     396 B |
| ReadAny  | X64      | .NET 9.0           | 2.1245 ns | 0.0104 ns |  3.87 |     405 B |
| ReadByte | X64      | .NET 10.0          | 0.5491 ns | 0.0016 ns |  1.00 |     358 B |
| Read1    | X64      | .NET 10.0          | 0.6730 ns | 0.0053 ns |  1.23 |     397 B |
| ReadAny  | X64      | .NET 10.0          | 2.1071 ns | 0.0116 ns |  3.84 |     404 B |
| ReadByte | X64      | .NET Framework 4.8 | 0.9355 ns | 0.0041 ns |  1.70 |     629 B |
| Read1    | X64      | .NET Framework 4.8 | 2.5742 ns | 0.0073 ns |  4.69 |     803 B |
| ReadAny  | X64      | .NET Framework 4.8 | 6.5052 ns | 0.0222 ns | 11.85 |     806 B |
| ReadByte | X86      | .NET Framework 4.8 | 2.8689 ns | 0.0450 ns |  5.23 |        NA |
| Read1    | X86      | .NET Framework 4.8 | 3.0622 ns | 0.0585 ns |  5.58 |        NA |
| ReadAny  | X86      | .NET Framework 4.8 | 8.4695 ns | 0.0460 ns | 15.43 |        NA |
