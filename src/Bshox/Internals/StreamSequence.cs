using System.Buffers;
using System.Diagnostics;

namespace Bshox.Internals;

// TODO: consider just using CopyTo instead of this class

internal sealed class StreamSequence(Stream stream) : IDisposable
{
    private const int MinimumSegmentSize = 512;
    private readonly List<SteamSegment> _segments = [];
    private SteamSegment? _firstSegment;

    private long _index;
    private byte[]? _lastBuff;
    private int _lastBuffIndex;
    private SteamSegment? _lastSegment;

    public ReadOnlySequence<byte> Sequence
    {
        get
        {
            if (_segments.Count != 0)
            {
                Debug.Assert(_firstSegment is not null, "_firstSegment is not null");
                Debug.Assert(_lastSegment is not null, "_lastSegment is not null");
                return new ReadOnlySequence<byte>(_firstSegment!, 0, _lastSegment!, _lastSegment!.Length);
            }

            Debug.Assert(_firstSegment is null, "_firstSegment is null");
            Debug.Assert(_lastSegment is null, "_lastSegment is null");
            return new ReadOnlySequence<byte>([]);
        }
    }

    public void Dispose()
    {
        foreach (var segment in _segments)
        {
            if (segment.OwnBuffer != null)
            {
                ArrayPool<byte>.Shared.Return(segment.OwnBuffer, true);
            }
        }
        _segments.Clear();
    }

    public void ReadAll()
    {
        while (ReadMore())
        { }
    }

    public async Task ReadAllAsync(CancellationToken cancellationToken)
    {
        while (await ReadMoreAsync(cancellationToken).ConfigureAwait(false))
        { }
    }

    private bool ReadMore()
    {
        if (_lastBuff is null || _lastBuffIndex == _lastBuff.Length)
        {
            _lastBuff = ArrayPool<byte>.Shared.Rent(MinimumSegmentSize);
            _lastBuffIndex = 0;
        }

        var bytesRead = stream.Read(_lastBuff, _lastBuffIndex, _lastBuff.Length - _lastBuffIndex);
        if (bytesRead == 0)
        {
            return false;
        }

        var segment = new SteamSegment(_index, _lastBuff, _lastBuffIndex, bytesRead);
        if (_segments.Count == 0)
        {
            _firstSegment = segment;
        }
        _index += bytesRead;
        _segments.Add(segment);
        _lastSegment?.SetNext(segment);
        _lastSegment = segment;
        _lastBuffIndex += bytesRead;

        return true;
    }

    private async Task<bool> ReadMoreAsync(CancellationToken cancellationToken)
    {
        if (_lastBuff is null || _lastBuffIndex == _lastBuff.Length)
        {
            _lastBuff = ArrayPool<byte>.Shared.Rent(MinimumSegmentSize);
            _lastBuffIndex = 0;
        }
#pragma warning disable CA1835 // Prefer the 'Memory'-based overloads for 'ReadAsync' and 'WriteAsync'
        var bytesRead = await stream.ReadAsync(_lastBuff, _lastBuffIndex, _lastBuff.Length - _lastBuffIndex, cancellationToken).ConfigureAwait(false);
#pragma warning restore CA1835 // Prefer the 'Memory'-based overloads for 'ReadAsync' and 'WriteAsync'
        if (bytesRead == 0)
        {
            return false;
        }

        var segment = new SteamSegment(_index, _lastBuff, _lastBuffIndex, bytesRead);
        if (_segments.Count == 0)
        {
            _firstSegment = segment;
        }
        _index += bytesRead;
        _segments.Add(segment);
        _lastSegment?.SetNext(segment);
        _lastSegment = segment;
        _lastBuffIndex += bytesRead;

        return true;
    }

    private sealed class SteamSegment : ReadOnlySequenceSegment<byte>
    {
        internal readonly byte[]? OwnBuffer;

        public SteamSegment(long index, byte[] buffer, int start, int count)
        {
            OwnBuffer = start == 0 ? buffer : null;
            Memory = buffer.AsMemory(start, count);
            RunningIndex = index;
        }

        public int Length => Memory.Length;

        internal void SetNext(SteamSegment segment)
        {
            Next = segment;
        }
    }
}
