namespace AssemblyRemapper.Models;

/// <summary>
/// Remap config
/// </summary>
internal class Settings
{
    public AppSettings AppSettings { get; set; }
    public RemapperSettings RemapperSettings { get; set; }

    public AutoMapperSettings AutoMapperSettings { get; set; }
}

internal class AppSettings
{
    public bool Debug { get; set; } = false;
    public bool SilentMode { get; set; } = true;
    public bool MatchMode { get; set; } = false;
}

internal class RemapperSettings
{
    public int MaxMatchCount { get; set; } = 5;

    public bool Publicize { get; set; } = false;
    public bool Unseal { get; set; } = false;

    public string AssemblyPath { get; set; } = string.Empty;
    public string OutputPath { get; set; } = string.Empty;
    public string MappingPath { get; set; } = string.Empty;
}

internal class AutoMapperSettings
{
    public bool Publicize { get; set; } = false;
    public bool Unseal { get; set; } = false;
}