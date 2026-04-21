using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using Bshox.Attributes;

namespace TestModels;

[ExcludeFromCodeCoverage]
[BshoxSerializable<DateTimeOffset>(Surrogate = typeof(DateTimeOffsetSurrogate))]
public partial class DateTimeOffsetSerializer;

[ExcludeFromCodeCoverage]
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
