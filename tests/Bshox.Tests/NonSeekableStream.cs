namespace Bshox.Tests;

/// <summary>
/// A stream that does not support seeking.
/// </summary>
internal sealed class NonSeekableStream(Stream innerStream) : Stream
{
    public override bool CanRead => innerStream.CanRead;

    public override bool CanSeek => false;

    public override bool CanWrite => innerStream.CanWrite;

    public override long Length => innerStream.Length;

    private static NotSupportedException NotSupportedExceptionInstance => new("This stream does not support seeking.");

    public override void Flush() => innerStream.Flush();

    public override int Read(byte[] buffer, int offset, int count) => innerStream.Read(buffer, offset, count);

    public override long Seek(long offset, SeekOrigin origin) => throw NotSupportedExceptionInstance;

    public override void SetLength(long value) => throw NotSupportedExceptionInstance;

    public override void Write(byte[] buffer, int offset, int count) => innerStream.Write(buffer, offset, count);

    public override long Position
    {
        get => throw NotSupportedExceptionInstance;
        set => throw NotSupportedExceptionInstance;
    }
}
