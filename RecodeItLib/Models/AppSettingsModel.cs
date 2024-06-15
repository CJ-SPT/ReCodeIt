namespace ReCodeIt.Models;

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
    public bool Debug { get; set; }
    public bool SilentMode { get; set; }
    public string AssemblyPath { get; set; }
    public string OutputPath { get; set; }
    public string MappingPath { get; set; }
    public bool RenameFields { get; set; }
    public bool RenameProperties { get; set; }
    public bool Publicize { get; set; }
    public bool Unseal { get; set; }
}

public class RemapperSettings
{
    public int MaxMatchCount { get; set; }
}

public class AutoMapperSettings
{
    public int RequiredMatches { get; set; }

    public int MinLengthToMatch { get; set; }

    public List<string> TypesToIgnore { get; set; }

    public List<string> TokensToMatch { get; set; }

    public List<string> PropertyFieldBlackList { get; set; }
}