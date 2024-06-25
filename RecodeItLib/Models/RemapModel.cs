using Newtonsoft.Json;
using ReCodeIt.Enums;

namespace ReCodeIt.Models;

/// <summary>
/// Object to store linq statements in inside of json to search and remap classes
/// </summary>
public class RemapModel
{
    [JsonIgnore]
    public bool Succeeded { get; set; } = false;

    [JsonIgnore]
    public List<ENoMatchReason> NoMatchReasons { get; set; } = [];

    public string NewTypeName { get; set; } = string.Empty;

    public string OriginalTypeName { get; set; } = string.Empty;

    public bool UseForceRename { get; set; }

    public SearchParams SearchParams { get; set; } = new();
}

/// <summary>
/// Search filters to find types and remap them
/// </summary>
public class SearchParams
{
    #region BOOL_PARAMS

    public bool? IsPublic { get; set; } = null;
    public bool? IsAbstract { get; set; } = null;
    public bool? IsInterface { get; set; } = null;
    public bool? IsEnum { get; set; } = null;
    public bool? IsNested { get; set; } = null;
    public bool? IsSealed { get; set; } = null;
    public bool? HasAttribute { get; set; } = null;
    public bool? IsDerived { get; set; } = null;
    public bool? HasGenericParameters { get; set; } = null;

    #endregion BOOL_PARAMS

    #region STR_PARAMS

    public string? ParentName { get; set; } = null;
    public string? MatchBaseClass { get; set; } = null;
    public string? IgnoreBaseClass { get; set; } = null;

    #endregion STR_PARAMS

    #region INT_PARAMS

    public int? ConstructorParameterCount { get; set; } = null;
    public int? MethodCount { get; set; } = null;
    public int? FieldCount { get; set; } = null;
    public int? PropertyCount { get; set; } = null;
    public int? NestedTypeCount { get; set; } = null;

    #endregion INT_PARAMS

    #region LISTS

    public List<string> IncludeMethods { get; set; }
    public List<string> ExcludeMethods { get; set; }
    public List<string> IncludeFields { get; set; }
    public List<string> ExcludeFields { get; set; }
    public List<string> IncludeProperties { get; set; }
    public List<string> ExcludeProperties { get; set; }
    public List<string> IncludeNestedTypes { get; set; }
    public List<string> ExcludeNestedTypes { get; set; }

    #endregion LISTS

    public SearchParams()
    {
    }
}

internal class AdvancedSearchParams
{
}