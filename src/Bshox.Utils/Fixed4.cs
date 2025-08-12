using System.Diagnostics;
using System.Globalization;
using System.Runtime.CompilerServices;
using System.Text;

namespace Bshox.Utils;

public sealed class Fixed4(float Value) : BshoxValue(BshoxCode.Fixed4)
{
    public float Value { get; set; } = Value;

    public static Fixed4 Read(ref BshoxReader reader) => new(reader.ReadSingle());

    /// <inheritdoc />
    public override void Write(ref BshoxWriter writer) => writer.WriteSingle(Value);

    internal override void Write(StringBuilder text, ref uint indent) => _ = text.Append(ToString());

    public override string ToString()
    {
        if (float.IsPositiveInfinity(Value))
            return Constants.PositiveInfinity32;
        if (float.IsNegativeInfinity(Value))
            return Constants.NegativeInfinity32;
        if (float.IsNaN(Value))
        {
            float f = Value;
            uint value = Unsafe.As<float, uint>(ref f);
            return $"{Constants.HexPrefix}{value:X}{Constants.Fixed4Suffix}";
        }

        var text = Value.ToString("G9", CultureInfo.InvariantCulture);
        if (!text.Contains('.'))
        {
            // Value is an integer, so add ".0" to the end
            Debug.Assert(Value.Equals((long)Value), "Value.Equals((long)Value)");
            return text + ".0" + Constants.Fixed4Suffix;
        }

        Debug.Assert(text.Contains('.'), "text.Contains('.')");
        return text + Constants.Fixed4Suffix;
    }
}
