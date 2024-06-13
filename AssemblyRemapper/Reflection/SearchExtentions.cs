using AssemblyRemapper.Enums;
using AssemblyRemapper.Models;
using Mono.Cecil;
using Mono.Cecil.Rocks;

namespace AssemblyRemapper.Reflection;

internal static class SearchExtentions
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

        if (type.BaseType != null && (bool)parms.IsDerived)
        {
            score.Score++;
            return EMatchResult.Match;
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
        if (parms.IsPublic is null)
        {
            return EMatchResult.Disabled;
        }

        if (parms.IsPublic is false && type.IsNotPublic is true)
        {
            score.Score++;
            return EMatchResult.Match;
        }
        else if (parms.IsPublic is true && type.IsPublic is true)
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

        if (type.HasCustomAttributes == parms.HasAttribute)
        {
            score.Score++;
            return EMatchResult.Match;
        }

        score.FailureReason = EFailureReason.HasAttribute;
        return EMatchResult.NoMatch;
    }

    public static EMatchResult MatchMethods(this TypeDefinition type, SearchParams parms, ScoringModel score)
    {
        // We're not searching for methods and this type contains methods
        if (parms.MethodNamesToMatch.Count == 0 && parms.MethodNamesToIgnore.Count == 0)
        {
            return type.HasMethods
                ? EMatchResult.NoMatch
                : EMatchResult.Match;
        }

        var skippAll = parms.MethodNamesToIgnore.Contains("*");

        // The type has methods and we dont want any
        if (type.HasMethods is true && skippAll is true)
        {
            foreach (var method in type.Methods)
            {
                if (method.Name == ".ctor")
                {
                    continue;
                }

                score.Score--;
                return EMatchResult.NoMatch;
            }

            score.Score++;
            return EMatchResult.Match;
        }

        var matchCount = 0;

        foreach (var method in type.Methods)
        {
            // Type contains a method we dont want
            if (parms.MethodNamesToIgnore.Contains(method.Name))
            {
                score.FailureReason = EFailureReason.HasMethods;
                return EMatchResult.NoMatch;
            }

            foreach (var name in parms.MethodNamesToMatch)
            {
                // Method name match
                if (method.Name == name)
                {
                    matchCount += 1;
                    score.Score++;
                }
            }
        }

        return matchCount > 0 ? EMatchResult.Match : EMatchResult.NoMatch;
    }

    public static EMatchResult MatchFields(this TypeDefinition type, SearchParams parms, ScoringModel score)
    {
        if (parms.FieldNamesToMatch.Count == 0 && parms.FieldNamesToIgnore.Count == 0)
        {
            return EMatchResult.Disabled;
        }

        // `*` is the wildcard to ignore all fields that exist on types
        if (!type.HasFields && parms.FieldNamesToIgnore.Contains("*"))
        {
            return EMatchResult.Match;
        }

        int matchCount = 0;

        foreach (var field in type.Fields)
        {
            if (parms.FieldNamesToIgnore.Contains(field.Name))
            {
                // Type contains blacklisted field
                score.FailureReason = EFailureReason.HasFields;
                return EMatchResult.NoMatch;
            }

            if (parms.FieldNamesToMatch.Contains(field.Name))
            {
                matchCount++;
                score.Score++;
            }
        }

        return matchCount > 0 ? EMatchResult.Match : EMatchResult.NoMatch;
    }

    public static EMatchResult MatchProperties(this TypeDefinition type, SearchParams parms, ScoringModel score)
    {
        if (parms.PropertyNamesToMatch.Count == 0 && parms.PropertyNamesToIgnore.Count == 0)
        {
            return EMatchResult.Disabled;
        }

        // `*` is the wildcard to ignore all fields that exist on types
        if (!type.HasProperties && parms.PropertyNamesToIgnore.Contains("*"))
        {
            score.Score++;
            return EMatchResult.Match;
        }

        int matchCount = 0;

        foreach (var property in type.Properties)
        {
            if (parms.PropertyNamesToIgnore.Contains(property.Name))
            {
                // Type contains blacklisted property
                score.FailureReason = EFailureReason.HasProperties;
                return EMatchResult.NoMatch;
            }

            if (parms.PropertyNamesToMatch.Contains(property.Name))
            {
                matchCount++;
                score.Score++;
            }
        }

        return matchCount > 0 ? EMatchResult.Match : EMatchResult.NoMatch;
    }

    public static EMatchResult MatchNestedTypes(this TypeDefinition type, SearchParams parms, ScoringModel score)
    {
        if (parms.NestedTypesToMatch.Count == 0 && parms.NestedTypesToIgnore.Count == 0)
        {
            return EMatchResult.Disabled;
        }

        // `*` is the wildcard to ignore all fields that exist on types
        if (type.HasNestedTypes && parms.NestedTypesToIgnore.Contains("*"))
        {
            score.FailureReason = EFailureReason.HasNestedTypes;
            return EMatchResult.NoMatch;
        }

        int matchCount = 0;

        foreach (var nestedType in type.NestedTypes)
        {
            if (parms.NestedTypesToIgnore.Contains(nestedType.Name))
            {
                // Type contains blacklisted nested type
                score.FailureReason = EFailureReason.HasNestedTypes;
                return EMatchResult.NoMatch;
            }

            if (parms.NestedTypesToMatch.Contains(nestedType.Name))
            {
                matchCount++;
                score.Score++;
            }
        }

        return matchCount > 0 ? EMatchResult.Match : EMatchResult.NoMatch;
    }
}