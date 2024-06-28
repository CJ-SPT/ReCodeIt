using CliFx;
using CliFx.Attributes;
using CliFx.Infrastructure;
using ReCodeIt.ReMapper;
using ReCodeIt.Utils;

namespace ReCodeIt.Commands;

[Command("ReMap", Description = "Generates a re-mapped dll provided a mapping file and de-obfuscated dll")]
public class ReMap : ICommand
{
    private ReCodeItRemapper _remapper { get; set; } = new();

    [CommandParameter(0, IsRequired = true, Description = "The absolute path to your mapping.json file, supports .json and .jsonc")]
    public string MappingJsonPath { get; init; }

    [CommandParameter(1, IsRequired = true, Description = "The absolute path to your de-obfuscated dll, containing all references that it needs to resolve.")]
    public string AssemblyPath { get; init; }

    [CommandParameter(2, IsRequired = true, Description = "If true, the re-mapper will publicize all types, methods, and properties")]
    public bool Publicize { get; init; }

    [CommandParameter(3, IsRequired = false, Description = "If true, the re-mapper will rename all changed types associated variable names to be the same as the declaring type")]
    public bool? ReName { get; init; }

    public ValueTask ExecuteAsync(IConsole console)
    {
        DataProvider.IsCli = true;
        DataProvider.LoadAppSettings();

        var remapperSettings = DataProvider.Settings.Remapper.MappingSettings;

        remapperSettings.RenameFields = ReName ?? false;
        remapperSettings.RenameProperties = ReName ?? false;
        remapperSettings.Publicize = Publicize;

        var remaps = DataProvider.LoadMappingFile(MappingJsonPath);
        
        _remapper.InitializeRemap(remaps, AssemblyPath, Path.GetDirectoryName(AssemblyPath));

        // Wait for log termination
        Logger.Terminate();
        while(Logger.IsRunning()) {}
        
        return default;
    }
}