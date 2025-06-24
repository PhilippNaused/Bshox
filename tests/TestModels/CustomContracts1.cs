using System.Diagnostics.CodeAnalysis;
using Bshox;
using Bshox.Attributes;

namespace TestModels;

[ExcludeFromCodeCoverage]
[BshoxSerializer(typeof(int))]
[BshoxDefaultContract(typeof(CustomIntContract), nameof(CustomIntContract.Instance))]
public partial class CustomContracts1;

[ExcludeFromCodeCoverage]
[BshoxSerializer(typeof(int))]
[BshoxDefaultContract(typeof(DefaultContracts), nameof(DefaultContracts.Int32Z))]
public partial class CustomContracts2;

[ExcludeFromCodeCoverage]
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
