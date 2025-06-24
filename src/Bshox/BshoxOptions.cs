using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace Bshox;

public readonly record struct BshoxOptions
{
    public const int DefaultMaxDepth = 64; // same as System.Text.Json.JsonReaderOptions.DefaultMaxDepth

    public readonly int MaxDepth
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        get
        {
            Debug.Assert(field >= 0, "field >= 0");
            return field is 0 ? DefaultMaxDepth : field;
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
    }
}
