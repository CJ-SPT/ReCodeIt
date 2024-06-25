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
    public static void GetTypeByParameterCount(TypeDef type, SearchParams parms, ScoringModel score)
    {
        if (parms.ConstructorParameterCount is null) return;

        var match = type.FindConstructors()
            .Any(c => c.Parameters.Count() == parms.ConstructorParameterCount);

        if (match)
        {
            score.Score++;
            return;
        }

        score.NoMatchReasons.Add(ENoMatchReason.ConstructorParameterCount);
    }
}