using CliFx;
using CliFx.Attributes;
using CliFx.Infrastructure;
using ReCodeIt.CrossCompiler;
using ReCodeIt.Utils;
using ReCodeItLib.Utils;

namespace ReCodeIt.Commands;

[Command("Build", Description = "Build your project and get a dll output for the original assembly. You dont need to provide a path if the last project you built is the one you want to target")]
public class BuildCommand : ICommand
{
    private ReCodeItCrossCompiler CrossCompiler { get; set; }

    [CommandParameter(0, IsRequired = false, Description = "the location of your project file")]
    public string ProjectJsonPath { get; init; }

    public ValueTask ExecuteAsync(IConsole console)
    {
        if (ProjectJsonPath is not null && ProjectJsonPath != string.Empty)
        {
            CrossCompiler = new();
            ProjectManager.LoadProject(ProjectJsonPath);
            CrossCompiler.StartCrossCompile();

            return default;
        }

        console.Output.WriteLine(RegistryHelper.GetRegistryValue<string>("LastLoadedProject"));

        if (RegistryHelper.GetRegistryValue<string>("LastLoadedProject") != null)
        {
            CrossCompiler = new();

            DataProvider.LoadAppSettings();

            ProjectManager.LoadProject(RegistryHelper.GetRegistryValue<string>("LastLoadedProject"), true);
            CrossCompiler.StartCrossCompile();

            DataProvider.SaveAppSettings();
            return default;
        }

        return default;
    }
}