using ReCodeIt.Models;
using ReCodeIt.Utils;
using Mono.Cecil;
using Mono.Collections.Generic;

namespace ReCodeIt.ReMapper;

internal static class Renamer
{
    public static void RenameAll(ScoringModel score)
    {
        var types = DataProvider.ModuleDefinition.Types;

        // Rename all fields and properties first
        RenameAllFields(score, types);
        RenameAllProperties(score, types);

        score.Definition.Name = score.ProposedNewName;
    }

    public static void RenameAllDirect(RemapModel remap, TypeDefinition type)
    {
        var directRename = new ScoringModel();
        directRename.Definition = type;
        directRename.ReMap = remap;

        RenameAll(directRename);
    }

    private static void RenameAllFields(
        ScoringModel score,
        Collection<TypeDefinition> typesToCheck)
    {
        foreach (var type in typesToCheck)
        {
            int fieldCount = 0;
            foreach (var field in type.Fields)
            {
                if (field.FieldType.Name == score.Definition.Name)
                {
                    var newFieldName = GetNewFieldName(score.ReMap.NewTypeName, field.IsPrivate, fieldCount);

                    if (field.Name == newFieldName) { continue; }

                    Logger.Log($"Renaming field: `{field.Name}` on TypeRef `{type.Name}` to {newFieldName}", ConsoleColor.Green);

                    field.Name = newFieldName;

                    fieldCount++;
                }
            }

            if (type.HasNestedTypes)
            {
                foreach (var _ in type.NestedTypes)
                {
                    RenameAllFields(score, type.NestedTypes);
                }
            }
        }
    }

    private static void RenameAllProperties(
        ScoringModel score,
        Collection<TypeDefinition> typesToCheck)
    {
        foreach (var type in typesToCheck)
        {
            int propertyCount = 0;

            foreach (var property in type.Properties)
            {
                if (property.PropertyType.Name == score.Definition.Name)
                {
                    var newName = propertyCount > 0 ? $"{score.ReMap.NewTypeName}_{propertyCount}" : score.ReMap.NewTypeName;

                    Logger.Log($"Renaming Property: `{property.Name}` on TypeRef `{type}` to {newName}", ConsoleColor.Green);
                    property.Name = newName;
                }
            }

            if (type.HasNestedTypes)
            {
                foreach (var _ in type.NestedTypes)
                {
                    RenameAllProperties(score, type.NestedTypes);
                }
            }
        }
    }

    private static string GetNewFieldName(string TypeName, bool isPrivate, int fieldCount = 0)
    {
        var discard = isPrivate ? "_" : "";
        string newFieldCount = fieldCount > 0 ? $"_{fieldCount}" : string.Empty;

        return $"{discard}{char.ToLower(TypeName[0])}{TypeName[1..]}{newFieldCount}";
    }
}