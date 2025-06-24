using System.Text;
using Bshox.Generator.Data;
using Bshox.Generator.Extensions;
using Bshox.Generator.Helpers;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;

namespace Bshox.Generator.Contracts;

internal sealed class ContractGenerator(ContractParameters parameters, List<MemberInfo> members, string generatedTypeName) : IContractGenerator
{
    public string GeneratedTypeName => generatedTypeName;

    public void WriteXmlDocRemarks(SourceWriter code, IContractResolver resolver)
    {
        code.WriteLine("/// Bshox member layout:");
        foreach (var member in members)
        {
            var sb = new StringBuilder().AppendFormat("/// <para><c>{0}</c>: ", member.Key);
            if (member.MemberType.TypeKind is TypeKind.TypeParameter)
            {
                string displayString = member.MemberType.ToDisplayString(NullableFlowState.NotNull, SymbolDisplayFormat.MinimallyQualifiedFormat);
                _ = sb.AppendFormat("<typeparamref name=\"{0}\"/> {1}", displayString, member.Name);
            }
            else
            {
                string displayString = member.MemberType.ToXmlCommentString();
                _ = sb.Append(displayString);
                _ = sb.Append(' ');
                _ = sb.Append(member.Name);
            }
            if (member.DefaultValue is not null)
            {
                _ = sb.AppendFormat(" (default: <c>{0}</c>)", member.DefaultValue.Value.ToCSharpString());
            }
            if (member.ImplicitDefault)
            {
                _ = sb.Append(" (implicit default)");
            }
            _ = sb.Append("</para>");
            code.WriteLine(sb.ToString());
        }
    }

    public bool TryGenerate(ContractInfo contract, SerializerInfo serializer)
    {
        var code = new SourceWriter();

        code.WriteComment($"ImplicitDefaultValues = {parameters.ImplicitDefaultValues}");
        code.WriteComment($"ImplicitMembers = {parameters.ImplicitMembers}");

        // check if layout is implicit or explicit
        if (parameters.ImplicitMembers)
        {
            // layout is implicit => assign keys in order
            for (int i = 0; i < members.Count; i++)
            {
                members[i].Key = (uint)(i + 1);
            }
            // members cannot have explicit key
            var badMembers = members.Where(static x => x.HasExplicitKey).ToList();
            if (badMembers.Count > 0)
            {
                foreach (var badMember in badMembers)
                {
                    serializer.ReportDiagnostic(Diagnostics.ImplicitMemberMustNotHaveKey, badMember.Symbol, badMember.Symbol);
                }
                return false;
            }
        }
        else
        {
            // layout is explicit => all members must have explicit key!
            var badMembers = members.Where(static x => !x.HasExplicitKey).ToList();
            if (badMembers.Count > 0)
            {
                foreach (var badMember in badMembers)
                {
                    serializer.ReportDiagnostic(Diagnostics.MemberMustHaveExplicitKey, badMember.Symbol, badMember.Symbol);
                }
                return false;
            }
            // sort members by key
            members.Sort(static (x, y) => x.Key.CompareTo(y.Key));
        }

        // check if keys are unique
        var duplicateKeys = members.GroupBy(static x => x.Key).Where(static x => x.Count() > 1).ToList();
        foreach (var duplicateKey in duplicateKeys)
        {
            foreach (var member in duplicateKey)
            {
                serializer.ReportDiagnostic(Diagnostics.KeyMustBeUnique, member.Symbol, member.Name, member.Key);
            }
        }
        if (duplicateKeys.Count != 0)
            return false;

        // check that keys are in the valid range
        var badKeys = members.Where(static x => x.Key is < BshoxConstants.MinKey or > BshoxConstants.MaxKey).ToList();
        foreach (var badKey in badKeys)
        {
            serializer.ReportDiagnostic(Diagnostics.KeyMustBeInValidRange, badKey.Symbol, badKey.Symbol, badKey.Key);
        }
        if (badKeys.Count > 0)
        {
            return false;
        }

        bool error = false;

        // resolve contracts for members
        foreach (var member in members)
        {
            if (!member.TryFindContract(serializer.ContractResolver))
            {
                error = true;
            }
        }
        if (error)
            return false;

        code.DeclareAlias(serializer);
        code.WriteLine();

        var ns = serializer.ClassSymbol.ContainingNamespace;
        if (!ns.IsGlobalNamespace)
        {
            code.WriteLine($"namespace {ns};");
            code.WriteLine();
        }

        string serializerClassName = serializer.ClassSymbol.ToDisplayString(NullableFlowState.None, SymbolDisplayFormat.MinimallyQualifiedFormat);
        code.WriteLine($"partial class {serializerClassName}");
        code.OpenScope();

        GenerateContractClass(code, contract);

        code.CloseScope(); // partial class {serializerClassName}

        string fullType = serializer.ClassSymbol.FullyQualifiedToStringNG()
            .Replace('<', '(')
            .Replace('>', ')');
        string fileName = Constants.InvalidPathChars.Replace($"{fullType}.{generatedTypeName}.g.cs", string.Empty);

        serializer.AddSource(fileName, code);
        return true;
    }

    private void GenerateContractClass(SourceWriter code, ContractInfo contract)
    {
        string typeName = contract.Type.ToDisplayString(NullableFlowState.None, SymbolExtensions.FullyQualifiedFormat);

        code.WriteLine(Constants.GeneratedCodeAttributeText);
        code.WriteLine($$"""
                        private sealed class {{generatedTypeName}} : bsx::BshoxContract<{{typeName}}>
                        {
                            internal {{generatedTypeName}}() : base(bsx::BshoxCode.SubObject)
                            {
                            }
                        """);
        code.Indentation++;
        code.WriteLine();

        code.WriteLine($"public override void Serialize(ref bsx::BshoxWriter writer, scoped ref readonly {typeName} value)");
        code.OpenScope();

        // TODO: check if this contract can cause infinite recursion. If not, skip the DepthLock.
        code.WriteLine("using var _ = writer.DepthLock();");

        for (int i = 0; i < members.Count; i++)
        {
            var member = members[i];
            member.WriteSerialize(code);
        }

        code.WriteLine("writer.WriteByte(0);");

        code.CloseScope(); // Serialize
        code.WriteLine();

        code.WriteLine($"public override void Deserialize(ref bsx::BshoxReader reader, out {typeName} value)");
        code.OpenScope();

        foreach (var member in members)
        {
            string defaultValue = member.DefaultValueString ?? "default";
            code.WriteLine($"{member.MemberType.FullyQualifiedToString()} {member.LocalVariableName} = {defaultValue};");
        }

        // TODO: check if this contract can cause infinite recursion. If not, skip the DepthLock.
        code.WriteLine("using var _ = reader.DepthLock();");

        code.WriteLine("""
                        while (true)
                        {
                            uint key = reader.ReadTag(out bsx::BshoxCode encoding);
                            switch (key)
                            {
                                case 0:
                        """);
        code.Indentation += 2;
        code.OpenScope();
        {
            code.WriteComment("Encoding: Null");

            // throw exception if encoding is not the expected encoding (Null)
            code.WriteLine("bsx::BshoxException.ThrowIfWrongEncoding(encoding, bsx::BshoxCode.Null);");
            code.WriteLine($"value = new {typeName}"); // object initializer
            code.OpenScope();
            foreach (var member in members)
            {
                code.WriteLine($"{member.Name} = {member.LocalVariableName},");
            }
            code.Indentation--;
            code.WriteLine("};");
            code.WriteLine("return;");
        }
        code.CloseScope();

        foreach (var member in members)
        {
            member.WriteDeserialize(code);
        }

        code.WriteLine("""
                        default:
                            reader.SkipValue(encoding);
                            break;
                        """);

        code.CloseScope(); // switch
        code.CloseScope(); // while
        code.CloseScope(); // Deserialize
        code.CloseScope(); // private sealed class _{0}BshoxContract()
    }
}
