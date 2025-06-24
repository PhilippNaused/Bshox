namespace Bshox.Meta;

internal static class Constants
{
    public const char CommentChar = '#';
    public const char HexDelimiter = '`';
    public const char TextDelimiter = '"';
    public const char TagDelimiter = ':';
    public const char Escape = '\\';
    public const string Fixed4Suffix = "i32";
    public const string Fixed8Suffix = "i64";
    public const string HexPrefix = "0x";

    public const string Null = "null";
    public const string True = "true";
    public const string False = "false";
    public const string PositiveInfinity32 = "inf32";
    public const string NegativeInfinity32 = "-inf32";
    public const string PositiveInfinity64 = "inf64";
    public const string NegativeInfinity64 = "-inf64";
    public const string NaN32 = "nan32";
    public const string NaN64 = "nan64";

    public const char ZigZagSuffix = 'z';
    public const char StartObject = '{';
    public const char EndObject = '}';
    public const char StartArray = '[';
    public const char EndArray = ']';
}
