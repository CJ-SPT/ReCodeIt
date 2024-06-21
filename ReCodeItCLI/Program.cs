// See https://aka.ms/new-console-template for more information
using CliFx;

public static class Program
{
    public static async Task<int> Main() =>
        await new CliApplicationBuilder()
            .AddCommandsFromThisAssembly()
            .SetExecutableName("ReCodeIt")
            .Build()
            .RunAsync();
}