using dnlib.DotNet;
using ReCodeIt.Enums;
using ReCodeIt.Models;

namespace ReCodeIt.ReMapper.Search;

internal class NestedTypes
{
    public static void Include(TypeDef type, SearchParams parms, ScoringModel score)
    {
        if (parms.IncludeNestedTypes is null || parms.IncludeNestedTypes.Count == 0) return;

        foreach (var nt in type.NestedTypes)
        {
            var ntName = nt.Name.String;

            if (!parms.IncludeNestedTypes.Contains(ntName)) { continue; }

            score.Score++;
            return;
        }

        score.NoMatchReasons.Add(ENoMatchReason.NestedTypeInclude);
    }

    public static void Exclude(TypeDef type, SearchParams parms, ScoringModel score)
    {
        if (parms.ExcludeNestedTypes is null || parms.ExcludeNestedTypes.Count == 0) return;

        foreach (var nt in type.NestedTypes)
        {
            if (!parms.ExcludeNestedTypes.Contains(nt.Name)) continue;

            score.Score--;
            score.NoMatchReasons.Add(ENoMatchReason.NestedTypeExclude);
            return;
        }

        score.Score++;
    }

    public static void Count(TypeDef type, SearchParams parms, ScoringModel score)
    {
        if (parms.NestedTypeCount is null) return;

        var match = type.NestedTypes.Count() == parms.NestedTypeCount;

        score.Score += match ? type.NestedTypes.Count : -type.NestedTypes.Count - 1;

        if (!match)
        {
            score.NoMatchReasons.Add(ENoMatchReason.NestedTypeCount);
        }
    }
}