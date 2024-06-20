using ReCodeIt.Utils;

namespace ReCodeIt.Models;

public class CrossCompilerProjectModel
{
    #region REQUIRED_ON_CREATION

    /// <summary>
    /// The path of the original assembly
    ///
    /// (Required on creation)
    /// </summary>
    public string OriginalAssemblyPath { get; set; }

    /// <summary>
    /// Remapped output path
    ///
    /// (Required on creation)
    /// </summary>
    public string RemappedAssemblyPath { get; set; }

    /// <summary>
    /// The path to the working directory vs project
    ///
    /// (Required on creation)
    /// </summary>
    public string VisualStudioSolutionPath { get; set; }

    /// <summary>
    /// The path to the working directory vs project
    ///
    /// (Required on creation)
    /// </summary>
    public string VisualStudioDependencyPath { get; set; }

    /// <summary>
    /// This is where the final dll is built to
    ///
    /// (Required on creation)
    /// </summary>
    public string BuildDirectory { get; set; }

    #endregion REQUIRED_ON_CREATION

    /// <summary>
    /// The path to the working directory vs project
    /// </summary>
    public string VisualStudioSolutionDirectoryPath => Path.GetDirectoryName(VisualStudioSolutionPath)!;

    /// <summary>
    /// The path the the cloned solution
    /// </summary>
    public string VisualStudioClonedSolutionPath => Path.Combine(
        DataProvider.ReCodeItProjectsPath,
        SolutionName,
        SolutionName + ".sln");

    public string VisualStudioClonedSolutionDirectory => Path.Combine(
        DataProvider.ReCodeItProjectsPath,
        SolutionName);

    /// <summary>
    /// The path to the cloned solutions dependency folder
    /// </summary>
    public string VisualStudioClonedDependencyPath => Path.GetDirectoryName(VisualStudioDependencyPath)!;

    public string ProjectDllName => SolutionName + ".dll";

    public string OriginalAssemblyDllName => Path.GetFileName(OriginalAssemblyPath);

    /// <summary>
    /// Name of the solution
    /// </summary>
    public string SolutionName { get; set; }

    /// <summary>
    /// Remapped output hash
    /// </summary>
    public string OriginalAssemblyHash { get; set; }

    /// <summary>
    /// Remapped output hash
    /// </summary>
    public string RemappedAssemblyHash { get; set; }

    /// <summary>
    /// Key: Remapped name, value: old name
    /// </summary>
    public Dictionary<string, string> ChangedTypes { get; set; } = [];

    /// <summary>
    /// Remap models used on this project
    /// </summary>
    public List<RemapModel> RemapModels { get; set; } = [];
}