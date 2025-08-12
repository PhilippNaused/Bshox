using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.Serialization;
#if NET8_0_OR_GREATER
using Bshox.Internals;
#endif

namespace Bshox.Utils;

[Serializable]
public sealed class BshoxParserException : BshoxException
{
    private readonly BshoxTextParser.Token? _token;

    public (int Line, int Column)? Position => _token?.Position ?? null;

    public string? Token => _token?.ToString() ?? null;

    internal BshoxParserException(BshoxTextParser.Token token, string message, Exception? innerException = null) : base(message, innerException)
    {
        _token = token;
    }

    internal BshoxParserException(string message, Exception? innerException = null) : base(message, innerException)
    {
        _token = null;
    }

    [ExcludeFromCodeCoverage]
#if NET8_0_OR_GREATER
    [Obsolete(Obsoletions.LegacyFormatterMessage, DiagnosticId = Obsoletions.LegacyFormatterDiagId, UrlFormat = Obsoletions.SharedUrlFormat)]
#endif
    private BshoxParserException(SerializationInfo info, StreamingContext context) : base(info, context)
    {
        Debug.Fail("This constructor should not be called.");
    }

    [ExcludeFromCodeCoverage]
#if NET8_0_OR_GREATER
    [Obsolete(Obsoletions.LegacyFormatterMessage, DiagnosticId = Obsoletions.LegacyFormatterDiagId, UrlFormat = Obsoletions.SharedUrlFormat)]
#endif
    public override void GetObjectData(SerializationInfo info, StreamingContext context)
    {
        base.GetObjectData(info, context);
        Debug.Fail("This constructor should not be called.");
    }
}
