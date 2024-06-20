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

            return ValueTask.CompletedTask;
        }

        console.Output.WriteLine(RegistryHelper.GetRegistryValue<string>("LastLoadedProject"));

        if (RegistryHelper.GetRegistryValue<string>("LastLoadedProject") != null)
        {
            CrossCompiler = new();

            DataProvider.LoadAppSettings();
            DataProvider.IsCli = true;

            ProjectManager.LoadProject(RegistryHelper.GetRegistryValue<string>("LastLoadedProject"), true);

            if (!Validate(console)) { return ValueTask.CompletedTask; }

            CrossCompiler.StartCrossCompile();

            DataProvider.SaveAppSettings();
            return ValueTask.CompletedTask;
        }

        return ValueTask.CompletedTask;
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