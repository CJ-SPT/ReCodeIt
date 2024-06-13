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
        if (parms.ConstructorParameterCount is null) return EMatchResult.Disabled;

        var match = type.GetConstructors()
            .Where(c => c.Parameters.Count == parms.ConstructorParameterCount)
            .Any();

        return match
            ? EMatchResult.Match
            : EMatchResult.NoMatch;
    }
}