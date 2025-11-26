using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.Serialization;
#if NET8_0_OR_GREATER
using Bshox.Internals;
#endif

namespace Bshox;

/// <summary>
/// Represents an exception that is thrown when an error occurs during Bshox serialization or deserialization.
/// </summary>
[Serializable]
public class BshoxException : Exception
{
    /// <inheritdoc/>
    public BshoxException(string message) : base(message)
    {
        Debug.Assert(!string.IsNullOrWhiteSpace(message), "!string.IsNullOrWhiteSpace(message)");
    }

    /// <inheritdoc/>
    public BshoxException(string message, Exception? inner) : base(message, inner)
    {
        Debug.Assert(!string.IsNullOrWhiteSpace(message), "!string.IsNullOrWhiteSpace(message)");
    }

    /// <inheritdoc/>
    [ExcludeFromCodeCoverage]
#if NET8_0_OR_GREATER
    [Obsolete(Obsoletions.LegacyFormatterMessage, DiagnosticId = Obsoletions.LegacyFormatterDiagId, UrlFormat = Obsoletions.SharedUrlFormat)]
#endif
    protected BshoxException(SerializationInfo info, StreamingContext context) : base(info, context) { }

    /// <inheritdoc/>
    [ExcludeFromCodeCoverage]
#if NET8_0_OR_GREATER
    [Obsolete(Obsoletions.LegacyFormatterMessage, DiagnosticId = Obsoletions.LegacyFormatterDiagId, UrlFormat = Obsoletions.SharedUrlFormat)]
#endif
    public override void GetObjectData(SerializationInfo info, StreamingContext context)
    {
        base.GetObjectData(info, context);
    }

    /// <summary>
    /// Throws a BshoxException if the specified <paramref name="encoding"/> does not match the <paramref name="expected"/> encoding.
    /// </summary>
    /// <param name="encoding">The actual encoding to validate.</param>
    /// <param name="expected">The expected encoding to compare against.</param>
    /// <exception cref="BshoxException">Thrown if encoding does not equal expected.</exception>
    [StackTraceHidden]
    public static void ThrowIfWrongEncoding(BshoxCode encoding, BshoxCode expected)
    {
        if (encoding != expected)
        {
            throw new BshoxException($"Invalid encoding: {encoding}. Expected: {expected}");
        }
    }

    internal static BshoxException ContractNotFound(Type type) => new($"No serialization contract for \"{type}\" could be found.");

    /// <summary>
    /// Creates a new exception indicating that the specified encoding is invalid.
    /// </summary>
    /// <param name="encoding">The encoding value that caused the error.</param>
    public static BshoxException InvalidEncoding(BshoxCode encoding) => new($"Invalid encoding: {encoding}");
}
