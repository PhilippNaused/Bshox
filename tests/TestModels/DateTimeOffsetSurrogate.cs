using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using Bshox.Attributes;

namespace TestModels;

[ExcludeFromCodeCoverage]
[BshoxSerializer(typeof(DateTimeOffset), Surrogates = [typeof(DateTimeOffsetSurrogate)])]
public partial class DateTimeOffsetSerializer;

[ExcludeFromCodeCoverage]
#if NETCOREAPP
[BshoxSurrogate<DateTimeOffset>]
#else
// The mono runtime will crash if you add a generic attribute to a struct.
// See: https://github.com/mono/mono/issues/21852 and https://gitlab.winehq.org/mono/mono/-/issues/25
[BshoxSurrogate(typeof(DateTimeOffset))]
#endif
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
