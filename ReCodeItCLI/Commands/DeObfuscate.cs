using CliFx;
using CliFx.Attributes;
using CliFx.Infrastructure;
using ReCodeIt.Utils;
using ReCodeItLib.Remapper;

namespace ReCodeIt.Commands;

[Command("DeObfuscate", Description = "Generates a de-obfuscated -cleaned dll in the folder your assembly is in")]
public class DeObfuscate : ICommand
{
    [CommandParameter(0, IsRequired = true, Description = "The absolute path to your obfuscated assembly file, folder must contain all references to be resolved.")]
    public string AssemblyPath { get; init; }

    public ValueTask ExecuteAsync(IConsole console)
    {
        DataProvider.LoadAppSettings();
        DataProvider.IsCli = true;

        Logger.Log("Deobfuscating assembly...");

        Deobfuscator.Deobfuscate(AssemblyPath);

        Logger.Log("Complete", ConsoleColor.Green);

        return default;
    }
}