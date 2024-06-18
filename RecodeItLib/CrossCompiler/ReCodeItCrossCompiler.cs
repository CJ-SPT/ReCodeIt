using ReCodeIt.Models;
using ReCodeIt.ReMapper;
using ReCodeIt.Utils;

namespace ReCodeIt.CrossPatcher;

public class ReCodeItCrossCompiler
{
    public ReCodeItCrossCompiler()
    {
        Remapper = new(this);
        FindProjects();
    }

    private ReCodeItRemapper Remapper { get; }
    private CrossCompilerSettings Settings => DataProvider.Settings.CrossCompiler;
    public List<string> ProjectPaths { get; private set; } = [];

    /// <summary>
    /// Key: Remapped name, value: old name
    /// </summary>
    public Dictionary<string, string> ChangedTypes { get; set; } = [];

    private CrossCompilerProjectModel Cache;

    private static HashSet<string> CopyIgnoreDirectories { get; } =
    [
        ".vs",
        ".git"
    ];

    public void CreateProject()
    {
        Logger.Log("-----------------------------------------------", ConsoleColor.Yellow);
        Logger.Log($"Generating Cross Compiler project", ConsoleColor.Yellow);
        Logger.Log($"Original Assembly Path {Settings.OriginalAssemblyPath}", ConsoleColor.Yellow);
        Logger.Log($"Remapped Assembly Path: {Settings.RemappedOutput}", ConsoleColor.Yellow);
        Logger.Log($"Visual Studio Solution Directory: {Settings.VisualStudioSolutionDirectory}", ConsoleColor.Yellow);

        // Build the cache model
        Cache = new CrossCompilerProjectModel
        {
            OriginalAssemblyPath = Settings.OriginalAssemblyPath,
            RemappedAssemblyPath = Settings.RemappedOutput,
            VisualStudioSolutionDirectory = Settings.VisualStudioSolutionDirectory,
            OriginalAssemblyHash = HashUtil.GetFileHash(Settings.OriginalAssemblyPath),
            RemappedAssemblyHash = "",
            ChangedTypes = [],
            RemapModels = []
        };

        // Now copy over the visual studio project
        CopyVisualStudioProject(Cache);

        // Now save the cache object inside the copied directory
        DataProvider.SaveCrossCompilerProjectModel(Cache);

        Logger.Log($"Found Solution: {Cache.SolutionPath}", ConsoleColor.Yellow);
        Logger.Log($"Original Assembly Checksum: {Cache.OriginalAssemblyHash}", ConsoleColor.Yellow);
        Logger.Log($"Project Generated to: {Cache.ReCodeItProjectDir}", ConsoleColor.Green);
        Logger.Log("-----------------------------------------------", ConsoleColor.Yellow);
    }

    public void StartRemap()
    {
        ChangedTypes.Clear();
        Remapper.InitializeRemap(Settings.OriginalAssemblyPath, Settings.RemappedOutput, true);

        if (Cache == null)
        {
            Logger.Log("ERROR: No Cross Compiler Project is loaded, create or load one first.", ConsoleColor.Red);
            return;
        }

        if (Cache.ReCodeItProjectDir == string.Empty)
        {
            Logger.Log("ERROR: No ReCodeIt Project directory is set. (Project Creation Failed)", ConsoleColor.Red);
            return;
        }

        Logger.Log("-----------------------------------------------", ConsoleColor.Yellow);
        Logger.Log($"Cross patch remap result", ConsoleColor.Yellow);
        Logger.Log($"Changed {ChangedTypes.Count} types", ConsoleColor.Yellow);
        Logger.Log($"Original assembly path: {Cache.OriginalAssemblyPath}", ConsoleColor.Yellow);
        Logger.Log($"Original assembly hash: {Cache.OriginalAssemblyHash}", ConsoleColor.Yellow);
        Logger.Log($"Original patched assembly path: {Cache.RemappedAssemblyPath}", ConsoleColor.Yellow);
        Logger.Log($"Original patched assembly hash: {Cache.RemappedAssemblyHash}", ConsoleColor.Yellow);
        Logger.Log("-----------------------------------------------", ConsoleColor.Yellow);
    }

    public void StartCrossCompile()
    {
        //CopyVisualStudioProject();
    }

    private void CopyVisualStudioProject(CrossCompilerProjectModel cache)
    {
        var solutionDirPath = Settings.VisualStudioSolutionDirectory;
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
        var destination = Path.Combine(DataProvider.ProjectPath, solutionName);

        cache.ReCodeItProjectDir = destination;
        cache.SolutionPath = solutionFile;

        Logger.Log($"Copying solution: {solutionName} to {destination}", ConsoleColor.Yellow);

        CopyProjectRecursive(solutionDirPath, destination);
    }

    private void CopyProjectRecursive(string sourceDirPath, string destinationDirPath)
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

    private void FindProjects()
    {
        DirectoryInfo sourceDir = new DirectoryInfo(DataProvider.ProjectPath);

        // Only search top level directories here
        foreach (var directory in sourceDir.GetDirectories())
        {
            var files = directory.GetFiles();

            foreach (var file in files)
            {
                if (file.Name == "ReCodeItProj.json")
                {
                    ProjectPaths.Add(file.FullName);
                    Logger.Log($"Found ReCodeIt Project at {file.FullName}");
                }
            }
        }
    }
}