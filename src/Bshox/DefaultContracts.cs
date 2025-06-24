using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Bshox.Contracts;
using Bshox.Internals;

namespace Bshox;

/// <summary>
/// This static class contains all the built-in Bshox contracts.
/// </summary>
public static partial class DefaultContracts
{
    /// <summary>
    /// A Bshox contract for a <see cref="System.Collections.Generic.Dictionary{TKey,TValue}"/>.
    /// </summary>
    public static BshoxContract<Dictionary<TKey, TValue>> Dictionary<TKey, TValue>(BshoxContract<TKey> keyContract, BshoxContract<TValue> valueContract) where TKey : notnull
    {
        return new DictionaryContract<TKey, TValue>(keyContract, valueContract);
    }

    /// <summary>
    /// A Bshox contract for a <see cref="System.Collections.Generic.List{T}"/>.
    /// </summary>
    public static BshoxContract<List<T>> List<T>(BshoxContract<T> contract)
    {
        return new ListContract<T>(contract);
    }

    /// <summary>
    /// A Bshox contract for an array of <typeparamref name="T"/>.
    /// </summary>
    public static BshoxContract<T[]> Array<T>(BshoxContract<T> contract)
    {
        return new ArrayContract<T>(contract);
    }

    private partial class GuidContract
    {
        public override partial void Deserialize(ref BshoxReader reader, out Guid value)
        {
            const int sizeofGuid = 16; // sizeof(Guid);
            byte length = reader.ReadByte();
            if (length != sizeofGuid)
            {
                throw new BshoxException($"Expected {sizeofGuid} bytes but got {length}");
            }
            value = reader.ReadUnsafe<Guid>();

            // UUID is big-endian. See https://datatracker.ietf.org/doc/html/rfc4122#section-4.1.2
            if (BitConverter.IsLittleEndian)
            {
                value = EndiannessHelper.Reverse(value);
            }
        }

        public override partial void Serialize(ref BshoxWriter writer, scoped ref readonly Guid value)
        {
            const int sizeofGuid = 16;
            ref byte bytes = ref writer.GetRef(sizeofGuid + 1);
            bytes = sizeofGuid;
            if (BitConverter.IsLittleEndian) // UUID is big-endian. See https://datatracker.ietf.org/doc/html/rfc4122#section-4.1.2
            {
                Unsafe.WriteUnaligned(ref Unsafe.Add(ref bytes, 1), EndiannessHelper.Reverse(value));
            }
            else
            {
                Unsafe.WriteUnaligned(ref Unsafe.Add(ref bytes, 1), value);
            }
            writer.Advance(sizeofGuid + 1);
        }
    }

    private partial class DateTimeContract
    {
        public override partial void Deserialize(ref BshoxReader reader, out DateTime value)
        {
            long ticks = unchecked((long)reader.ReadUInt64());
            value = new DateTime(ticks, DateTimeKind.Utc);
        }

        public override partial void Serialize(ref BshoxWriter writer, scoped ref readonly DateTime value)
        {
            // Local and Unspecified are both treated as Local
            long ticks = TimeZoneInfo.ConvertTimeToUtc(value).Ticks;
            writer.WriteUInt64(unchecked((ulong)ticks));
        }
    }

    private partial class TimeSpanContract
    {
        public override partial void Deserialize(ref BshoxReader reader, out TimeSpan value)
        {
            value = new TimeSpan(reader.ReadZigZagVarInt64());
        }

        public override partial void Serialize(ref BshoxWriter writer, scoped ref readonly TimeSpan value)
        {
            writer.WriteZigZagVarInt64(value.Ticks);
        }
    }

#pragma warning disable CA1822 // Mark members as static https://github.com/dotnet/roslyn-analyzers/issues/7447

    private partial class SingleContract
    {
        public partial void Deserialize(ref BshoxReader reader, Span<float> destination)
        {
            reader.CopyTo(MemoryMarshal.AsBytes(destination));
            if (BitConverter.IsLittleEndian)
            {
                var span = MemoryMarshal.Cast<float, int>(destination);
                EndiannessHelper.Reverse(span, span);
            }
        }

        public partial void Serialize(ref BshoxWriter writer, ReadOnlySpan<float> values)
        {
            int size = values.Length * sizeof(float);
            var source = MemoryMarshal.Cast<float, int>(values);
            var dest = MemoryMarshal.Cast<byte, int>(writer.GetSpan(size).Slice(0, size));
            if (BitConverter.IsLittleEndian)
            {
                EndiannessHelper.Reverse(source, dest);
            }
            else
            {
                source.CopyTo(dest);
            }
            writer.Advance(size);
        }
    }

    private partial class DoubleContract
    {
        public partial void Deserialize(ref BshoxReader reader, Span<double> destination)
        {
            reader.CopyTo(MemoryMarshal.AsBytes(destination));
            if (BitConverter.IsLittleEndian)
            {
                var span = MemoryMarshal.Cast<double, long>(destination);
                EndiannessHelper.Reverse(span, span);
            }
        }

        public partial void Serialize(ref BshoxWriter writer, ReadOnlySpan<double> values)
        {
            int size = values.Length * sizeof(double);
            var source = MemoryMarshal.Cast<double, long>(values);
            var dest = MemoryMarshal.Cast<byte, long>(writer.GetSpan(size).Slice(0, size));
            if (BitConverter.IsLittleEndian)
            {
                EndiannessHelper.Reverse(source, dest);
            }
            else
            {
                source.CopyTo(dest);
            }
            writer.Advance(size);
        }
    }

#pragma warning restore CA1822 // Mark members as static

    /// <summary>
    /// TODO
    /// </summary>
    /// <typeparam name="TEnum">The enum type</typeparam>
    /// <typeparam name="TInner">The underlying primitive type of the enum</typeparam>
    /// <param name="contract">TODO</param>
    /// <returns>TODO</returns>
    /// <exception cref="ArgumentException">TODO</exception>
    public static BshoxContract<TEnum> Enum<TEnum, TInner>(BshoxContract<TInner> contract) where TEnum : unmanaged, Enum where TInner : unmanaged
    {
        if (typeof(TEnum).GetEnumUnderlyingType() != typeof(TInner))
        {
            throw new ArgumentException("The underlying type of the enum must be the same as the provided contract type.");
        }
        return new EnumContract<TEnum, TInner>(contract);
    }

    private sealed class EnumContract<TEnum, TInner>(BshoxContract<TInner> contract) : BshoxContract<TEnum>(contract.Encoding) where TEnum : unmanaged, Enum where TInner : unmanaged
    {
        public override void Deserialize(ref BshoxReader reader, out TEnum value)
        {
            contract.Deserialize(ref reader, out TInner value2);
            value = Unsafe.As<TInner, TEnum>(ref value2);
        }

        public override void Serialize(ref BshoxWriter writer, scoped ref readonly TEnum value)
        {
            TEnum v2 = value;
            contract.Serialize(ref writer, in Unsafe.As<TEnum, TInner>(ref v2));
        }
    }
}
