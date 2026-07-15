using Bshox.Utils;
using TestModels;

namespace Bshox.Tests;

public class ValueTupleTests
{
    [Test]
    public async Task Test1()
    {
        await ValueTupleSerializer.ValueTupleInt32Int32Int32Int32.TestSerialization((1, 2, 3, 4), "080110021803200400");
        await ValueTupleSerializer.ValueTupleUInt32StringByte.TestSerialization((7u, "Test", (byte)0x23), "0807130454657374182300");
        await ValueTupleSerializer.ValueTupleUInt32StringByte.TestSerialization((0, null, 0), "0800180000");
    }

    [Test]
    public async Task Test2()
    {
        await ValueTupleSerializer.ValueTupleInt32Int32Int32Int32Int32Int32Int32ValueTupleInt32Int32
            .TestSerialization((1, 2, 3, 4, 5, 6, 7, 8, 9), "080110021803200428053006380745080810090000");
    }

    [Test]
    public async Task InvalidDataUnknownKey()
    {
        TestInvalidData(new BshoxObject
        {
            { 1, new VarInt(1) },
            { 3, new VarInt(2) } // wrong key
        });
    }

    [Test]
    public async Task InvalidDataWrongOrder()
    {
        TestInvalidData(new BshoxObject
        {
            { 2, new VarInt(2) }, // out of order key
            { 1, new VarInt(1) }
        });
    }

    [Test]
    public async Task InvalidDataWrongType()
    {
        TestInvalidData(new BshoxObject
        {
            { 1, new VarInt(1) },
            { 2, new Fixed4(2) } // wrong type
        });
    }

    [Test]
    public async Task InvalidDataTooLong()
    {
        TestInvalidData(new BshoxObject
        {
            { 1, new VarInt(1) },
            { 2, new VarInt(2) },
            { 3, new VarInt(3) }
        });
    }

    private static void TestInvalidData(BshoxObject x)
    {
        var data = x.ToBytes();
        var c = ValueTupleSerializer.ValueTupleInt32Int64;
        var ex = Assert.Throws<BshoxException>(() => c.Deserialize(data));
    }

    [Test]
    public async Task ProtoScope()
    {
        await ValueTupleSerializer.ValueTupleInt32Int32Int32Int32.TestProtoScope((1, 2, 3, 4), """
            {
              1: 1
              2: 2
              3: 3
              4: 4
            }
            """);

        await ValueTupleSerializer.ValueTupleUInt32StringByte.TestProtoScope((7u, "Test", (byte)0x23), """
            {
              1: 7
              2: "Test"
              3: 35
            }
            """);

        await ValueTupleSerializer.ValueTupleUInt32StringByte.TestProtoScope((0, null, 0), """
            {
              1: 0
              3: 0
            }
            """);
    }

    [Test]
    public async Task ProtoScope2()
    {
        await ValueTupleSerializer.ValueTupleInt32Int32Int32Int32Int32Int32Int32ValueTupleInt32Int32
            .TestProtoScope((1, 2, 3, 4, 5, 6, 7, 8, 9), """
             {
               1: 1
               2: 2
               3: 3
               4: 4
               5: 5
               6: 6
               7: 7
               8: {
                 1: 8
                 2: 9
               }
             }
             """);
    }
}
