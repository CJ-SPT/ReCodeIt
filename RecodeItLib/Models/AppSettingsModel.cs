using ReCodeIt.Utils;

namespace ReCodeIt.Models;

/// <summary>
/// All settings container
/// </summary>
public class Settings
{
    private AppSettings _appSettings;

    public AppSettings AppSettings
    {
        get { return _appSettings; }
        set
        {
            _appSettings = value;
            Save();
        }
    }

    private RemapperSettings _remapper;

    public RemapperSettings Remapper
    {
        get { return _remapper; }
        set
        {
            _remapper = value;
            Save();
        }
    }

    private AutoMapperSettings _autoMapper;

    public AutoMapperSettings AutoMapper
    {
        get { return _autoMapper; }
        set
        {
            _autoMapper = value;
            Save();
        }
    }

    private CrossCompilerSettings _crossCompiler;

    public CrossCompilerSettings CrossCompiler
    {
        get { return _crossCompiler; }
        set
        {
            _crossCompiler = value;
            Save();
        }
    }

    private void Save()
    {
        DataProvider.SaveAppSettings();
    }
}

/// <summary>
/// These are settings for the application
/// </summary>
public class AppSettings
{
    private bool _debug;

    public bool Debug
    {
        get { return _debug; }
        set
        {
            _debug = value;
            Save();
        }
    }

    private bool _silentMode;

    public bool SilentMode
    {
        get { return _silentMode; }
        set
        {
            _silentMode = value;
            Save();
        }
    }

    private void Save()
    {
        DataProvider.SaveAppSettings();
    }
}

/// <summary>
/// These are settings for the manual remapper
/// </summary>
public class RemapperSettings
{
    private string _assemblyPath;

    /// <summary>
    /// Path to the assembly we want to remap
    /// </summary>
    public string AssemblyPath
    {
        get { return _assemblyPath; }
        set
        {
            _assemblyPath = value;
            Save();
        }
    }

    private string _outputPath;

    /// <summary>
    /// Path including the filename and extension we want to write the changes to
    /// </summary>
    public string OutputPath
    {
        get { return _outputPath; }
        set
        {
            _outputPath = value;
            Save();
        }
    }

    private string _mappingPath;

    /// <summary>
    /// Path to the mapping file
    /// </summary>
    public string MappingPath
    {
        get { return _mappingPath; }
        set
        {
            _mappingPath = value;
            Save();
        }
    }

    private bool _useProjectMappings;

    /// <summary>
    /// Use the projects mappings instead of a standalone file
    /// </summary>
    public bool UseProjectMappings
    {
        get { return _useProjectMappings; }
        set
        {
            _useProjectMappings = value;
            Save();
        }
    }

    private MappingSettings _mappingSettings;

    public MappingSettings MappingSettings
    {
        get { return _mappingSettings; }
        set
        {
            _mappingSettings = value;
            Save();
        }
    }

    private void Save()
    {
        DataProvider.SaveAppSettings();
    }
}

/// <summary>
/// These are settings for the auto mapping
/// </summary>
public class AutoMapperSettings
{
    private string _assemblyPath;

    /// <summary>
    /// Path to the assembly we want to remap
    /// </summary>
    public string AssemblyPath
    {
        get { return _assemblyPath; }
        set
        {
            _assemblyPath = value;
            Save();
        }
    }

    private string _outputPath;

    /// <summary>
    /// Path including the filename and extension we want to write the changes to
    /// </summary>
    public string OutputPath
    {
        get { return _outputPath; }
        set
        {
            _outputPath = value;
            Save();
        }
    }

    private int _requiredMatches;

    /// <summary>
    /// Minimum number of times a member must have this name in the assembly before considering it
    /// for remapping
    /// </summary>
    public int RequiredMatches
    {
        get { return _requiredMatches; }
        set
        {
            _requiredMatches = value;
            Save();
        }
    }

    private int _minLengthToMatch;

    /// <summary>
    /// Minimum length of the field/property name in code before it will be considered for a rename
    /// </summary>
    public int MinLengthToMatch
    {
        get { return _minLengthToMatch; }
        set
        {
            _minLengthToMatch = value;
            Save();
        }
    }

    private bool _searchMethods;

    /// <summary>
    /// Will attempt to map types from method meta data and parameters
    /// </summary>
    public bool SearchMethods
    {
        get { return _searchMethods; }
        set
        {
            _searchMethods = value;
            Save();
        }
    }

    private MappingSettings _mappingSettings;

    public MappingSettings MappingSettings
    {
        get { return _mappingSettings; }
        set
        {
            _mappingSettings = value;
            Save();
        }
    }

    private List<string> _typesToIgnore;

    /// <summary>
    /// Any member name you want to ignore while iterating through the assembly
    /// </summary>
    public List<string> TypesToIgnore
    {
        get { return _typesToIgnore; }
        set
        {
            _typesToIgnore = value;
            Save();
        }
    }

    private List<string> _tokensToMatch;

    /// <summary>
    /// The auto mapper will look for these tokens in class names and prioritize those
    /// </summary>
    public List<string> TokensToMatch
    {
        get { return _tokensToMatch; }
        set
        {
            _tokensToMatch = value;
            Save();
        }
    }

    private List<string> _propertyFieldBlacklist;

    /// <summary>
    /// Property or fields names to ignore in the automap, these are case sanitized so case does not matter
    /// </summary>
    public List<string> PropertyFieldBlackList
    {
        get { return _propertyFieldBlacklist; }
        set
        {
            _propertyFieldBlacklist = value;
            Save();
        }
    }

    private List<string> _methodParamaterBlackList;

    /// <summary>
    /// method parameter names to ignore in the automap, these are case sanitized so case does not matter
    /// </summary>
    public List<string> MethodParamaterBlackList
    {
        get { return _methodParamaterBlackList; }
        set
        {
            _methodParamaterBlackList = value;
            Save();
        }
    }

    private void Save()
    {
        DataProvider.SaveAppSettings();
    }
}

/// <summary>
/// These are settings for the cross compiler module
/// </summary>
public class CrossCompilerSettings
{
    private string _lastLoadedProject;

    /// <summary>
    /// Last Loaded Project Path
    /// </summary>
    public string LastLoadedProject
    {
        get { return _lastLoadedProject; }
        set
        {
            _lastLoadedProject = value;
            Save();
        }
    }

    private bool _autoLoadLastActiveProj;

    /// <summary>
    /// Should the last active project be auto loaded
    /// </summary>
    public bool AutoLoadLastActiveProject
    {
        get { return _autoLoadLastActiveProj; }
        set
        {
            _autoLoadLastActiveProj = value;
            Save();
        }
    }

    private void Save()
    {
        DataProvider.SaveAppSettings();
    }
}

/// <summary>
/// These are settings that all versions of the remappers use
/// </summary>
public class MappingSettings
{
    private bool _renameFields;

    /// <summary>
    /// Names of fields of the matched type will be renamed to the type name with approproiate convention
    /// </summary>
    public bool RenameFields
    {
        get { return _renameFields; }
        set
        {
            _renameFields = value;
            Save();
        }
    }

    private bool _renameProps;

    /// <summary>
    /// Names of properties of the matched type will be renamed to the type name with approproiate convention
    /// </summary>
    public bool RenameProperties
    {
        get { return _renameProps; }
        set
        {
            _renameProps = value;
            Save();
        }
    }

    private bool _publicize;

    /// <summary>
    /// Publicize all types, methods, and properties : NOTE: Not run until after the remap has completed
    /// </summary>
    public bool Publicize
    {
        get { return _publicize; }
        set
        {
            _publicize = value;
            Save();
        }
    }

    private bool _unseal;

    /// <summary>
    /// Unseal all types : NOTE: Not run until after the remap has completed
    /// </summary>
    public bool Unseal
    {
        get { return _unseal; }
        set
        {
            _unseal = value;
            Save();
        }
    }

    private void Save()
    {
        DataProvider.SaveAppSettings();
    }
}