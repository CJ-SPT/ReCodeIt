using Mono.Cecil;
using Mono.Cecil.Rocks;
using ReCodeIt.Enums;
using ReCodeIt.Models;
using ReCodeIt.Utils;

namespace ReCodeIt.ReMapper.Search;

internal static class TypeDefExtensions
{
    public static EMatchResult MatchIsAbstract(this TypeDefinition type, SearchParams parms, ScoringModel score)
    {
        if (parms.IsAbstract is null)
        {
            return EMatchResult.Disabled;
        }

        // Interfaces cannot be abstract, and abstract cannot be static
        if (type.IsInterface || type.GetStaticConstructor() is not null)
        {
            score.FailureReason = EFailureReason.IsAbstract;
            return EMatchResult.NoMatch;
        }

        if (type.IsAbstract == parms.IsAbstract)
        {
            score.Score++;
            return EMatchResult.Match;
        }

        score.FailureReason = EFailureReason.IsAbstract;
        return EMatchResult.NoMatch;
    }

    public static EMatchResult MatchIsEnum(this TypeDefinition type, SearchParams parms, ScoringModel score)
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

    public static EMatchResult MatchIsNested(this TypeDefinition type, SearchParams parms, ScoringModel score)
    {
        if (parms.IsNested is null)
        {
            return EMatchResult.Disabled;
        }

        if (type.IsNested == parms.IsNested)
        {
            score.Score++;
            Logger.Log($"Match {type.Name}");
            Logger.Log($"Match {parms.IsNested}");
            Logger.Log($"type: {type.IsNested} \n");
            return EMatchResult.Match;
        }

        score.FailureReason = EFailureReason.IsNested;
        return EMatchResult.NoMatch;
    }

    public static EMatchResult MatchIsSealed(this TypeDefinition type, SearchParams parms, ScoringModel score)
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

    public static EMatchResult MatchIsDerived(this TypeDefinition type, SearchParams parms, ScoringModel score)
    {
        if (parms.IsDerived is null)
        {
            return EMatchResult.Disabled;
        }

        if (type.BaseType is not null && (bool)parms.IsDerived is true)
        {
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

    public static EMatchResult MatchIsInterface(this TypeDefinition type, SearchParams parms, ScoringModel score)
    {
        if (parms.IsInterface is null)
        {
            return EMatchResult.Disabled;
        }

        if (type.IsInterface == parms.IsInterface)
        {
            score.Score++;
            return EMatchResult.Match;
        }

        score.FailureReason = EFailureReason.IsInterface;
        return EMatchResult.NoMatch;
    }

    public static EMatchResult MatchHasGenericParameters(this TypeDefinition type, SearchParams parms, ScoringModel score)
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

    public static EMatchResult MatchIsPublic(this TypeDefinition type, SearchParams parms, ScoringModel score)
    {
        if (parms.IsPublic == null)
        {
            return EMatchResult.Disabled;
        }

        if (parms.IsPublic == false && type.IsNotPublic)
        {
            score.Score++;

            return EMatchResult.Match;
        }
        else if ((bool)parms.IsPublic && type.IsPublic)
        {
            score.Score++;
            return EMatchResult.Match;
        }

        score.FailureReason = EFailureReason.IsPublic;
        return EMatchResult.NoMatch;
    }

    public static EMatchResult MatchHasAttribute(this TypeDefinition type, SearchParams parms, ScoringModel score)
    {
        if (parms.HasAttribute is null)
        {
            return EMatchResult.Disabled;
        }
        var attrs = type.CustomAttributes;

        if (type.HasCustomAttributes == parms.HasAttribute)
        {
            score.Score++;
            return EMatchResult.Match;
        }

        score.FailureReason = EFailureReason.HasAttribute;
        return EMatchResult.NoMatch;
    }

    public static EMatchResult MatchConstructors(this TypeDefinition type, SearchParams parms, ScoringModel score)
    {
        var matches = new List<EMatchResult> { };

        if (parms.ConstructorParameterCount is not null)
        {
            matches.Add(Constructors.GetTypeByParameterCount(type, parms, score));
        }

        return matches.GetMatch();
    }

    /// <summary>
    /// Handle running all method matching routines
    /// </summary>
    /// <returns>Match if any search criteria met</returns>
    public static EMatchResult MatchMethods(this TypeDefinition type, SearchParams parms, ScoringModel score)
    {
        var matches = new List<EMatchResult>
        {
            Methods.IncludeMethods(type, parms, score),
            Methods.ExcludeMethods(type, parms, score),
            Methods.MatchMethodCount(type, parms, score)
        };

        // return match if any condition matched
        return matches.GetMatch();
    }

    public static EMatchResult MatchFields(this TypeDefinition type, SearchParams parms, ScoringModel score)
    {
        var matches = new List<EMatchResult>
        {
            Fields.IncludeFields(type, parms, score),
            Fields.ExcludeFields(type, parms, score),
            Fields.MatchFieldCount(type, parms, score)
        };

        // return match if any condition matched
        return matches.GetMatch();
    }

    public static EMatchResult MatchProperties(this TypeDefinition type, SearchParams parms, ScoringModel score)
    {
        var matches = new List<EMatchResult>
        {
            Properties.IncludeProperties(type, parms, score),
            Properties.ExcludeProperties(type, parms, score),
            Properties.MatchPropertyCount(type, parms, score)
        };

        // return match if any condition matched
        return matches.GetMatch();
    }

    public static EMatchResult MatchNestedTypes(this TypeDefinition type, SearchParams parms, ScoringModel score)
    {
        var matches = new List<EMatchResult>
        {
            NestedTypes.IncludeNestedTypes(type, parms, score),
            NestedTypes.ExcludeNestedTypes(type, parms, score),
            NestedTypes.MatchNestedTypeCount(type, parms, score)
        };

        // return match if any condition matched
        return matches.GetMatch();
    }
}