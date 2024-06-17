using Mono.Cecil;
using MoreLinq;
using ReCodeIt.Enums;
using ReCodeIt.Models;

namespace ReCodeIt.ReMapper.Search
{
    internal class Properties
    {
        public static EMatchResult Include(TypeDefinition type, SearchParams parms, ScoringModel score)
        {
            if (parms.IncludeProperties is null || parms.IncludeProperties.Count == 0) return EMatchResult.Disabled;

            var matches = type.Properties
                .Where(property => parms.IncludeProperties.Contains(property.Name))
                .Count();
            score.Score += matches;

            score.FailureReason = matches > 0 ? EFailureReason.None : EFailureReason.PropertiesInclude;

            return matches > 0
                ? EMatchResult.Match
                : EMatchResult.NoMatch;
        }

        public static EMatchResult Exclude(TypeDefinition type, SearchParams parms, ScoringModel score)
        {
            if (parms.ExcludeProperties is null || parms.ExcludeProperties.Count == 0) return EMatchResult.Disabled;

            var matches = type.Properties
                .Where(property => parms.ExcludeProperties.Contains(property.Name))
                .Count();

            score.Score += matches;

            score.FailureReason = matches > 0 ? EFailureReason.None : EFailureReason.PropertiesExclude;

            return matches > 0
                ? EMatchResult.NoMatch
                : EMatchResult.Match;
        }

        public static EMatchResult Count(TypeDefinition type, SearchParams parms, ScoringModel score)
        {
            if (parms.PropertyCount is null) return EMatchResult.Disabled;

            var match = type.Properties.Exactly((int)parms.PropertyCount);

            if (match) { score.Score++; }

            score.FailureReason = match ? EFailureReason.None : EFailureReason.PropertiesCount;

            return match
                ? EMatchResult.Match
                : EMatchResult.NoMatch;
        }
    }
}