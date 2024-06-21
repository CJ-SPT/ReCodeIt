using Mono.Cecil;
using Mono.Cecil.Rocks;
using ReCodeIt.Enums;
using ReCodeIt.Models;

namespace ReCodeIt.ReMapper.Search;

internal static class Methods
{
    /// <summary>
    /// returns a match on all types with the specified methods
    /// </summary>
    /// <param name="type"></param>
    /// <param name="parms"></param>
    /// <param name="score"></param>
    /// <returns>Match if type contains any supplied methods</returns>
    public static EMatchResult Include(TypeDefinition type, SearchParams parms, ScoringModel score)
    {
        if (parms.IncludeMethods is null || parms.IncludeMethods.Count == 0) return EMatchResult.Disabled;

        var matches = type.Methods
            .Where(method => parms.IncludeMethods.Any(include => method.Name.Contains(include)))
            .Count();

        score.Score += matches;

        score.FailureReason = matches > 0 ? EFailureReason.None : EFailureReason.MethodsInclude;

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
    public static EMatchResult Exclude(TypeDefinition type, SearchParams parms, ScoringModel score)
    {
        if (parms.ExcludeMethods is null || parms.ExcludeMethods.Count == 0) return EMatchResult.Disabled;

        var matches = type.Methods
            .Where(method => parms.ExcludeMethods.Contains(method.Name))
            .Count();

        score.Score -= matches;

        score.FailureReason = matches > 0 ? EFailureReason.MethodsExclude : EFailureReason.None;

        return matches > 0
            ? EMatchResult.NoMatch
            : EMatchResult.Match;
    }

    /// <summary>
    /// Returns a match if the type has the provided number of methods
    /// </summary>
    /// <param name="type"></param>
    /// <param name="parms"></param>
    /// <param name="score"></param>
    /// <returns></returns>
    public static EMatchResult Count(TypeDefinition type, SearchParams parms, ScoringModel score)
    {
        if (parms.MethodCount is null) return EMatchResult.Disabled;

        var numMethods = type.Methods.Count - type.GetConstructors().Count();
        bool match = numMethods == parms.MethodCount;

        if (match) { score.Score++; }

        score.FailureReason = match ? EFailureReason.None : EFailureReason.MethodsCount;

        return match
            ? EMatchResult.Match
            : EMatchResult.NoMatch;
    }
}