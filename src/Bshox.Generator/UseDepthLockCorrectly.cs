using System.Collections.Immutable;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Diagnostics;
using Microsoft.CodeAnalysis.Operations;

namespace Bshox.Generator;

[DiagnosticAnalyzer(LanguageNames.CSharp)]
public sealed class UseDepthLockCorrectly : DiagnosticAnalyzer
{
    public override ImmutableArray<DiagnosticDescriptor> SupportedDiagnostics { get; } = [Diagnostics.DepthLockNotUsedCorrectly];

    public override void Initialize(AnalysisContext context)
    {
        context.EnableConcurrentExecution();
        context.ConfigureGeneratedCodeAnalysis(GeneratedCodeAnalysisFlags.None);
        context.RegisterCompilationStartAction(OnCompilationStart);
    }

    private static void OnCompilationStart(CompilationStartAnalysisContext context)
    {
        context.RegisterOperationAction(AnalyzeInvocation, OperationKind.Invocation);
    }

    private static void AnalyzeInvocation(OperationAnalysisContext context)
    {
        var invocationOperation = (IInvocationOperation)context.Operation;
        var method = invocationOperation.TargetMethod;

        // look for signature: Bshox.BshoxReader.DepthLock() or Bshox.BshoxWriter.DepthLock()

        if (method.Name != "DepthLock" || !invocationOperation.Arguments.IsEmpty)
        {
            return;
        }

        if (method.ContainingType.Name is not "BshoxReader" and not "BshoxWriter" || method.ContainingType.ContainingNamespace.Name != "Bshox")
        {
            return;
        }

        if (invocationOperation is { Parent: IUsingOperation })
        {
            // using (reader.DepthLock()) { ... }
            return;
        }

        // is variable declaration
        if (invocationOperation is { Parent: IVariableInitializerOperation { Parent: IVariableDeclaratorOperation { Symbol: { } symbol } } })
        {
            if (symbol.IsUsing) // must be in using scope
            {
                if (symbol.Name is "" or "_") // variable must be unnamed or dispose pattern
                {
                    // using var _ = reader.DepthLock();
                    // OR
                    // using (var _ = reader.DepthLock()) { ... }
                    return;
                }
                if (!symbol.CanBeReferencedByName) // no idea if this can happen, but it's ok
                {
                    return;
                }

                // TODO: use different message here
            }
        }

        context.ReportDiagnostic(Diagnostic.Create(Diagnostics.DepthLockNotUsedCorrectly, invocationOperation.Syntax.GetLocation()));
    }
}
