using AssemblyRemapper.Models;
using AssemblyRemapper.Utils;
using Mono.Cecil;
using Mono.Cecil.Rocks;

namespace AssemblyRemapper.Reflection;

internal static class SearchProvider
{
    public static int MatchCount { get; private set; }

    public static EMatchResult MatchIsAbstract(this TypeDefinition type, RemapSearchParams parms, ScoringModel score)
    {
        if (parms.IsAbstract is null)
        {
            return EMatchResult.Disabled;
        }

        // Interfaces cannot be abstract, and abstract cannot be static
        if (type.IsInterface || type.GetStaticConstructor() != null)
        {
            Logger.Log($"Searching for an abstract type, skipping interface or static");
            return EMatchResult.NoMatch;
        }

        if (type.IsAbstract != parms.IsAbstract)
        {
            score.Score += 1;
            Logger.Log($"Matched `{type.Name}` on search `{score.ProposedNewName}` : IsAbstract");
            return EMatchResult.Match;
        }

        Logger.Log($"Skipping `{type.Name}` on search `{score.ProposedNewName}` IsAbstract does not match.");
        return EMatchResult.NoMatch;
    }

    public static EMatchResult MatchIsEnum(this TypeDefinition type, RemapSearchParams parms, ScoringModel score)
    {
        if (parms.IsEnum is null)
        {
            return EMatchResult.Disabled;
        }

        if (type.IsEnum == parms.IsEnum)
        {
            score.Score += 1;
            Logger.Log($"Matched `{type.Name}` on search `{score.ProposedNewName}` : IsEnum");
            return EMatchResult.Match;
        }

        Logger.Log($"Skipping `{type.Name}` on search `{score.ProposedNewName}` IsEnum does not match.");
        return EMatchResult.NoMatch;
    }

    public static EMatchResult MatchIsNested(this TypeDefinition type, RemapSearchParams parms, ScoringModel score)
    {
        if (parms.IsNested is null)
        {
            return EMatchResult.Disabled;
        }

        if (type.IsNested == parms.IsNested)
        {
            score.Score += 1;
            Logger.Log($"Matched `{type.Name}` on search `{score.ProposedNewName}` : IsNested");
            return EMatchResult.Match;
        }

        Logger.Log($"Skipping `{type.Name}` on search `{score.ProposedNewName}` IsNested does not match.");
        return EMatchResult.NoMatch;
    }

    public static EMatchResult MatchIsSealed(this TypeDefinition type, RemapSearchParams parms, ScoringModel score)
    {
        if (parms.IsSealed is null)
        {
            return EMatchResult.Disabled;
        }

        if (type.IsSealed == parms.IsSealed)
        {
            score.Score += 1;
            Logger.Log($"Matched `{type.Name}` on search `{score.ProposedNewName}` : IsSealed");
            return EMatchResult.Match;
        }

        Logger.Log($"Skipping `{type.Name}` on search `{score.ProposedNewName}` IsSealed does not match.");
        return EMatchResult.NoMatch;
    }

    public static EMatchResult MatchIsDerived(this TypeDefinition type, RemapSearchParams parms, ScoringModel score)
    {
        if (parms.IsDerived is null)
        {
            return EMatchResult.Disabled;
        }

        if (type.BaseType != null && (bool)parms.IsDerived)
        {
            score.Score += 1;
            Logger.Log($"Matched `{type.Name}` on search `{score.ProposedNewName}` : IsDerived");
            return EMatchResult.Match;
        }

        Logger.Log($"Skipping `{type.Name}` on search `{score.ProposedNewName}` IsDerived does not match.");
        return EMatchResult.NoMatch;
    }

    public static EMatchResult MatchIsInterface(this TypeDefinition type, RemapSearchParams parms, ScoringModel score)
    {
        if (parms.IsInterface is null)
        {
            return EMatchResult.Disabled;
        }

        // Interfaces cannot be a class
        if (type.IsClass)
        {
            return EMatchResult.NoMatch;
        }

        if (type.IsInterface != parms.IsInterface)
        {
            score.Score += 1;
            Logger.Log($"Matched `{type.Name}` on search `{score.ProposedNewName}` : IsInterface");
            return EMatchResult.Match;
        }

        Logger.Log($"Skipping `{type.Name}` on search `{score.ProposedNewName}` IsInterface does not match.");
        return EMatchResult.NoMatch;
    }

    public static EMatchResult MatchIsGeneric(this TypeDefinition type, RemapSearchParams parms, ScoringModel score)
    {
        if (parms.IsGeneric is null)
        {
            return EMatchResult.Disabled;
        }

        if (type.HasGenericParameters == parms.IsGeneric)
        {
            score.Score += 1;
            Logger.Log($"Matched `{type.Name}` on search `{score.ProposedNewName}` : IsGeneric");
            return EMatchResult.Match;
        }

        Logger.Log($"Skipping `{type.Name}` on search `{score.ProposedNewName}` IsGeneric does not match.");
        return EMatchResult.NoMatch;
    }

    public static EMatchResult MatchIsPublic(this TypeDefinition type, RemapSearchParams parms, ScoringModel score)
    {
        if (parms.IsPublic is null)
        {
            return EMatchResult.Disabled;
        }

        if (type.IsPublic == parms.IsPublic)
        {
            score.Score += 1;
            Logger.Log($"Matched `{type.Name}` on search `{score.ProposedNewName}` : IsPublic");
            return EMatchResult.Match;
        }

        Logger.Log($"Skipping `{type.Name}` on search `{score.ProposedNewName}` IsPublic does not match.");
        return EMatchResult.NoMatch;
    }

    public static EMatchResult MatchHasAttribute(this TypeDefinition type, RemapSearchParams parms, ScoringModel score)
    {
        if (parms.HasAttribute is null)
        {
            return EMatchResult.Disabled;
        }

        if (type.HasCustomAttributes == parms.HasAttribute)
        {
            score.Score += 1;
            Logger.Log($"Matched `{type.Name}` on search `{score.ProposedNewName}` : HasAttribute");
            return EMatchResult.Match;
        }

        Logger.Log($"Skipping `{type.Name}` on search `{score.ProposedNewName}` HasAttribute does not match.");
        return EMatchResult.NoMatch;
    }

    public static EMatchResult MatchMethods(this TypeDefinition type, RemapSearchParams parms, ScoringModel score)
    {
        // Ignore types that dont have methods when we are looking for them, and ignore types that
        // have methods while we're not looking for them
        if ((type.HasMethods is true && parms.MethodNamesToMatch.Count == 0) || (type.HasMethods is false && parms.MethodNamesToMatch.Count > 0))
        {
            return EMatchResult.NoMatch;
        }

        // `*` is the wildcard to ignore all methods that exist on types
        if (parms.MethodNamesToIgnore.Contains("*"))
        {
            return EMatchResult.NoMatch;
        }

        int matchCount = 0;

        foreach (var method in type.Methods)
        {
            if (parms.MethodNamesToIgnore.Contains(method.Name))
            {
                // Type contains blacklisted method
                return EMatchResult.NoMatch;
            }

            if (parms.MethodNamesToMatch.Contains(method.Name))
            {
                matchCount++;
                score.Score += 2;
            }
        }

        return matchCount > 0 ? EMatchResult.Match : EMatchResult.NoMatch;
    }

    public static EMatchResult MatchFields(this TypeDefinition type, RemapSearchParams parms, ScoringModel score)
    {
        // Ignore types that dont have fields when we are looking for them, and ignore types that
        // have fields while we're not looking for them
        if ((type.HasFields is true && parms.FieldNamesToMatch.Count == 0) || (type.HasFields is false && parms.FieldNamesToMatch.Count > 0))
        {
            return EMatchResult.NoMatch;
        }

        // `*` is the wildcard to ignore all fields that exist on types
        if (parms.FieldNamesToIgnore.Contains("*"))
        {
            return EMatchResult.NoMatch;
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

    public static EMatchResult MatchProperties(this TypeDefinition type, RemapSearchParams parms, ScoringModel score)
    {
        // Ignore types that dont have properties when we are looking for them, and ignore types
        // that have properties while we're not looking for them
        if ((type.HasProperties is true && parms.PropertyNamesToMatch.Count == 0) || (type.HasProperties is false && parms.PropertyNamesToMatch.Count > 0))
        {
            return EMatchResult.NoMatch;
        }

        // `*` is the wildcard to ignore all properties that exist on types
        if (parms.PropertyNamesToIgnore.Contains("*"))
        {
            return EMatchResult.NoMatch;
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

    public static EMatchResult MatchNestedTypes(this TypeDefinition type, RemapSearchParams parms, ScoringModel score)
    {
        // Ignore types that dont have nested types when we are looking for them, and ignore types
        // that have nested types while we're not looking for them
        if ((type.HasNestedTypes is true && parms.NestedTypesToMatch.Count == 0) || (type.HasNestedTypes is false && parms.NestedTypesToMatch.Count > 0))
        {
            return EMatchResult.NoMatch;
        }

        // `*` is the wildcard to ignore all nested types that exist on types
        if (parms.PropertyNamesToIgnore.Contains("*"))
        {
            return EMatchResult.NoMatch;
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