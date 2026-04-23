```

BenchmarkDotNet v0.15.8, Windows 11 (10.0.26200.8246/25H2/2025Update/HudsonValley2)
Intel Core i7-14700KF 3.40GHz, 1 CPU, 28 logical and 20 physical cores
.NET SDK 10.0.203
  [Host]     : .NET 10.0.7 (10.0.7, 10.0.726.21808), X64 RyuJIT x86-64-v3
  DefaultJob : .NET 10.0.7 (10.0.7, 10.0.726.21808), X64 RyuJIT x86-64-v3


```
| Method   |      Mean |     Error | Ratio | Code Size |
|----------|----------:|----------:|------:|----------:|
| ReadByte | 0.5506 ns | 0.0012 ns |  1.00 |     358 B |
| Read1    | 0.6947 ns | 0.0028 ns |  1.26 |     397 B |
| Read2    | 1.2901 ns | 0.0046 ns |  2.34 |     395 B |
| Read5    | 3.0970 ns | 0.0080 ns |  5.62 |     401 B |
