using AssemblyRemapper.Enums;
using AssemblyRemapper.Models;
using Mono.Cecil;
using MoreLinq;

namespace AssemblyRemapper.Remapper.Search;

internal static class Fields
{
    /// <summary>
    /// Returns a match on any type with the provided fields
    /// </summary>
    /// <param name="type"></param>
    /// <param name="parms"></param>
    /// <param name="score"></param>
    /// <returns></returns>
    public static EMatchResult IncludeFields(TypeDefinition type, SearchParams parms, ScoringModel score)
    {
        if (parms.MatchFields is null || parms.MatchFields.Count == 0) return EMatchResult.Disabled;

        var matches = type.Fields
            .Where(field => parms.MatchFields.Contains(field.Name))
            .Count();

        score.Score += matches;

        return matches > 0
            ? EMatchResult.Match
            : EMatchResult.NoMatch;
    }

    /// <summary>
    /// Returns a match on any type without the provided fields
    /// </summary>
    /// <param name="type"></param>
    /// <param name="parms"></param>
    /// <param name="score"></param>
    /// <returns></returns>
    public static EMatchResult ExcludeFields(TypeDefinition type, SearchParams parms, ScoringModel score)
    {
        if (parms.IgnoreFields is null || parms.IgnoreFields.Count == 0) return EMatchResult.Disabled;

        var matches = type.Fields
            .Where(field => parms.IgnoreFields.Contains(field.Name))
            .Count();

        score.Score += matches;

        return matches > 0
            ? EMatchResult.NoMatch
            : EMatchResult.Match;
    }

    /// <summary>
    /// Returns a match on any type with a matching number of fields
    /// </summary>
    /// <param name="type"></param>
    /// <param name="parms"></param>
    /// <param name="score"></param>
    /// <returns></returns>
    public static EMatchResult MatchFieldCount(TypeDefinition type, SearchParams parms, ScoringModel score)
    {
        if (parms.FieldCount is null) return EMatchResult.Disabled;

        var match = type.Fields.Exactly((int)parms.FieldCount);

        if (match) { score.Score++; }

        return match
            ? EMatchResult.Match
            : EMatchResult.NoMatch;
    }
}