using Bshox.TestUtils;

namespace Bshox.Tests;

internal static class ExampleData
{
    private const int TestCount = 20;
    private const int Seed = 42;

    private static Random Randomizer => new(Seed);

    public static IEnumerable<string> Strings()
    {
        yield return "";
        yield return "\0";
        yield return "\xFF";
        yield return "Hello, World!";
        yield return "👋🌍";
        yield return "你好，世界！";
        yield return "こんにちは、世界！";
        yield return "안녕하세요, 세계!";
        yield return "مرحبا بالعالم!";

        var rand = Randomizer;
        for (int i = 0; i < TestCount; i++)
        {
            yield return rand.NextString(20);
        }
    }

    public static IEnumerable<float> Floats()
    {
        yield return float.MaxValue;
        yield return float.MinValue;
        yield return float.NaN;
        yield return float.PositiveInfinity;
        yield return float.NegativeInfinity;
        yield return 0.0f;
        yield return 1.0f;
        yield return -1.0f;
        yield return float.Epsilon;
        yield return -float.Epsilon;

        var rand = Randomizer;
        for (int i = 0; i < TestCount; i++)
        {
            yield return (float)rand.NextDouble(float.MinValue, float.MaxValue);
        }
    }

    public static IEnumerable<double> Doubles()
    {
        yield return double.MaxValue;
        yield return double.MinValue;
        yield return double.NaN;
        yield return double.PositiveInfinity;
        yield return double.NegativeInfinity;
        yield return 0.0;
        yield return 1.0;
        yield return -1.0;
        yield return double.Epsilon;
        yield return -double.Epsilon;

        var rand = Randomizer;
        for (int i = 0; i < TestCount; i++)
        {
            yield return rand.NextDouble2();
        }
    }

    public static IEnumerable<decimal> Decimals()
    {
        yield return decimal.MaxValue;
        yield return decimal.MinValue;
        yield return decimal.Zero;
        yield return decimal.One;
        yield return decimal.MinusOne;
        yield return (decimal)Math.PI;

        var rand = Randomizer;
        for (int i = 0; i < TestCount; i++)
        {
            yield return new(rand.NextULong());
        }
    }

    public static IEnumerable<char> Chars()
    {
        yield return char.MaxValue;
        yield return char.MinValue;
        yield return 'A';
        yield return '\r';
        yield return '\n';
        yield return '안';
        yield return '你';

        var rand = Randomizer;
        for (int i = 0; i < TestCount; i++)
        {
            yield return (char)rand.Next(char.MinValue, char.MaxValue);
        }
    }

    public static IEnumerable<byte> Bytes()
    {
        yield return byte.MaxValue;
        yield return 0;
        yield return 1;

        var rand = Randomizer;
        for (int i = 0; i < TestCount; i++)
        {
            yield return (byte)rand.Next(byte.MinValue, byte.MaxValue);
        }
    }

    public static IEnumerable<sbyte> SBytes()
    {
        yield return sbyte.MaxValue;
        yield return sbyte.MinValue;
        yield return 0;
        yield return 1;
        yield return -1;

        var rand = Randomizer;
        for (int i = 0; i < TestCount; i++)
        {
            yield return (sbyte)rand.Next(sbyte.MinValue, sbyte.MaxValue);
        }
    }

    public static IEnumerable<short> Shorts()
    {
        yield return short.MaxValue;
        yield return short.MinValue;
        yield return 0;
        yield return 1;
        yield return -1;

        var rand = Randomizer;
        for (int i = 0; i < TestCount; i++)
        {
            yield return (short)rand.Next(short.MinValue, short.MaxValue);
        }
    }

    public static IEnumerable<ushort> UShorts()
    {
        yield return ushort.MaxValue;
        yield return 0;
        yield return 1;

        var rand = Randomizer;
        for (int i = 0; i < TestCount; i++)
        {
            yield return (ushort)rand.Next(ushort.MaxValue);
        }
    }

    public static IEnumerable<int> Ints()
    {
        yield return int.MaxValue;
        yield return int.MinValue;
        yield return 0;
        yield return 1;
        yield return -1;
        yield return BshoxConsts.Min3ByteInt;
        yield return BshoxConsts.Min3ByteInt - 1;
        yield return BshoxConsts.Max3ByteInt;
        yield return BshoxConsts.Max3ByteInt + 1;

        var rand = Randomizer;
        for (int i = 0; i < TestCount; i++)
        {
            yield return rand.Next(int.MinValue, int.MaxValue);
        }
    }

    public static IEnumerable<uint> UInts()
    {
        yield return uint.MaxValue;
        yield return 0;
        yield return 1;
        yield return BshoxConsts.Max3ByteInt;
        yield return BshoxConsts.Max3ByteInt + 1;

        var rand = Randomizer;
        for (int i = 0; i < TestCount; i++)
        {
            yield return (uint)rand.NextULong(0, uint.MaxValue);
        }
    }

    public static IEnumerable<long> Longs()
    {
        yield return long.MaxValue;
        yield return long.MinValue;
        yield return 0;
        yield return 1;
        yield return -1;
        yield return BshoxConsts.Min3ByteInt;
        yield return BshoxConsts.Min3ByteInt - 1;
        yield return BshoxConsts.Max3ByteInt;
        yield return BshoxConsts.Max3ByteInt + 1;

        var rand = Randomizer;
        for (int i = 0; i < TestCount; i++)
        {
            yield return rand.NextLong(long.MinValue, long.MaxValue);
        }
    }

    public static IEnumerable<ulong> ULongs()
    {
        yield return ulong.MaxValue;
        yield return 0;
        yield return 1;
        yield return BshoxConsts.Max3ByteInt;
        yield return BshoxConsts.Max3ByteInt + 1;

        var rand = Randomizer;
        for (int i = 0; i < TestCount; i++)
        {
            yield return rand.NextULong();
        }
    }

    public static IEnumerable<DateTime> DateTimes()
    {
        var rand = Randomizer;
        for (int i = 0; i < TestCount; i++)
        {
            yield return new DateTime(rand.NextLong(DateTime.MinValue.Ticks, DateTime.MaxValue.Ticks), DateTimeKind.Utc);
        }
    }

    public static IEnumerable<TimeSpan> TimeSpans()
    {
        yield return TimeSpan.MaxValue;
        yield return TimeSpan.MinValue;
        yield return TimeSpan.Zero;
        yield return TimeSpan.FromDays(1);
        yield return TimeSpan.FromHours(1);
        yield return TimeSpan.FromMinutes(1);
        yield return TimeSpan.FromSeconds(1);
        yield return TimeSpan.FromMilliseconds(1);
        yield return TimeSpan.FromTicks(1);

        var rand = Randomizer;
        for (int i = 0; i < TestCount; i++)
        {
            yield return new TimeSpan(rand.NextScaledLong());
        }
    }

    public static IEnumerable<Guid> Guids()
    {
        yield return Guid.Empty;

        var rand = Randomizer;
        for (int i = 0; i < TestCount; i++)
        {
            byte[] bytes = new byte[16];
            rand.NextBytes(bytes);
            yield return new Guid();
        }
    }

    public static IEnumerable<byte[]> ByteArrays()
    {
        yield return [];
        yield return [0];
        yield return [byte.MaxValue];
        yield return [0, 1];
        yield return [0, 1, 2];

        var rand = Randomizer;
        for (int i = 0; i < TestCount; i++)
        {
            byte[] bytes = new byte[rand.Next(1, 11)];
            rand.NextBytes(bytes);
            yield return bytes;
        }
    }

    public static IEnumerable<List<int>> IntLists()
    {
        yield return [];
        yield return [0];
        yield return [0, 1];
        yield return [0, 1, 2];

        var rand = Randomizer;
        for (int i = 0; i < TestCount; i++)
        {
            List<int> list = [];
            for (int j = 0; j < rand.Next(1, 11); j++)
            {
                list.Add(rand.NextScaledInt());
            }
            yield return list;
        }
    }

    public static IEnumerable<List<string>> StringLists()
    {
        yield return [];
        yield return [""];
        yield return ["Hello", "World"];
        yield return ["👋", "🌍"];
        yield return ["你好", "世界"];
        yield return ["こんにちは", "世界"];

        var rand = Randomizer;
        for (int i = 0; i < TestCount; i++)
        {
            List<string> list = [];
            for (int j = 0; j < rand.Next(1, 11); j++)
            {
                list.Add(rand.NextString());
            }
            yield return list;
        }
    }

    public static IEnumerable<Version> Versions()
    {
        yield return new Version(0, 0);
        yield return new Version(1, 0);
        yield return new Version(1, 2);
        yield return new Version(int.MaxValue, int.MaxValue);
        yield return new Version(1, 2, 3);
        yield return new Version(int.MaxValue, int.MaxValue, int.MaxValue);
        yield return new Version(1, 2, 3, 4);
        yield return new Version(int.MaxValue, int.MaxValue, int.MaxValue, int.MaxValue);

        var rand = Randomizer;
        for (int i = 0; i < TestCount / 3; i++)
        {
            yield return new Version(NextScaledInt2(rand), NextScaledInt2(rand));
            yield return new Version(NextScaledInt2(rand), NextScaledInt2(rand), NextScaledInt2(rand));
            yield return new Version(NextScaledInt2(rand), NextScaledInt2(rand), NextScaledInt2(rand), NextScaledInt2(rand));
        }
        yield break;

        static int NextScaledInt2(Random rand)
        {
            int bits = rand.Next(0, 32);
            return rand.Next(int.MaxValue >> bits);
        }
    }
}
