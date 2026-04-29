using System.ComponentModel;
using Bshox.Attributes;

namespace TestModels;

[BshoxSerializable<DateTimeOffset>(Surrogate = typeof(DateTimeOffsetSurrogate))]
public partial class DateTimeOffsetSerializer;

[BshoxContract]
internal struct DateTimeOffsetSurrogate
{
    public DateTimeOffsetSurrogate(DateTimeOffset value)
    {
        UtcTicks = value.UtcTicks;
#if NET8_0_OR_GREATER
        TotalOffsetMinutes = (short)value.TotalOffsetMinutes;
#else
        TotalOffsetMinutes = (short)value.Offset.TotalMinutes;
#endif
    }

    [BshoxMember(1)]
    public long UtcTicks { get; set; }

    [BshoxMember(2)]
    [DefaultValue(0)]
    public short TotalOffsetMinutes { get; set; }

    public readonly DateTimeOffset Convert() => new DateTimeOffset(UtcTicks, TimeSpan.Zero).ToOffset(TimeSpan.FromMinutes(TotalOffsetMinutes));
}
