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
    /// <summary>
    /// Path to the assembly we want to remap
    /// </summary>
    public string AssemblyPath { get; set; }

    /// <summary>
    /// Path including the filename and extension we want to write the changes to
    /// </summary>
    public string OutputPath { get; set; }

    /// <summary>
    /// Path to the mapping file
    /// </summary>
    public string MappingPath { get; set; }

    public MappingSettings MappingSettings { get; set; }
}

/// <summary>
/// These are settings for the auto mapping
/// </summary>
public class AutoMapperSettings
{
    /// <summary>
    /// Path to the assembly we want to remap
    /// </summary>
    public string AssemblyPath { get; set; }

    /// <summary>
    /// Path including the filename and extension we want to write the changes to
    /// </summary>
    public string OutputPath { get; set; }

    /// <summary>
    /// Minimum number of times a member must have this name in the assembly before considering it
    /// for remapping
    /// </summary>
    public int RequiredMatches { get; set; }

    /// <summary>
    /// Minimum length of the field/property name in code before it will be considered for a rename
    /// </summary>
    public int MinLengthToMatch { get; set; }

    /// <summary>
    /// Will attempt to map types from method meta data and parameters
    /// </summary>
    public bool SearchMethods { get; set; }

    public MappingSettings MappingSettings { get; set; }

    /// <summary>
    /// Any member name you want to ignore while iterating through the assembly
    /// </summary>
    public List<string> TypesToIgnore { get; set; }

    /// <summary>
    /// The auto mapper will look for these tokens in class names and prioritize those
    /// </summary>
    public List<string> TokensToMatch { get; set; }

    /// <summary>
    /// Property or fields names to ignore in the automap, these are case sanitized so case does not matter
    /// </summary>
    public List<string> PropertyFieldBlackList { get; set; }

    /// <summary>
    /// method parameter names to ignore in the automap, these are case sanitized so case does not matter
    /// </summary>
    public List<string> MethodParamaterBlackList { get; set; }
}

/// <summary>
/// These are settings for the cross patching module
/// </summary>
public class CrossPatchingSettings
{
    /// <summary>
    /// The path to the original assembly to use as a reference, for unity games its
    /// 'Assembly-CSharp.dll' from the games Data/Managed Folder
    /// </summary>
    public string OriginalAssemblyPath { get; set; }

    /// <summary>
    /// Where should the new reference dll be placed? (This is the remapped one you reference in
    /// your project)
    /// </summary>
    public string ReferencePath { get; set; }

    /// <summary>
    /// Path to the mapping file
    /// </summary>
    public string MappingPath { get; set; }

    /// <summary>
    /// The build path of your project, specifically the absolute path of your projects output assembly.
    /// </summary>
    public string TargetProjectAssemblyBuildPath { get; set; }

    /// <summary>
    /// Where should the final cross patched dll be placed? (This is the one you test/distribute with)
    /// </summary>
    public string OutputPath { get; set; }
}

/// <summary>
/// These are settings that all versions of the remappers use
/// </summary>
public class MappingSettings
{
    /// <summary>
    /// Names of fields of the matched type will be renamed to the type name with approproiate convention
    /// </summary>
    public bool RenameFields { get; set; }

    /// <summary>
    /// Names of properties of the matched type will be renamed to the type name with approproiate convention
    /// </summary>
    public bool RenameProperties { get; set; }

    /// <summary>
    /// Publicize all types, methods, and properties : NOTE: Not run until after the remap has completed
    /// </summary>
    public bool Publicize { get; set; }

    /// <summary>
    /// Unseal all types : NOTE: Not run until after the remap has completed
    /// </summary>
    public bool Unseal { get; set; }
}