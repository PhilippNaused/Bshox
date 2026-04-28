```

BenchmarkDotNet v0.15.8, Windows 11 (10.0.26200.8246/25H2/2025Update/HudsonValley2)
Intel Core i7-14700KF 3.40GHz, 1 CPU, 28 logical and 20 physical cores
.NET SDK 10.0.203
  [Host]     : .NET 10.0.7 (10.0.7, 10.0.726.21808), X64 RyuJIT x86-64-v3
  DefaultJob : .NET 10.0.7 (10.0.7, 10.0.726.21808), X64 RyuJIT x86-64-v3


```
| Method    |      Mean |     Error | Ratio | Code Size |
|-----------|----------:|----------:|------:|----------:|
| WriteByte | 0.3347 ns | 0.0013 ns |  1.00 |     460 B |
| Write1    | 0.5034 ns | 0.0021 ns |  1.50 |     562 B |
| WriteAny  | 1.1451 ns | 0.0014 ns |  3.42 |     567 B |
