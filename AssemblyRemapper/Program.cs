using AssemblyRemapper.Commands;

namespace AssemblyRemapper;

public static class Program
{
    public static void Main(string[] args)
    {
        var cmd = new CommandProcessor();

        cmd.CommandLoop();
    }
}