using AssemblyRemapper.Enums;

namespace AssemblyRemapper.Utils;

internal static class EnumExtensions
{
    /// <summary>
    /// Returns a match from a list of match results
    /// </summary>
    /// <param name="matches"></param>
    /// <returns>highest EMatchResult</returns>
    public static EMatchResult GetMatch(this List<EMatchResult> matches)
    {
        if (matches.Contains(EMatchResult.Disabled)) { return EMatchResult.Disabled; }

        if (matches.Contains(EMatchResult.NoMatch)) { return EMatchResult.NoMatch; }

        if (matches.Contains(EMatchResult.Match)) { return EMatchResult.Match; }

        // default to disabled
        return EMatchResult.Disabled;
    }
}