using System.Text;
using Bshox.Internals;

namespace Bshox.Utils;

public sealed class BshoxBlob(byte[] Data) : BshoxValue(BshoxCode.Prefixed)
{
    public BshoxBlob(string utf8String) : this(EncodingHelper.Utf8NoBom.GetBytes(utf8String)) { }

#pragma warning disable CA1819 // Properties should not return arrays
    public byte[] Data { get; set; } = Data;
#pragma warning restore CA1819 // Properties should not return arrays

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
        foreach (char c in text)
        {
            _ = c switch
            {
                '\n' => sb.Append(Constants.Escape).Append('n'),
                '\r' => sb.Append(Constants.Escape).Append('r'),
                '\t' => sb.Append(Constants.Escape).Append('t'),
                Constants.TextDelimiter => sb.Append(Constants.Escape).Append(Constants.TextDelimiter),
                Constants.Escape => sb.Append(Constants.Escape).Append(Constants.Escape),
                _ => sb.Append(c),
            };
        }
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
