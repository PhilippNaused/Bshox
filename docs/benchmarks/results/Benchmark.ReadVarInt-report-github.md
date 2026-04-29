```

BenchmarkDotNet v0.15.8, Windows 11 (10.0.26200.8246/25H2/2025Update/HudsonValley2)
Intel Core i7-14700KF 3.40GHz, 1 CPU, 28 logical and 20 physical cores
.NET SDK 10.0.203
  [Host]     : .NET 10.0.7 (10.0.7, 10.0.726.21808), X64 RyuJIT x86-64-v3
  DefaultJob : .NET 10.0.7 (10.0.7, 10.0.726.21808), X64 RyuJIT x86-64-v3


```
| Method   |      Mean |     Error | Ratio | Code Size |
|----------|----------:|----------:|------:|----------:|
| ReadByte | 0.3721 ns | 0.0009 ns |  1.00 |        NA |
| Read1    | 0.5101 ns | 0.0043 ns |  1.37 |        NA |
| ReadAny  | 2.2825 ns | 0.0114 ns |  6.13 |     534 B |
