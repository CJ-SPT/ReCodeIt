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
    public static void Include(TypeDef type, SearchParams parms, ScoringModel score)
    {
        if (parms.IncludeFields is null || parms.IncludeFields.Count == 0) return;

        foreach (var field in type.Fields)
        {
            if (!parms.IncludeFields.Contains(field.Name)) continue;

            score.Score++;
            return;
        }

        score.Score--;
        score.NoMatchReasons.Add(ENoMatchReason.FieldsInclude);
    }

    /// <summary>
    /// Returns a match on any type without the provided fields
    /// </summary>
    /// <param name="type"></param>
    /// <param name="parms"></param>
    /// <param name="score"></param>
    /// <returns></returns>
    public static void Exclude(TypeDef type, SearchParams parms, ScoringModel score)
    {
        if (parms.ExcludeFields is null || parms.ExcludeFields.Count == 0) return;

        foreach (var field in type.Fields)
        {
            if (!parms.ExcludeFields.Contains(field.Name)) continue;

            score.Score--;
            score.NoMatchReasons.Add(ENoMatchReason.FieldsExclude);
            return;
        }

        score.Score++;
    }

    /// <summary>
    /// Returns a match on any type with a matching number of fields
    /// </summary>
    /// <param name="type"></param>
    /// <param name="parms"></param>
    /// <param name="score"></param>
    /// <returns></returns>
    public static void Count(TypeDef type, SearchParams parms, ScoringModel score)
    {
        if (parms.FieldCount is null) return;

        var match = type.Fields.Count() == parms.FieldCount;

        score.Score += match ? (int)parms.FieldCount : -(int)parms.FieldCount;

        if (!match)
        {
            score.NoMatchReasons.Add(ENoMatchReason.FieldsCount);
        }
    }
}