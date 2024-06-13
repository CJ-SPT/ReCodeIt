using AssemblyRemapper.Enums;
using AssemblyRemapper.Models;
using Mono.Cecil;
using MoreLinq;

namespace AssemblyRemapper.Remapper.Search;

internal class NestedTypes
{
    public static EMatchResult IncludeNestedTypes(TypeDefinition type, SearchParams parms, ScoringModel score)
    {
        if (parms.IncludeNestedTypes is null || parms.IncludeNestedTypes.Count == 0) return EMatchResult.Disabled;

        var matches = type.NestedTypes
            .Where(nt => parms.IncludeNestedTypes.Contains(nt.Name))
            .Count();

        score.Score += matches;

        return matches > 0
            ? EMatchResult.Match
            : EMatchResult.NoMatch;
    }

    public static EMatchResult ExcludeNestedTypes(TypeDefinition type, SearchParams parms, ScoringModel score)
    {
        if (parms.ExcludeNestedTypes is null || parms.ExcludeNestedTypes.Count == 0) return EMatchResult.Disabled;

        var matches = type.NestedTypes
            .Where(nt => parms.ExcludeNestedTypes.Contains(nt.Name))
            .Count();

        score.Score += matches;

        return matches > 0
            ? EMatchResult.NoMatch
            : EMatchResult.Match;
    }

    public static EMatchResult MatchNestedTypeCount(TypeDefinition type, SearchParams parms, ScoringModel score)
    {
        if (parms.NestedTypeCount is null) return EMatchResult.Disabled;

        var match = type.NestedTypes.Exactly((int)parms.NestedTypeCount);

        if (match) { score.Score++; }

        return match
            ? EMatchResult.Match
            : EMatchResult.NoMatch;
    }
}