```

BenchmarkDotNet v0.15.8, Windows 11 (10.0.26200.8246/25H2/2025Update/HudsonValley2)
Intel Core i7-14700KF 3.40GHz, 1 CPU, 28 logical and 20 physical cores
.NET SDK 10.0.203
  [Host]     : .NET 10.0.7 (10.0.7, 10.0.726.21808), X64 RyuJIT x86-64-v3
  DefaultJob : .NET 10.0.7 (10.0.7, 10.0.726.21808), X64 RyuJIT x86-64-v3


```
| Method  | Length |       Mean |     Error | Code Size |
|---------|--------|-----------:|----------:|----------:|
| Unicode | 10     |   9.762 ns | 0.1578 ns |   3,509 B |
| Ascii   | 10     |   3.528 ns | 0.0195 ns |   3,485 B |
| Unicode | 100    |  92.481 ns | 0.4549 ns |   4,668 B |
| Ascii   | 100    |  10.161 ns | 0.0363 ns |   4,566 B |
| Unicode | 1000   | 907.351 ns | 2.8238 ns |   4,631 B |
| Ascii   | 1000   |  63.853 ns | 1.1552 ns |   4,652 B |
