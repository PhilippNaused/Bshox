using System.Text;

namespace Bshox.Utils;

public sealed class BshoxNull : BshoxValue
{
    private BshoxNull() : base(BshoxCode.Null) { }

    public static BshoxNull Instance { get; } = new();

    public override void Write(ref BshoxWriter writer) { /* no-op */ }

    internal override void Write(StringBuilder text, ref uint indent) => _ = text.Append(Constants.Null);
}
