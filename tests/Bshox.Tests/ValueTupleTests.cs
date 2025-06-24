using TestModels;

namespace Bshox.Tests;

public class ValueTupleTests
{
    [Test]
    public async Task Test1()
    {
        await ValueTupleSerializer.ValueTupleInt32Int32Int32Int32.TestSerialization((1, 2, 3, 4), "090111021903210400");
        await ValueTupleSerializer.ValueTupleUInt32StringByte.TestSerialization((7u, "Test", (byte)0x23), "0907140454657374192300");
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
    }
}
