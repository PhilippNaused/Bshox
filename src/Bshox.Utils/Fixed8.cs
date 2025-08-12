using System.Diagnostics;
using System.Globalization;
using System.Runtime.CompilerServices;
using System.Text;

namespace Bshox.Utils;

public sealed class Fixed8(double Value) : BshoxValue(BshoxCode.Fixed8)
{
    public double Value { get; set; } = Value;

    public static Fixed8 Read(ref BshoxReader reader) => new(reader.ReadDouble());

    /// <inheritdoc />
    public override void Write(ref BshoxWriter writer) => writer.WriteDouble(Value);

    internal override void Write(StringBuilder text, ref uint indent) => _ = text.Append(ToString());

    public override string ToString()
    {
        if (double.IsPositiveInfinity(Value))
            return Constants.PositiveInfinity64;
        if (double.IsNegativeInfinity(Value))
            return Constants.NegativeInfinity64;
        if (double.IsNaN(Value))
        {
            double d = Value;
            ulong value = Unsafe.As<double, ulong>(ref d);
            return $"{Constants.HexPrefix}{value:X}{Constants.Fixed8Suffix}";
        }

        var text = Value.ToString("G17", CultureInfo.InvariantCulture);
        if (!text.Contains('.'))
        {
            // Value is an integer, so add ".0" to the end
            Debug.Assert(Value.Equals((long)Value), "Value.Equals((long)Value)");
            return text + ".0" + Constants.Fixed8Suffix;
        }

        Debug.Assert(text.Contains('.'), "text.Contains('.')");
        return text + Constants.Fixed8Suffix;
    }
}
