using System.Globalization;

namespace Bshox.Meta;

public partial class BshoxTextParser
{
    internal readonly struct Token : IEquatable<Token>, IEquatable<string>, IEquatable<char>
    {
        private readonly string Text;
        public readonly int Offset;
        public readonly int Length;

        public Token(string text, int offset, int length)
        {
#if NETCOREAPP
            ArgumentNullException.ThrowIfNull(text);
#else
            if (text is null)
                throw new ArgumentNullException(nameof(text));
#endif
            Text = text;
            Offset = offset;
            Length = length;
            if (offset < 0 || text.Length < offset + length)
                throw new ArgumentOutOfRangeException(nameof(offset));
        }

        public char this[int index] => Text[Offset + index];

        public (int Line, int Column) Position
        {
            get
            {
                int line = 1;
                int column = 1;
                for (int i = 0; i < Offset; i++)
                {
                    if (Text[i] == '\n')
                    {
                        line++;
                        column = 1;
                    }
                    else
                    {
                        column++;
                    }
                }
                return (line, column);
            }
        }

        public ReadOnlySpan<char> Span => Text.AsSpan(Offset, Length);

        public Token SubToken(int offset) => SubToken(offset, Length - offset);

        public Token SubToken(int offset, int length) => new(Text, Offset + offset, length);

        public bool Contains(char c)
        {
            foreach (char x in Span)
            {
                if (x == c)
                    return true;
            }
            return false;
        }

        public bool StartsWith(char c) => Length > 0 && Text[Offset] == c;

        public bool EndsWith(char c) => Length > 0 && Text[Offset + Length - 1] == c;

        public bool StartsWith(string s)
        {
#if NETCOREAPP
            return Span.StartsWith(s.AsSpan());
#else
            if (s.Length > Length)
                return false;
            return s.AsSpan().SequenceEqual(Text.AsSpan(Offset, s.Length));
#endif
        }

        public bool EndsWith(string s)
        {
#if NETCOREAPP
            return Span.EndsWith(s.AsSpan());
#else
            if (s.Length > Length)
                return false;
            return s.AsSpan().SequenceEqual(Text.AsSpan(Offset + Length - s.Length, s.Length));
#endif
        }

        public bool EmptyOrWhitespace()
        {
            foreach (char c in Span)
            {
                if (!char.IsWhiteSpace(c))
                    return false;
            }
            return true;
        }

        #region Parse

#if NETCOREAPP
        private ReadOnlySpan<char> Parsable => Span;
#else
        private string Parsable => Span.ToString();
#endif

        private const NumberStyles SignedInteger = NumberStyles.AllowLeadingSign; // -123, 567
        private const NumberStyles UnsignedInteger = NumberStyles.None; // 123, 567
        private const NumberStyles HexInteger = NumberStyles.AllowHexSpecifier; // 123, acb, DEF
        private const NumberStyles FloatingPointNumber = NumberStyles.AllowLeadingSign | NumberStyles.AllowDecimalPoint | NumberStyles.AllowExponent; // e.g. -123.456, 567.89e-12

        public ulong ParseULong() => ulong.Parse(Parsable, UnsignedInteger, CultureInfo.InvariantCulture);

        public ulong ParseHexULong() => ulong.Parse(Parsable, HexInteger, CultureInfo.InvariantCulture);

        public uint ParseUInt() => uint.Parse(Parsable, UnsignedInteger, CultureInfo.InvariantCulture);

        public uint ParseHexUInt() => uint.Parse(Parsable, HexInteger, CultureInfo.InvariantCulture);

        public long ParseLong() => long.Parse(Parsable, SignedInteger, CultureInfo.InvariantCulture);

        public int ParseInt() => int.Parse(Parsable, SignedInteger, CultureInfo.InvariantCulture);

        public float ParseFloat() => float.Parse(Parsable, FloatingPointNumber, CultureInfo.InvariantCulture);

        public double ParseDouble() => double.Parse(Parsable, FloatingPointNumber, CultureInfo.InvariantCulture);

        public bool TryParseULong(out ulong value) => ulong.TryParse(Parsable, UnsignedInteger, CultureInfo.InvariantCulture, out value);

        public bool TryParseLong(out long value) => long.TryParse(Parsable, SignedInteger, CultureInfo.InvariantCulture, out value);

        public bool TryParseDouble(out double value) => double.TryParse(Parsable, FloatingPointNumber, CultureInfo.InvariantCulture, out value);

        #endregion Parse

        #region Operators and Equals

        public static bool operator ==(Token token, string str) => token.Equals(str);

        public static bool operator !=(Token token, string str) => !token.Equals(str);

        public static bool operator ==(string str, Token token) => token.Equals(str);

        public static bool operator !=(string str, Token token) => !token.Equals(str);

        public static bool operator ==(Token token, char c) => token.Equals(c);

        public static bool operator !=(Token token, char c) => !token.Equals(c);

        public static bool operator ==(char c, Token token) => token.Equals(c);

        public static bool operator !=(char c, Token token) => !token.Equals(c);

        public override bool Equals(object? obj)
        {
            if (obj is string s && Equals(s))
                return true;
            if (obj is char c && Equals(c))
                return true;
            return obj is Token t && Equals(t);
        }

        public bool Equals(Token other) => Text == other.Text
                                        && Offset == other.Offset
                                        && Length == other.Length;

        public bool Equals(string? other) => other is not null && other.AsSpan().SequenceEqual(Span);

        public bool Equals(char other) => Length == 1 && Text[Offset] == other;

        public override int GetHashCode()
        {
#if NETCOREAPP
            return HashCode.Combine(Text, Offset, Length);
#else
            return Text.GetHashCode() ^ (Offset.GetHashCode() * 2718281) ^ (Length.GetHashCode() * 314159);
#endif
        }

        #endregion Operators and Equals

        public override string ToString() => Span.ToString();
    }
}
