using Bshox;
using Bshox.Utils;

SharpFuzz.Fuzzer.OutOfProcess.Run(stream =>
{
    var ms = new MemoryStream();
    stream.CopyTo(ms);
    ms.Position = 0;
    var array = ms.ToArray();
#if NETCOREAPP
    foreach (BshoxCode code in Enum.GetValues<BshoxCode>())
#else
    foreach (BshoxCode code in Enum.GetValues(typeof(BshoxCode)))
#endif
    {
        var reader = new BshoxReader(array);
        try
        {
            _ = BshoxValue.Read(ref reader, code);
        }
        catch (BshoxException) { }
    }
});
