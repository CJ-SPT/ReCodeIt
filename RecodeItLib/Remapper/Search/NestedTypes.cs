using Mono.Cecil;
using MoreLinq;
using ReCodeIt.Enums;
using ReCodeIt.Models;

namespace ReCodeIt.ReMapper.Search;

internal class NestedTypes
{
    public static EMatchResult Include(TypeDefinition type, SearchParams parms, ScoringModel score)
    {
        if (parms.IncludeNestedTypes is null || parms.IncludeNestedTypes.Count == 0) return EMatchResult.Disabled;

        var matches = type.NestedTypes
            .Where(nt => parms.IncludeNestedTypes.Contains(nt.Name))
            .Count();
        score.Score += matches;

        score.FailureReason = matches > 0 ? EFailureReason.None : EFailureReason.NestedTypeInclude;

        return matches > 0
            ? EMatchResult.Match
            : EMatchResult.NoMatch;
    }

    public static EMatchResult Exclude(TypeDefinition type, SearchParams parms, ScoringModel score)
    {
        if (parms.ExcludeNestedTypes is null || parms.ExcludeNestedTypes.Count == 0) return EMatchResult.Disabled;

        var matches = type.NestedTypes
            .Where(nt => parms.ExcludeNestedTypes.Contains(nt.Name))
            .Count();

        score.Score += matches;

        score.FailureReason = matches > 0 ? EFailureReason.None : EFailureReason.NestedTypeExclude;

        return matches > 0
            ? EMatchResult.NoMatch
            : EMatchResult.Match;
    }

    public static EMatchResult Count(TypeDefinition type, SearchParams parms, ScoringModel score)
    {
        if (parms.NestedTypeCount is null) return EMatchResult.Disabled;

        var match = type.NestedTypes.Exactly((int)parms.NestedTypeCount);

        if (match) { score.Score++; }

        score.FailureReason = match ? EFailureReason.None : EFailureReason.NestedTypeCount;

        return match
            ? EMatchResult.Match
            : EMatchResult.NoMatch;
    }
}