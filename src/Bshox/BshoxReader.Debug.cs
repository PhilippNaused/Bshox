using System.Diagnostics;

#if DEBUG
#pragma warning disable CS0282 // Don't care
#else
#pragma warning disable CA1822 // Mark members as static
#endif

namespace Bshox;

public ref partial struct BshoxReader
{
    [Conditional("DEBUG")]
    private readonly void Check()
    {
#if DEBUG
        Debug.Assert(_depth <= Options.MaxDepth, "_depth <= Options.MaxDepth");
        Debug.Assert(_depth >= 0, "_depth >= 0");
        Debug.Assert(Length >= 0, "Length >= 0");
        Debug.Assert(Consumed >= 0, "Consumed >= 0");
        Debug.Assert(Consumed <= Length, "Consumed <= Length");
        if (_usingSequence)
        {
            Debug.Assert(!_sequence.IsEmpty, "!_sequence.IsEmpty");
            Debug.Assert(_sequence.Length == Length, "_sequence.Length == Length");
        }
        else
        {
            Debug.Assert(_sequence.IsEmpty, "_sequence.IsEmpty");
            Debug.Assert(_next.GetObject() is null, "_next.GetObject() is null");
        }

        if (_moreData)
        {
            Debug.Assert(!_span.IsEmpty, "!_span.IsEmpty");
            Debug.Assert(Consumed < Length, "Consumed < Length");
            Debug.Assert(Remaining > 0, "Remaining > 0");
        }
        else
        {
            Debug.Assert(_span.IsEmpty, "_span.IsEmpty");
            //Debug.Assert(Consumed == Length, "Consumed == Length"); // Not necessarily true
            //Debug.Assert(Remaining == 0, "Remaining == 0");
        }

        if (!_moreData && _usingSequence)
        {
            Debug.Assert(_next.GetObject() is null, "_next.GetObject() is null");
        }
#endif
    }
}
