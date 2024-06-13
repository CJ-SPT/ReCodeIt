using AssemblyRemapper.Enums;
using AssemblyRemapper.Models;
using Mono.Cecil;
using MoreLinq;

namespace AssemblyRemapper.Remapper.Search;

internal static class Methods
{
    /// <summary>
    /// returns a match on all types with the specified methods
    /// </summary>
    /// <param name="type"></param>
    /// <param name="parms"></param>
    /// <param name="score"></param>
    /// <returns>Match if type contains any supplied methods</returns>
    public static EMatchResult IncludeMethods(TypeDefinition type, SearchParams parms, ScoringModel score)
    {
        if (parms.MatchMethods is null || parms.MatchMethods.Count == 0) return EMatchResult.Disabled;

        var matches = type.Methods
            .Where(method => parms.MatchMethods.Contains(method.Name))
            .Count();

        score.Score += matches;

        return matches > 0
            ? EMatchResult.Match
            : EMatchResult.NoMatch;
    }

    /// <summary>
    /// Returns a match on all types without methods
    /// </summary>
    /// <param name="type"></param>
    /// <param name="parms"></param>
    /// <param name="score"></param>
    /// <returns>Match if type has no methods</returns>
    public static EMatchResult ExcludeMethods(TypeDefinition type, SearchParams parms, ScoringModel score)
    {
        if (parms.IgnoreMethods is null || parms.IgnoreMethods.Count == 0) return EMatchResult.Disabled;

        var matches = type.Methods
            .Where(method => parms.IgnoreMethods.Contains(method.Name))
            .Count();

        score.Score += matches;

        return matches > 0
            ? EMatchResult.NoMatch
            : EMatchResult.Match;
    }

    public static EMatchResult MatchMethodCount(TypeDefinition type, SearchParams parms, ScoringModel score)
    {
        if (parms.MethodCount is null) return EMatchResult.Disabled;

        var match = type.Methods.Exactly((int)parms.MethodCount);

        if (match) { score.Score++; }

        return match
            ? EMatchResult.Match
            : EMatchResult.NoMatch;
    }
}