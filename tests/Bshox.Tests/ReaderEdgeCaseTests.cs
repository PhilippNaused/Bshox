using Bshox.TestUtils;

namespace Bshox.Tests;

/// <summary>
/// Covers the various edge cases of the BshoxReader. (mostly throwing exceptions)
/// </summary>
public class ReaderEdgeCaseTests
{
    [Test]
    public async Task ReaderAdvanceNegative()
    {
        Assert.Throws<ArgumentOutOfRangeException>(() =>
        {
            var memory = new byte[10];
            var reader = new BshoxReader(memory);
            reader.Advance(-1);
        });
    }

    [Test]
    public async Task ReaderCopyToOverflow()
    {
        var ex = Assert.Throws<BshoxException>(() =>
        {
            var memory = new byte[10];
            var memory2 = new byte[11];
            var reader = new BshoxReader(memory);
            reader.CopyTo(memory2);
        });
        await Assert.That(ex.InnerException).IsTypeOf<EndOfStreamException>();
    }

    [Test]
    public async Task ReaderSequenceCopyToOverflow()
    {
        var ex = Assert.Throws<BshoxException>(() =>
        {
            var memory = new byte[10];
            var seq = SequenceSegmenter.MakeSegmentedSequence(memory, 5);
            var memory2 = new byte[11];
            var reader = new BshoxReader(seq);
            reader.CopyTo(memory2);
        });
        await Assert.That(ex.InnerException).IsTypeOf<EndOfStreamException>();
    }

    [Test]
    public async Task ReaderAdvanceOverflow()
    {
        var ex = Assert.Throws<BshoxException>(() =>
        {
            var memory = new byte[10];
            var reader = new BshoxReader(memory);
            reader.Advance(11);
        });
        await Assert.That(ex.InnerException).IsTypeOf<EndOfStreamException>();
    }

    [Test]
    public async Task InvalidSkipValueEncoding()
    {
        var ex = Assert.Throws<BshoxException>(() =>
        {
            var memory = new byte[10];
            var reader = new BshoxReader(memory);
            reader.SkipValue((BshoxCode)6); // valid values are 0-5, so this should throw an exception
        });
        await Assert.That(ex.InnerException).IsNull();
    }

    [Test]
    public async Task ReaderAdvanceSequence()
    {
        var memory = new byte[130];
        var seq = SequenceSegmenter.MakeSegmentedSequence(memory, 7);
        var reader = new BshoxReader(seq);
        for (int i = 0; i < 10; i++)
        {
            // 7 and 13 are relative primes, so this test covers the edge case of advancing across segment boundaries without consuming the entire segment
            reader.Advance(13);
        }
        long c = reader.Consumed;
        long r = reader.Remaining;
        await Assert.That(c).IsEqualTo(130);
        await Assert.That(r).IsEqualTo(0);
    }

    [Test]
    public async Task ReaderCopyToSequence()
    {
        const int segmentSize = 13;
        var memory = new byte[segmentSize * 10];
        var seq = SequenceSegmenter.MakeSegmentedSequence(memory, segmentSize);
        var reader = new BshoxReader(seq);
        for (int i = 0; i < 10; i++)
        {
            // Each copy will copy *exactly* one segment, which covers the edge case of needing to retrieve the next segment immediately after copying.
            byte[] destination = new byte[segmentSize];
            reader.CopyTo(destination);
        }
        long c = reader.Consumed;
        long r = reader.Remaining;
        await Assert.That(c).IsEqualTo(memory.Length);
        await Assert.That(r).IsEqualTo(0);
    }

    [Test]
    public async Task ReaderAdvanceSequenceBad()
    {
        var ex = Assert.Throws<BshoxException>(() =>
        {
            var memory = new byte[130 - 1];
            var seq = SequenceSegmenter.MakeSegmentedSequence(memory, 7);
            var reader = new BshoxReader(seq);
            for (int i = 0; i < 10; i++)
            {
                // The last advance will attempt to advance past the end of the sequence, which should throw an exception
                // This covers a different code branch than ReaderAdvanceOverflow()
                reader.Advance(13);
            }
        });
        await Assert.That(ex.InnerException).IsTypeOf<EndOfStreamException>();
    }
}
