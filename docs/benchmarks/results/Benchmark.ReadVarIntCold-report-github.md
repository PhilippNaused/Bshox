```

BenchmarkDotNet v0.15.8, Windows 11 (10.0.26200.8246/25H2/2025Update/HudsonValley2)
Intel Core i7-14700KF 3.40GHz, 1 CPU, 28 logical and 20 physical cores
.NET SDK 10.0.203
  [Host] : .NET 10.0.7 (10.0.7, 10.0.726.21808), X64 RyuJIT x86-64-v3
  Dry    : .NET 10.0.7 (10.0.7, 10.0.726.21808), X64 RyuJIT x86-64-v3

Job=Dry  IterationCount=1  LaunchCount=25
RunStrategy=ColdStart  UnrollFactor=1  WarmupCount=1

```
| Method   |     Mean |    Error | Ratio | Code Size |
|----------|---------:|---------:|------:|----------:|
| ReadByte | 629.6 ns | 27.42 ns |  1.00 |        NA |
| Read1    | 672.0 ns | 18.78 ns |  1.07 |     581 B |
| ReadAny  | 682.3 ns | 15.91 ns |  1.09 |     584 B |
