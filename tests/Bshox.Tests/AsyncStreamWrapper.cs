namespace Bshox.Tests;

#pragma warning disable CA1835 // Prefer the 'Memory'-based overloads for 'ReadAsync' and 'WriteAsync'

/// <summary>
/// Wrapper for a <see cref="Stream"/> that throws <see cref="NotSupportedException"/> for all synchronous methods.
/// </summary>
/// <param name="stream"></param>
/// <param name="yield">Indicates whether to yield before each asynchronous operation.</param>
internal sealed class AsyncStreamWrapper(Stream stream, bool yield = true) : Stream, IAsyncDisposable
{
    /// <summary></summary>
    public Stream Inner { get; } = stream;

    /// <inheritdoc />
    public override void Flush() => throw new NotSupportedException();

    /// <inheritdoc />
    public override int Read(byte[] buffer, int offset, int count) => throw new NotSupportedException();

    /// <inheritdoc />
    public override long Seek(long offset, SeekOrigin origin) => Inner.Seek(offset, origin);

    /// <inheritdoc />
    public override void SetLength(long value) => throw new NotSupportedException();

    /// <inheritdoc />
    public override void Write(byte[] buffer, int offset, int count) => throw new NotSupportedException();

    /// <inheritdoc />
    public override int ReadByte() => throw new NotSupportedException();
#if NETCOREAPP
    /// <inheritdoc />
    public override int Read(Span<byte> buffer) => throw new NotSupportedException();

    /// <inheritdoc />
    public override void Write(ReadOnlySpan<byte> buffer) => throw new NotSupportedException();
#endif
    /// <inheritdoc />
    public override void WriteByte(byte value) => throw new NotSupportedException();

    /// <inheritdoc />
    public override bool CanRead => Inner.CanRead;

    /// <inheritdoc />
    public override bool CanSeek => Inner.CanSeek;

    /// <inheritdoc />
    public override bool CanWrite => Inner.CanWrite;

    /// <inheritdoc />
    public override long Length => Inner.Length;

    /// <inheritdoc />
    public override long Position
    {
        get => Inner.Position;
        set => Inner.Position = value;
    }

    /// <inheritdoc />
    public override async Task CopyToAsync(Stream destination, int bufferSize, CancellationToken cancellationToken)
    {
        if (yield)
            await Task.Yield();
        await Inner.CopyToAsync(destination, bufferSize, cancellationToken);
    }

    /// <inheritdoc />
    public override void Close() => Inner.Close();

#if NETCOREAPP

    /// <inheritdoc />
    public override async ValueTask DisposeAsync()
    {
        if (yield)
            await Task.Yield();
        await Inner.DisposeAsync();
        await base.DisposeAsync();
    }

    /// <inheritdoc />
    public override void CopyTo(Stream destination, int bufferSize) => throw new NotSupportedException();

    /// <inheritdoc />
    public override async ValueTask<int> ReadAsync(Memory<byte> buffer, CancellationToken cancellationToken = new CancellationToken())
    {
        if (yield)
            await Task.Yield();
        return await Inner.ReadAsync(buffer, cancellationToken);
    }

    /// <inheritdoc />
    public override async ValueTask WriteAsync(ReadOnlyMemory<byte> buffer, CancellationToken cancellationToken = new CancellationToken())
    {
        if (yield)
            await Task.Yield();
        await Inner.WriteAsync(buffer, cancellationToken);
    }
#else

    /// <inheritdoc />
    public async ValueTask DisposeAsync()
    {
        if (yield)
            await Task.Yield();
        if (Inner is IAsyncDisposable asyncDisposable)
            await asyncDisposable.DisposeAsync();
        else
        {
            Inner.Dispose();
        }
    }
#endif

    /// <inheritdoc />
    public override async Task FlushAsync(CancellationToken cancellationToken)
    {
        if (yield)
            await Task.Yield();
        await Inner.FlushAsync(cancellationToken);
    }

    /// <inheritdoc />
    public override async Task<int> ReadAsync(byte[] buffer, int offset, int count, CancellationToken cancellationToken)
    {
        if (yield)
            await Task.Yield();

        return await Inner.ReadAsync(buffer, offset, count, cancellationToken);
    }

    /// <inheritdoc />
    public override async Task WriteAsync(byte[] buffer, int offset, int count, CancellationToken cancellationToken)
    {
        if (yield)
            await Task.Yield();
        await Inner.WriteAsync(buffer, offset, count, cancellationToken);
    }

    /// <inheritdoc />
    protected override void Dispose(bool disposing)
    {
        Inner.Dispose();
        base.Dispose(disposing);
    }
}

internal static class TestExtensions
{
    public static AsyncStreamWrapper AsAsyncStream(this Stream stream, bool yield = false) => new AsyncStreamWrapper(stream, yield);
}
