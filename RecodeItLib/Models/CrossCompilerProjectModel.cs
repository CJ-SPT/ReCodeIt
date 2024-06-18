namespace ReCodeIt.Models;

public class CrossCompilerProjectModel
{
    public string OriginalAssemblyPath { get; set; }

    public string OriginalAssemblyHash { get; set; }

    public string VisualStudioSolutionDirectory { get; set; }

    public string SolutionPath { get; set; }

    public string ReCodeItProjectDir { get; set; }

    public string RemappedAssemblyPath { get; set; }

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