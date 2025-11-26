namespace Bshox;

/// <summary>
/// Compile-time constants for Bshox.
/// </summary>
public static class BshoxConstants
{
    /// <summary>
    /// The smallest valid key (i.e. field number) that can be assigned to a field in a message.
    /// <c>1</c>
    /// </summary>
    public const uint MinKey = 1;

    /// <summary>
    /// The largest valid key (i.e. field number) that can be assigned to a field in a message.
    /// <c>2^29-1</c> or <c>536870911</c>
    /// <seealso href="https://protobuf.dev/programming-guides/proto3/#assigning"/>
    /// </summary>
    public const uint MaxKey = uint.MaxValue >> 3;
}
