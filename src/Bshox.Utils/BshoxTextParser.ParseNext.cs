using System.Diagnostics;

namespace Bshox.Utils;

public partial class BshoxTextParser
{
    internal BshoxObject ParseNextObject()
    {
        if (_tokens.Count == 0)
            throw BshoxException.EndOfInput();
        var start = _tokens.Dequeue();
        if (start != Constants.StartObject)
            throw new BshoxParserException(start, $"Expected '{Constants.StartObject}' but got '{start}'.");
        BshoxObject obj = [];
        while (_tokens.Count > 0)
        {
            Token tag = _tokens.Dequeue();
            if (tag == Constants.EndObject) // end of object
                return obj;

            uint key = ParseTag(tag);
            BshoxValue value = ParseNextValue();
            obj.Add(key, value);
        }
        throw BshoxException.EndOfInput();
    }

    private static uint ParseTag(Token tag)
    {
        // tags must be numbers with a ':' at the end
        if (!tag.EndsWith(Constants.TagDelimiter))
            throw new BshoxParserException(tag, $"Expected a tag, but got '{tag}'.");
        tag = tag.SubToken(0, tag.Length - 1); // remove the delimiter

        uint key;
        try
        {
            if (tag.StartsWith(Constants.HexPrefix)) // hex tags are also allowed
            {
                key = tag.SubToken(2).ParseHexUInt();
            }
            else // decimal tags
            {
                key = tag.ParseUInt();
            }
        }
        catch (FormatException e)
        {
            throw BshoxException.CannotParse(tag, BshoxCode.VarInt, e);
        }
        catch (OverflowException e)
        {
            throw BshoxException.CannotParse(tag, BshoxCode.VarInt, e);
        }
        return key;
    }

    internal BshoxArray ParseNextArray()
    {
        if (_tokens.Count == 0)
            throw BshoxException.EndOfInput();
        var start = _tokens.Dequeue();
        if (start != Constants.StartArray)
            throw new BshoxParserException(start, $"Expected '{Constants.StartArray}' but got '{start}'.");
        BshoxArray array = [];
        var encoding = BshoxCode.Null;
        while (_tokens.Count > 0)
        {
            if (_tokens.Peek() == Constants.EndArray) // end of array
            {
                _ = _tokens.Dequeue();
                return array;
            }

            if (encoding is BshoxCode.Null)
                encoding = GuessNextEncoding();

            var token = _tokens.Peek();
            var value = ParseNextValue(encoding);
            if (value.Encoding is BshoxCode.Null)
                throw new BshoxParserException(token, "Null values are not allowed in arrays");
            array.Add(value);
            Debug.Assert(value.Encoding == array.ElementEncoding, "value.Encoding == array.ElementEncoding");
        }
        throw BshoxException.EndOfInput();
    }

    private int depth;

    internal BshoxValue ParseNextValue(BshoxCode? encoding = null)
    {
        if (_tokens.Count == 0)
            throw BshoxException.EndOfInput();
        depth++;
        if (depth >= BshoxOptions.DefaultMaxDepth) // TODO: make configurable
            throw new BshoxParserException(_tokens.Peek(), $"Maximum depth of {depth} reached.");

        try
        {
            encoding ??= GuessNextEncoding();
            switch (encoding)
            {
                case BshoxCode.Null:
                    _ = _tokens.Dequeue();
                    return BshoxValue.Null;
                case BshoxCode.VarInt:
                    return new VarInt(ParseVarInt(_tokens.Dequeue()));
                case BshoxCode.Fixed4:
                    return new Fixed4(ParseFixed4(_tokens.Dequeue()));
                case BshoxCode.Fixed8:
                    return new Fixed8(ParseFixed8(_tokens.Dequeue()));
                case BshoxCode.Prefixed:
                    return new BshoxBlob(ParseBlob(_tokens.Dequeue()));
                case BshoxCode.Array:
                    return ParseNextArray();
                case BshoxCode.SubObject:
                    return ParseNextObject();
                default:
                    throw new NotSupportedException($"Unsupported encoding: {encoding}");
            }
        }
        finally
        {
            depth--;
        }
    }

    private BshoxCode GuessNextEncoding()
    {
        return GuessEncoding(_tokens.Peek());
    }
}
