using CliFx;
using CliFx.Attributes;
using CliFx.Infrastructure;
using ReCodeIt.CrossCompiler;
using ReCodeIt.Utils;

namespace ReCodeIt.Commands;

[Command("BuildRef", Description = "(Compile Time Reflection) Builds or rebuilds a new reference DLL for your project")]
public class BuildRef : ICommand
{
    private ReCodeItCrossCompiler CrossCompiler { get; set; }

    public ValueTask ExecuteAsync(IConsole console)
    {
        if (DataProvider.Settings.CrossCompiler.LastLoadedProject != null)
        {
            CrossCompiler = new();

            DataProvider.LoadAppSettings();
            DataProvider.IsCli = true;

            ProjectManager.LoadProject(DataProvider.Settings.CrossCompiler.LastLoadedProject);

            if (!Validate(console)) { return default; }

            CrossCompiler.StartRemap();

            DataProvider.SaveAppSettings();
        }

        // Wait for log termination
        Logger.Terminate();
        while(Logger.IsRunning()) {}
        
        return default;
    }

    private bool Validate(IConsole console)
    {
        if (ProjectManager.ActiveProject == null)
        {
            console.Output.WriteLine("No project loaded, please load a project first");
            return false;
        }

        if (ProjectManager.ActiveProject.RemapModels.Count == 0)
        {
            console.Output.WriteLine("No Remaps present, go to the gui and create some first");
            return false;
        }

        return true;
    }
}