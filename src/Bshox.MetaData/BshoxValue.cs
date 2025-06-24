using System.Diagnostics;
using System.Text;

namespace Bshox.Meta;

public abstract class BshoxValue
{
    private protected BshoxValue(BshoxCode encoding)
    {
        Encoding = encoding;
    }

    public static BshoxValue Null => BshoxNull.Instance;

    public BshoxCode Encoding { get; }

    public static BshoxValue Read(ref BshoxReader reader, BshoxCode encoding)
    {
        // TODO: move lock to inner methods
        using var _ = reader.DepthLock();
        return encoding switch
        {
            BshoxCode.Null => BshoxNull.Instance,
            BshoxCode.VarInt => VarInt.Read(ref reader),
            BshoxCode.Fixed4 => Fixed4.Read(ref reader),
            BshoxCode.Fixed8 => Fixed8.Read(ref reader),
            BshoxCode.Prefixed => BshoxBlob.Read(ref reader),
            BshoxCode.SubObject => BshoxObject.Read(ref reader),
            BshoxCode.Array => BshoxArray.Read(ref reader),
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
