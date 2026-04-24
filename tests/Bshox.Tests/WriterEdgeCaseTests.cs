using Bshox.TestUtils;

namespace Bshox.Tests;

/// <summary>
/// Covers the various edge cases of the BshoxWriter.
/// </summary>
public class WriterEdgeCaseTests
{
    [Test]
    public async Task FlushTwice()
    {
        var buffer = new FixedBufferWriter();
        var writer = new BshoxWriter(buffer);
        writer.WriteByte(1);
        writer.Flush();
        writer.Flush();
        await Assert.That(writer.UnflushedBytes).IsEqualTo(0);
    }

    [Test]
    public async Task WriteBytes0()
    {
        var buffer = new FixedBufferWriter();
        var writer = new BshoxWriter(buffer);
        writer.WriteBytes([]);
        writer.Flush();
    }
}
