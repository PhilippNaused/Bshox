using Bshox.Utils;

namespace Bshox.Tests;

public class TextEncodingTests
{
    [Test]
    [Arguments(1f, "1.0i32")]
    [Arguments(-9999f, "-9999.0i32")]
    [Arguments(0.682287216f, "0.682287216i32")]
    [Arguments(float.NaN, "0xFFC00000i32")]
    [Arguments(float.PositiveInfinity, "inf32")]
    [Arguments(float.NegativeInfinity, "-inf32")]
    public async Task Floats(float value, string text)
    {
        var actual = DefaultContracts.Single.ToBshoxString(value);
        await Assert.That(actual).IsEqualTo(text);
    }

    [Test]
    [Arguments(1d, "1.0i64")]
    [Arguments(-9999d, "-9999.0i64")]
    [Arguments(0.68228719991740006d, "0.68228719991740006i64")]
    [Arguments(double.NaN, "0xFFF8000000000000i64")]
    [Arguments(double.PositiveInfinity, "inf64")]
    [Arguments(double.NegativeInfinity, "-inf64")]
    public async Task Doubles(double value, string text)
    {
        var actual = DefaultContracts.Double.ToBshoxString(value);
        await Assert.That(actual).IsEqualTo(text);
    }

    [Test]
    [Arguments(1, "1")]
    [Arguments(100, "100")]
    [Arguments(-1, "4294967295")]
    [Arguments(int.MaxValue, "2147483647")]
    [Arguments(int.MinValue, "2147483648")]
    public async Task Ints(int value, string text)
    {
        var actual = DefaultContracts.Int32.ToBshoxString(value);
        await Assert.That(actual).IsEqualTo(text);
    }

    [Test]
    public async Task EmptyArray()
    {
        await Assert.That(new BshoxArray().ToString()).IsEqualTo("[]");
    }

    [Test]
    public async Task EmptyObject()
    {
        await Assert.That(new BshoxObject().ToString()).IsEqualTo("{ }");
    }

    [Test]
    [Arguments("Hello, World!", @"""Hello, World!""")] // no escaping
    [Arguments("""Hello, "World"!""", @"""Hello, \""World\""!""")] // quotes are escaped
    [Arguments(@"Hello, \World!", @"""Hello, \\World!""")] // backslashes are escaped
    [Arguments("Hello, \nWorld!", "`48656C6C6F2C200A576F726C6421`")] // non-printable strings are hex encoded
    public async Task Utf8String(string input, string expected)
    {
        await Assert.That(new BshoxBlob(input).ToString()).IsEqualTo(expected);
    }
}
