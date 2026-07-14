using Bshox;
using Bshox.Utils;

SharpFuzz.Fuzzer.OutOfProcess.Run(stream =>
{
    var ms = new MemoryStream();
    stream.CopyTo(ms);
    ms.Position = 0;
    var array = ms.ToArray();
#if NETCOREAPP
    foreach (BshoxEncoding code in Enum.GetValues<BshoxEncoding>())
#else
    foreach (BshoxEncoding code in Enum.GetValues(typeof(BshoxEncoding)))
#endif
    {
        var reader = new BshoxReader(array);
        try
        {
            _ = BshoxValue.Read(ref reader, code);
        }
        catch (BshoxException) { }
        reader = new BshoxReader(array);
        try
        {
            reader.SkipValue(code);
        }
        catch (BshoxException) { }
    }
});
