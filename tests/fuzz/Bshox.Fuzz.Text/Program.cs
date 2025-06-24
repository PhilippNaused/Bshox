using Bshox.Meta;

SharpFuzz.Fuzzer.OutOfProcess.Run(text =>
{
    try
    {
        _ = BshoxTextParser.Parse(text);
    }
    catch (BshoxParserException) { }
});
