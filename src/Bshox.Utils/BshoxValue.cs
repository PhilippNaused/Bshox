using System.Diagnostics;
using System.Text;

namespace Bshox.Utils;

public abstract class BshoxValue
{
    private protected BshoxValue(BshoxEncoding encoding)
    {
        Encoding = encoding;
    }

    public BshoxEncoding Encoding { get; }

    public static BshoxValue Read(ref BshoxReader reader, BshoxEncoding encoding)
    {
        return encoding switch
        {
            BshoxEncoding.VarInt => VarInt.Read(ref reader),
            BshoxEncoding.Fixed4 => Fixed4.Read(ref reader),
            BshoxEncoding.Fixed8 => Fixed8.Read(ref reader),
            BshoxEncoding.Prefixed => BshoxBlob.Read(ref reader),
            BshoxEncoding.Object => BshoxObject.Read(ref reader),
            BshoxEncoding.Array => BshoxArray.Read(ref reader),
            _ => throw BshoxException.InvalidEncoding(encoding),
        };
    }

    public override string ToString()
    {
        var sb = new StringBuilder();
        uint indent = 0;
        Write(sb, ref indent);
        Debug.Assert(indent == 0, "indent == 0");
        return sb.ToString();
    }

    internal abstract void Write(StringBuilder text, ref uint indent);

    private protected static void WriteIndent(StringBuilder text, uint indent)
    {
        _ = text.Append(' ', (int)(2 * indent));
    }

    public abstract void Write(ref BshoxWriter writer);
}
