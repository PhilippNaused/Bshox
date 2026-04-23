```

BenchmarkDotNet v0.15.8, Windows 11 (10.0.26200.8246/25H2/2025Update/HudsonValley2)
Intel Core i7-14700KF 3.40GHz, 1 CPU, 28 logical and 20 physical cores
.NET SDK 10.0.203
  [Host]     : .NET 10.0.7 (10.0.7, 10.0.726.21808), X64 RyuJIT x86-64-v3
  DefaultJob : .NET 10.0.7 (10.0.7, 10.0.726.21808), X64 RyuJIT x86-64-v3


```
| Method    |      Mean |     Error | Ratio |
|-----------|----------:|----------:|------:|
| WriteByte | 0.3219 ns | 0.0008 ns |  1.00 |
| Write1    | 0.4940 ns | 0.0013 ns |  1.53 |
| Write2    | 0.7350 ns | 0.0014 ns |  2.28 |
| Write5    | 1.6380 ns | 0.0039 ns |  5.09 |
| WriteAny  | 1.1436 ns | 0.0028 ns |  3.55 |
