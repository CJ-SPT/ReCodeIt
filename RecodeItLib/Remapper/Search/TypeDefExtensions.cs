using dnlib.DotNet;
using ReCodeIt.Enums;
using ReCodeIt.Models;

namespace ReCodeIt.ReMapper.Search;

internal static class TypeDefExtensions
{
    public static void MatchIsEnum(this TypeDef type, SearchParams parms, ScoringModel score)
    {
        if (parms.IsEnum is null)
        {
            return;
        }

        if (type.IsEnum == parms.IsEnum)
        {
            score.Score++;
            return;
        }

        score.Score--;
        score.NoMatchReasons.Add(ENoMatchReason.IsEnum);
    }

    public static void MatchIsNested(this TypeDef type, SearchParams parms, ScoringModel score)
    {
        if (parms.IsNested is null)
        {
            return;
        }

        if (parms.ParentName is not null)
        {
            if (type.Name == parms.ParentName)
            {
                score.Score++;
                return;
            }
        }

        if (type.IsNested == parms.IsNested)
        {
            score.Score++;
            return;
        }

        score.NoMatchReasons.Add(ENoMatchReason.IsNested);
    }

    public static void MatchIsSealed(this TypeDef type, SearchParams parms, ScoringModel score)
    {
        if (parms.IsSealed is null)
        {
            return;
        }

        if (type.IsSealed == parms.IsSealed)
        {
            score.Score++;
            return;
        }

        score.NoMatchReasons.Add(ENoMatchReason.IsSealed);
    }

    public static void MatchIsDerived(this TypeDef type, SearchParams parms, ScoringModel score)
    {
        if (parms.IsDerived is null)
        {
            return;
        }

        if (type.BaseType is not null && (bool)parms.IsDerived)
        {
            if (type.BaseType.Name.Contains("Object")) { return; }

            if (type.BaseType?.Name == parms.IgnoreBaseClass)
            {
                score.NoMatchReasons.Add(ENoMatchReason.IsDerived);
                score.Score--;
                return;
            }

            if (type.BaseType?.Name == parms.MatchBaseClass)
            {
                score.Score++;
            }

            score.Score++;
            return;
        }

        score.Score--;
        score.NoMatchReasons.Add(ENoMatchReason.IsDerived);
    }

    public static void MatchHasGenericParameters(this TypeDef type, SearchParams parms, ScoringModel score)
    {
        if (parms.HasGenericParameters is null)
        {
            return;
        }

        if (type.HasGenericParameters == parms.HasGenericParameters)
        {
            score.Score++;
            return;
        }

        score.Score--;
        score.NoMatchReasons.Add(ENoMatchReason.HasGenericParameters);
    }

    public static void MatchHasAttribute(this TypeDef type, SearchParams parms, ScoringModel score)
    {
        if (parms.HasAttribute is null)
        {
            return;
        }

        if (type.HasCustomAttributes == parms.HasAttribute)
        {
            score.Score++;
            return;
        }

        score.NoMatchReasons.Add(ENoMatchReason.HasAttribute);
    }

    public static void MatchConstructors(this TypeDef type, SearchParams parms, ScoringModel score)
    {
        Constructors.GetTypeByParameterCount(type, parms, score);
    }

    /// <summary>
    /// Handle running all method matching routines
    /// </summary>
    /// <returns>Match if any search criteria met</returns>
    public static void MatchMethods(this TypeDef type, SearchParams parms, ScoringModel score)
    {
        Methods.Include(type, parms, score);
        Methods.Exclude(type, parms, score);
        Methods.Count(type, parms, score);
    }

    public static void MatchFields(this TypeDef type, SearchParams parms, ScoringModel score)
    {
        Fields.Include(type, parms, score);
        Fields.Exclude(type, parms, score);
        Fields.Count(type, parms, score);
    }

    public static void MatchProperties(this TypeDef type, SearchParams parms, ScoringModel score)
    {
        Properties.Include(type, parms, score);
        Properties.Exclude(type, parms, score);
        Properties.Count(type, parms, score);
    }

    public static void MatchNestedTypes(this TypeDef type, SearchParams parms, ScoringModel score)
    {
        NestedTypes.Include(type, parms, score);
        NestedTypes.Exclude(type, parms, score);
        NestedTypes.Count(type, parms, score);
    }
}