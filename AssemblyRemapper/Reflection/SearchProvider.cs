using AssemblyRemapper.Enums;
using AssemblyRemapper.Models;
using Mono.Cecil;
using Mono.Cecil.Rocks;

namespace AssemblyRemapper.Reflection;

internal static class SearchProvider
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
            score.Score += 1;
            return EMatchResult.Match;
        }

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
            score.Score += 1;
            return EMatchResult.Match;
        }

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
            score.Score += 1;
            return EMatchResult.Match;
        }

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
            score.Score += 1;
            return EMatchResult.Match;
        }

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
            score.Score += 1;
            return EMatchResult.Match;
        }

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
            score.Score += 1;
            return EMatchResult.Match;
        }

        return EMatchResult.NoMatch;
    }

    public static EMatchResult MatchIsGeneric(this TypeDefinition type, SearchParams parms, ScoringModel score)
    {
        if (parms.HasGenericParameters is null)
        {
            return EMatchResult.Disabled;
        }

        if (type.HasGenericParameters == parms.HasGenericParameters)
        {
            score.Score += 1;
            return EMatchResult.Match;
        }

        return EMatchResult.NoMatch;
    }

    public static EMatchResult MatchIsPublic(this TypeDefinition type, SearchParams parms, ScoringModel score)
    {
        if (parms.IsPublic is null)
        {
            return EMatchResult.Disabled;
        }

        var boolToCheck = parms.IsPublic == true ? type.IsPublic : type.IsNotPublic;

        if (boolToCheck == !parms.IsPublic)
        {
            score.Score += 1;
            return EMatchResult.Match;
        }

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
            score.Score += 1;
            return EMatchResult.Match;
        }

        return EMatchResult.NoMatch;
    }

    public static EMatchResult MatchMethods(this TypeDefinition type, SearchParams parms, ScoringModel score)
    {
        if (parms.MethodNamesToMatch.Count == 0) { return EMatchResult.Disabled; }

        if (type.HasMethods)
        {
            // `*` is the wildcard to ignore all methods that exist on types
            if (parms.MethodNamesToIgnore.Contains("*"))
            {
                return EMatchResult.NoMatch;
            }
        }

        var matchCount = 0;

        foreach (var method in type.Methods)
        {
            if (parms.MethodNamesToIgnore.Contains(method.Name))
            {
                // Type contains blacklisted method
                return EMatchResult.NoMatch;
            }

            foreach (var name in parms.MethodNamesToMatch)
            {
                if (method.Name == name)
                {
                    matchCount += 1;
                    score.Score += 2;
                    continue;
                }
            }
        }

        return matchCount > 0 ? EMatchResult.Match : EMatchResult.NoMatch;
    }

    public static EMatchResult MatchFields(this TypeDefinition type, SearchParams parms, ScoringModel score)
    {
        if (parms.FieldNamesToMatch.Count == 0) { return EMatchResult.Disabled; }

        if (type.HasFields)
        {
            // `*` is the wildcard to ignore all fields that exist on types
            if (parms.FieldNamesToIgnore.Contains("*"))
            {
                return EMatchResult.NoMatch;
            }
        }

        int matchCount = 0;

        foreach (var field in type.Fields)
        {
            if (parms.FieldNamesToIgnore.Contains(field.Name))
            {
                // Type contains blacklisted field
                return EMatchResult.NoMatch;
            }

            if (parms.FieldNamesToMatch.Contains(field.Name))
            {
                matchCount++;
                score.Score += 2;
            }
        }

        return matchCount > 0 ? EMatchResult.Match : EMatchResult.NoMatch;
    }

    public static EMatchResult MatchProperties(this TypeDefinition type, SearchParams parms, ScoringModel score)
    {
        if (parms.PropertyNamesToMatch.Count == 0) { return EMatchResult.Disabled; }

        if (type.HasProperties)
        {
            // `*` is the wildcard to ignore all fields that exist on types
            if (parms.PropertyNamesToIgnore.Contains("*"))
            {
                return EMatchResult.NoMatch;
            }
        }

        int matchCount = 0;

        foreach (var property in type.Properties)
        {
            if (parms.PropertyNamesToIgnore.Contains(property.Name))
            {
                // Type contains blacklisted property
                return EMatchResult.NoMatch;
            }

            if (parms.PropertyNamesToMatch.Contains(property.Name))
            {
                matchCount++;
                score.Score += 2;
            }
        }

        return matchCount > 0 ? EMatchResult.Match : EMatchResult.NoMatch;
    }

    public static EMatchResult MatchNestedTypes(this TypeDefinition type, SearchParams parms, ScoringModel score)
    {
        if (parms.NestedTypesToMatch.Count == 0) { return EMatchResult.Disabled; }

        if (type.HasNestedTypes)
        {
            // `*` is the wildcard to ignore all fields that exist on types
            if (parms.NestedTypesToIgnore.Contains("*"))
            {
                return EMatchResult.NoMatch;
            }
        }

        int matchCount = 0;

        foreach (var nestedType in type.NestedTypes)
        {
            if (parms.NestedTypesToIgnore.Contains(nestedType.Name))
            {
                // Type contains blacklisted nested type
                return EMatchResult.NoMatch;
            }

            if (parms.NestedTypesToMatch.Contains(nestedType.Name))
            {
                matchCount++;
                score.Score += 2;
            }
        }

        return matchCount > 0 ? EMatchResult.Match : EMatchResult.NoMatch;
    }
}