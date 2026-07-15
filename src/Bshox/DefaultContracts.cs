#if NETCOREAPP
using System.Buffers;
#else
using System.Buffers.Text;
#endif
using System.Diagnostics;
using System.Numerics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Bshox.Internals;

// ReSharper disable InconsistentNaming

namespace Bshox;

/// <summary>
/// This static class contains all the built-in Bshox contracts.
/// </summary>
public static partial class DefaultContracts
{
    /// <summary>
    /// A Bshox contract for <see cref="System.Nullable{T}"/>.
    /// </summary>
    /// <remarks>
    /// This contract does not support <see langword="null"/> values.
    /// It will throw an <see cref="ArgumentNullException"/> if you try to serialize a <see langword="null"/> value, and it will never return <see langword="null"/> when deserializing.
    /// </remarks>
    public static BshoxContract<T?> Nullable<T>(BshoxContract<T> contract) where T : struct
    {
        return new NullableContract<T>(contract);
    }

    private sealed class NullableContract<T>(BshoxContract<T> contract) : BshoxContract<T?>(contract.Encoding) where T : struct
    {
        public override void Deserialize(ref BshoxReader reader, out T? value)
        {
            contract.Deserialize(ref reader, out T value2);
            value = value2;
        }

        public override void Serialize(ref BshoxWriter writer, scoped ref readonly T? value)
        {
            if (value is null)
            {
                throw new ArgumentNullException(nameof(value));
            }
            T value2 = value.Value;
            contract.Serialize(ref writer, in value2);
        }
    }

    private partial class GuidContract
    {
        public override partial void Deserialize(ref BshoxReader reader, out Guid value)
        {
            const int sizeofGuid = 16; // sizeof(Guid);
            byte length = reader.ReadByte();
            if (length != sizeofGuid)
            {
                throw Fail(length);
            }
            reader.ReadUnsafe(out value);

            // UUID is big-endian. See https://datatracker.ietf.org/doc/html/rfc4122#section-4.1.2
            if (BitConverter.IsLittleEndian)
            {
                EndiannessHelper.Reverse(ref value);
            }

            [MethodImpl(MethodImplOptions.NoInlining)]
            static BshoxException Fail(int length) => new($"Expected {sizeofGuid} bytes but got {length}");
        }

        public override partial void Serialize(ref BshoxWriter writer, scoped ref readonly Guid value)
        {
            const int sizeofGuid = 16;
            ref byte bytes = ref writer.GetRef(sizeofGuid + 1);
            bytes = sizeofGuid; // length prefix
            ref Guid guid = ref Unsafe.As<byte, Guid>(ref Unsafe.Add(ref bytes, 1));
            guid = value;
            if (BitConverter.IsLittleEndian)
            {
                // UUID is big-endian. See https://datatracker.ietf.org/doc/html/rfc4122#section-4.1.2
                EndiannessHelper.Reverse(ref guid);
            }
            writer.Advance(sizeofGuid + 1);
        }
    }

    private partial class DecimalContract
    {
        private const int DecimalMaxLength = 31; // Decimal can be up to 29 digits, plus sign and decimal point

        public override partial void Deserialize(ref BshoxReader reader, out decimal value)
        {
            byte length = reader.ReadByte();
            if (length > DecimalMaxLength)
            {
                throw Fail(length);
            }
            Span<byte> buffer = stackalloc byte[length];
            reader.CopyTo(buffer);
#if NETCOREAPP
            var success = decimal.TryParse(buffer, System.Globalization.NumberStyles.Number, System.Globalization.CultureInfo.InvariantCulture, out value);
#else
            var success = Utf8Parser.TryParse(buffer, out value, out int consumed) && consumed == length;
#endif
            if (!success)
            {
                throw Fail2();
            }

            return;
            [MethodImpl(MethodImplOptions.NoInlining)]
            static BshoxException Fail(int length) => new($"Invalid decimal length: {length}");

            [MethodImpl(MethodImplOptions.NoInlining)]
            static BshoxException Fail2() => new("Invalid decimal encoding");
        }

        public override partial void Serialize(ref BshoxWriter writer, scoped ref readonly decimal value)
        {
            var span = writer.GetSpan(DecimalMaxLength + 1);
#if NETCOREAPP
            var success = value.TryFormat(span.Slice(1), out int bytesWritten, default, System.Globalization.CultureInfo.InvariantCulture);
#else
            var success = Utf8Formatter.TryFormat(value, span.Slice(1), out int bytesWritten);
#endif
            // This can only fail if the buffer is too small.
            Debug.Assert(success, "decimal formatting failed!");
            Debug.Assert(bytesWritten <= DecimalMaxLength, "bytesWritten <= DecimalMaxLength");
            span[0] = (byte)bytesWritten;
            writer.Advance(bytesWritten + 1);
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

    private partial class ComplexContract
    {
        private const byte _tagReal = (1 << 3) | (byte)BshoxEncoding.Fixed8;
        private const byte _tagImaginary = (2 << 3) | (byte)BshoxEncoding.Fixed8;

        public override partial void Deserialize(ref BshoxReader reader, out Complex value)
        {
            double re = 0, im = 0;
            byte tag = reader.ReadByte();
            if (tag == _tagReal)
            {
                re = reader.ReadDouble();
                tag = reader.ReadByte();
            }
            if (tag == _tagImaginary)
            {
                im = reader.ReadDouble();
                tag = reader.ReadByte();
            }
            if (tag == 0)
            {
                value = new Complex(re, im);
                return;
            }
            throw BshoxException.UnexpectedTag(tag);
        }

        public override partial void Serialize(ref BshoxWriter writer, scoped ref readonly Complex value)
        {
            if (value.Real != 0)
            {
                writer.WriteByte(_tagReal);
                writer.WriteDouble(value.Real);
            }
            if (value.Imaginary != 0)
            {
                writer.WriteByte(_tagImaginary);
                writer.WriteDouble(value.Imaginary);
            }
            writer.WriteByte(0);
        }
    }

    private partial class BigIntegerContract
    {
        [SkipLocalsInit] // prevents zeroing the stack-allocated buffer
        public override partial void Deserialize(ref BshoxReader reader, out BigInteger value)
        {
#if NETCOREAPP
            var size = (int)reader.ReadVarInt32();
            if (size == 0)
            {
                value = System.Numerics.BigInteger.Zero;
                return;
            }
            const int MaxStackAllocSize = 256;
            if (size <= MaxStackAllocSize)
            {
                unsafe
                {
                    Span<byte> span = stackalloc byte[size];
                    reader.CopyTo(span);
                    value = new BigInteger(span, isBigEndian: !reader.Options.LittleEndian);
                }
            }
            else
            {
                DeserializeSlow(ref reader, size, out value);
            }
#else
            var bytes = reader.ReadByteArray();
            if (bytes.Length == 0)
            {
                value = System.Numerics.BigInteger.Zero;
                return;
            }
            if (!reader.Options.LittleEndian)
            {
                Span<byte> span = bytes;
                span.Reverse();
            }
            value = new BigInteger(bytes); // this constructor expects little-endian
#endif
        }

#if NETCOREAPP
        [MethodImpl(MethodImplOptions.NoInlining)]
        private static void DeserializeSlow(ref BshoxReader reader, int size, out BigInteger value)
        {
            var bytes = ArrayPool<byte>.Shared.Rent(size);
            try
            {
                var span = new Span<byte>(bytes, 0, size);
                reader.CopyTo(span);
                value = new BigInteger(span, isBigEndian: !reader.Options.LittleEndian);
            }
            finally
            {
                ArrayPool<byte>.Shared.Return(bytes);
            }
        }
#endif

        public override partial void Serialize(ref BshoxWriter writer, scoped ref readonly BigInteger value)
        {
            if (value.IsZero)
            {
                writer.WriteByte(0);
                return;
            }
#if NETCOREAPP
            var size = value.GetByteCount();
            writer.WriteVarInt32((uint)size);
            var span = writer.GetSpan(size);
            var success = value.TryWriteBytes(span, out int bytesWritten, isBigEndian: !writer.Options.LittleEndian);
            writer.Advance(bytesWritten);
            Debug.Assert(success, "BigInteger formatting failed!");
            Debug.Assert(bytesWritten == size, "bytesWritten == size");
#else
            var bytes = value.ToByteArray(); // this is always little-endian
            if (!writer.Options.LittleEndian)
            {
                Span<byte> span = bytes;
                span.Reverse();
            }
            writer.WriteByteArray(bytes);
#endif
        }
    }

#pragma warning disable CA1822 // Mark members as static (false-positive: https://github.com/dotnet/roslyn-analyzers/issues/7447)

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
    /// A Bshox contract for an enum of type <typeparamref name="T"/>, using the built-in default contract for the underlying type.
    /// </summary>
    /// <typeparam name="T">The enum type</typeparam>
    /// <returns>A Bshox contract for <typeparamref name="T"/></returns>
    /// <exception cref="ArgumentException"><typeparamref name="T"/> has an unsupported underlying type.</exception>
    public static BshoxContract<T> Enum<T>() where T : unmanaged, Enum
    {
        return EnumContractCache<T>.Instance;
    }

    private static class EnumContractCache<T> where T : unmanaged, Enum
    {
        // This cache ensures that we only create one instance for each enum type.

        internal static readonly BshoxContract<T> Instance = Make();

        private static BshoxContract<T> Make()
        {
#pragma warning disable IDE0072 // Add missing cases
            return Type.GetTypeCode(typeof(T)) switch
#pragma warning restore IDE0072 // Add missing cases
            {
                TypeCode.SByte => new EnumContract<T, sbyte>(SByte),
                TypeCode.Byte => new EnumContract<T, byte>(Byte),
                TypeCode.Int16 => new EnumContract<T, short>(Int16),
                TypeCode.UInt16 => new EnumContract<T, ushort>(UInt16),
                TypeCode.Int32 => new EnumContract<T, int>(Int32),
                TypeCode.UInt32 => new EnumContract<T, uint>(UInt32),
                TypeCode.Int64 => new EnumContract<T, long>(Int64),
                TypeCode.UInt64 => new EnumContract<T, ulong>(UInt64),

                // The runtime also allows these types, but the language doesn't, so we don't support them:
                // char, float, double, nint, nuint, bool
                // See: https://github.com/dotnet/runtime/blob/d3425021075c54d095e7d6b3afd611c4fd81b913/src/coreclr/System.Private.CoreLib/src/System/Enum.CoreCLR.cs#L35
                _ => throw new ArgumentException($"Unsupported enum underlying type: {typeof(T).GetEnumUnderlyingType()}"),
            };
        }
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
