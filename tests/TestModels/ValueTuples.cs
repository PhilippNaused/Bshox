using System.Diagnostics.CodeAnalysis;
using Bshox.Attributes;

namespace TestModels;

[ExcludeFromCodeCoverage]
[BshoxSerializable<ValueTuple<int>>] // 1
[BshoxSerializable<(int, long)>] // 2
[BshoxSerializable(typeof((uint, string?, byte)))] // 3
[BshoxSerializable<(int, int, int, int)>] // 4
[BshoxSerializable<(int, int, int, int, int)>] // 5
[BshoxSerializable<(int, int, int, int, int, int)>] // 6
[BshoxSerializable<(int, int, int, int, int, int, int)>] // 7
[BshoxSerializable<(int, int, int, int, int, int, int, int)>] // 8
[BshoxSerializable<(int, int, int, int, int, int, int, int, int)>] // 9
public partial class ValueTupleSerializer;
