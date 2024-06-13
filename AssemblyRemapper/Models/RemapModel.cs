using AssemblyRemapper.Enums;
using Newtonsoft.Json;

namespace AssemblyRemapper.Models;

/// <summary>
/// Object to store linq statements in inside of json to search and remap classes
/// </summary>
internal class RemapModel
{
    [JsonIgnore]
    public bool Succeeded { get; set; } = false;

    [JsonIgnore]
    public EFailureReason FailureReason { get; set; }

    [JsonIgnore]
    public HashSet<ScoringModel> Scores { get; set; } = [];

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
    public string MatchBaseClass { get; set; }
    public string IgnoreBaseClass { get; set; }
    public List<string> MatchMethods { get; set; }
    public List<string> IgnoreMethods { get; set; }
    public List<string> MatchFields { get; set; }
    public List<string> IgnoreFields { get; set; }
    public List<string> MatchProperties { get; set; }
    public List<string> IgnorePropterties { get; set; }
    public List<string> MatchNestedTypes { get; set; }
    public List<string> IgnoreNestedTypes { get; set; }

    public SearchParams()
    {
    }
}

internal class AdvancedSearchParams
{
}