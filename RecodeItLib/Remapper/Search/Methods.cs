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
    public static void Include(TypeDef type, SearchParams parms, ScoringModel score)
    {
        if (parms.IncludeMethods is null || parms.IncludeMethods.Count == 0) return;

        int count = 0;

        foreach (var method in type.Methods)
        {
            if (parms.IncludeMethods.Contains(method.Name.String))
            {
                count++;
                score.Score++;
                continue;
            }

            if (parms.IncludeMethods.Contains(method.Name.String.Split(".").Last()))
            {
                count++;
                score.Score++;
            }
        }

        if (count > 0)
        {
            return;
        }

        score.NoMatchReasons.Add(ENoMatchReason.MethodsInclude);
    }

    /// <summary>
    /// Returns a match on all types without methods
    /// </summary>
    /// <param name="type"></param>
    /// <param name="parms"></param>
    /// <param name="score"></param>
    /// <returns>Match if type has no methods</returns>
    public static void Exclude(TypeDef type, SearchParams parms, ScoringModel score)
    {
        if (parms.ExcludeMethods is null || parms.ExcludeMethods.Count == 0) return;

        foreach (var method in type.Methods)
        {
            if (!parms.ExcludeMethods.Contains(method.Name.String)) continue;

            score.NoMatchReasons.Add(ENoMatchReason.MethodsExclude);
            return;
        }

        score.Score++;
    }
}