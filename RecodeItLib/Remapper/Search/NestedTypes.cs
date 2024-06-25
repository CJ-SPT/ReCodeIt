using dnlib.DotNet;
using ReCodeIt.Enums;
using ReCodeIt.Models;

namespace ReCodeIt.ReMapper.Search;

internal class NestedTypes
{
    public static EMatchResult Include(TypeDef type, SearchParams parms, ScoringModel score)
    {
        if (parms.IncludeNestedTypes is null || parms.IncludeNestedTypes.Count == 0) return EMatchResult.Disabled;

        foreach (var nt in type.NestedTypes)
        {
            var ntName = nt.Name.String;

            if (!parms.IncludeNestedTypes.Contains(ntName)) { continue; }

            score.Score++;

            return EMatchResult.Match;
        }

        score.FailureReason = EFailureReason.NestedTypeInclude;
        return EMatchResult.NoMatch;
    }

    public static EMatchResult Exclude(TypeDef type, SearchParams parms, ScoringModel score)
    {
        if (parms.ExcludeNestedTypes is null || parms.ExcludeNestedTypes.Count == 0) return EMatchResult.Disabled;

        foreach (var nt in type.NestedTypes)
        {
            if (!parms.ExcludeNestedTypes.Contains(nt.Name)) continue;

            score.Score--;
            score.FailureReason = EFailureReason.NestedTypeExclude;
            return EMatchResult.NoMatch;
        }

        score.Score++;
        return EMatchResult.Match;
    }

    public static EMatchResult Count(TypeDef type, SearchParams parms, ScoringModel score)
    {
        if (parms.NestedTypeCount is null) return EMatchResult.Disabled;

        var match = type.NestedTypes.Count() == parms.NestedTypeCount;

        score.Score += match ? type.NestedTypes.Count : -type.NestedTypes.Count - 1;

        score.FailureReason = match ? EFailureReason.None : EFailureReason.NestedTypeCount;

        return match
            ? EMatchResult.Match
            : EMatchResult.NoMatch;
    }
}