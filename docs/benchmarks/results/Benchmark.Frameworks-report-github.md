```

BenchmarkDotNet v0.15.2, Windows 11 (10.0.26100.4946/24H2/2024Update/HudsonValley)
Unknown processor
.NET SDK 10.0.100-preview.7.25380.108
  [Host]      : .NET 10.0.0 (10.0.25.38108), X64 RyuJIT AVX2
  net8.0-x64  : .NET 8.0.19 (8.0.1925.36514), X64 RyuJIT AVX2
  net9.0-x64  : .NET 9.0.8 (9.0.825.36511), X64 RyuJIT AVX2
  net10.0-x64 : .NET 10.0.0 (10.0.25.38108), X64 RyuJIT AVX2
  net48-x64   : .NET Framework 4.8.1 (4.8.9310.0), X64 RyuJIT VectorSize=256
  net48-x86   : .NET Framework 4.8.1 (4.8.9310.0), X86 LegacyJIT


```
| Method      | Platform | Runtime            |       Mean |    Error | Ratio |
|-------------|----------|--------------------|-----------:|---------:|------:|
| Deserialize | X64      | .NET 8.0           |   999.8 ns |  4.25 ns |  1.00 |
| Deserialize | X64      | .NET 9.0           |   819.8 ns |  2.06 ns |  0.82 |
| Deserialize | X64      | .NET 10.0          |   914.4 ns |  1.95 ns |  0.91 |
| Deserialize | X64      | .NET Framework 4.8 | 3,743.9 ns | 10.40 ns |  3.74 |
| Deserialize | X86      | .NET Framework 4.8 | 3,574.2 ns |  6.23 ns |  3.58 |
|             |          |                    |            |          |       |
| Serialize   | X64      | .NET 8.0           |   756.5 ns |  1.45 ns |  1.00 |
| Serialize   | X64      | .NET 9.0           |   685.9 ns |  1.72 ns |  0.91 |
| Serialize   | X64      | .NET 10.0          |   703.5 ns |  2.45 ns |  0.93 |
| Serialize   | X64      | .NET Framework 4.8 | 2,993.4 ns |  7.87 ns |  3.96 |
| Serialize   | X86      | .NET Framework 4.8 | 4,310.4 ns | 26.94 ns |  5.70 |
