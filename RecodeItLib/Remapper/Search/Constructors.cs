using dnlib.DotNet;
using ReCodeIt.Enums;
using ReCodeIt.Models;

namespace ReCodeIt.ReMapper.Search;

internal static class Constructors
{
    /// <summary>
    /// Search for types with a constructor of a given length
    /// </summary>
    /// <param name="parms"></param>
    /// <param name="score"></param>
    /// <returns>Match if constructor parameters matches</returns>
    public static EMatchResult GetTypeByParameterCount(TypeDef type, SearchParams parms, ScoringModel score)
    {
        if (parms.ConstructorParameterCount is null) return EMatchResult.Disabled;

        var match = type.FindConstructors()
            .Any(c => c.Parameters.Count() == parms.ConstructorParameterCount);

        score.FailureReason = match
            ? EFailureReason.None
            : EFailureReason.ConstructorParameterCount;

        return match
            ? EMatchResult.Match
            : EMatchResult.NoMatch;
    }
}