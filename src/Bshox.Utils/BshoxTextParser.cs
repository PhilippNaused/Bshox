using System.Diagnostics;

namespace Bshox.Utils;

// https://github.com/protocolbuffers/protoscope/blob/main/language.txt
public partial class BshoxTextParser
{
    private readonly Queue<Token> _tokens;

    private BshoxTextParser(string text)
    {
        Debug.Assert(text is not null, "text is not null");
        _tokens = SplitTokens(text!);
    }

    private static class BshoxException
    {
        public static BshoxParserException CannotParse(Token token, BshoxCode encoding, Exception? innerException = null)
        {
            return new BshoxParserException(token, $"Cannot parse '{token}' as a {encoding} value.", innerException);
        }

        public static BshoxParserException CannotGuessEncoding(Token token, Exception? innerException = null)
        {
            return new BshoxParserException(token, $"Cannot determine the encoding of '{token}'.", innerException);
        }

        public static BshoxParserException EndOfInput()
        {
            return new BshoxParserException("Unexpected end of input.");
        }
    }

    internal bool IsEmpty => _tokens.Count == 0;

    public static BshoxValue Parse(string text)
    {
        var parser = Create(text);
        Debug.Assert(parser.depth == 0, "parser.depth == 0");
        var value = parser.ParseNextValue();
        Debug.Assert(parser.depth == 0, "parser.depth == 0");
        if (parser.IsEmpty)
            return value;
        throw BshoxException.EndOfInput();
    }

    /// <summary>
    /// Try to guess the encoding of the next token.
    /// </summary>
    internal static BshoxCode GuessEncoding(Token token)
    {
        if (token.EmptyOrWhitespace())
            throw BshoxException.CannotGuessEncoding(token);

        // first, check if the text is a literal
        switch (token.Span)
        {
            case Constants.Null:
                return BshoxCode.Null;
            case Constants.True:
            case Constants.False:
                return BshoxCode.VarInt;
            case Constants.PositiveInfinity32:
            case Constants.NegativeInfinity32:
            case Constants.NaN32:
                return BshoxCode.Fixed4;
            case Constants.PositiveInfinity64:
            case Constants.NegativeInfinity64:
            case Constants.NaN64:
                return BshoxCode.Fixed8;
        }

        // check if the first character is unambiguous
        switch (token[0])
        {
            case Constants.HexDelimiter:
            case Constants.TextDelimiter:
                return BshoxCode.Prefixed;
        }

        if (token.Length == 1)
        {
            // check if the text is a special character
            switch (token[0])
            {
                case Constants.StartObject:
                    return BshoxCode.SubObject;
                case Constants.StartArray:
                    return BshoxCode.Array;
            }
        }

        // check if the text has a suffix
        if (token.EndsWith(Constants.Fixed4Suffix))
            return BshoxCode.Fixed4;
        if (token.EndsWith(Constants.Fixed8Suffix))
            return BshoxCode.Fixed8;
        if (token.EndsWith(Constants.ZigZagSuffix))
            return BshoxCode.VarInt;

        // hex number
        if (token.StartsWith(Constants.HexPrefix))
            return BshoxCode.VarInt;

        // check if the text is an unsigned integer
        if (token.TryParseULong(out _))
            return BshoxCode.VarInt;

        // check if the text is a negative integer
        if (token.TryParseLong(out long l) && l < 0)
            throw BshoxException.CannotGuessEncoding(token); // TODO: add special exception for ambiguous encoding

        // try a floating point number
        if (token.TryParseDouble(out _))
            return BshoxCode.Fixed8;

        throw BshoxException.CannotGuessEncoding(token);
    }

    internal static Queue<Token> SplitTokens(string text)
    {
        Queue<Token> tokens = new();
        // split the input text into tokens and store them in a queue
        // tokens are separated by whitespace (space, tab, newline, return carriage)
        // the whitespace characters are not included in the tokens
        // text can be enclosed in double quotes, in which case whitespace is included in the token
        // text can contain escape sequences, e.g. \", \\, \n, \t, \r, \x00-\xFF
        // escape sequences are only valid inside text
        // Comments start with a # and continue until the end of the line

        // also, the special characters {, }, [, and ] are tokens by themselves
        const string specialChars = "{}[]";

        var start = 0;
        var pos = 0;
        var inText = false;
        var inEscape = false;
        var inComment = false;

        while (pos < text.Length)
        {
            var c = text[pos];
            if (inEscape) // the previous character was an escape character
            {
                Debug.Assert(inText, "inText"); // escape sequences are only valid inside text
                inEscape = false;
            }
            else if (c == Constants.CommentChar && !inText) // start of a comment. => end of token
            {
                Debug.Assert(!inEscape, "!inEscape"); // escape sequences are only valid inside text
                inComment = true;
                // add the token before the comment
                EnqueueToken(start, pos);
            }
            else if (inComment && IsLineBreak(c)) // end of a comment
            {
                inComment = false;
                start = pos + 1; // start of the next token
            }
            else if (inComment) // inside a comment
            {
                // ignore the character
            }
            else if (c == Constants.Escape) // the next character will be escaped
            {
                if (inText)
                {
                    inEscape = true;
                }
                else
                {
                    // escape character outside of text are not allowed
                    var token = new Token(text, pos, 1);
                    throw new BshoxParserException(token, $"Unexpected escape character '{Constants.Escape}'");
                }
            }
            else if (c == Constants.TextDelimiter) // unescaped double quote => start or end of text
            {
                inText = !inText;
            }
            else if (!inText && specialChars.Contains(c)) // special character => end of token
            {
                // add the token before the special character
                EnqueueToken(start, pos);
                // add the special character as a separate token
                EnqueueToken(pos, pos + 1);
                start = pos + 1;
            }
            else if (!inText && IsWhiteSpace(c)) // whitespace outside of text => end of token
            {
                EnqueueToken(start, pos);
                start = pos + 1;
            }
            pos++;
        }

        if (!inComment) // add the last token (unless it is a comment)
        {
            EnqueueToken(start, pos);
        }

        return tokens;

        void EnqueueToken(int begin, int end)
        {
            if (end > begin) // only add non-empty tokens
                tokens.Enqueue(new Token(text, begin, end - begin));
        }
    }

    internal static BshoxTextParser Create(string text)
    {
#if NETCOREAPP
        ArgumentNullException.ThrowIfNull(text);
#else
        if (text is null)
            throw new ArgumentNullException(nameof(text));
#endif
        return new BshoxTextParser(text);
    }

    private static bool IsWhiteSpace(char c)
    {
        // space, tab, newline, return carriage
        return c is ' ' or '\t' or '\n' or '\r';
    }

    private static bool IsLineBreak(char c)
    {
        // newline, return carriage
        return c is '\n' or '\r';
    }
}
