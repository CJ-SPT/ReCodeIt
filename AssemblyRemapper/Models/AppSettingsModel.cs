namespace AssemblyRemapper.Models;

/// <summary>
/// Remap config
/// </summary>
internal class AppSettings
{
    public bool Debug { get; set; }
    public bool SilentMode { get; set; }
    public int MaxMatchCount { get; set; }
    public bool ScoringMode { get; set; }
    public bool Publicize { get; set; }
    public bool Unseal { get; set; }

    public string AssemblyPath { get; set; }
    public string OutputPath { get; set; }
    public string MappingPath { get; set; }
}