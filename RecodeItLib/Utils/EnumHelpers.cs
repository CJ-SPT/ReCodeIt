using ReCodeIt.Enums;

namespace ReCodeIt.Utils;

internal static class EnumExtensions
{
    /// <summary>
    /// Returns a match from a list of match results
    /// </summary>
    /// <param name="matches"></param>
    /// <returns>highest EMatchResult</returns>
    public static EMatchResult GetMatch(this List<EMatchResult> matches)
    {
        // Prioritize returning matches
        if (matches.Contains(EMatchResult.Match)) { return EMatchResult.Match; }

        // Then NoMatches
        if (matches.Contains(EMatchResult.NoMatch)) { return EMatchResult.NoMatch; }

        // Then Disabled
        if (matches.Contains(EMatchResult.Disabled)) { return EMatchResult.Disabled; }

        // default to disabled
        return EMatchResult.Disabled;
    }
}