using System.Diagnostics;

namespace Bshox;

public sealed record BshoxOptions
{
    public const int DefaultMaxDepth = 64; // same as System.Text.Json.JsonReaderOptions.DefaultMaxDepth

    public static readonly BshoxOptions Default = new();

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

    public bool LittleEndian { get; init; }

    internal bool ReverseEndianness => LittleEndian != BitConverter.IsLittleEndian;
}
