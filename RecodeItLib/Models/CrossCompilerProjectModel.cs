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
    /// This is where the final dll is built to
    ///
    /// (Required on creation)
    /// </summary>
    public string BuildDirectory { get; set; }

    #endregion REQUIRED_ON_CREATION

    /// <summary>
    /// Name of the solution
    /// </summary>
    public string SolutionName { get; set; }

    /// <summary>
    /// The ReCodeIt.json path
    /// </summary>
    public string ReCodeItProjectPath { get; set; }

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