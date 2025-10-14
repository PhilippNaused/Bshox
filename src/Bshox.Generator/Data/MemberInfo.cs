using System.Diagnostics;
using Bshox.Generator.Contracts;
using Bshox.Generator.Extensions;
using Bshox.Generator.Helpers;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;

namespace Bshox.Generator.Data;

internal sealed class MemberInfo
{
    public MemberInfo(ISymbol symbol, ContractParameters parameters, IGeneratorContext context)
    {
        Symbol = symbol;

        MemberType = symbol switch
        {
            IFieldSymbol f => f.Type,
            IPropertySymbol p => p.Type,
            _ => throw new InvalidOperationException("member is neither field nor property.")
        };

        if (Symbol.TryParseBshoxMemberAttribute(context.KnownSymbols, out uint key))
        {
            Key = key;
            HasExplicitKey = true;
        }

        if (Symbol.TryParseDefaultValueAttribute(context, out TypedConstant? value))
        {
            DefaultValue = value;
        }
        else if (parameters.ImplicitDefaultValues || IsReferenceType)
        {
            ImplicitDefault = true; // only set this if no explicit default value is set
        }

        ContractDemand = ContractDemand.DefaultForType(MemberType);
        LocalVariableName = $"__{Name}";
    }

    public ISymbol Symbol { get; }
    public string Name => Symbol.Name;
    public string LocalVariableName { get; }
    public ITypeSymbol MemberType { get; }
    public ContractDemand ContractDemand { get; }

    public bool IsValueType => MemberType.IsValueType;
    public bool IsReferenceType => MemberType.IsReferenceType;

    public uint Key { get; set; }
    public bool HasExplicitKey { get; }
    public TypedConstant? DefaultValue { get; }

    public ContractInfo? Contract { get; private set; }

    public string? DefaultValueString
    {
        get
        {
            if (DefaultValue is null)
            {
                return null;
            }
            var constant = DefaultValue.Value;
            string sharpString = constant.ToCSharpString();
            return MemberType.SpecialType switch
            {
                SpecialType.System_Single => $"{sharpString}F",
                SpecialType.System_Double => $"{sharpString}D",
                SpecialType.System_UInt16 => $"{sharpString}U",
                SpecialType.System_UInt32 => $"{sharpString}U",
                SpecialType.System_UInt64 => $"{sharpString}UL",
                SpecialType.System_UIntPtr => $"{sharpString}U",
                SpecialType.System_Int64 => $"{sharpString}L",
                SpecialType.System_Decimal => $"{sharpString}M",
                _ => sharpString
            };
        }
    }

    public bool ImplicitDefault { get; }

    public bool TryFindContract(IContractResolver resolver)
    {
        if (resolver.TryResolveContract(ContractDemand, out var contract))
        {
            Contract = contract;
            Debug.Assert(SymbolEqualityComparer.Default.Equals(Contract.Type, MemberType), "SymbolEqualityComparer.Default.Equals(Contract.Type, MemberType)");
        }
        return Contract is not null;
    }

    private bool NeedsExplicitNullSupport()
    {
        // TODO: check if this is still needed
        if (IsValueType)
            return false; // value types are never null
        if (ImplicitDefault)
            return false; // the implicit default value for reference types is null
        if (DefaultValue is not null)
        {
            if (DefaultValue.Value.IsNull)
                return false; // the default value is null
            else
                return true; // the default value is not null, so we need to explicitly check for null
        }
        return true; // no default value is set, so we need to explicitly check for null
    }

    public void WriteSerialize(SourceWriter code)
    {
        code.WriteLine($"var {LocalVariableName} = value.{Name};");
        if (ImplicitDefault)
        {
            if (IsValueType)
                code.WriteLine($"if ({LocalVariableName} != default)");
            else
                code.WriteLine($"if ({LocalVariableName} != null)"); // "default" is not allowed on unconstrained type parameters
        }
        else if (DefaultValue is not null)
        {
            code.WriteLine($"if ({LocalVariableName} != {DefaultValueString})");
        }

        code.OpenScope();
        code.WriteComment($"Key: {Key}");

        if (NeedsExplicitNullSupport())
        {
            code.WriteLine($"if ({LocalVariableName} is not null)");
            code.OpenScope();
            {
                WriteInnerSerialize(code);
            }
            code.CloseScope();
        }
        else
        {
            WriteInnerSerialize(code);
        }

        code.CloseScope();
    }

    private void WriteInnerSerialize(SourceWriter code)
    {
        Debug.Assert(Contract is not null, "Contract is not null");
        if (Contract!.InlineData is { } inlineData)
        {
            uint tag = (Key << 3) | (uint)inlineData.Encoding;
            code.WriteComment($"Encoding: {inlineData.Encoding}");
            if (tag < 128)
            {
                code.WriteLine($"writer.WriteByte({tag});");
            }
            else
            {
                code.WriteLine($"writer.WriteVarInt32({tag});");
            }
            var line = string.Format(inlineData.SerializeFormat, LocalVariableName);
            code.WriteLine(line);
        }
        else
        {
            code.WriteLine($"writer.WriteTag({Key}, {GetContractFieldName()}.Encoding);");
            code.WriteLine($"{GetContractFieldName()}.Serialize(ref writer, in {LocalVariableName});");
        }
    }

    private string GetContractFieldName()
    {
        Debug.Assert(Contract is not null, "Contract is not null");
        return Contract!.VariableFullName;
    }

    public void WriteDeserialize(SourceWriter code)
    {
        code.WriteLine($"case {Key}:");
        code.OpenScope();
        {
            WriteInnerDeserialize(code);
            code.WriteLine("break;");
        }
        code.CloseScope();
    }

    private void WriteInnerDeserialize(SourceWriter code)
    {
        Debug.Assert(Contract is not null, "Contract is not null");
        if (Contract!.InlineData is { } inlineData)
        {
            code.WriteComment($"Encoding: {inlineData.Encoding}");

            code.WriteLine($"bsx::BshoxException.ThrowIfWrongEncoding(encoding, bsx::BshoxCode.{inlineData.Encoding});");

            code.WriteLine($"{LocalVariableName} = {inlineData.DeserializeString};");
        }
        else
        {
            code.WriteLine($"bsx::BshoxException.ThrowIfWrongEncoding(encoding, {GetContractFieldName()}.Encoding);");

            code.WriteLine($"{GetContractFieldName()}.Deserialize(ref reader, out {LocalVariableName});");
        }
    }
}
