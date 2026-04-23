```

BenchmarkDotNet v0.15.8, Windows 11 (10.0.26200.8246/25H2/2025Update/HudsonValley2)
Intel Core i7-14700KF 3.40GHz, 1 CPU, 28 logical and 20 physical cores
.NET SDK 10.0.203
  [Host]     : .NET 10.0.7 (10.0.7, 10.0.726.21808), X64 RyuJIT x86-64-v3
  DefaultJob : .NET 10.0.7 (10.0.7, 10.0.726.21808), X64 RyuJIT x86-64-v3


```
| Method    |      Mean |     Error | Ratio |
|-----------|----------:|----------:|------:|
| WriteByte | 0.3231 ns | 0.0009 ns |  1.00 |
| Write1    | 0.5013 ns | 0.0023 ns |  1.55 |
| WriteAny  | 1.1309 ns | 0.0038 ns |  3.50 |
