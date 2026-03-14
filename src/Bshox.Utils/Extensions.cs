using Bshox.Internals;

namespace Bshox.Utils;

#pragma warning disable CA1034 // Nested types should not be visible (false positive for extension blocks)

public static class Extensions
{
    extension<T>(BshoxContract<T> contract)
    {
        public BshoxValue ToBshoxValue(scoped in T value)
        {
            using var buffer = new PooledByteBufferWriter();
            contract.Serialize(buffer, value);
            var reader = new BshoxReader(buffer.GetReadOnlySequence());
            return BshoxValue.Read(ref reader, contract.Encoding);
        }

        public string ToBshoxString(scoped in T value)
        {
            return contract.ToBshoxValue(value).ToString();
        }
    }
}
