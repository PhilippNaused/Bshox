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
    public static BshoxContract<List<T>> List<T>(BshoxContract<T> contract) where T : notnull
    {
        return new ListContract<T>(contract);
    }

    /// <summary>
    /// A Bshox contract for an array of <typeparamref name="T"/>.
    /// </summary>
    public static BshoxContract<T[]> Array<T>(BshoxContract<T> contract) where T : notnull
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
            if (reader.Options.ReverseEndianness)
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
            if (writer.Options.ReverseEndianness)
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
            if (reader.Options.ReverseEndianness)
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
            if (writer.Options.ReverseEndianness)
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
    /// A Bshox contract for an enum of type <typeparamref name="T"/>, using the specified underlying type contract.
    /// </summary>
    /// <typeparam name="T">The enum type</typeparam>
    /// <param name="contract">The contract for the underlying type of <typeparamref name="T"/>.</param>
    /// <returns>A Bshox contract for <typeparamref name="T"/></returns>
    /// <exception cref="ArgumentException"><typeparamref name="T"/> has an unsupported underlying type.</exception>
    /// <exception cref="InvalidCastException"><paramref name="contract"/> is not a <see cref="BshoxContract{T}"/> for the underlying type of <typeparamref name="T"/>.</exception>
    public static BshoxContract<T> Enum<T>(IBshoxContract contract) where T : unmanaged, Enum
    {
#pragma warning disable IDE0072 // Add missing cases
        return Type.GetTypeCode(typeof(T)) switch
#pragma warning restore IDE0072 // Add missing cases
        {
            TypeCode.SByte => new EnumContract<T, sbyte>((BshoxContract<sbyte>)contract),
            TypeCode.Byte => new EnumContract<T, byte>((BshoxContract<byte>)contract),
            TypeCode.Int16 => new EnumContract<T, short>((BshoxContract<short>)contract),
            TypeCode.UInt16 => new EnumContract<T, ushort>((BshoxContract<ushort>)contract),
            TypeCode.Int32 => new EnumContract<T, int>((BshoxContract<int>)contract), // default value => hot path
            TypeCode.UInt32 => new EnumContract<T, uint>((BshoxContract<uint>)contract),
            TypeCode.Int64 => new EnumContract<T, long>((BshoxContract<long>)contract),
            TypeCode.UInt64 => new EnumContract<T, ulong>((BshoxContract<ulong>)contract),

            // The runtime also allows these types, but the language doesn't, so we don't support them:
            // char, float, double, nint, nuint, bool
            // See: https://github.com/dotnet/runtime/blob/d3425021075c54d095e7d6b3afd611c4fd81b913/src/coreclr/System.Private.CoreLib/src/System/Enum.CoreCLR.cs#L35
            _ => throw new ArgumentException($"Unsupported enum underlying type: {typeof(T).GetEnumUnderlyingType()}"),
        };
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
