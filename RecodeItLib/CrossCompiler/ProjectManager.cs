using Newtonsoft.Json;
using ReCodeIt.Models;
using ReCodeIt.Utils;

namespace ReCodeIt.CrossCompiler;

public static class ProjectManager
{
    /// <summary>
    /// Key: Path of the project, Value: Project file
    /// </summary>
    public static Dictionary<string, CrossCompilerProjectModel> Projects { get; private set; } = [];

    public static CrossCompilerProjectModel ActiveProject { get; private set; }

    private static CrossCompilerSettings Settings => DataProvider.Settings.CrossCompiler;

    private static HashSet<string> CopyIgnoreDirectories { get; } =
    [
        ".vs",
        ".git"
    ];

    public static void CreateProject(
        string OrigAssemblyPath,
        string RemappedAssemblyOutputPath,
        string VSSolutionDirPath,
        string BuildPath)
    {
        Logger.Log("-----------------------------------------------", ConsoleColor.Yellow);
        Logger.Log($"Generating Cross Compiler project", ConsoleColor.Yellow);
        Logger.Log($"Original Assembly Path {OrigAssemblyPath}", ConsoleColor.Yellow);
        Logger.Log($"Remapped Assembly Path: {RemappedAssemblyOutputPath}", ConsoleColor.Yellow);
        Logger.Log($"Visual Studio Solution Directory: {VSSolutionDirPath}", ConsoleColor.Yellow);
        Logger.Log($"Build Path: {BuildPath}", ConsoleColor.Yellow);

        // Build the project model
        ActiveProject = new CrossCompilerProjectModel
        {
            OriginalAssemblyPath = OrigAssemblyPath,
            RemappedAssemblyPath = RemappedAssemblyOutputPath,
            VisualStudioSolutionPath = VSSolutionDirPath,
            BuildDirectory = BuildPath,
            OriginalAssemblyHash = HashUtil.GetFileHash(OrigAssemblyPath),
            RemappedAssemblyHash = "",
            ChangedTypes = [],
            RemapModels = []
        };

        // Now copy over the visual studio project
        CopyVisualStudioProject(ActiveProject);

        // Now save the project json inside the original solution directory
        SaveCrossCompilerProjectModel(ActiveProject);

        Logger.Log($"Found Solution: {ActiveProject.SolutionName}", ConsoleColor.Yellow);
        Logger.Log($"Original Assembly Checksum: {ActiveProject.OriginalAssemblyHash}", ConsoleColor.Yellow);
        Logger.Log($"Project Generated to: {DataProvider.Settings.CrossCompiler.LastActiveProjectPath}", ConsoleColor.Green);
        Logger.Log("-----------------------------------------------", ConsoleColor.Yellow);
    }

    public static void LoadProject(string path)
    {
        ActiveProject = LoadCrossCompilerProjModel(path);
        CopyVisualStudioProject(ActiveProject);
        Logger.Log($"Found and Loaded ReCodeIt Project at {path}");
    }

    private static void CopyVisualStudioProject(CrossCompilerProjectModel proj)
    {
        var solutionDirPath = proj.VisualStudioSolutionPath;
        var solutionFiles = Directory.GetFiles(solutionDirPath, "*.sln", SearchOption.AllDirectories);
        var solutionFile = string.Empty;

        if (solutionFiles.Length > 1)
        {
            Logger.Log("ERROR More than one solution in a directory is not supported, Why tho?", ConsoleColor.Red);
            return;
        }

        foreach (var file in solutionFiles)
        {
            solutionFile = file;
        }

        var solutionName = Path.GetFileNameWithoutExtension(solutionFile);
        var destination = Path.Combine(DataProvider.ReCodeItProjectsPath, solutionName);

        proj.ReCodeItProjectPath = destination;
        proj.SolutionName = solutionName;

        Logger.Log($"Copying solution: {solutionName} to {destination}", ConsoleColor.Yellow);

        CopyProjectRecursive(solutionDirPath, destination);
    }

    private static void CopyProjectRecursive(string sourceDirPath, string destinationDirPath)
    {
        DirectoryInfo sourceDir = new DirectoryInfo(sourceDirPath);

        if (!sourceDir.Exists)
        {
            throw new DirectoryNotFoundException(
                "Solution directory does not exist or could not be found: "
                + sourceDirPath);
        }

        DirectoryInfo[] dirs = sourceDir.GetDirectories();

        // If the destination directory doesn't exist, create it.
        Directory.CreateDirectory(destinationDirPath);

        // Get the files in the directory and copy them to the new location.
        FileInfo[] files = sourceDir.GetFiles();

        foreach (FileInfo file in files)
        {
            string tempPath = Path.Combine(destinationDirPath, file.Name);
            file.CopyTo(tempPath, true);
        }

        foreach (DirectoryInfo subdir in dirs)
        {
            if (CopyIgnoreDirectories.Contains(subdir.Name)) { continue; }

            string tempPath = Path.Combine(destinationDirPath, subdir.Name);
            CopyProjectRecursive(subdir.FullName, tempPath);
        }
    }

    private static void SaveCrossCompilerProjectModel(CrossCompilerProjectModel model)
    {
        var path = Path.Combine(model.VisualStudioSolutionPath, "ReCodeItProj.json");

        JsonSerializerSettings settings = new()
        {
            Formatting = Formatting.Indented
        };

        var jsonText = JsonConvert.SerializeObject(model, settings);

        File.WriteAllText(path, jsonText);

        DataProvider.Settings.CrossCompiler.LastActiveProjectPath = path;
        DataProvider.SaveAppSettings();

        Logger.Log($"Cross Compiler project json generated to {path}", ConsoleColor.Green);
    }

    private static CrossCompilerProjectModel LoadCrossCompilerProjModel(string path)
    {
        if (!File.Exists(path))
        {
            Logger.Log($"Error loading cache model from `{path}`", ConsoleColor.Red);
        }

        var jsonText = File.ReadAllText(path);

        var model = JsonConvert.DeserializeObject<CrossCompilerProjectModel>(jsonText);

        DataProvider.Settings.CrossCompiler.LastActiveProjectPath = path;
        DataProvider.SaveAppSettings();

        Logger.Log($"Loaded Cross Compiler Project: {model?.RemappedAssemblyPath}");

        return model!;
    }
}