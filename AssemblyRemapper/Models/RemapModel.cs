namespace AssemblyRemapper.Models;

/// <summary>
/// Object to store linq statements in inside of json to search and remap classes
/// </summary>
internal class RemapModel
{
    public string NewTypeName { get; set; } = string.Empty;

    public string OriginalTypeName { get; set; } = string.Empty;

    public bool UseForceRename { get; set; }

    public SearchParams SearchParams { get; set; } = new();
}

/// <summary>
/// Search filters to find types and remap them
/// </summary>
internal class SearchParams
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
    public bool? HasGenericParameters { get; set; } = null;
    public List<string> MethodNamesToMatch { get; set; } = [];
    public List<string> MethodNamesToIgnore { get; set; } = [];

    public List<string> FieldNamesToMatch { get; set; } = [];
    public List<string> FieldNamesToIgnore { get; set; } = [];
    public List<string> PropertyNamesToMatch { get; set; } = [];
    public List<string> PropertyNamesToIgnore { get; set; } = [];

    public List<string> NestedTypesToMatch { get; set; } = [];
    public List<string> NestedTypesToIgnore { get; set; } = [];

    public SearchParams()
    {
    }
}

internal class AdvancedSearchParams
{
}