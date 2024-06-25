using dnlib.DotNet;
using ReCodeIt.Models;

namespace ReCodeIt.ReMapper.Search;

internal static class TypeDefExtensions
{
    public static void MatchConstructors(this TypeDef type, SearchParams parms, ScoringModel score)
    {
        Constructors.GetTypeByParameterCount(type, parms, score);
    }

    /// <summary>
    /// Handle running all method matching routines
    /// </summary>
    /// <returns>Match if any search criteria met</returns>
    public static void MatchMethods(this TypeDef type, SearchParams parms, ScoringModel score)
    {
        Methods.Include(type, parms, score);
        Methods.Exclude(type, parms, score);
        Methods.Count(type, parms, score);
    }

    public static void MatchFields(this TypeDef type, SearchParams parms, ScoringModel score)
    {
        Fields.Include(type, parms, score);
        Fields.Exclude(type, parms, score);
        Fields.Count(type, parms, score);
    }

    public static void MatchProperties(this TypeDef type, SearchParams parms, ScoringModel score)
    {
        Properties.Include(type, parms, score);
        Properties.Exclude(type, parms, score);
        Properties.Count(type, parms, score);
    }

    public static void MatchNestedTypes(this TypeDef type, SearchParams parms, ScoringModel score)
    {
        NestedTypes.Include(type, parms, score);
        NestedTypes.Exclude(type, parms, score);
        NestedTypes.Count(type, parms, score);
    }
}