#if NET9_0_OR_GREATER

using System.Buffers;
using System.Diagnostics;
using System.IO.Pipelines;

namespace Bshox.Tests;

public sealed class TestPipeReader(ReadOnlySequence<byte> data, TestPipeReader.Mode mode, bool yield = true) : PipeReader
{
    private long _lastExamined;
    private bool _isCompleted;
    private readonly Random _random = new Random(42);

    public enum Mode
    {
        /// <summary>
        /// Returns the minimal amount of data possible, i.e. one byte more than what has been examined.
        /// </summary>
        Minimal,
        /// <summary>
        /// Returns a random amount of data between the minimal and the maximal amount possible.
        /// </summary>
        Random,
        /// <summary>
        /// Returns the entire remaining data in one read.
        /// </summary>
        All
    }

    public override void AdvanceTo(SequencePosition consumed)
    {
        AdvanceTo(consumed, consumed);
    }

    public override void AdvanceTo(SequencePosition consumed, SequencePosition examined)
    {
        if (_isCompleted)
        {
            throw new InvalidOperationException("Cannot advance a completed PipeReader.");
        }
        data = data.Slice(consumed);
        _lastExamined = data.GetOffset(examined);
        Debug.Assert(_lastExamined >= 0);
    }

    public override void Complete(Exception? exception = null)
    {
        _isCompleted = true;
    }

    public override async ValueTask CompleteAsync(Exception? exception = null)
    {
        if (yield)
        {
            await (Task.Yield());
        }
        Complete(exception);
    }

    public override ValueTask<ReadResult> ReadAsync(CancellationToken cancellationToken = default)
    {
        return ReadAtLeastAsyncCore(1, cancellationToken);
    }

    protected override async ValueTask<ReadResult> ReadAtLeastAsyncCore(int minimumSize, CancellationToken cancellationToken)
    {
        if (_isCompleted)
        {
            throw new InvalidOperationException("Cannot read a completed PipeReader.");
        }
        cancellationToken.ThrowIfCancellationRequested();
        if (yield)
        {
            await Task.Yield();
        }

        long min = minimumSize;
        long max = data.Length;
        if (min < _lastExamined + 1)
            min = _lastExamined + 1;
        if (min > max)
            min = max;
        long size = mode switch
        {
            Mode.Minimal => min,
            Mode.Random => _random.NextInt64(min, max + 1),
            Mode.All => max,
            _ => throw new InvalidOperationException($"Unsupported mode: {mode}")
        };
        var slice = data.Slice(0, size);
        return new ReadResult(slice, isCanceled: false, isCompleted: size == data.Length);
    }

    public override void CancelPendingRead()
    {
        throw new NotImplementedException();
    }

    public override bool TryRead(out ReadResult result)
    {
        throw new NotImplementedException();
    }

    public override Stream AsStream(bool leaveOpen = false)
    {
        throw new NotImplementedException();
    }

    public override Task CopyToAsync(PipeWriter destination, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public override Task CopyToAsync(Stream destination, CancellationToken cancellationToken = default)
    {
        return base.CopyToAsync(destination, cancellationToken);
    }
}

#endif
