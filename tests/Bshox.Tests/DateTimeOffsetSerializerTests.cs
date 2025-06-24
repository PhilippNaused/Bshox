using Bshox.Meta;
using TestModels;

namespace Bshox.Tests;

public class DateTimeOffsetSerializerTests
{
    [Before(Class)]
    public static void Setup()
    {
        // fail if using MONO
        if (Type.GetType("Mono.Runtime") is not null)
        {
            // TODO: fix usage of generic attribute in .NET Framework
            Assert.Fail("This test is not supported on MONO");
        }
    }

    [Test]
    public async Task Test1()
    {
        DateTimeOffset value = new(631524443600111719, TimeSpan.FromHours(5));
        await DateTimeOffsetSerializer.DateTimeOffset.TestSerialization(value, "09E780B3B78CFFE7E10811AC0200");
        value = new DateTimeOffset(631524443600111719, TimeSpan.Zero);
        await DateTimeOffsetSerializer.DateTimeOffset.TestSerialization(value, "09E7908BFEAA84E8E10800");
    }

    [Test]
    public async Task Test2()
    {
        DateTimeOffset value = new(631524443600111719, TimeSpan.FromHours(5));
        var meta = DateTimeOffsetSerializer.DateTimeOffset.ToBshoxValue(value);
        await Assert.That(meta).IsAssignableTo<BshoxObject>();
        var obj = (BshoxObject)meta;
        await Assert.That(obj).HasCount(2);

        var obj1 = obj[1];
        await Assert.That(obj1).IsAssignableTo<VarInt>();
        var ticks = ((VarInt)obj1).Value;
        await Assert.That(ticks).IsEqualTo((ulong)value.UtcTicks);

        var obj2 = obj[2];
        await Assert.That(obj2).IsAssignableTo<VarInt>();
        var offset = ((VarInt)obj2).Value;
        await Assert.That(offset).IsEqualTo((ulong)value.Offset.TotalMinutes);
    }

    [Test]
    public async Task Now()
    {
        var value = DateTimeOffset.Now;
        await DateTimeOffsetSerializer.DateTimeOffset.TestSerialization(value);
    }

    [Test]
    public async Task MaxValue()
    {
        var value = DateTimeOffset.MaxValue;
        await DateTimeOffsetSerializer.DateTimeOffset.TestSerialization(value, "09FFFFDCA1DF8E8AE52B00");
    }

    [Test]
    public async Task MinValue()
    {
        var value = DateTimeOffset.MinValue;
        await DateTimeOffsetSerializer.DateTimeOffset.TestSerialization(value, "090000");
        await DateTimeOffsetSerializer.DateTimeOffset.TestProtoScope(value, """
            {
              1: 0
            }
            """);
    }

    [Test]
    public async Task ProtoScope()
    {
        var value = DateTimeOffset.Parse("2016-05-11T12:05:52.9726744+02:00");
        await DateTimeOffsetSerializer.DateTimeOffset.TestProtoScope(value, $$"""
            {
              1: {{value.UtcTicks}}
              2: 120
            }
            """);

        value = DateTimeOffset.Parse("2016-05-11T12:05:52.9726744+00:00");
        await DateTimeOffsetSerializer.DateTimeOffset.TestProtoScope(value, $$"""
            {
              1: {{value.UtcTicks}}
            }
            """);
    }

    [Test]
    public async Task ChangeOffset()
    {
        var value = DateTimeOffset.Parse("2016-05-11T12:05:52.9726744+02:00");
        var utcTicks = value.UtcTicks;
        await DateTimeOffsetSerializer.DateTimeOffset.TestProtoScope(value, $$"""
            {
              1: {{utcTicks}}
              2: 120
            }
            """);

        value = value.ToUniversalTime();
        // the ticks should not change!
        await DateTimeOffsetSerializer.DateTimeOffset.TestProtoScope(value, $$"""
            {
              1: {{utcTicks}}
            }
            """);
    }

    [Test]
    public async Task UtcNow()
    {
        var value = DateTimeOffset.UtcNow;
        await DateTimeOffsetSerializer.DateTimeOffset.TestSerialization(value);
        var meta = DateTimeOffsetSerializer.DateTimeOffset.ToBshoxValue(value);
        await Assert.That(meta).IsAssignableTo<BshoxObject>();
        var obj = (BshoxObject)meta;
        await Assert.That(obj).HasCount(1);
        var obj1 = obj[1];
        await Assert.That(obj1).IsAssignableTo<VarInt>();
        var ticks = ((VarInt)obj1).Value;
        await Assert.That(ticks).IsEqualTo((ulong)value.UtcTicks);
    }
}
