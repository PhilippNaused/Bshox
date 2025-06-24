using Microsoft.CodeAnalysis;

namespace Bshox.Generator;

#pragma warning disable CA1032 // Implement standard exception constructors
#pragma warning disable CA1064 // Exceptions should be public

internal sealed class DiagnosticException : Exception
{
    public DiagnosticException(string message, Location? location) : this(message, Diagnostic.Create(Diagnostics._internalError, location, message, null))
    {
    }

    public DiagnosticException(string message, ISymbol symbol) : this(message, symbol.Locations.FirstOrDefault())
    {
    }

    private DiagnosticException(string message, Diagnostic diagnostic) : base(message)
    {
        Diagnostic = diagnostic;
    }

    public Diagnostic Diagnostic { get; }
}
