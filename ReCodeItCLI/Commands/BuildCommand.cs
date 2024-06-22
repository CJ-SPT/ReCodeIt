using CliFx;
using CliFx.Attributes;
using CliFx.Infrastructure;
using ReCodeIt.CrossCompiler;
using ReCodeIt.Utils;
using ReCodeItLib.Utils;

namespace ReCodeIt.Commands;

[Command("Build", Description = "(Compile Time Reflection) Build your project and get a dll output for the original assembly.")]
public class BuildCommand : ICommand
{
    private ReCodeItCrossCompiler CrossCompiler { get; set; }

    [CommandParameter(0, IsRequired = false, Description = "the location of your project file (ReCodeItProj.json). You don't need to provide a path if the last project you built is the one you want to target, or you are running this command from inside a directory where a project file exists.")]
    public string ProjectJsonPath { get; init; }

    public async ValueTask ExecuteAsync(IConsole console)
    {
        var isLocal = await UseLocalProject(console);

        if (isLocal) { return; }

        var isRemote = await UseRemoteProject(console);

        if (isRemote) { return; }

        await UseLastLoadedProject(console);
    }

    private async Task<bool> UseLocalProject(IConsole console)
    {
        var jsonPath = Path.Combine(Directory.GetCurrentDirectory(), "ReCodeItProj.json");

        if (File.Exists(jsonPath))
        {
            Logger.Log("Found a project file in the current directory, loading it", ConsoleColor.Yellow);

            CrossCompiler = new();

            DataProvider.LoadAppSettings();
            DataProvider.IsCli = true;

            ProjectManager.LoadProject(jsonPath);
            await CrossCompiler.StartCrossCompile();

            return true;
        }

        return false;
    }

    private async Task<bool> UseRemoteProject(IConsole console)
    {
        if (ProjectJsonPath is not null && ProjectJsonPath != string.Empty)
        {
            if (!File.Exists(ProjectJsonPath))
            {
                console.Output.WriteLine("The project file you provided does not exist");
                return false;
            }

            CrossCompiler = new();

            DataProvider.LoadAppSettings();
            DataProvider.IsCli = true;

            ProjectManager.LoadProject(ProjectJsonPath);
            await CrossCompiler.StartCrossCompile();

            return true;
        }

        return false;
    }

    private async Task<bool> UseLastLoadedProject(IConsole console)
    {
        if (RegistryHelper.GetRegistryValue<string>("LastLoadedProject") != null)
        {
            string currentDirectory = Directory.GetCurrentDirectory();

            console.Output.WriteLine($"Project: {RegistryHelper.GetRegistryValue<string>("LastLoadedProject")}");
            console.Output.WriteLine($"Working Dir: {currentDirectory}");

            CrossCompiler = new();

            DataProvider.LoadAppSettings();
            DataProvider.IsCli = true;

            ProjectManager.LoadProject(RegistryHelper.GetRegistryValue<string>("LastLoadedProject"), true);

            if (!Validate(console)) { return false; }

            await CrossCompiler.StartCrossCompile();

            DataProvider.SaveAppSettings();

            return true;
        }

        return false;
    }

    private bool Validate(IConsole console)
    {
        if (ProjectManager.ActiveProject == null)
        {
            console.Output.WriteLine("No project loaded, go create or load a project in the gui first");
            return false;
        }

        if (ProjectManager.ActiveProject.RemapModels.Count == 0)
        {
            console.Output.WriteLine("No Remaps present, go to the gui and create some first");
            return false;
        }

        if (ProjectManager.ActiveProject.ChangedTypes.Count == 0)
        {
            console.Output.WriteLine("There are no changed types, have you created and used a remapped reference? \nRun the command `buildRef` to generate a project reference");
            return false;
        }

        return true;
    }
}