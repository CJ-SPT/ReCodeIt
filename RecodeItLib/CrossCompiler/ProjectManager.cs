using Microsoft.CodeAnalysis;
using Microsoft.Win32;
using Newtonsoft.Json;
using ReCodeIt.Models;
using ReCodeIt.Utils;
using ReCodeItLib.Utils;

namespace ReCodeIt.CrossCompiler;

public static class ProjectManager
{
    public static CrossCompilerProjectModel ActiveProject { get; private set; }
    private static CrossCompilerSettings Settings => DataProvider.Settings.CrossCompiler;

    public static List<string> AllProjectSourceFiles { get; private set; } = [];

    public static void CreateProject(
        string OrigAssemblyPath,
        string VSSolutionDirPath,
        string DependencyPath,
        string BuildPath)
    {
        Logger.Log("-----------------------------------------------", ConsoleColor.Yellow);
        Logger.Log($"Generating Cross Compiler project", ConsoleColor.Yellow);
        Logger.Log($"Original Assembly Path {OrigAssemblyPath}", ConsoleColor.Yellow);
        Logger.Log($"Remapped Assembly Path: {DependencyPath}", ConsoleColor.Yellow);
        Logger.Log($"Visual Studio Solution Directory: {VSSolutionDirPath}", ConsoleColor.Yellow);
        Logger.Log($"Build Path: {BuildPath}", ConsoleColor.Yellow);

        // Build the project model
        ActiveProject = new CrossCompilerProjectModel
        {
            OriginalAssemblyPath = OrigAssemblyPath,
            VisualStudioSolutionPath = VSSolutionDirPath,
            VisualStudioDependencyPath = DependencyPath,
            BuildDirectory = BuildPath,
            OriginalAssemblyHash = HashUtil.GetFileHash(OrigAssemblyPath),
            RemappedAssemblyHash = "",
            ChangedTypes = [],
            RemapModels = []
        };

        // Now save the project json inside the original solution directory
        SaveCrossCompilerProjectModel(ActiveProject);

        Logger.Log($"Found Solution: {ActiveProject.SolutionName}", ConsoleColor.Yellow);
        Logger.Log($"Original Assembly Checksum: {ActiveProject.OriginalAssemblyHash}", ConsoleColor.Yellow);
        Logger.Log($"Project Generated to: {DataProvider.Settings.CrossCompiler.LastLoadedProject}", ConsoleColor.Green);
        Logger.Log("-----------------------------------------------", ConsoleColor.Yellow);
    }

    /// <summary>
    /// Saves the provided project model to disk, used from the GUI
    /// </summary>
    /// <param name="model"></param>
    public static void SaveCrossCompilerProjectModel(CrossCompilerProjectModel model)
    {
        var path = Path.Combine(model.VisualStudioSolutionDirectoryPath, "ReCodeItProj.json");

        JsonSerializerSettings settings = new()
        {
            Formatting = Formatting.Indented
        };

        var jsonText = JsonConvert.SerializeObject(model, settings);

        File.WriteAllText(path, jsonText);

        DataProvider.Settings.CrossCompiler.LastLoadedProject = path;

        RegistryHelper.SetRegistryValue("LastLoadedProject", path, RegistryValueKind.String);
        DataProvider.SaveAppSettings();

        Logger.Log($"Cross Compiler project json saved to {path}", ConsoleColor.Green);
    }

    /// <summary>
    /// The "LoadProject" method only loads the project file from disk, used for initiating the GUI
    /// </summary>
    /// <param name="path"></param>
    /// <param name="cli"></param>
    public static void LoadProject(string path, bool cli = false)
    {
        ActiveProject = LoadCrossCompilerProjModel(path, cli);
        Logger.Log($"Found and Loaded ReCodeIt Project at {path}");
    }

    /// <summary>
    /// Loads the project model from disk
    /// </summary>
    /// <param name="path"></param>
    /// <param name="cli"></param>
    /// <returns></returns>
    private static CrossCompilerProjectModel LoadCrossCompilerProjModel(string path, bool cli = false)
    {
        if (!File.Exists(path))
        {
            Logger.Log($"Error loading cache model from `{path}`", ConsoleColor.Red);
        }

        var jsonText = File.ReadAllText(path);

        var model = JsonConvert.DeserializeObject<CrossCompilerProjectModel>(jsonText);

        if (!cli)
        {
            DataProvider.Settings.CrossCompiler.LastLoadedProject = path;
        }

        RegistryHelper.SetRegistryValue("LastLoadedProject", path, RegistryValueKind.String);
        DataProvider.SaveAppSettings();

        Logger.Log($"Loaded Cross Compiler Project: {model?.VisualStudioSolutionDirectoryPath}");

        return model!;
    }

    /// <summary>
    /// Gathers all the projects source files
    /// </summary>
    private static void LoadProjectSourceFiles()
    {
        var path = Path.Combine(
            DataProvider.ReCodeItProjectsPath,
            ActiveProject.SolutionName);

        // Find all the source files in the project, we dont want anything from the obj folder.
        AllProjectSourceFiles = Directory.GetFiles(path, "*.cs", SearchOption.AllDirectories)
            .Where(file => !file.Contains(Path.DirectorySeparatorChar + "obj" + Path.DirectorySeparatorChar))
            .ToList();

        Logger.Log($"Found {AllProjectSourceFiles.Count} source files in the project", ConsoleColor.Yellow);
    }
}