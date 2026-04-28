using Bshox.Tests;
#if !NETCOREAPP
using Bshox.TestUtils;
#endif

[assembly: UnicodeEscapeFormatter]

namespace Bshox.Tests;

/// <summary>
/// Escapes non-ASCII characters in test display names using Unicode escape sequences.
/// </summary>
/// <remarks>
/// Needed since 'EnricoMi/publish-unit-test-result-action@v2' has trouble detecting added/removed tests when the name contains certain characters.
/// </remarks>
public sealed class UnicodeEscapeFormatterAttribute : DisplayNameFormatterAttribute
{
    protected override string FormatDisplayName(DiscoveredTestContext context)
    {
        return EscapeUnicode(context.GetDisplayName());
    }

    private static string EscapeUnicode(string input)
    {
        return string.Concat(input.Select(selector));
        static string selector(char c) => c switch
        {
            '\n' => "\\n",
            '\r' => "\\r",
            '\t' => "\\t",
            _ => char.IsAscii(c) ? c.ToString() : $"\\u{(int)c:x4}"
        };
    }
}
