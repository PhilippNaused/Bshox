using System.Runtime.CompilerServices;

namespace Bshox.TestUtils;

#pragma warning disable IDE0051 // Remove unused private members (false positive for extension blocks)
#pragma warning disable CA1034 // Nested types should not be visible (false positive for extension blocks)

public static class RandomExtension
{
    extension(Random rand)
    {
        public int NextScaledInt()
        {
            int bits = rand.Next(0, 32);
            bool sign = rand.NextBool();
            int value = rand.Next(int.MaxValue >> bits);
            return sign ? -value : value;
        }

        public float NextSingle(float min, float max)
        {
            return (float)rand.NextDouble(min, max);
        }

        public DateTime NextDateTime()
        {
            return new DateTime(rand.NextLong(DateTime.MinValue.Ticks, DateTime.MaxValue.Ticks), DateTimeKind.Utc);
        }

        public float[] NextArray(int count, float min, float max)
        {
            var list = new float[count];
            for (int i = 0; i < count; i++)
            {
                list[i] = rand.NextSingle(min, max);
            }
            return list;
        }

        public int[] NextArray(int count, int min, int max)
        {
            var list = new int[count];
            for (int i = 0; i < count; i++)
            {
                list[i] = rand.Next(min, max);
            }
            return list;
        }

        [SkipLocalsInit]
        public string NextString(int length = 20)
        {
#if NETCOREAPP
            Span<char> chars = stackalloc char[length];
#else
            char[] chars = new char[length];
#endif
            for (int i = 0; i < length; i++)
            {
                chars[i] = (char)rand.Next(char.MinValue, char.MaxValue + 1);
                if (char.IsSurrogate(chars[i]))
                {
                    i--; // Retry this position if a surrogate character is generated
                }
            }
            // TODO: add chance to return string with a valid surrogate pair.
            return new string(chars);
        }

        public DateTime[] NextArray(int count)
        {
            var list = new DateTime[count];
            for (int i = 0; i < count; i++)
            {
                list[i] = rand.NextDateTime();
            }
            return list;
        }

        public uint NextScaledUInt()
        {
            int bits = rand.Next(0, 32);
            return (uint)rand.NextULong(0, uint.MaxValue >> bits);
        }

        public bool NextBool()
        {
            return rand.NextDouble() < 0.5;
        }

        public ulong NextULong()
        {
            var buffer = new byte[sizeof(ulong)];
            rand.NextBytes(buffer);
            return BitConverter.ToUInt64(buffer, 0);
        }

        public double NextDouble(double min, double max)
        {
            double num = max - min;
            return rand.NextDouble() * num + min;
        }

        public long NextLong(long min, long max)
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

        public ulong NextULong(ulong min, ulong max)
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

        public ulong NextScaledULong()
        {
            int bits = rand.Next(0, 64);
            return rand.NextULong(0, ulong.MaxValue >> bits);
        }

        public long NextScaledLong()
        {
            int bits = rand.Next(0, 64);
            bool sign = rand.NextBool();
            long value = rand.NextLong(0, long.MaxValue >> bits);
            return sign ? -value : value;
        }

        public double NextDouble2()
        {
            return BitConverter.Int64BitsToDouble(rand.NextLong(long.MinValue, long.MaxValue));
        }

        public T NextEnum<T>() where T : struct, Enum
        {
#if NETCOREAPP
            var values = Enum.GetValues<T>();
#else
            var values = Enum.GetValues(typeof(T)).Cast<T>().ToArray();
#endif
            return values[rand.Next(values.Length)];
        }

        [SkipLocalsInit]
        public Guid NextGuid()
        {
#if NETCOREAPP
            Span<byte> bytes = stackalloc byte[16];
#else
            byte[] bytes = new byte[16];
#endif
            rand.NextBytes(bytes);
            // Guid.NewGuid(); NewGuid will only generate version 4 guids, but we want to test any possible 16 byte value.
            return new Guid(bytes);
        }
    }
}
