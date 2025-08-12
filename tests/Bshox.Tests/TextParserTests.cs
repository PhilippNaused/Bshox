using System.Runtime.CompilerServices;
using Bshox.Meta;
using Bshox.TestUtils;
using TUnit.Assertions.AssertConditions.Throws;

namespace Bshox.Tests;

public class TextParserTests
{
    [Test]
    [Arguments("123456", new[] { "123456" })] // no whitespace
    [Arguments("{\"abc\"}", new[] { "{", "\"abc\"", "}" })]
    [Arguments("[\"abc\" 8]", new[] { "[", "\"abc\"", "8", "]" })]
    [Arguments("123456 ", new[] { "123456" })] // trailing whitespace is ignored
    [Arguments(" 123456", new[] { "123456" })] // leading whitespace is ignored
    [Arguments("123 456", new[] { "123", "456" })] // split on whitespace
    [Arguments("123 \"45 6\" 789", new[] { "123", "\"45 6\"", "789" })] // split on whitespace
    [Arguments("123\n456", new[] { "123", "456" })] // split on newline
    [Arguments("123\t456", new[] { "123", "456" })] // split on tab
    [Arguments("123\r456", new[] { "123", "456" })] // split on return carriage
    [Arguments("123 \r\n\t\t \n456", new[] { "123", "456" })] // split on all whitespace characters
    [Arguments("\"123 456\"", new[] { "\"123 456\"" })] // text enclosed in double quotes
    [Arguments("\"123 \r\n\t\t \n456\"", new[] { "\"123 \r\n\t\t \n456\"" })] // text enclosed in double quotes
    [Arguments("\"123 456\" 789", new[] { "\"123 456\"", "789" })] // text enclosed in double quotes
    [Arguments("\"123 \\\"456\"", new[] { "\"123 \\\"456\"" })] // text enclosed in double quotes with escaped quote
    [Arguments("\"123 456\\\"", new[] { "\"123 456\\\"" })] // escaped escape character
    [Arguments("123456{}", new[] { "123456", "{", "}" })] // special characters
    [Arguments("123456 { } ", new[] { "123456", "{", "}" })] // special characters
    [Arguments("123456 \"{}\"", new[] { "123456", "\"{}\"" })] // special characters in text
    [Arguments("123456[]", new[] { "123456", "[", "]" })] // special characters
    [Arguments("123456 \"[]\"", new[] { "123456", "\"[]\"" })] // special characters in text
    [Arguments("", new string[] { })] // edge case: empty string
    [Arguments(" ", new string[] { })] // edge case: whitespace
    [Arguments("""
              {
                1: 1.0
                2: []
              }
              """, new[] { "{", "1:", "1.0", "2:", "[", "]", "}" })]
    [Arguments("""
              {
                # This is a comment
                1: 1.0
                2: []# This is also a comment
              }
              # This is the last comment
              """, new[] { "{", "1:", "1.0", "2:", "[", "]", "}" })]
    public async Task SplitTokens(string text, string[] expected)
    {
        var actual = BshoxTextParser.SplitTokens(text).Select(t => t.ToString()).ToArray();
        await Assert.That(actual).IsEquivalentTo(expected);
    }

    [Test]
    [Arguments("   \\\"123 456\\\"")] // escape character outside of text are not allowed
    public async Task InvalidEscape(string text)
    {
        await Assert.That(delegate
        {
            _ = BshoxTextParser.SplitTokens(text);
        }).Throws<BshoxParserException>().WithMessage($"Unexpected escape character '{Constants.Escape}'");
    }

    [Test]
    [Arguments("``", "")]
    [Arguments("`AABBCC`", "AABBCC")]
    [Arguments("`aabbcc`", "AABBCC")]
    [Arguments("`aABbcC`", "AABBCC")]

    [Arguments("\"\"", "")]
    [Arguments("\"Hello, World!\"", "48656C6C6F2C20576F726C6421")]
    [Arguments("\"\0\"", "00")]

    [Arguments(@"""\\""", "5C")]
    [Arguments(@"""\n""", "0A")]
    [Arguments(@"""\r""", "0D")]
    [Arguments(@"""\t""", "09")]
    [Arguments(@"""\x00""", "00")]
    [Arguments(@"""\xFF""", "FF")]
    [Arguments(@"""\xff""", "FF")]

    public async Task ParseBlob(string text, string hex)
    {
        var blob = BshoxTextParser.ParseBlob(GetToken(text));
        var actual = blob.ToHex();
        await Assert.That(actual).IsEqualTo(hex);
    }

    private static BshoxTextParser.Token GetToken(string text) => new(text, 0, text.Length);

    [Test]
    [Arguments("""
              {
              }
              """, new uint[0])]
    [Arguments("""
              {
                1: 1.0
                2: 2
              }
              """, new uint[] { 1, 2 })]
    [Arguments("""
              {
                1: 0.0i32
                2: 3.14e-2i64
                3: 9
                4: -9z
                5: 0x9
                6: 6z
                7: [0 0]
                8: `00`
                9: "0" #0
                10: null
                9999: {0: {0: 0i64}}
              }
              """, new uint[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 9999 })]
    [Arguments("{}", new uint[0])] // edge case: empty object
    public async Task ParseObject(string text, uint[] keys)
    {
        var actual = await GetValue<BshoxObject>(text);
        await Assert.That(actual.Select(kv => kv.Key)).IsEquivalentTo(keys);
    }

    [Test]
    [Arguments("[]", 0, BshoxCode.Null)]
    [Arguments("    [ # This is a comment\n  ]   ", 0, BshoxCode.Null)]
    [Arguments("[1 2 3 4]", 4, BshoxCode.VarInt)]
    [Arguments("[1.0i32 2i32 0xAi32 1e-5]", 4, BshoxCode.Fixed4)]
    [Arguments("[1.0 2i64 0xAi64 1e-5]", 4, BshoxCode.Fixed8)]
    [Arguments("[\"Hi!\" `AABBCC`]", 2, BshoxCode.Prefixed)]
    [Arguments("[[][]]", 2, BshoxCode.Array)]
    [Arguments("[{}{}{}]", 3, BshoxCode.SubObject)]
    public async Task ParseArray(string text, int count, BshoxCode encoding)
    {
        var actual = await GetValue<BshoxArray>(text);
        await Assert.That(actual).IsNotNull();
        await Assert.That(actual).HasCount(count);
        await Assert.That(actual.ElementEncoding).IsEqualTo(encoding);
    }

    [Test]
    [Arguments("")] // nothing
    [Arguments("     ")] // whitespace
    [Arguments("1 2 3 4 5 6")] // no delimiters
    [Arguments("{1 2 3 4 5 6}")] // Wrong delimiters
    [Arguments("[ 1 2 3 4 5 6")] // Unexpected end of array
    [Arguments("[ 1, 2, 3, 4, 5, 6 ]")] // Unexpected comma
    [Arguments("[ `AABB` 1 ]")] // Inconsistent element encoding
    [Arguments("[ 1.0i64 1z ]")] // Inconsistent element encoding
    [Arguments("[ -5 ]")] // Ambiguous element encoding
    [Arguments("[ Hi! ]")] // Ambiguous element encoding
    public async Task ParseArrayFail(string text)
    {
        await Assert.That(delegate
        {
            var parser = BshoxTextParser.Create(text);
            _ = parser.ParseNextArray();
        }).Throws<BshoxParserException>();
    }

    [Test]
    public async Task ParseObject2()
    {
        const string text = """
                            {
                                1: 1.0i32
                                2: 2
                                3: `AABBCC`
                                4: "Hello, World!"
                                # This is a comment
                                5: null
                                6: true
                                7: false
                                8: inf32
                                9: -inf32
                                10: nan32
                                11: inf64
                                12: -inf64
                                13: nan64
                                14: {}
                                15: {1: {1: {1: 2}}} # nested objects
                            }
                            """;
        var actual = await GetValue<BshoxObject>(text);
        await Assert.That(actual.Select(kv => kv.Key)).IsEquivalentTo(new uint[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15 });
        await Assert.That(actual[1]).IsTypeOf<Fixed4>();
    }

    private static async Task<T> GetValue<T>(string text) where T : BshoxValue
    {
        var parser = BshoxTextParser.Create(text);
        await Assert.That(parser.IsEmpty).IsEqualTo(string.IsNullOrWhiteSpace(text));
        var obj = parser.ParseNextValue();
        await Assert.That(parser.IsEmpty).IsTrue();
        await Assert.That(obj).IsNotNull();
        await Assert.That(obj).IsTypeOf<T>();
        var actual = (T)obj;
        return actual;
    }

    [Test]
    [Arguments("")] // no delimiters
    [Arguments("\"`")] // bad delimiters
    [Arguments("`\"")] // bad delimiters
    [Arguments("''")] // bad delimiters
    [Arguments("´´")] // bad delimiters

    [Arguments("`A`")] // uneven count
    [Arguments("`ABC`")] // uneven count

    [Arguments(@"""\""")] // bad escape sequence
    [Arguments(@"""\e""")] // bad escape sequence
    [Arguments(@"""\0""")] // bad escape sequence

    [Arguments(@"""Hi \x""")] // hex escape sequence too short
    [Arguments(@"""Hi \xA""")] // hex escape sequence too short
    [Arguments(@"""Hi \xAG""")] // hex escape sequence invalid
    [Arguments(@"""Hi \x☺☺""")] // hex escape sequence invalid
    public async Task ParseBlobFail(string text)
    {
        await Assert.That(delegate
        {
            _ = BshoxTextParser.ParseBlob(GetToken(text));
        }).Throws<BshoxParserException>().WithMessage($"Cannot parse '{text}' as a Prefixed value.");
    }

    [Test]
    [MethodDataSource(typeof(ExampleData), nameof(ExampleData.ByteArrays))]
    public async Task BlobRoundTrip(byte[] value)
    {
        string text = DefaultContracts.ByteArray.ToBshoxString(value);
        var actual = BshoxTextParser.ParseBlob(GetToken(text));
        await Assert.That(actual).IsEquivalentTo(value);
    }

    [Test]
    [Arguments("123456", BshoxCode.VarInt)] // no suffix
    [Arguments("123456z", BshoxCode.VarInt)] // zigzag suffix
    [Arguments("0x123456", BshoxCode.VarInt)] // hex without suffix
    [Arguments("18446744073709551615", BshoxCode.VarInt)] // ulong.MaxValue
    [Arguments("123456i32", BshoxCode.Fixed4)] // i32 suffix
    [Arguments("123456i64", BshoxCode.Fixed8)] // i64 suffix
    [Arguments("123.0", BshoxCode.Fixed8)] // floats are assumed to be fixed8 unless they have a suffix
    [Arguments("123.4567890123456789", BshoxCode.Fixed8)]
    [Arguments("`AABBCC`", BshoxCode.Prefixed)] // hex literal
    [Arguments("\"AABBCC\"", BshoxCode.Prefixed)] // uft8 literal

    [Arguments("null", BshoxCode.Null)] // literal
    [Arguments("true", BshoxCode.VarInt)] // literal
    [Arguments("false", BshoxCode.VarInt)] // literal
    [Arguments("inf32", BshoxCode.Fixed4)] // literal
    [Arguments("-inf32", BshoxCode.Fixed4)] // literal
    [Arguments("nan32", BshoxCode.Fixed4)] // literal
    [Arguments("inf64", BshoxCode.Fixed8)] // literal
    [Arguments("-inf64", BshoxCode.Fixed8)] // literal
    [Arguments("nan64", BshoxCode.Fixed8)] // literal

    [Arguments("{", BshoxCode.SubObject)]
    [Arguments("[", BshoxCode.Array)]
    public async Task GuessEncoding(string text, BshoxCode value)
    {
        await Assert.That(BshoxTextParser.GuessEncoding(GetToken(text))).IsEqualTo(value);
    }

    [Test]
    [Arguments("Hello, World!")] // text without delimiters
    [Arguments(" 123456")] // leading whitespace
    [Arguments("123456 ")] // trailing whitespace
    [Arguments("-5")] // Ambiguous number
    [Arguments("")] // empty string
    [Arguments("{}")] // this token must be a single character
    [Arguments("[]")] // this token must be a single character
    [Arguments("}")] // invalid leading character
    [Arguments("]")] // invalid leading character
    [Arguments(" ")] // whitespace
    public async Task GuessEncodingFail(string text)
    {
        await Assert.That(delegate
        {
            _ = BshoxTextParser.GuessEncoding(GetToken(text));
        }).Throws<BshoxParserException>().WithMessage($"Cannot determine the encoding of '{text}'.");
    }

    #region VarInt

    [Test]
    [Arguments("9999999999999999999", 9999999999999999999ul)]
    [Arguments("1234", 1234ul)]
    [Arguments("0", 0ul)]
    [Arguments("true", 1ul)]
    [Arguments("false", 0ul)]
    [Arguments("-2z", 3ul)]
    [Arguments("2z", 4ul)]
    [Arguments("0xFFFFFFFF", uint.MaxValue)]
    [Arguments("0xFFFFFFFFFFFFFFFF", ulong.MaxValue)]
    public async Task ParseAsVarInt(string text, ulong value)
    {
        await Assert.That(BshoxTextParser.ParseVarInt(GetToken(text))).IsEqualTo(value);
    }

    [Test]
    [MethodDataSource(typeof(ExampleData), nameof(ExampleData.ULongs))]
    public async Task VarIntRoundtrip(ulong value)
    {
        string text = DefaultContracts.UInt64.ToBshoxString(value);
        ulong varint = BshoxTextParser.ParseVarInt(GetToken(text));
        await Assert.That(varint).IsEqualTo(value);
    }

    [Test]
    [Arguments("Hello, World!")] // not a numeric value
    [Arguments("1234i64")] // wrong suffix
    [Arguments("1234i32")] // wrong suffix
    [Arguments("FF")] // hex number without prefix
    [Arguments("-0xFF")] // negative hex number
    [Arguments("0xF.F")] // hex float
    [Arguments("inf64")] // invalid literal
    [Arguments("")] // empty string
    [Arguments("99999999999999999999")] // too large
    [Arguments("-2")] // negative number without zigzag suffix
    public async Task ParseBadVarInt(string text)
    {
        await Assert.That(delegate
        {
            _ = BshoxTextParser.ParseVarInt(GetToken(text));
        }).Throws<BshoxParserException>().WithMessage($"Cannot parse '{text}' as a VarInt value.");
    }

    #endregion VarInt

    #region Fixed4

    [Test]
    [Arguments("1234", 1234u)]
    [Arguments("1234i32", 1234u)]
    [Arguments("nan", 0xFFC00000u)]
    [Arguments("nan32", 0xFFC00000u)]
    [Arguments("0", 0u)]
    [Arguments("0i32", 0u)]
    [Arguments("-1234", unchecked((uint)-1234))]
    [Arguments("-1234i32", unchecked((uint)-1234))]
    [Arguments("0xFFFFFFFF", uint.MaxValue)]
    [Arguments("0xFFFFFFFFi32", uint.MaxValue)]
    public async Task ParseAsFixed4(string text, uint value)
    {
        float f = BshoxTextParser.ParseFixed4(GetToken(text));
        uint actual = Unsafe.As<float, uint>(ref f);
        await Assert.That(actual).IsEqualTo(value);
    }

    [Test]
    [Arguments("1.0", 1f)]
    [Arguments("1.0i32", 1f)]
    [Arguments("0.682287216i32", 0.682287216f)]
    [Arguments("0xFFC00000i32", float.NaN)]
    [Arguments("inf32", float.PositiveInfinity)]
    [Arguments("inf", float.PositiveInfinity)]
    [Arguments("-inf32", float.NegativeInfinity)]
    [Arguments("-inf", float.NegativeInfinity)]
    //[Arguments("0xf.f", 15.9375f)] // hex floats are not supported in .NET
    public async Task ParseAsFixed4(string text, float value)
    {
        await Assert.That(BshoxTextParser.ParseFixed4(GetToken(text))).IsEqualTo(value);
    }

    [Test]
    [MethodDataSource(typeof(ExampleData), nameof(ExampleData.Floats))]
    public async Task Fixed4FloatRoundtrip(float value)
    {
        string text = DefaultContracts.Single.ToBshoxString(value);
        await Assert.That(BshoxTextParser.ParseFixed4(GetToken(text))).IsEqualTo(value);
    }

    [Test]
    [Arguments("Hello, World!")] // not a numeric value
    [Arguments("1234i64")] // wrong suffix
    [Arguments("FF")] // hex number without prefix
    [Arguments("-0xFF")] // negative hex number
    [Arguments("0xF.F")] // hex float
    [Arguments("inf64")] // invalid literal
    [Arguments("")] // empty string
    [Arguments("999999999999")] // too large
    public async Task ParseBadFixed4(string text)
    {
        await Assert.That(delegate
        {
            _ = BshoxTextParser.ParseFixed4(GetToken(text));
        }).Throws<BshoxParserException>().WithMessage($"Cannot parse '{text}' as a Fixed4 value.");
    }

    #endregion Fixed4

    #region Fixed8

    [Test]
    [Arguments("1234", 1234ul)]
    [Arguments("1234i64", 1234ul)]
    [Arguments("nan", 0xFFF8000000000000u)]
    [Arguments("nan64", 0xFFF8000000000000u)]
    [Arguments("0", 0ul)]
    [Arguments("0i64", 0ul)]
    [Arguments("-1234", unchecked((ulong)-1234))]
    [Arguments("-1234i64", unchecked((ulong)-1234))]
    [Arguments("0xFFFFFFFFFFFFFFFF", ulong.MaxValue)]
    [Arguments("0xFFFFFFFFFFFFFFFFi64", ulong.MaxValue)]
    public async Task ParseAsFixed8(string text, ulong value)
    {
        double d = BshoxTextParser.ParseFixed8(GetToken(text));
        ulong actual = Unsafe.As<double, ulong>(ref d);
        await Assert.That(actual).IsEqualTo(value);
    }

    [Test]
    [Arguments("1.0", 1d)]
    [Arguments("1.0i64", 1d)]
    [Arguments("0.682287216i64", 0.682287216d)]
    [Arguments("0xFFF8000000000000", double.NaN)]
    [Arguments("0xFFF8000000000000i64", double.NaN)]
    [Arguments("inf64", double.PositiveInfinity)]
    [Arguments("inf", double.PositiveInfinity)]
    [Arguments("-inf64", double.NegativeInfinity)]
    [Arguments("-inf", double.NegativeInfinity)]
    //[Arguments("0xf.f", 15.9375d)] // hex floats are not supported in .NET
    public async Task ParseAsFixed8(string text, double value)
    {
        await Assert.That(BshoxTextParser.ParseFixed8(GetToken(text))).IsEqualTo(value);
    }

    [Test]
    [MethodDataSource(typeof(ExampleData), nameof(ExampleData.Doubles))]
    public async Task Fixed8FloatRoundtrip(double value)
    {
        string text = DefaultContracts.Double.ToBshoxString(value);
        await Assert.That(BshoxTextParser.ParseFixed8(GetToken(text))).IsEqualTo(value);
    }

    [Test]
    [Arguments("Hello, World!")] // not a numeric value
    [Arguments("1234i32")] // wrong suffix
    [Arguments("FF")] // hex number without prefix
    [Arguments("-0xFF")] // negative hex number
    [Arguments("inf32")] // invalid literal
    [Arguments("")] // empty string
    public async Task ParseBadFixed8(string text)
    {
        await Assert.That(delegate
        {
            return BshoxTextParser.ParseFixed8(GetToken(text));
        }).Throws<BshoxParserException>().WithMessage($"Cannot parse '{text}' as a Fixed8 value.");
    }

    #endregion Fixed8
}
