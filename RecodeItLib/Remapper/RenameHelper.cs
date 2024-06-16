using Mono.Cecil;
using Mono.Collections.Generic;
using ReCodeIt.Models;
using ReCodeIt.Utils;

namespace ReCodeIt.ReMapper;

internal static class RenameHelper
{
    /// <summary>
    /// Only used by the manual remapper, should probably be removed
    /// </summary>
    /// <param name="score"></param>
    public static void RenameAll(ScoringModel score)
    {
        var types = DataProvider.ModuleDefinition.Types;

        // Rename all fields and properties first
        RenameAllFields(score.Definition.Name, score.ReMap.NewTypeName, types);
        RenameAllProperties(score.Definition.Name, score.ReMap.NewTypeName, types);

        score.Definition.Name = score.ProposedNewName;

        Logger.Log($"{score.Definition.Name} Renamed.", ConsoleColor.Green);
    }

    /// <summary>
    /// Only used by the manual remapper, should probably be removed
    /// </summary>
    /// <param name="score"></param>
    public static void RenameAllDirect(RemapModel remap, TypeDefinition type)
    {
        var directRename = new ScoringModel();
        directRename.Definition = type;
        directRename.ReMap = remap;

        RenameAll(directRename);
    }

    /// <summary>
    /// Rename all fields recursively, returns number of fields changed
    /// </summary>
    /// <param name="oldTypeName"></param>
    /// <param name="newTypeName"></param>
    /// <param name="typesToCheck"></param>
    /// <returns></returns>
    public static int RenameAllFields(
        string oldTypeName,
        string newTypeName,
        Collection<TypeDefinition> typesToCheck,
        int overAllCount = 0)
    {
        foreach (var type in typesToCheck)
        {
            int fieldCount = 0;
            foreach (var field in type.Fields)
            {
                if (field.FieldType.Name == oldTypeName)
                {
                    var newFieldName = GetNewFieldName(newTypeName, field.IsPrivate, fieldCount);

                    if (field.Name == newFieldName) { continue; }

                    Logger.Log($"Renaming original field type name: `{field.FieldType.Name}` with name `{field.Name}` to `{newFieldName}`", ConsoleColor.Green);

                    field.Name = newFieldName;

                    fieldCount++;
                    overAllCount++;
                }
            }

            if (type.HasNestedTypes)
            {
                RenameAllFields(oldTypeName, newTypeName, type.NestedTypes, overAllCount);
            }
        }

        return overAllCount;
    }

    /// <summary>
    /// Rename all properties recursively, returns number of fields changed
    /// </summary>
    /// <param name="oldTypeName"></param>
    /// <param name="newTypeName"></param>
    /// <param name="typesToCheck"></param>
    /// <returns></returns>
    public static int RenameAllProperties(
        string oldTypeName,
        string newTypeName,
        Collection<TypeDefinition> typesToCheck,
        int overAllCount = 0)
    {
        foreach (var type in typesToCheck)
        {
            int propertyCount = 0;

            foreach (var property in type.Properties)
            {
                if (property.PropertyType.Name == oldTypeName)
                {
                    var newName = GetNewPropertyName(newTypeName, propertyCount);

                    Logger.Log($"Renaming original property type name: `{property.PropertyType.Name}` with name `{property.Name}` to `{newName}`", ConsoleColor.Green);
                    property.Name = newName;
                    propertyCount++;
                    overAllCount++;
                }
            }

            if (type.HasNestedTypes)
            {
                RenameAllProperties(oldTypeName, newTypeName, type.NestedTypes, overAllCount);
            }
        }

        return overAllCount;
    }

    public static string GetNewFieldName(string NewName, bool isPrivate, int fieldCount = 0)
    {
        var discard = isPrivate ? "_" : "";
        string newFieldCount = fieldCount > 0 ? $"_{fieldCount}" : string.Empty;

        return $"{discard}{char.ToLower(NewName[0])}{NewName[1..]}{newFieldCount}";
    }

    public static string GetNewPropertyName(string newName, int propertyCount = 0)
    {
        return propertyCount > 0 ? $"{newName}_{propertyCount}" : newName;
    }
}