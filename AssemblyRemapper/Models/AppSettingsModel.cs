namespace AssemblyRemapper.Models;

/// <summary>
/// Remap config
/// </summary>
public class Settings
{
    public AppSettings AppSettings { get; set; }
    public RemapperSettings Remapper { get; set; }

    public AutoMapperSettings AutoMapper { get; set; }
}

public class AppSettings
{
    public bool Debug { get; set; } = false;
    public bool SilentMode { get; set; } = true;
    public bool MatchMode { get; set; } = false;
}

public class RemapperSettings
{
    public int MaxMatchCount { get; set; } = 5;

    public bool Publicize { get; set; } = false;
    public bool Unseal { get; set; } = false;

    public string AssemblyPath { get; set; } = string.Empty;
    public string OutputPath { get; set; } = string.Empty;
    public string MappingPath { get; set; } = string.Empty;
}

public class AutoMapperSettings
{
    public int RequiredMatches { get; set; } = 5;
    public bool MatchMode { get; set; } = true;
    public bool Publicize { get; set; } = false;
    public bool Unseal { get; set; } = false;
    public List<string> NamesToIgnore { get; set; } = [];
}