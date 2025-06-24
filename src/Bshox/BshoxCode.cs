namespace Bshox;

#pragma warning disable CA1028 // Enum Storage should be Int32

/// <summary>
/// A 3 bit code specifying the encoding of Bshox serialized data. Similar to the <i>wire type</i> in Protobuf.
/// </summary>
public enum BshoxCode : byte
{
    /// <summary>
    /// No value.<br/>
    /// This code cannot be used as the root encoding of a Bshox contract.<br/>
    /// It can only be used as the encoding of a field in a <see cref="SubObject"/> to indicate that the field <b>explicitly</b> has no value.
    /// </summary>
    Null = 0,
    /// <summary>
    /// A base-128 variable-length integer.
    /// </summary>
    /// <remarks>
    /// Equivalent to protobuf's <c>VARINT</c> wire type.
    /// <seealso href="https://protobuf.dev/programming-guides/encoding/#varints"/>
    /// </remarks>
    VarInt = 1,
    /// <summary>
    /// A fixed-length 4-byte value.
    /// </summary>
    /// <remarks>
    /// Equivalent to protobuf's <c>I32</c> wire type.
    /// </remarks>
    Fixed4 = 2,
    /// <summary>
    /// A fixed-length 8-byte value.
    /// </summary>
    /// <remarks>
    /// Equivalent to protobuf's <c>I64</c> wire type.
    /// </remarks>
    Fixed8 = 3,
    /// <summary>
    /// A length-prefixed binary blob.<br/>
    /// Encoded as a <i>varint</i> encoded unsigned integer of value <c>n</c> followed by <c>n</c> bytes of data.
    /// </summary>
    /// <remarks>
    /// Equivalent to protobuf's <c>LEN</c> wire type. <see href="https://protobuf.dev/programming-guides/encoding/#length-types"/>
    /// </remarks>
    Prefixed = 4,
    /// <summary>
    /// An array of values.
    /// Begins with a <i>varint</i> encoded header, followed by the encoded values.<br/>
    /// The least significant three bits of the header are the encoding type of the array elements.<br/>
    /// The remaining bits are the number of elements in the array.
    /// </summary>
    /// <remarks>
    /// This format is similar, but not identical, to the <i>packed repeated fields</i> encoding in Protobuf.<br/>
    /// The length prefix in Protobuf is the number of bytes, while the length prefix in Bshox is the number of elements.<br/>
    /// Protobuf also doesn't encode the wire type of the elements and only supports primitive numeric types encoded as <c>VARINT</c>, <c>I32</c>, or <c>I64</c>.<br/>
    /// Bshox supports arrays of any type except <see cref="Null"/>.
    /// </remarks>
    Array = 5,
    /// <summary>
    /// A <c>0</c>-terminated list of fields.<br/>
    /// Each field is a pair of a <i>varint</i> encoded tag and a value.<br/>
    /// The least significant three bits of the tag is the encoding type of the value.<br/>
    /// The remaining bits are the field number.<br/>
    /// Field numbers must be positive integers.
    /// </summary>
    /// <remarks>
    /// This format is similar to the <i>submessage</i> encoding in Protobuf.<br/>
    /// The main difference is that Protobuf's submessages use the <c>LEN</c> encoding, while Bshox's subobjects have a dedicated encoding that is <c>0</c>-terminated.
    /// </remarks>
    SubObject = 6,
    // Unused = 7
}
