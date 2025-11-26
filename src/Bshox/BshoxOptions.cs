using System.Diagnostics;

namespace Bshox;

/// <summary>
/// Provides configuration options for Bshox serialization and deserialization.
/// </summary>
/// <remarks>Instances of this type are immutable. If no value is specified, the implementation should use <see cref="BshoxOptions.Default"/>.</remarks>
public sealed record BshoxOptions
{
    /// <summary>
    /// The default value for <see cref="MaxDepth"/>
    /// </summary>
    /// <remarks>
    /// This is the same value as <c>System.Text.Json.JsonReaderOptions.DefaultMaxDepth</c>
    /// </remarks>
    public const int DefaultMaxDepth = 64;

    /// <summary>
    /// The default value for <see cref="BshoxOptions"/>
    /// </summary>
    public static readonly BshoxOptions Default = new();

    /// <summary>
    /// The maximum depth of nested objects and arrays allowed during serialization and deserialization.<br/>
    /// If this value is exceeded, a <see cref="BshoxException"/> will be thrown.<br/>
    /// </summary>
    public int MaxDepth
    {
        get
        {
            Debug.Assert(field >= 0, "field >= 0");
            return field;
        }
        init
        {
#if NETCOREAPP
            ArgumentOutOfRangeException.ThrowIfNegativeOrZero(value);
#else
            if (value <= 0)
                throw new ArgumentOutOfRangeException(nameof(value));
#endif
            field = value;
        }
    } = DefaultMaxDepth;

    /// <summary>
    /// Sets whether multi-byte numeric values are encoded in little-endian byte order.<br/>
    /// This doesn't affect the encoding of variable-length integers or <see cref="Guid"/>.
    /// </summary>
    public bool LittleEndian { get; init; }

    /// <summary>
    /// <c>true</c> if the endianness of multi-byte numeric values should be reversed when reading or writing data.
    /// </summary>
    internal bool ReverseEndianness => LittleEndian != BitConverter.IsLittleEndian;
}
