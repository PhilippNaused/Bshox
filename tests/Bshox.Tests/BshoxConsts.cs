// ReSharper disable InconsistentNaming
namespace Bshox.Tests;

public static class BshoxConsts
{
    public const uint Max3ByteUint = uint.MaxValue >>> 11;

    private const uint Max3ByteUint_M1 = Max3ByteUint - 1;

    public const int Min3ByteInt = (int)(((long)Max3ByteUint >>> 1) ^ -((long)Max3ByteUint & 1));
    public const int Max3ByteInt = (int)(((long)Max3ByteUint_M1 >>> 1) ^ -((long)Max3ByteUint_M1 & 1));
}
