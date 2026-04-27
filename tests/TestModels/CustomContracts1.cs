using Bshox;
using Bshox.Attributes;

namespace TestModels;

[BshoxSerializable<int>]
[BshoxDefaultContract(typeof(CustomIntContract), nameof(CustomIntContract.Instance))]
public partial class CustomContracts1;

[BshoxSerializable<int>]
[BshoxSerializable<List<int>>]
[BshoxDefaultContract(typeof(DefaultContracts), nameof(DefaultContracts.Int32Z))]
public partial class CustomContracts2;

[BshoxSerializable<List<int>>]
[BshoxDefaultContract(typeof(CustomListContract<int>), nameof(CustomListContract<>.Instance))]
public partial class CustomContracts3;

// unbound generic are not yet supported. TODO: fix
//[BshoxSerializable<List<int>>]
//[BshoxDefaultContract(typeof(CustomListContract<>), nameof(CustomListContract<>.Instance))]
//public partial class CustomContracts4;

public sealed class CustomIntContract() : BshoxContract<int>(BshoxCode.Fixed4)
{
    /// <inheritdoc />
    public override void Serialize(ref BshoxWriter writer, scoped ref readonly int value)
    {
        writer.WriteUInt32(unchecked((uint)value));
    }

    /// <inheritdoc />
    public override void Deserialize(ref BshoxReader reader, out int value)
    {
        value = unchecked((int)reader.ReadUInt32());
    }

    public static BshoxContract<int> Instance { get; } = new CustomIntContract();
}

public sealed class CustomListContract<T>() : BshoxContract<List<T>>(BshoxCode.VarInt)
{
#pragma warning disable CA1000 // Do not declare static members on generic types
    public static BshoxContract<List<T>> Instance { get; } = new CustomListContract<T>();
#pragma warning restore CA1000 // Do not declare static members on generic types

    /// <inheritdoc />
    public override void Serialize(ref BshoxWriter writer, scoped ref readonly List<T> value)
    {
        writer.WriteVarInt32((uint)value.Count);
    }

    /// <inheritdoc />
    public override void Deserialize(ref BshoxReader reader, out List<T> value)
    {
        var count = reader.ReadVarInt32();
        value = new List<T>((int)count);
    }
}
