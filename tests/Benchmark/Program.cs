using BenchmarkDotNet.Running;

#if DEBUG
Console.Error.WriteLine("You are in debug mode!");
#endif

var gitDir = Path.Combine(Environment.CurrentDirectory, ".git");

if (!Directory.Exists(gitDir))
{
    Console.Error.WriteLine($"Something is wrong with the working directory.\nCannot find ${gitDir}.");
    return -1;
}

BenchmarkSwitcher.FromAssembly(typeof(Program).Assembly).Run(args);
return 0;
