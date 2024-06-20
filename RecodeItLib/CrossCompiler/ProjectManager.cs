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
        string RemappedAssemblyOutputPath,
        string VSSolutionDirPath,
        string DependencyPath,
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
            VisualStudioDependencyPath = DependencyPath,
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
        Logger.Log($"Project Generated to: {DataProvider.Settings.CrossCompiler.LastLoadedProject}", ConsoleColor.Green);
        Logger.Log("-----------------------------------------------", ConsoleColor.Yellow);
    }

    public static void LoadProject(string path, bool cli = false)
    {
        ActiveProject = LoadCrossCompilerProjModel(path, cli);

        CopyVisualStudioProject(ActiveProject);

        LoadProjectSourceFiles();
        Logger.Log($"Found and Loaded ReCodeIt Project at {path}");
    }

    public static void MoveOriginalReference()
    {
        var outPath = Path.Combine(
            ActiveProject.VisualStudioClonedSolutionDirectory,
            ActiveProject.OriginalAssemblyDllName);

        Logger.Log(ActiveProject.VisualStudioClonedDependencyPath, ConsoleColor.Red);

        Logger.Log($"Placing original reference `{ActiveProject.OriginalAssemblyPath}` into cloned build directory `{outPath}`", ConsoleColor.Green);
        File.Copy(ActiveProject.OriginalAssemblyPath, outPath, true);
    }

    public static void CopyVisualStudioProject(CrossCompilerProjectModel proj)
    {
        var solutionDirPath = proj.VisualStudioSolutionDirectoryPath;
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

            if (File.Exists(tempPath)) { File.Delete(tempPath); }

            file.CopyTo(tempPath, true);
        }

        // We dont want git and vs directories they are often locked leading to problems
        List<string> copyIgnoreDirectories =
        [
            ".vs",
            ".git"
        ];

        foreach (DirectoryInfo subdir in dirs)
        {
            if (copyIgnoreDirectories.Contains(subdir.Name)) { continue; }

            string tempPath = Path.Combine(destinationDirPath, subdir.Name);
            CopyProjectRecursive(subdir.FullName, tempPath);
        }
    }

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