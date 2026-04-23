using Bshox.TestUtils;

#pragma warning disable CA1051 // Do not declare visible instance fields

namespace Benchmark;

public abstract class VarIntBase
{
    public const int Count = 1000;
    protected readonly uint[] values1;
    protected readonly uint[] values2;
    protected readonly uint[] values3;
    protected readonly uint[] values4;
    protected readonly uint[] values5;
    protected readonly uint[] valuesX;

    protected VarIntBase()
    {
        var random = new Random(42);
        values1 = new uint[Count];
        values2 = new uint[Count];
        values3 = new uint[Count];
        values4 = new uint[Count];
        values5 = new uint[Count];
        valuesX = new uint[Count];
        for (int i = 0; i < Count; i++)
        {
            const uint max1 = 1 << (1 * 7);
            const uint max2 = 1 << (2 * 7);
            const uint max3 = 1 << (3 * 7);
            const uint max4 = 1 << (4 * 7);
            const uint max5 = uint.MaxValue;
            values1[i] = (uint)random.NextULong(0, max1);
            values2[i] = (uint)random.NextULong(max1, max2);
            values3[i] = (uint)random.NextULong(max2, max3);
            values4[i] = (uint)random.NextULong(max3, max4);
            values5[i] = (uint)random.NextULong(max4, max5);
            valuesX[i] = random.NextScaledUInt();
        }
    }
}
