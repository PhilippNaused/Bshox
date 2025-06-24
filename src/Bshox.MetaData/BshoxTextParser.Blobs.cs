using System.Buffers;
using System.Diagnostics;
using Bshox.Internals;

namespace Bshox.Meta;

public partial class BshoxTextParser
{
    internal static byte[] ParseBlob(Token token)
    {
        if (token.EmptyOrWhitespace() || token.Length < 2)
            throw BshoxException.CannotParse(token, BshoxCode.Prefixed);
        try
        {
            char first = token[0];
            char last = token[token.Length - 1];
            var inner = token.SubToken(1, token.Length - 2);
            return (first, last) switch
            {
                (Constants.HexDelimiter, Constants.HexDelimiter) => ParseHex(inner.Span),
                (Constants.TextDelimiter, Constants.TextDelimiter) => ParseUtf8(inner.Span),
                _ => throw BshoxException.CannotParse(token, BshoxCode.Prefixed)
            };
        }
        catch (FormatException e)
        {
            throw BshoxException.CannotParse(token, BshoxCode.Prefixed, e);
        }
    }

    private static byte[] ParseUtf8(ReadOnlySpan<char> input)
    {
        // TODO: optimize this method. Maybe converting to string and then to bytes is faster/easier to read
        byte[] bufferArray = ArrayPool<byte>.Shared.Rent(input.Length * 3);

        try
        {
            int size = EncodingHelper.Utf8Encode(input, bufferArray);
            Span<byte> buffer = bufferArray.AsSpan(0, size);
            size = UnescapeUtf8(buffer);
            return buffer.Slice(0, size).ToArray();
        }
        finally
        {
            ArrayPool<byte>.Shared.Return(bufferArray);
        }
    }

    private static int UnescapeUtf8(Span<byte> buffer)
    {
        int i = 0, j = 0;
        while (i < buffer.Length)
        {
            byte c = buffer[i++];
            if (c == Constants.Escape)
            {
                // escape the next character and add it to the buffer
                if (i == buffer.Length) // no more characters => invalid escape sequence
                    throw new FormatException("Input string is too short for the escape sequence");
                c = buffer[i++];

                if (c == 'x') // special case: hex escape sequence
                {
                    if (i + 1 >= buffer.Length) // not enough characters for a hex escape sequence
                        throw new FormatException("Input string is too short for the escape sequence");
                    buffer[j++] = ParseHex([(char)buffer[i], (char)buffer[i + 1]])[0];
                    i += 2;
                    continue;
                }

                buffer[j++] = c switch
                {
                    (byte)'n' => (byte)'\n',
                    (byte)'t' => (byte)'\t',
                    (byte)'r' => (byte)'\r',
                    (byte)Constants.Escape => (byte)Constants.Escape,
                    (byte)Constants.TextDelimiter => (byte)Constants.TextDelimiter,
                    _ => throw new FormatException($"Invalid escape sequence: '{Constants.Escape}{c}'")
                };
            }
            else
            {
                buffer[j++] = c;
            }
        }
        Debug.Assert(j <= i, "j <= i");
        Debug.Assert(j <= buffer.Length, "j <= buffer.Length");

        return j;
    }

    private static byte[] ParseHex(ReadOnlySpan<char> input)
    {
#if NETCOREAPP
        return Convert.FromHexString(input);
#else
        if (input.Length % 2 != 0)
            throw new FormatException("Hex string must have an even number of characters.");
        byte[] bytes = new byte[input.Length / 2];
        if (!TryDecodeFromUtf16(input, bytes, out int charsProcessed))
            throw new FormatException($"Invalid character in hex string: '{input[charsProcessed]}'");
        return bytes;
#endif
    }

#if !NETCOREAPP
    private static bool TryDecodeFromUtf16(ReadOnlySpan<char> chars, Span<byte> bytes, out int charsProcessed)
    {
        Debug.Assert(chars.Length % 2 == 0, "Un-even number of characters provided");
        Debug.Assert(chars.Length / 2 == bytes.Length, "Target buffer not right-sized for provided characters");

        int i = 0;
        int j = 0;
        int byteLo = 0;
        int byteHi = 0;
        while (j < bytes.Length)
        {
            byteLo = FromChar(chars[i + 1]);
            byteHi = FromChar(chars[i]);

            // byteHi hasn't been shifted to the high half yet, so the only way the bitwise or produces this pattern
            // is if either byteHi or byteLo was not a hex character.
            if ((byteLo | byteHi) == 0xFF)
                break;

            bytes[j++] = (byte)((byteHi << 4) | byteLo);
            i += 2;
        }

        if (byteLo == 0xFF)
            i++;

        charsProcessed = i;
        return (byteLo | byteHi) != 0xFF;
    }

    [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
    private static int FromChar(int c)
    {
        return c >= CharToHexLookup.Length ? 0xFF : CharToHexLookup[c];
    }

    /// <summary>Map from an ASCII char to its hex value, e.g. arr['b'] == 11. 0xFF means it's not a hex digit.</summary>
    private static ReadOnlySpan<byte> CharToHexLookup =>
    [
        0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, // 15
        0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, // 31
        0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, // 47
        0x0,  0x1,  0x2,  0x3,  0x4,  0x5,  0x6,  0x7,  0x8,  0x9,  0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, // 63
        0xFF, 0xA,  0xB,  0xC,  0xD,  0xE,  0xF,  0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, // 79
        0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, // 95
        0xFF, 0xa,  0xb,  0xc,  0xd,  0xe,  0xf,  0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, // 111
        0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, // 127
        0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, // 143
        0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, // 159
        0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, // 175
        0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, // 191
        0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, // 207
        0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, // 223
        0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, // 239
        0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF  // 255
    ];
#endif
}
