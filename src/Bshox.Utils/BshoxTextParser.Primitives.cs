using Bshox.Internals;

namespace Bshox.Utils;

public partial class BshoxTextParser
{
    internal static ulong ParseVarInt(Token token)
    {
        try
        {
            return ParseVarIntInternal(token);
        }
        catch (FormatException e)
        {
            throw BshoxException.CannotParse(token, BshoxCode.VarInt, e);
        }
        catch (OverflowException e)
        {
            throw BshoxException.CannotParse(token, BshoxCode.VarInt, e);
        }
    }

    private static ulong ParseVarIntInternal(Token token)
    {
        // there are several possible formats:
        // - decimal number, e.g. 1234
        // - hexadecimal number, e.g. 0x1234
        // - "true", "false" as literals for 1 and 0
        // - integers with the suffix "z" to indicate zigzag encoding, e.g. 1234z == 2468

        // first check if the token is a literal
        switch (token.Span)
        {
            case Constants.True:
                return 1;
            case Constants.False:
                return 0;
        }

        // check if the token is a hexadecimal number
        if (token.StartsWith(Constants.HexPrefix))
        {
            // e.g. 0xFF
            return token.SubToken(2).ParseHexULong();
        }

        // check for the suffix "z" for zigzag encoding
        if (token.EndsWith(Constants.ZigZagSuffix))
        {
            long value = token.SubToken(0, token.Length - 1).ParseLong();
            return EncodingHelper.ZigZag64(value);
        }

        // signed integers are not allowed

        //if (token.StartsWith('-'))
        //{
        //    // signed integer
        //    long value = token.ParseLong();
        //    return unchecked((ulong)value);
        //}

        // assume the token is an unsigned integer
        return token.ParseULong();
    }

    /// <summary>
    /// Parse the next token as a fixed-size number with 4 bytes.
    /// </summary>
    internal static float ParseFixed4(Token token)
    {
        try
        {
            return ParseFixed4Internal(token);
        }
        // TODO: consider adding a custom exception type
        catch (FormatException e)
        {
            throw BshoxException.CannotParse(token, BshoxCode.Fixed4, e);
        }
        catch (OverflowException e)
        {
            throw BshoxException.CannotParse(token, BshoxCode.Fixed4, e);
        }
    }

    private static float ParseFixed4Internal(Token token)
    {
        // there are several possible formats:
        // - decimal number, e.g. 1234
        // - hexadecimal number, e.g. 0x1234
        // - floating-point number, e.g. 1234.0
        // - floating-point number with exponent, e.g. 1234.0e0
        // - "inf" and "-inf" for positive and negative infinity
        // - "nan" for NaN

        // numeric values CAN have the suffix "i32" (e.g. 3.14i32)
        // literals CAN have the suffix "32" (e.g. inf32)

        // first check if the token is a literal
        switch (token.Span)
        {
            case Constants.PositiveInfinity32 or "inf":
                return float.PositiveInfinity;
            case Constants.NegativeInfinity32 or "-inf":
                return float.NegativeInfinity;
            case Constants.NaN32 or "nan":
                return float.NaN;
        }

        // remove the suffix "i32" if it exists
        if (token.EndsWith(Constants.Fixed4Suffix))
        {
            token = token.SubToken(0, token.Length - Constants.Fixed4Suffix.Length);
        }

        // check if the token is a hexadecimal number
        if (token.StartsWith(Constants.HexPrefix))
        {
            token = token.SubToken(2);
            // e.g. 0xFF
            return token.ParseHexUInt().AsFloat();
        }

        // check if the token is a floating-point number
        if (token.Contains('.') || token.Contains('e'))
        {
            // e.g. 3.14, 3.14e0
            return token.ParseFloat();
        }

        // signed integer
        if (token.StartsWith('-'))
        {
            int i = token.ParseInt();
            return unchecked((uint)i).AsFloat();
        }

        // assume the token is an unsigned integer
        return token.ParseUInt().AsFloat();
    }

    /// <summary>
    /// Parse the next token as a fixed-size number with 8 bytes.
    /// </summary>
    internal static double ParseFixed8(Token token)
    {
        try
        {
            return ParseFixed8Internal(token);
        }
        // TODO: consider adding a custom exception type
        catch (FormatException e)
        {
            throw BshoxException.CannotParse(token, BshoxCode.Fixed8, e);
        }
        catch (OverflowException e)
        {
            throw BshoxException.CannotParse(token, BshoxCode.Fixed8, e);
        }
    }

    private static double ParseFixed8Internal(Token token)
    {
        // there are several possible formats:
        // - decimal number, e.g. 1234
        // - hexadecimal number, e.g. 0x1234
        // - floating-point number, e.g. 1234.0
        // - floating-point number with exponent, e.g. 1234.0e0
        // - "inf" and "-inf" for positive and negative infinity
        // - "nan" for NaN

        // numeric values CAN have the suffix "i64" (e.g. 3.14i64)
        // literals CAN have the suffix "64" (e.g. inf64)

        // first check if the token is a literal
        switch (token.Span)
        {
            case Constants.PositiveInfinity64 or "inf":
                return double.PositiveInfinity;
            case Constants.NegativeInfinity64 or "-inf":
                return double.NegativeInfinity;
            case Constants.NaN64 or "nan":
                return double.NaN;
        }

        // remove the suffix "i64" if it exists
        if (token.EndsWith(Constants.Fixed8Suffix))
        {
            token = token.SubToken(0, token.Length - Constants.Fixed8Suffix.Length);
        }

        // check if the token is a hexadecimal number
        if (token.StartsWith(Constants.HexPrefix))
        {
            token = token.SubToken(2);
            // e.g. 0xFF
            return token.ParseHexULong().AsDouble();
        }

        // check if the token is a floating-point number
        if (token.Contains('.') || token.Contains('e'))
        {
            // e.g. 3.14, -3.14e-5
            return token.ParseDouble();
        }

        // signed integer
        if (token.StartsWith('-'))
        {
            long l = token.ParseLong();
            return unchecked((ulong)l).AsDouble();
        }

        // assume the token is an unsigned integer
        return token.ParseULong().AsDouble();
    }
}
