namespace AssemblyRemapper.Models;

/// <summary>
/// Remap config
/// </summary>
internal class AppSettings
{
    public bool Debug { get; set; }
    public bool SilentMode { get; set; }

    public bool ScoringMode { get; set; }
    public bool Publicize { get; set; }
    public bool Unseal { get; set; }

    public string AssemblyPath { get; set; }
    public string OutputPath { get; set; }

    public HashSet<Remap> Remaps { get; set; } = [];
}

/// <summary>
/// Object to store linq statements in inside of json to search and remap classes
/// </summary>
internal class Remap
{
    public string NewTypeName { get; set; } = string.Empty;

    public string OldTypeName { get; set; } = string.Empty;

    public bool UseDirectRename { get; set; }

    public RemapSearchParams SearchParams { get; set; } = new();
}

/// <summary>
/// Search filters to find types and remap them
/// </summary>
internal class RemapSearchParams
{
    public bool? IsPublic { get; set; } = null;
    public bool? IsAbstract { get; set; } = null;
    public bool? IsInterface { get; set; } = null;
    public bool? IsEnum { get; set; } = null;
    public bool? IsNested { get; set; } = null;
    public string? ParentName { get; set; } = null;
    public bool? IsSealed { get; set; } = null;
    public bool? HasAttribute { get; set; } = null;
    public bool? IsDerived { get; set; } = null;
    public string? BaseClassName { get; set; } = null;
    public bool? IsGeneric { get; set; } = null;
    public HashSet<string> MethodNamesToMatch { get; set; } = [];
    public HashSet<string> MethodNamesToIgnore { get; set; } = [];

    public HashSet<string> FieldNamesToMatch { get; set; } = [];
    public HashSet<string> FieldNamesToIgnore { get; set; } = [];
    public HashSet<string> PropertyNamesToMatch { get; set; } = [];
    public HashSet<string> PropertyNamesToIgnore { get; set; } = [];

    public HashSet<string> NestedTypesToMatch { get; set; } = [];
    public HashSet<string> NestedTypesToIgnore { get; set; } = [];

    public RemapSearchParams()
    {
    }
}