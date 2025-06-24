using System.Diagnostics.CodeAnalysis;

namespace Bshox.Attributes;

#pragma warning disable CA1813 // Avoid unsealed attributes

/// <summary>
/// Indicates that the class is a contract for Bshox serialization.
/// </summary>
[ExcludeFromCodeCoverage]
[AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct, AllowMultiple = false, Inherited = false)]
public class BshoxContractAttribute : Attribute
{
    /// <summary>
    /// Indicates that the default values of members are implicitly set to the default value of their declared type if no explicit value is set.
    /// </summary>
    public bool ImplicitDefaultValues { get; set; }

    /// <summary>
    /// Indicates that all public members are implicitly serialized in the order they are declared.<br/>
    /// </summary>
    public bool ImplicitMembers { get; set; }
}

/// <summary>
/// TODO
/// </summary>
/// <typeparam name="T">TODO</typeparam>
[ExcludeFromCodeCoverage]
[AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct, AllowMultiple = false, Inherited = false)]
public sealed class BshoxSurrogateAttribute<T>() : BshoxSurrogateAttribute(typeof(T));

/// <summary>
/// TODO
/// </summary>
/// <param name="type">TODO</param>
[ExcludeFromCodeCoverage]
[AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct, AllowMultiple = false, Inherited = false)]
public class BshoxSurrogateAttribute(Type type) : BshoxContractAttribute
{
    public Type Type { get; } = type;
}

/// <summary>
/// Indicates that the property or field is a member of a Bshox contract.
/// </summary>
/// <param name="key">The unique identifier of this member. Must be positive and cannot be greater than <c>536870911</c></param>
[ExcludeFromCodeCoverage]
[AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, AllowMultiple = false, Inherited = true)]
public sealed class BshoxMemberAttribute(uint key) : Attribute
{
    /// <summary>
    /// The unique identifier of this member. Must be positive and cannot be greater than <c>536870911</c>
    /// </summary>
    public uint Key { get; } = key;
}

/// <summary>
/// TODO
/// </summary>
/// <param name="types">TODO</param>
[ExcludeFromCodeCoverage]
[AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = false)]
public sealed class BshoxSerializerAttribute(params Type[] types) : Attribute
{
    public Type[] Types { get; } = types;

    public Type[] Surrogates { get; set; } = [];
}

/// <summary>
/// When used on a type with the <see cref="BshoxSerializerAttribute"/>, indicates that the specified symbol should be used to get the default contract for the specified type.
/// </summary>
/// <param name="containingType">The type that declares the symbol which returns the contract.</param>
/// <param name="symbolName">The name of the symbol that returns the contract.</param>
[ExcludeFromCodeCoverage]
[AttributeUsage(AttributeTargets.Class, AllowMultiple = true, Inherited = false)]
public sealed class BshoxDefaultContractAttribute(Type containingType, string symbolName) : Attribute
{
    public Type ContainingType { get; } = containingType;
    public string SymbolName { get; } = symbolName;
}
