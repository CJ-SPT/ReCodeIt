using dnlib.DotNet;
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
    public static EMatchResult Include(TypeDef type, SearchParams parms, ScoringModel score)
    {
        if (parms.IncludeMethods is null || parms.IncludeMethods.Count == 0) return EMatchResult.Disabled;

        var matches = type.Methods
            .Count(method => parms.IncludeMethods.Any(include => method.Name.Contains(include)));

        score.Score += matches > 0 ? matches : -matches;

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
    public static EMatchResult Exclude(TypeDef type, SearchParams parms, ScoringModel score)
    {
        if (parms.ExcludeMethods is null || parms.ExcludeMethods.Count == 0) return EMatchResult.Disabled;

        var matches = type.Methods
            .Count(method => parms.ExcludeMethods.Contains(method.Name));

        score.Score += matches > 0 ? -matches : 1;

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
    public static EMatchResult Count(TypeDef type, SearchParams parms, ScoringModel score)
    {
        if (parms.MethodCount is null) return EMatchResult.Disabled;

        var numMethods = type.Methods.Count - type.FindConstructors().Count();
        bool match = numMethods == parms.MethodCount;

        score.Score += match ? (int)parms.MethodCount : -(int)parms.MethodCount;

        score.FailureReason = match ? EFailureReason.None : EFailureReason.MethodsCount;

        return match
            ? EMatchResult.Match
            : EMatchResult.NoMatch;
    }
}