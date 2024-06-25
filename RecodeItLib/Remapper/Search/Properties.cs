using dnlib.DotNet;
using ReCodeIt.Enums;
using ReCodeIt.Models;

namespace ReCodeIt.ReMapper.Search
{
    internal class Properties
    {
        public static void Include(TypeDef type, SearchParams parms, ScoringModel score)
        {
            if (parms.IncludeProperties is null || parms.IncludeProperties.Count == 0) return;

            int count = 0;

            foreach (var prop in type.Properties)
            {
                if (parms.IncludeProperties.Contains(prop.Name))
                {
                    count++;
                    score.Score++;
                }
            }

            if (count > 0)
            {
                return;
            }

            score.NoMatchReasons.Add(ENoMatchReason.PropertiesInclude);
        }

        public static void Exclude(TypeDef type, SearchParams parms, ScoringModel score)
        {
            if (parms.ExcludeProperties is null || parms.ExcludeProperties.Count == 0) return;

            foreach (var prop in type.Properties)
            {
                if (!parms.ExcludeProperties.Contains(prop.Name)) continue;

                score.NoMatchReasons.Add(ENoMatchReason.PropertiesExclude);
                return;
            }

            score.Score++;
        }

        public static void Count(TypeDef type, SearchParams parms, ScoringModel score)
        {
            if (parms.PropertyCount is null) return;

            var match = type.Properties.Count() == parms.PropertyCount;

            score.Score += match ? (int)parms.PropertyCount : 0;

            if (!match)
            {
                score.NoMatchReasons.Add(ENoMatchReason.PropertiesCount);
            }
        }
    }
}