#if !NETCOREAPP
using System.Runtime.CompilerServices;
using System.Text;

namespace Bshox;

/// <summary>
/// Polyfills for APIs that are not available in .NET Standard 2.0.
/// </summary>
internal static class PolyFills
{
    internal static unsafe string GetString(this Encoding encoding, ReadOnlySpan<byte> bytes)
    {
        if (bytes.Length == 0)
        {
            return string.Empty;
        }

        fixed (byte* pBytes = bytes)
        {
            return encoding.GetString(pBytes, bytes.Length);
        }
    }
    extension(ArgumentOutOfRangeException)
    {
        internal static void ThrowIfNegativeOrZero(int value, [CallerArgumentExpression(nameof(value))] string? paramName = null)
        {
            if (value <= 0)
            {
                throw new ArgumentOutOfRangeException(paramName);
            }
        }

        internal static void ThrowIfNegative(int value, [CallerArgumentExpression(nameof(value))] string? paramName = null)
        {
            if (value < 0)
            {
                throw new ArgumentOutOfRangeException(paramName);
            }
        }
    }
    extension(ArgumentNullException)
    {
        internal static void ThrowIfNull(object? value, [CallerArgumentExpression(nameof(value))] string? paramName = null)
        {
            if (value is null)
            {
                throw new ArgumentNullException(paramName);
            }
        }
    }
}
#endif
