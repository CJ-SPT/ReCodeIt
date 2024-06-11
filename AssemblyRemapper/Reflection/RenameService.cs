using AssemblyRemapper.Utils;
using Mono.Collections.Generic;

namespace AssemblyRemapper.Reflection;

internal static class RenameService
{
    public static void RenameAllFields(
        string oldName,
        string newName,
        Collection<Mono.Cecil.TypeDefinition> types)
    {
        foreach (var type in types)
        {
            int fieldCount = 0;

            foreach (var field in type.Fields)
            {
                if (field.FieldType.ToString() == newName)
                {
                    Logger.Log($"Renaming Field: `{field.Name}` on Type `{type}`");
                    field.Name = GetNewFieldName(newName, field.IsPrivate, fieldCount);
                    fieldCount++;
                }
            }

            if (type.HasNestedTypes)
            {
                foreach (var _ in type.NestedTypes)
                {
                    RenameAllFields(oldName, newName, type.NestedTypes);
                }
            }
        }
    }

    public static void RenameAllProperties(
        string oldName,
        string newName,
        Collection<Mono.Cecil.TypeDefinition> types)
    {
        foreach (var type in types)
        {
            int propertyCount = 0;

            foreach (var property in type.Properties)
            {
                if (property.PropertyType.ToString() == newName)
                {
                    Logger.Log($"Renaming Property: `{property.Name}` on Type `{type}`");
                    property.Name = propertyCount > 0 ? $"{newName}_{propertyCount}" : newName;
                }
            }

            if (type.HasNestedTypes)
            {
                foreach (var _ in type.NestedTypes)
                {
                    RenameAllProperties(oldName, newName, type.NestedTypes);
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