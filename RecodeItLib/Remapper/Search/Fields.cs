using dnlib.DotNet;
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

        foreach (var field in type.Fields)
        {
            if (!parms.IncludeFields.Contains(field.Name)) continue;

            score.Score++;
            return EMatchResult.Match;
        }

        score.Score--;
        score.FailureReason = EFailureReason.FieldsInclude;
        return EMatchResult.NoMatch;
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

        foreach (var field in type.Fields)
        {
            if (!parms.ExcludeFields.Contains(field.Name)) continue;

            score.Score--;
            score.FailureReason = EFailureReason.FieldsExclude;
            return EMatchResult.NoMatch;
        }

        score.Score++;
        return EMatchResult.Match;
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

        var match = type.Fields.Count() == parms.FieldCount;

        score.Score += match ? (int)parms.FieldCount : -(int)parms.FieldCount;

        score.FailureReason = match ? EFailureReason.None : EFailureReason.FieldsCount;

        return match
            ? EMatchResult.Match
            : EMatchResult.NoMatch;
    }
}