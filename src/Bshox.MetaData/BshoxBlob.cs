using System.Text;
using Bshox.Internals;

namespace Bshox.Meta;

public sealed class BshoxBlob(byte[] Data) : BshoxValue(BshoxCode.Prefixed)
{
    public BshoxBlob(string utf8String) : this(EncodingHelper.Utf8NoBom.GetBytes(utf8String)) { }

#pragma warning disable CA1819
    public byte[] Data { get; set; } = Data;
#pragma warning restore CA1819

    public static BshoxBlob Read(ref BshoxReader reader) => new(reader.ReadByteArray());

    /// <inheritdoc />
    public override void Write(ref BshoxWriter writer) => writer.WriteByteArray(Data);

    internal override void Write(StringBuilder text, ref uint indent)
    {
        // check if the data is printable or a space
        if (Data.All(static b => b is >= 0x20 and <= 0x7E)) // https://www.ascii-code.com/characters/printable-characters + 0x20 (Space)
        {
            _ = text.Append(Constants.TextDelimiter);
            string value = AsUtf8String();
            AppendEscaped(value, text);
            _ = text.Append(Constants.TextDelimiter);
            return;
        }

        _ = text.Append(Constants.HexDelimiter)
            .Append(AsHexString())
            .Append(Constants.HexDelimiter);
    }

    private static void AppendEscaped(string text, StringBuilder sb)
    {
#pragma warning disable IDE0058 // Expression value is never used
        foreach (char c in text)
        {
            switch (c)
            {
                case '\n':
                    sb.Append(Constants.Escape).Append('n');
                    break;
                case '\r':
                    sb.Append(Constants.Escape).Append('r');
                    break;
                case '\t':
                    sb.Append(Constants.Escape).Append('t');
                    break;
                case Constants.TextDelimiter:
                    sb.Append(Constants.Escape).Append(Constants.TextDelimiter);
                    break;
                case Constants.Escape:
                    sb.Append(Constants.Escape).Append(Constants.Escape);
                    break;
                default:
                    sb.Append(c);
                    break;
            }
        }
#pragma warning restore IDE0058 // Expression value is never used
    }

    public string AsHexString()
    {
#if NET6_0_OR_GREATER
        return Convert.ToHexString(Data);
#else
        return BitConverter.ToString(Data).Replace("-", "");
#endif
    }

    public string AsUtf8String()
    {
        return EncodingHelper.Utf8NoBom.GetString(Data);
    }
}
