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
| WriteByte | X64      | .NET 8.0           | 0.6729 ns | 0.0015 ns |  2.08 |
| Write1    | X64      | .NET 8.0           | 0.8012 ns | 0.0012 ns |  2.47 |
| WriteAny  | X64      | .NET 8.0           | 1.7202 ns | 0.0115 ns |  5.31 |
| WriteByte | X64      | .NET 9.0           | 0.3375 ns | 0.0013 ns |  1.04 |
| Write1    | X64      | .NET 9.0           | 0.5034 ns | 0.0019 ns |  1.55 |
| WriteAny  | X64      | .NET 9.0           | 1.3189 ns | 0.0112 ns |  4.07 |
| WriteByte | X64      | .NET 10.0          | 0.3238 ns | 0.0009 ns |  1.00 |
| Write1    | X64      | .NET 10.0          | 0.5039 ns | 0.0027 ns |  1.56 |
| WriteAny  | X64      | .NET 10.0          | 1.1326 ns | 0.0015 ns |  3.50 |
| WriteByte | X64      | .NET Framework 4.8 | 1.3067 ns | 0.0032 ns |  4.04 |
| Write1    | X64      | .NET Framework 4.8 | 1.8297 ns | 0.0022 ns |  5.65 |
| WriteAny  | X64      | .NET Framework 4.8 | 2.4248 ns | 0.0067 ns |  7.49 |
| WriteByte | X86      | .NET Framework 4.8 | 1.7171 ns | 0.0144 ns |  5.30 |
| Write1    | X86      | .NET Framework 4.8 | 2.1686 ns | 0.0130 ns |  6.70 |
| WriteAny  | X86      | .NET Framework 4.8 | 2.5234 ns | 0.0107 ns |  7.79 |
