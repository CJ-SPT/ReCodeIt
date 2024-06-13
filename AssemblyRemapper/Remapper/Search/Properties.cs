using AssemblyRemapper.Enums;
using AssemblyRemapper.Models;
using Mono.Cecil;
using MoreLinq;

namespace AssemblyRemapper.Remapper.Search
{
    internal class Properties
    {
        public static EMatchResult IncludeProperties(TypeDefinition type, SearchParams parms, ScoringModel score)
        {
            if (parms.MatchProperties is null || parms.MatchProperties.Count == 0) return EMatchResult.Disabled;

            var matches = type.Properties
                .Where(property => parms.MatchProperties.Contains(property.Name))
                .Count();

            score.Score += matches;

            return matches > 0
                ? EMatchResult.Match
                : EMatchResult.NoMatch;
        }

        public static EMatchResult ExcludeProperties(TypeDefinition type, SearchParams parms, ScoringModel score)
        {
            if (parms.IgnorePropterties is null || parms.IgnorePropterties.Count == 0) return EMatchResult.Disabled;

            var matches = type.Properties
                .Where(property => parms.IgnorePropterties.Contains(property.Name))
                .Count();

            score.Score += matches;

            return matches > 0
                ? EMatchResult.NoMatch
                : EMatchResult.Match;
        }

        public static EMatchResult MatchPropertyCount(TypeDefinition type, SearchParams parms, ScoringModel score)
        {
            if (parms.PropertyCount is null) return EMatchResult.Disabled;

            var match = type.Properties.Exactly((int)parms.PropertyCount);

            if (match) { score.Score++; }

            return match
                ? EMatchResult.Match
                : EMatchResult.NoMatch;
        }
    }
}