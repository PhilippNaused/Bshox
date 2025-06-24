using System.Diagnostics.CodeAnalysis;

namespace Bshox.Utils;


[ExcludeFromCodeCoverage]
public static class RandomExtension
{
    public static int NextScaledInt(this Random rand)
    {
        int bits = rand.Next(0, 32);
        bool sign = rand.NextBool();
        int value = rand.Next(int.MaxValue >> bits);
        return sign ? -value : value;
    }

    public static float NextSingle(this Random rand, float min, float max)
    {
        return (float)rand.NextDouble(min, max);
    }

    public static DateTime NextDateTime(this Random rand)
    {
        return new DateTime(rand.NextLong(DateTime.MinValue.Ticks, DateTime.MaxValue.Ticks), DateTimeKind.Utc);
    }

    public static float[] NextArray(this Random rand, int count, float min, float max)
    {
        var list = new float[count];
        for (int i = 0; i < count; i++)
        {
            list[i] = rand.NextSingle(min, max);
        }
        return list;
    }

    public static int[] NextArray(this Random rand, int count, int min, int max)
    {
        var list = new int[count];
        for (int i = 0; i < count; i++)
        {
            list[i] = rand.Next(min, max);
        }
        return list;
    }

    public static char[] NextChars(this Random rand, int count = 20)
    {
        var list = new char[count];
        for (int i = 0; i < count; i++)
        {
            list[i] = (char)rand.Next(0, 256);
        }
        return list;
    }

    public static string NextString(this Random rand, int length = 20)
    {
        var chars = NextChars(rand, length);
        return new string(chars);
    }

    public static DateTime[] NextArray(this Random rand, int count)
    {
        var list = new DateTime[count];
        for (int i = 0; i < count; i++)
        {
            list[i] = rand.NextDateTime();
        }
        return list;
    }

    public static uint NextScaledUInt(this Random rand)
    {
        int bits = rand.Next(0, 32);
        return (uint)rand.NextULong(0, uint.MaxValue >> bits);
    }

    public static bool NextBool(this Random rand)
    {
        return rand.NextDouble() < 0.5;
    }

    public static ulong NextULong(this Random rand)
    {
        var buffer = new byte[sizeof(ulong)];
        rand.NextBytes(buffer);
        return BitConverter.ToUInt64(buffer, 0);
    }

    public static double NextDouble(this Random rand, double min, double max)
    {
        double num = max - min;
        return rand.NextDouble() * num + min;
    }

    public static long NextLong(this Random rand, long min, long max)
    {
        if (min == max)
            return min;

        ulong range = (ulong)(max - min);

        // Avoid introduction of modulo bias
        ulong limit = ulong.MaxValue - ulong.MaxValue % range;
        ulong raw;
        do
        {
            raw = rand.NextULong();
        }
        while (raw > limit);

        return (long)(raw % range + (ulong)min);
    }

    public static ulong NextULong(this Random rand, ulong min, ulong max)
    {
        if (min == max)
            return min;

        ulong range = max - min;

        if (range == 0)
            return min;

        // Avoid introduction of modulo bias
        ulong limit = ulong.MaxValue - ulong.MaxValue % range;
        ulong raw;
        do
        {
            raw = rand.NextULong();
        }
        while (raw > limit);

        return unchecked(raw % range + min);
    }

    public static ulong NextScaledULong(this Random rand)
    {
        int bits = rand.Next(0, 64);
        return rand.NextULong(0, ulong.MaxValue >> bits);
    }

    public static long NextScaledLong(this Random rand)
    {
        int bits = rand.Next(0, 64);
        bool sign = rand.NextBool();
        long value = rand.NextLong(0, long.MaxValue >> bits);
        return sign ? -value : value;
    }

    public static double NextDouble2(this Random rand)
    {
        return BitConverter.Int64BitsToDouble(rand.NextLong(long.MinValue, long.MaxValue));
    }

    public static T NextEnum<T>(this Random rand) where T : struct, Enum
    {
#if NETCOREAPP
        var values = Enum.GetValues<T>();
#else
        var values = Enum.GetValues(typeof(T)).Cast<T>().ToArray();
#endif
        return values[rand.Next(values.Length)];
    }
}
