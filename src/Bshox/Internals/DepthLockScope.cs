using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace Bshox.Internals;

[Obsolete("This type should only be referenced implicitly")]
public readonly ref struct DepthLockScope
{
#if NETCOREAPP
    private readonly ref int _depth;
#else
    private readonly Span<int> _ref;
#endif

    internal static DepthLockScope Create(scoped ref int depth, int max)
    {
        Debug.Assert(depth <= max, "depth <= max");
        if (depth >= max)
        {
            throw new BshoxException($"The maximum depth of {max} has been reached.");
        }
        depth++;
        return new DepthLockScope(ref depth);
    }

    private DepthLockScope(scoped ref int depth)
    {
#if NETCOREAPP
        _depth = ref Unsafe.AsRef(in depth);
#else
        unsafe
        {
            _ref = new Span<int>(Unsafe.AsPointer(ref depth), 1);
        }
#endif
    }

    public void Dispose()
    {
#if NETCOREAPP
        int d = _depth--;
#else
        int d = _ref[0]--;
#endif
        Debug.Assert(d >= 0, "depth >= 0");
    }
}
