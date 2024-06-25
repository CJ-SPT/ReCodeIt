using dnlib.DotNet;
using MoreLinq;
using ReCodeIt.Enums;
using ReCodeIt.Models;

namespace ReCodeIt.ReMapper.Search
{
    internal class Properties
    {
        public static EMatchResult Include(TypeDef type, SearchParams parms, ScoringModel score)
        {
            if (parms.IncludeProperties is null || parms.IncludeProperties.Count == 0) return EMatchResult.Disabled;

            var matches = type.Properties
                .Count(property => parms.IncludeProperties.Contains(property.Name));

            score.Score += matches > 0 ? matches : -matches;

            score.FailureReason = matches > 0 ? EFailureReason.None : EFailureReason.PropertiesInclude;

            return matches > 0
                ? EMatchResult.Match
                : EMatchResult.NoMatch;
        }

        public static EMatchResult Exclude(TypeDef type, SearchParams parms, ScoringModel score)
        {
            if (parms.ExcludeProperties is null || parms.ExcludeProperties.Count == 0) return EMatchResult.Disabled;

            var matches = type.Properties
                .Count(property => parms.ExcludeProperties.Contains(property.Name));

            score.Score += matches > 0 ? -matches : 1;

            score.FailureReason = matches > 0 ? EFailureReason.PropertiesExclude : EFailureReason.None;

            return matches > 0
                ? EMatchResult.NoMatch
                : EMatchResult.Match;
        }

        public static EMatchResult Count(TypeDef type, SearchParams parms, ScoringModel score)
        {
            if (parms.PropertyCount is null) return EMatchResult.Disabled;

            var match = type.Properties.Exactly((int)parms.PropertyCount);

            score.Score += match ? (int)parms.PropertyCount : -(int)parms.PropertyCount;

            return match
                ? EMatchResult.Match
                : EMatchResult.NoMatch;
        }
    }
}