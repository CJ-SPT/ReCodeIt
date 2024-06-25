using dnlib.DotNet;
using MoreLinq;
using ReCodeIt.Enums;
using ReCodeIt.Models;

namespace ReCodeIt.ReMapper.Search;

internal static class Fields
{
    /// <summary>
    /// Returns a match on any type with the provided fields
    /// </summary>
    /// <param name="type"></param>
    /// <param name="parms"></param>
    /// <param name="score"></param>
    /// <returns></returns>
    public static EMatchResult Include(TypeDef type, SearchParams parms, ScoringModel score)
    {
        if (parms.IncludeFields is null || parms.IncludeFields.Count == 0) return EMatchResult.Disabled;

        var matches = type.Fields
            .Count(field => parms.IncludeFields.Contains(field.Name));

        score.Score += matches > 0 ? matches : -matches;

        score.FailureReason = matches > 0 ? EFailureReason.None : EFailureReason.FieldsInclude;

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
    public static EMatchResult Exclude(TypeDef type, SearchParams parms, ScoringModel score)
    {
        if (parms.ExcludeFields is null || parms.ExcludeFields.Count == 0) return EMatchResult.Disabled;

        var matches = type.Fields
            .Count(field => parms.ExcludeFields.Contains(field.Name));

        score.Score += matches > 0 ? -matches : 1;

        score.FailureReason = matches > 0 ? EFailureReason.FieldsExclude : EFailureReason.None;

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
    public static EMatchResult Count(TypeDef type, SearchParams parms, ScoringModel score)
    {
        if (parms.FieldCount is null) return EMatchResult.Disabled;

        var match = type.Fields.Exactly((int)parms.FieldCount);

        score.Score += match ? (int)parms.FieldCount : -(int)parms.FieldCount;

        score.FailureReason = match ? EFailureReason.None : EFailureReason.FieldsCount;

        return match
            ? EMatchResult.Match
            : EMatchResult.NoMatch;
    }
}