```

BenchmarkDotNet v0.15.8, Windows 11 (10.0.26200.8246/25H2/2025Update/HudsonValley2)
Intel Core i7-14700KF 3.40GHz, 1 CPU, 28 logical and 20 physical cores
.NET SDK 10.0.203
  [Host]     : .NET 10.0.7 (10.0.7, 10.0.726.21808), X64 RyuJIT x86-64-v3
  DefaultJob : .NET 10.0.7 (10.0.7, 10.0.726.21808), X64 RyuJIT x86-64-v3


```
| Method  | Length |       Mean |     Error | Code Size |
|---------|--------|-----------:|----------:|----------:|
| Unicode | 10     |   9.836 ns | 0.1941 ns |   3,719 B |
| Ascii   | 10     |   3.523 ns | 0.0051 ns |   3,815 B |
| Unicode | 100    |  89.793 ns | 0.6933 ns |   4,934 B |
| Ascii   | 100    |  10.023 ns | 0.0260 ns |   4,893 B |
| Unicode | 1000   | 915.770 ns | 2.2570 ns |   4,958 B |
| Ascii   | 1000   |  63.715 ns | 0.5293 ns |   4,925 B |
