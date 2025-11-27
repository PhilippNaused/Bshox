#if NET8_0_OR_GREATER
#define USE_REF
#endif

#if DEBUG
#pragma warning disable CS0282 // Don't care
#else
#pragma warning disable CA1822 // Mark members as static
#pragma warning disable IDE0251 // Member can be made readonly
#endif

using System.Diagnostics;

namespace Bshox;

public ref partial struct BshoxWriter
{
#if DEBUG
    private bool _waitingForAdvance; // debug only: to ensure Advance is called after GetSpan/GetRef
#endif

    [Conditional("DEBUG")]
    private void WaitingForAdvance(bool waiting)
    {
#if DEBUG
        _waitingForAdvance = waiting;
#endif
    }

    [Conditional("DEBUG")]
    private readonly void CheckWaitingForAdvance(bool waiting)
    {
#if DEBUG
        Debug.Assert(_waitingForAdvance == waiting, "_waitingForAdvance == waiting");
#endif
    }

    [Conditional("DEBUG")]
    private readonly void Check()
    {
#if DEBUG
        Debug.Assert(_depth <= Options.MaxDepth, "_depth < Options.MaxDepth");
        Debug.Assert(_depth >= 0, "_depth >= 0");
#if USE_REF
        Debug.Assert(_unflushed >= 0);
        Debug.Assert(_length >= 0);
        if (_waitingForAdvance)
        {
            Debug.Assert(!System.Runtime.CompilerServices.Unsafe.IsNullRef(ref _ref));
            Debug.Assert(_length > 0);
        }
        if (System.Runtime.CompilerServices.Unsafe.IsNullRef(ref _ref))
        {
            Debug.Assert(_length == 0);
            Debug.Assert(_unflushed == 0);
            Debug.Assert(!_waitingForAdvance);
        }
#else
        Debug.Assert(_span.Length >= 0);
        Debug.Assert(_index >= 0);
        Debug.Assert(_span.Length >= _index);
        if (_waitingForAdvance)
        {
            Debug.Assert(!_span.IsEmpty);
        }
#pragma warning disable CA2265 // Do not compare Span<T> to 'null' or 'default'
        if (_span == default)
#pragma warning restore CA2265 // Do not compare Span<T> to 'null' or 'default'
        {
            Debug.Assert(_index == 0);
            Debug.Assert(!_waitingForAdvance);
        }
#endif
#endif
    }
}
