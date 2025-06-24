using System.Globalization;
using System.Text;

namespace Bshox.Meta;

public sealed class VarInt(ulong Value) : BshoxValue(BshoxCode.VarInt)
{
    public VarInt(int value) : this((ulong)value) { }

    public ulong Value { get; set; } = Value;

    public static VarInt Read(ref BshoxReader reader) => new(reader.ReadVarInt64());

    /// <inheritdoc />
    public override void Write(ref BshoxWriter writer) => writer.WriteVarInt64(Value);

    internal override void Write(StringBuilder text, ref uint indent) => _ = text.Append(ToString());

    public override string ToString()
    {
        return Value.ToString("D", CultureInfo.InvariantCulture);
    }
}
