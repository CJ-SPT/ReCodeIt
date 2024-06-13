using AssemblyRemapper.Enums;
using AssemblyRemapper.Models;
using Mono.Cecil;
using Mono.Cecil.Rocks;

namespace AssemblyRemapper.Remapper.Search;

internal static class Constructors
{
    /// <summary>
    /// Search for types with a constructor of a given length
    /// </summary>
    /// <param name="parms"></param>
    /// <param name="score"></param>
    /// <returns>Match if constructor parameters matches</returns>
    public static EMatchResult GetTypeByParameterCount(TypeDefinition type, SearchParams parms, ScoringModel score)
    {
        if (parms.ConstructorParameterCount is null)
        {
            return EMatchResult.Disabled;
        }

        var constructors = type.GetConstructors();

        foreach (var constructor in constructors)
        {
            if (constructor.Parameters.Count == parms.ConstructorParameterCount)
            {
                score.Score++;
                return EMatchResult.Match;
            }
        }

        return EMatchResult.NoMatch;
    }
}