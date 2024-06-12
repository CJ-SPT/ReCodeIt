using AssemblyRemapper.Models;
using AssemblyRemapper.Utils;
using Mono.Cecil;
using Mono.Collections.Generic;

namespace AssemblyRemapper.Reflection;

internal static class RenameService
{
    public static void RenameAllFields(
        Remap remap,
        Collection<TypeDefinition> typesToCheck)
    {
        foreach (var type in typesToCheck)
        {
            int fieldCount = 0;

            foreach (var field in type.Fields)
            {
                if (field.FieldType.ToString() == remap.NewTypeName)
                {
                    Logger.Log($"Renaming Field: `{field.Name}` on Type `{type}`");
                    field.Name = GetNewFieldName(remap.NewTypeName, field.IsPrivate, fieldCount);
                    fieldCount++;
                }
            }

            if (type.HasNestedTypes)
            {
                foreach (var _ in type.NestedTypes)
                {
                    RenameAllFields(remap, type.NestedTypes);
                }
            }
        }
    }

    public static void RenameAllProperties(
        Remap remap,
        Collection<TypeDefinition> typesToCheck)
    {
        foreach (var type in typesToCheck)
        {
            int propertyCount = 0;

            foreach (var property in type.Properties)
            {
                if (property.PropertyType.ToString() == remap.NewTypeName)
                {
                    Logger.Log($"Renaming Property: `{property.Name}` on Type `{type}`");
                    property.Name = propertyCount > 0 ? $"{remap.NewTypeName}_{propertyCount}" : remap.NewTypeName;
                }
            }

            if (type.HasNestedTypes)
            {
                foreach (var _ in type.NestedTypes)
                {
                    RenameAllProperties(remap, type.NestedTypes);
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