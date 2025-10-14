using System.Diagnostics.CodeAnalysis;
using Bshox.Attributes;

namespace TestModels;

[ExcludeFromCodeCoverage]
[BshoxSerializer(
    typeof(ValueTuple<int>), // 1
    typeof((int, long)), // 2
    typeof((uint, string?, byte)), // 3
    typeof((int, int, int, int)), // 4
    typeof((int, int, int, int, int)), // 5
    typeof((int, int, int, int, int, int)), // 6
    typeof((int, int, int, int, int, int, int)), // 7
    typeof((int, int, int, int, int, int, int, int)), // 8
    typeof((int, int, int, int, int, int, int, int, int)) // 9
    )]
public partial class ValueTupleSerializer;
