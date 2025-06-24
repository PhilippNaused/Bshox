using System.Diagnostics;
using System.Reflection;
using System.Reflection.PortableExecutable;
using System.Text;
using ICSharpCode.Decompiler;
using ICSharpCode.Decompiler.CSharp;
using ICSharpCode.Decompiler.CSharp.OutputVisitor;
using ICSharpCode.Decompiler.CSharp.Syntax;
using ICSharpCode.Decompiler.CSharp.Transforms;
using ICSharpCode.Decompiler.Metadata;
using ICSharpCode.Decompiler.TypeSystem;
using Attribute = ICSharpCode.Decompiler.CSharp.Syntax.Attribute;
using MemberType = ICSharpCode.Decompiler.CSharp.Syntax.MemberType;

namespace ApiValidation;

public static class ICSharpCodeExtensions
{
    private static CSharpDecompiler GetDecompiler(string filePath)
    {
        var settings = new DecompilerSettings(LanguageVersion.Latest)
        {
            UsingDeclarations = false, // always use fully qualified names
            DecompileMemberBodies = false, // we only need the signatures
            AlwaysShowEnumMemberValues = true, // changes to enum values are breaking changes => always show them
            ShowXmlDocumentation = false, // the docs are not part of the API
            AutoLoadAssemblyReferences = false, // we don't need the references
            UsePrimaryConstructorSyntax = false, // primary constructors make the API less readable
            UsePrimaryConstructorSyntaxForNonRecordTypes = false,
            UseDebugSymbols = false, // we don't need the debug symbols
            ShowDebugInfo = false, // we don't need the debug info
            LoadInMemory = true, // faster than loading from disk
            FileScopedNamespaces = false
        };
        var format = settings.CSharpFormattingOptions;
        format.IndentationString = "    "; // 4 spaces is the de facto standard for C#
        // use as few lines as possible:
        format.AutoPropertyFormatting = PropertyFormatting.SingleLine;
        format.MinimumBlankLinesBetweenMembers = 0;
        format.MinimumBlankLinesBetweenTypes = 0;
        return new CSharpDecompiler(filePath, settings);
    }

    private static string GetPlatformDisplayName(PEFile module)
    {
        var headers = module.Reader.PEHeaders;
        var architecture = headers.CoffHeader.Machine;
        var characteristics = headers.CoffHeader.Characteristics;
        var corFlags = headers.CorHeader?.Flags ?? throw new BadImageFormatException("Missing COR header");
        switch (architecture)
        {
            case Machine.I386:
                if (corFlags.HasFlag(CorFlags.Prefers32Bit))
                    return "AnyCPU (32-bit preferred)";
                if (corFlags.HasFlag(CorFlags.Requires32Bit))
                    return "x86";
                // According to ECMA-335, II.25.3.3.1 CorFlags.Requires32Bit and Characteristics.Bit32Machine must be in sync
                // for assemblies containing managed code. However, this is not true for C++/CLI assemblies.
                if (!corFlags.HasFlag(CorFlags.ILOnly) && characteristics.HasFlag(Characteristics.Bit32Machine))
                    return "x86";
                return "AnyCPU (64-bit preferred)";
            case Machine.Amd64:
                return "x64";
            case Machine.IA64:
                return "Itanium";
            case Machine.Arm64:
                return "ARM64";
            default:
                return architecture.ToString();
        }
    }

    public static string Decompile(this Assembly assembly)
    {
        return Decompile(assembly.Location);
    }

    public static string Decompile(string assembly)
    {
        var decompiler = GetDecompiler(assembly);
        decompiler.AstTransforms.Add(new RemovePrivateItemsVisitor());

        var ts = decompiler.TypeSystem;
        var module = ts.MainModule;
        using var peFile = new PEFile(assembly);
        var sb = new StringBuilder().AppendLine($"// {module.FullAssemblyName}")
            .AppendLine($"// Platform: {GetPlatformDisplayName(peFile)}")
            .AppendLine($"// Runtime: {module.MetadataFile!.Metadata.MetadataVersion}");

        foreach (var reference in module.MetadataFile.AssemblyReferences)
        {
            sb = sb.AppendLine($"// Reference: {reference.FullName}");
        }

        foreach (var reference in module.MetadataFile.ModuleReferences)
        {
            sb = sb.AppendLine($"// Reference: {reference.Name}");
        }

        return sb.AppendLine(decompiler.DecompileWholeModuleAsString())
            .Replace("\r\n", "\n") // normalize line endings
            .Replace("\n\n", "\n") // remove empty lines
            .ToString();
    }

    internal class RemovePrivateItemsVisitor : DepthFirstAstVisitor, IAstTransform
    {
        void IAstTransform.Run(AstNode rootNode, TransformContext context)
        {
            rootNode.AcceptVisitor(this);
        }

        public override void VisitSyntaxTree(SyntaxTree syntaxTree)
        {
            // sort namespaces by name
            var members = syntaxTree.Members.OrderBy(m => m is NamespaceDeclaration e ? e.Name : "");
            syntaxTree.Members.ReplaceWith(members);
            base.VisitSyntaxTree(syntaxTree);
        }

        public override void VisitConstructorDeclaration(ConstructorDeclaration declaration)
        {
            base.VisitConstructorDeclaration(declaration);
            Process(declaration);
        }

        public override void VisitTypeDeclaration(TypeDeclaration declaration)
        {
            IEnumerable<EntityDeclaration> members = declaration.Members;
            if (declaration.GetSymbol() is IType { Kind: TypeKind.Enum })
            {
                // sort enum members by value
                members = members.OrderBy(m => m.GetSymbol() is IField field ? field.GetConstantValue() : null);
            }
            else
            {
                members = members
                    .OrderBy(m => m.SymbolKind is not SymbolKind.Constructor) // constructors first
                    .ThenBy(m => m.SymbolKind) // then sort by kind (fields, properties, methods, etc.)
                    .ThenByDescending(m => m.GetSymbol() as IEntity is { } e ? e.Accessibility : Accessibility.None) // then sort by accessibility (public first)
                    .ThenBy(m => m.Name); // then sort by name
            }
            declaration.Members.ReplaceWith(members);
            base.VisitTypeDeclaration(declaration);
            Process(declaration);
        }

        public override void VisitMethodDeclaration(MethodDeclaration declaration)
        {
            base.VisitMethodDeclaration(declaration);
            Process(declaration);
        }

        public override void VisitPropertyDeclaration(PropertyDeclaration declaration)
        {
            base.VisitPropertyDeclaration(declaration);
            Process(declaration);
        }

        public override void VisitAccessor(Accessor accessor)
        {
            base.VisitAccessor(accessor);
            Process(accessor);
        }

        public override void VisitNamespaceDeclaration(NamespaceDeclaration declaration)
        {
            // sort types by name
            var members = declaration.Members.OrderBy(m => m is EntityDeclaration e ? e.Name : "");
            declaration.Members.ReplaceWith(members);
            base.VisitNamespaceDeclaration(declaration);
            if (declaration.Members.Count == 0) // remove empty namespaces
            {
                declaration.Remove();
            }
        }

        private static readonly string[] ExcludeAttributes =
        [
            "System.Diagnostics.DebuggableAttribute", // changes when you build debug vs release
            "System.Reflection.AssemblyConfigurationAttribute", // changes when you build debug vs release
            "System.Reflection.AssemblyInformationalVersionAttribute", // contains the git commit
            "System.Runtime.CompilerServices.CompilerGeneratedAttribute", // don't care. Also ruins the formatting.
            "System.Runtime.CompilerServices.MethodImplAttribute", // doesn't affect the API
            "System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverageAttribute" // doesn't affect the API
        ];

        public override void VisitAttributeSection(AttributeSection attributeSection)
        {
            //if (attributeSection.AttributeTarget is "assembly" or "module")
            //{
            //    attributeSection.Remove();
            //    return;
            //}

            foreach (Attribute attribute in attributeSection.Attributes)
            {
                if (attribute.Type is MemberType member)
                {
                    // remove attributes that we don't care about
                    var sym = member.GetSymbol() as IEntity;
                    var name = sym?.FullName;
                    if (ExcludeAttributes.Contains(name))
                    {
                        attribute.Remove();
                    }
                }
                else
                {
                    Debug.Fail("Unexpected attribute type");
                }
            }
            if (attributeSection.Attributes.Count == 0)
            {
                // if we don't remove it, we'll get an empty [] element
                attributeSection.Remove();
                return;
            }
            base.VisitAttributeSection(attributeSection);
        }

        public override void VisitFieldDeclaration(FieldDeclaration declaration)
        {
            base.VisitFieldDeclaration(declaration);
            Process(declaration);
        }

        public override void VisitIndexerDeclaration(IndexerDeclaration declaration)
        {
            base.VisitIndexerDeclaration(declaration);
            Process(declaration);
        }

        public override void VisitEventDeclaration(EventDeclaration declaration)
        {
            base.VisitEventDeclaration(declaration);
            Process(declaration);
        }

        public override void VisitCustomEventDeclaration(CustomEventDeclaration declaration)
        {
            base.VisitCustomEventDeclaration(declaration);
            Process(declaration);
        }

        public override void VisitOperatorDeclaration(OperatorDeclaration declaration)
        {
            base.VisitOperatorDeclaration(declaration);
            Process(declaration);
        }

        public override void VisitDelegateDeclaration(DelegateDeclaration declaration)
        {
            base.VisitDelegateDeclaration(declaration);
            Process(declaration);
        }

        private static void Process(EntityDeclaration declaration)
        {
            // remove all members that are not visible from the outside
            if (declaration.GetSymbol() is IEntity symbol)
            {
                if (symbol.EffectiveAccessibility() is not Accessibility.Public and not Accessibility.Protected and not Accessibility.ProtectedOrInternal)
                {
                    declaration.Remove();
                }
            }
        }
    }
}
