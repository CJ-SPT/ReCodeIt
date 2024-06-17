namespace ReCodeIt.Models;

/// <summary>
/// All settings container
/// </summary>
public class Settings
{
    public AppSettings AppSettings { get; set; }
    public RemapperSettings Remapper { get; set; }
    public AutoMapperSettings AutoMapper { get; set; }
    public CrossPatchingSettings CrossPatching { get; set; }
}

/// <summary>
/// These are settings for the application
/// </summary>
public class AppSettings
{
    public bool Debug { get; set; }
    public bool SilentMode { get; set; }
    public bool RenameFields { get; set; }
    public bool RenameProperties { get; set; }
}

/// <summary>
/// These are settings for the manual remapper
/// </summary>
public class RemapperSettings
{
    public string AssemblyPath { get; set; }
    public string OutputPath { get; set; }
    public string MappingPath { get; set; }
    public MappingSettings MappingSettings { get; set; }
}

/// <summary>
/// These are settings for the auto mapping
/// </summary>
public class AutoMapperSettings
{
    public string AssemblyPath { get; set; }
    public string OutputPath { get; set; }
    public int RequiredMatches { get; set; }
    public int MinLengthToMatch { get; set; }
    public bool SearchMethods { get; set; }
    public MappingSettings MappingSettings { get; set; }
    public List<string> TypesToIgnore { get; set; }

    public List<string> TokensToMatch { get; set; }

    public List<string> PropertyFieldBlackList { get; set; }

    public List<string> MethodParamaterBlackList { get; set; }
}

/// <summary>
/// These are settings for the cross patching module
/// </summary>
public class CrossPatchingSettings
{
    public string OriginalAssemblyPath { get; set; }
    public string TargetProjectAssemblyBuildPath { get; set; }
    public string ReferencePath { get; set; }
    public string MappingPath { get; set; }
    public string OutputPath { get; set; }
}

/// <summary>
/// These are settings that all versions of the remappers use
/// </summary>
public class MappingSettings
{
    public bool RenameFields { get; set; }
    public bool RenameProperties { get; set; }
    public bool Publicize { get; set; }
    public bool Unseal { get; set; }
}