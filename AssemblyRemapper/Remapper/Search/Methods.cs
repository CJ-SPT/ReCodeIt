using AssemblyRemapper.Enums;
using AssemblyRemapper.Models;
using Mono.Cecil;
using Mono.Cecil.Rocks;

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
    public static EMatchResult GetTypeWithMethods(TypeDefinition type, SearchParams parms, ScoringModel score)
    {
        var matchCount = 0;

        // Handle match methods
        foreach (var method in type.Methods)
        {
            foreach (var name in parms.MatchMethods)
            {
                // Method name match
                if (method.Name == name)
                {
                    matchCount += 1;
                    score.Score++;
                }
            }
        }

        return matchCount > 0
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
    public static EMatchResult GetTypeWithoutMethods(TypeDefinition type, SearchParams parms, ScoringModel score)
    {
        if (parms.IgnoreMethods is null) return EMatchResult.Disabled;

        var skippAll = parms.IgnoreMethods.Contains("*");
        var methodCount = type.Methods.Count - type.GetConstructors().Count();

        // Subtract method count from constructor count to check for real methods
        if (methodCount is 0 && skippAll is true)
        {
            score.Score++;
            return EMatchResult.Match;
        }

        return EMatchResult.NoMatch;
    }

    /// <summary>
    /// Get all types without the specified method
    /// </summary>
    /// <param name="type"></param>
    /// <param name="parms"></param>
    /// <param name="score"></param>
    /// <returns></returns>
    public static EMatchResult GetTypeWithNoMethods(TypeDefinition type, SearchParams parms, ScoringModel score)
    {
        if (parms.IgnoreMethods is null) { return EMatchResult.Disabled; }

        foreach (var method in type.Methods)
        {
            if (parms.IgnoreMethods.Contains(method.Name))
            {
                return EMatchResult.NoMatch;
            }
        }

        score.Score++;
        return EMatchResult.Match;
    }

    public static EMatchResult GetTypeByNumberOfMethods(TypeDefinition type, SearchParams parms, ScoringModel score)
    {
        if (parms.MethodCount is null) { return EMatchResult.Disabled; }

        if (type.Methods.Count == parms.MethodCount)
        {
            score.Score++;
            return EMatchResult.Match;
        }

        return EMatchResult.NoMatch;
    }
}