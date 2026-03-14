using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace Bshox.Generator.Extensions;

internal static class SyntaxExtensions
{
    public static bool IsPartial(this TypeDeclarationSyntax typeDeclaration)
    {
        return typeDeclaration.Modifiers.Any(m => m.IsKind(SyntaxKind.PartialKeyword));
    }

    public static Location GetLocation(this SyntaxReference reference)
    {
        return reference.GetSyntax().GetLocation();
    }
}
