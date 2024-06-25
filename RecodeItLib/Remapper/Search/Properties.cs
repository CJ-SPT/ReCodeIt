using dnlib.DotNet;
using ReCodeIt.Enums;
using ReCodeIt.Models;

namespace ReCodeIt.ReMapper.Search
{
    internal class Properties
    {
        public static EMatchResult Include(TypeDef type, SearchParams parms, ScoringModel score)
        {
            if (parms.IncludeProperties is null || parms.IncludeProperties.Count == 0) return EMatchResult.Disabled;

            foreach (var prop in type.Properties)
            {
                if (!parms.IncludeProperties.Contains(prop.Name)) continue;

                score.Score++;
                return EMatchResult.Match;
            }

            score.Score--;
            score.FailureReason = EFailureReason.PropertiesInclude;
            return EMatchResult.NoMatch;
        }

        public static EMatchResult Exclude(TypeDef type, SearchParams parms, ScoringModel score)
        {
            if (parms.ExcludeProperties is null || parms.ExcludeProperties.Count == 0) return EMatchResult.Disabled;

            foreach (var prop in type.Properties)
            {
                if (!parms.ExcludeProperties.Contains(prop.Name)) continue;

                score.Score--;
                score.FailureReason = EFailureReason.PropertiesExclude;
                return EMatchResult.NoMatch;
            }

            score.Score++;
            return EMatchResult.Match;
        }

        public static EMatchResult Count(TypeDef type, SearchParams parms, ScoringModel score)
        {
            if (parms.PropertyCount is null) return EMatchResult.Disabled;

            var match = type.Properties.Count() == parms.PropertyCount;

            score.Score += match ? (int)parms.PropertyCount : -(int)parms.PropertyCount;

            score.FailureReason = match
                ? EFailureReason.None
                : EFailureReason.PropertiesCount;

            return match
                ? EMatchResult.Match
                : EMatchResult.NoMatch;
        }
    }
}