using dnlib.DotNet;
using ReCodeIt.Enums;
using ReCodeIt.Models;

namespace ReCodeIt.ReMapper.Search;

internal static class TypeDefExtensions
{
    public static EMatchResult MatchIsEnum(this TypeDef type, SearchParams parms, ScoringModel score)
    {
        if (parms.IsEnum is null)
        {
            return EMatchResult.Disabled;
        }

        if (type.IsEnum == parms.IsEnum)
        {
            score.Score++;
            return EMatchResult.Match;
        }

        score.FailureReason = EFailureReason.IsEnum;
        return EMatchResult.NoMatch;
    }

    public static EMatchResult MatchIsNested(this TypeDef type, SearchParams parms, ScoringModel score)
    {
        if (parms.IsNested is null)
        {
            return EMatchResult.Disabled;
        }

        if (parms.ParentName is not null)
        {
            if (type.Name == parms.ParentName)
            {
                score.Score++;
                return EMatchResult.Match;
            }
        }

        if (type.IsNested == parms.IsNested)
        {
            score.Score++;
            return EMatchResult.Match;
        }

        score.FailureReason = EFailureReason.IsNested;
        return EMatchResult.NoMatch;
    }

    public static EMatchResult MatchIsSealed(this TypeDef type, SearchParams parms, ScoringModel score)
    {
        if (parms.IsSealed is null)
        {
            return EMatchResult.Disabled;
        }

        if (type.IsSealed == parms.IsSealed)
        {
            score.Score++;
            return EMatchResult.Match;
        }

        score.FailureReason = EFailureReason.IsSealed;
        return EMatchResult.NoMatch;
    }

    public static EMatchResult MatchIsDerived(this TypeDef type, SearchParams parms, ScoringModel score)
    {
        if (parms.IsDerived is null)
        {
            return EMatchResult.Disabled;
        }

        if (type.BaseType is not null && (bool)parms.IsDerived is true)
        {
            if (type.BaseType.Name.Contains("Object")) { return EMatchResult.NoMatch; }

            score.Score++;
            return EMatchResult.Match;
        }

        if (type.BaseType?.Name == parms.MatchBaseClass)
        {
            return EMatchResult.Match;
        }

        if (type.BaseType?.Name == parms.IgnoreBaseClass)
        {
            score.FailureReason = EFailureReason.IsDerived;
            return EMatchResult.NoMatch;
        }

        score.FailureReason = EFailureReason.IsDerived;
        return EMatchResult.NoMatch;
    }

    public static EMatchResult MatchHasGenericParameters(this TypeDef type, SearchParams parms, ScoringModel score)
    {
        if (parms.HasGenericParameters is null)
        {
            return EMatchResult.Disabled;
        }

        if (type.HasGenericParameters == parms.HasGenericParameters)
        {
            score.Score++;
            return EMatchResult.Match;
        }

        score.FailureReason = EFailureReason.HasGenericParameters;
        return EMatchResult.NoMatch;
    }

    public static EMatchResult MatchHasAttribute(this TypeDef type, SearchParams parms, ScoringModel score)
    {
        if (parms.HasAttribute is null)
        {
            return EMatchResult.Disabled;
        }

        if (type.HasCustomAttributes == parms.HasAttribute)
        {
            score.Score++;
            return EMatchResult.Match;
        }

        score.FailureReason = EFailureReason.HasAttribute;
        return EMatchResult.NoMatch;
    }

    public static EMatchResult MatchConstructors(this TypeDef type, SearchParams parms, ScoringModel score)
    {
        var count = Constructors.GetTypeByParameterCount(type, parms, score);

        if (count == EMatchResult.Match)
        {
            return EMatchResult.Match;
        }

        if (count == EMatchResult.NoMatch)
        {
            return EMatchResult.NoMatch;
        }

        return EMatchResult.Disabled;
    }

    /// <summary>
    /// Handle running all method matching routines
    /// </summary>
    /// <returns>Match if any search criteria met</returns>
    public static EMatchResult MatchMethods(this TypeDef type, SearchParams parms, ScoringModel score)
    {
        var include = Methods.Include(type, parms, score);

        if (include is EMatchResult.Match)
        {
            return EMatchResult.Match;
        }

        var exclude = Methods.Exclude(type, parms, score);

        if (exclude is EMatchResult.Match)
        {
            return EMatchResult.Match;
        }

        var count = Methods.Count(type, parms, score);

        if (count == EMatchResult.Match)
        {
            return EMatchResult.Match;
        }

        if (include is EMatchResult.NoMatch || exclude is EMatchResult.NoMatch || count is EMatchResult.NoMatch)
        {
            return EMatchResult.NoMatch;
        }

        // return match if any condition matched
        return EMatchResult.Disabled;
    }

    public static EMatchResult MatchFields(this TypeDef type, SearchParams parms, ScoringModel score)
    {
        var include = Fields.Include(type, parms, score);

        if (include is EMatchResult.Match)
        {
            return EMatchResult.Match;
        }

        var exclude = Fields.Exclude(type, parms, score);

        if (exclude is EMatchResult.Match)
        {
            return EMatchResult.Match;
        }

        var count = Fields.Count(type, parms, score);

        if (count == EMatchResult.Match)
        {
            return EMatchResult.Match;
        }

        if (include is EMatchResult.NoMatch || exclude is EMatchResult.NoMatch || count is EMatchResult.NoMatch)
        {
            return EMatchResult.NoMatch;
        }

        return EMatchResult.Disabled;
    }

    public static EMatchResult MatchProperties(this TypeDef type, SearchParams parms, ScoringModel score)
    {
        var include = Properties.Include(type, parms, score);

        if (include is EMatchResult.Match)
        {
            return EMatchResult.Match;
        }

        var exclude = Properties.Exclude(type, parms, score);

        if (exclude is EMatchResult.Match)
        {
            return EMatchResult.Match;
        }

        var count = Properties.Count(type, parms, score);

        if (count == EMatchResult.Match)
        {
            return EMatchResult.Match;
        }

        if (include is EMatchResult.NoMatch || exclude is EMatchResult.NoMatch || count is EMatchResult.NoMatch)
        {
            return EMatchResult.NoMatch;
        }

        return EMatchResult.Disabled;
    }

    public static EMatchResult MatchNestedTypes(this TypeDef type, SearchParams parms, ScoringModel score)
    {
        var include = NestedTypes.Include(type, parms, score);

        if (include is EMatchResult.Match)
        {
            return EMatchResult.Match;
        }

        var exclude = NestedTypes.Exclude(type, parms, score);

        if (exclude is EMatchResult.Match)
        {
            return EMatchResult.Match;
        }

        var count = NestedTypes.Count(type, parms, score);

        if (count == EMatchResult.Match)
        {
            return EMatchResult.Match;
        }

        if (include is EMatchResult.NoMatch || exclude is EMatchResult.NoMatch || count is EMatchResult.NoMatch)
        {
            return EMatchResult.NoMatch;
        }

        return EMatchResult.Disabled;
    }
}