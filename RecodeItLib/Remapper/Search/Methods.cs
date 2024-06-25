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
            if (parms.IncludeMethods.Contains(method.Name))
            {
                count++;
                score.Score++;
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

        score.Score--;
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
            if (!parms.ExcludeMethods.Contains(method.ResolveMethodDef().Name)) continue;

            score.Score--;
            score.NoMatchReasons.Add(ENoMatchReason.MethodsExclude);
            return;
        }

        score.Score++;
    }

    /// <summary>
    /// Returns a match if the type has the provided number of methods
    /// </summary>
    /// <param name="type"></param>
    /// <param name="parms"></param>
    /// <param name="score"></param>
    /// <returns></returns>
    public static void Count(TypeDef type, SearchParams parms, ScoringModel score)
    {
        if (parms.MethodCount is null) return;

        var numMethods = type.Methods.Count - type.FindConstructors().Count();
        bool match = numMethods == parms.MethodCount;

        score.Score += match ? (int)parms.MethodCount : -(int)parms.MethodCount;

        if (!match)
        {
            score.NoMatchReasons.Add(ENoMatchReason.MethodsCount);
        }
    }
}