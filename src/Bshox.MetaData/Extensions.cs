using Bshox.Internals;

namespace Bshox.Meta;

public static class Extensions
{
    public static BshoxValue ToBshoxValue<T>(this BshoxContract<T> contract, scoped in T value)
    {
        using var buffer = new PooledByteBufferWriter();
        contract.Serialize(buffer, value);
        var reader = new BshoxReader(buffer.WrittenMemory);
        return BshoxValue.Read(ref reader, contract.Encoding);
    }

    public static string ToBshoxString<T>(this BshoxContract<T> contract, scoped in T value)
    {
        return contract.ToBshoxValue(value).ToString();
    }
}
