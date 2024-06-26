using dnlib.DotNet;
using ReCodeIt.Models;
using ReCodeIt.Utils;

namespace ReCodeIt.ReMapper;

internal static class RenameHelper
{
    private static List<string> TokensToMatch => DataProvider.Settings.AutoMapper.TokensToMatch;

    /// <summary>
    /// Only used by the manual remapper, should probably be removed
    /// </summary>
    /// <param name="module"></param>
    /// <param name="remap"></param>
    /// <param name="direct"></param>
    public static void RenameAll(IEnumerable<TypeDef> types, RemapModel remap, bool direct = false)
    {
        // Rename all fields and properties first
        if (DataProvider.Settings.Remapper.MappingSettings.RenameFields)
        {
            RenameAllFields(
                remap.TypePrimeCandidate.Name.String,
                remap.NewTypeName,
                types);
        }

        if (DataProvider.Settings.Remapper.MappingSettings.RenameProperties)
        {
            RenameAllProperties(
                remap.TypePrimeCandidate.Name.String,
                remap.NewTypeName,
                types);
        }

        if (!direct)
        {
            RenameType(types, remap);
        }

        Logger.Log($"{remap.TypePrimeCandidate.Name.String} Renamed.", ConsoleColor.Green);
    }

    /// <summary>
    /// Only used by the manual remapper, should probably be removed
    /// </summary>
    /// <param name="module"></param>
    /// <param name="remap"></param>
    /// <param name="type"></param>
    public static void RenameAllDirect(ModuleDefMD module, RemapModel remap, TypeDef type)
    {
        //RenameAll(module.GetTypes(), remap, true);
    }

    /// <summary>
    /// Rename all fields recursively, returns number of fields changed
    /// </summary>
    /// <param name="oldTypeName"></param>
    /// <param name="newTypeName"></param>
    /// <param name="typesToCheck"></param>
    /// <returns></returns>
    public static IEnumerable<TypeDef> RenameAllFields(

        string oldTypeName,
        string newTypeName,
        IEnumerable<TypeDef> typesToCheck,
        int overAllCount = 0)
    {
        foreach (var type in typesToCheck)
        {
            var fields = type.Fields
                .Where(field => field.Name.IsFieldOrPropNameInList(TokensToMatch));

            if (!fields.Any()) { continue; }

            int fieldCount = 0;
            foreach (var field in fields)
            {
                if (field.FieldType.TypeName == oldTypeName)
                {
                    var newFieldName = GetNewFieldName(newTypeName, fieldCount);

                    // Dont need to do extra work
                    if (field.Name == newFieldName) { continue; }

                    Logger.Log($"Renaming field on type {type.Name} named `{field.Name}` with type `{field.FieldType.TypeName}` to `{newFieldName}`", ConsoleColor.Green);

                    var oldName = field.Name.ToString();

                    field.Name = newFieldName;

                    UpdateTypeFieldMemberRefs(type, field, oldName);
                    RenameAllFieldRefsInMethods(typesToCheck, field, oldName);

                    fieldCount++;
                    overAllCount++;
                }
            }
        }

        return typesToCheck;
    }

    private static void UpdateTypeFieldMemberRefs(TypeDef type, FieldDef newDef, string oldName)
    {
        foreach (var method in type.Methods)
        {
            if (!method.HasBody) continue;

            foreach (var instr in method.Body.Instructions)
            {
                if (instr.Operand is MemberRef memRef && memRef.Name == oldName)
                {
                    //if (!memRef.Name.IsFieldOrPropNameInList(TokensToMatch)) continue;

                    Logger.Log($"Renaming MemRef in method {method.DeclaringType.Name}::{method.Name} from `{memRef.Name}` to `{newDef.Name}`", ConsoleColor.Yellow);
                    memRef.Name = newDef.Name;
                }
            }
        }
    }

    private static void RenameAllFieldRefsInMethods(IEnumerable<TypeDef> typesToCheck, FieldDef newDef, string oldName)
    {
        foreach (var type in typesToCheck)
        {
            foreach (var method in type.Methods)
            {
                if (!method.HasBody) continue;

                if (type.HasGenericParameters)
                    IterateMethodInstructions(method.ResolveMethodDef(), newDef, oldName);

                IterateMethodInstructions(method, newDef, oldName);
            }
        }
    }

    /// <summary>
    /// Rename all field and member refs in a method
    /// </summary>
    /// <param name="method"></param>
    /// <param name="newDef"></param>
    /// <param name="oldName"></param>
    private static void IterateMethodInstructions(MethodDef method, FieldDef newDef, string oldName)
    {
        foreach (var instr in method.Body.Instructions)
        {
            if (instr.Operand is FieldDef fieldDef && fieldDef.Name == oldName)
            {
                if (!fieldDef.Name.IsFieldOrPropNameInList(TokensToMatch)) continue;

                Logger.Log($"Renaming fieldDef in method {method.Name} from `{fieldDef.Name}` to `{newDef.Name}`", ConsoleColor.Yellow);
                fieldDef.Name = newDef.Name;
            }

            if (instr.Operand is MemberRef memRef && memRef.Name == oldName)
            {
                if (!memRef.Name.IsFieldOrPropNameInList(TokensToMatch)) continue;

                Logger.Log($"Renaming MemRef in method {method.DeclaringType.Name}::{method.Name} from `{memRef.Name}` to `{newDef.Name}`", ConsoleColor.Yellow);
                memRef.Name = newDef.Name;
            }
        }
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
        IEnumerable<TypeDef> typesToCheck,
        int overAllCount = 0)
    {
        foreach (var type in typesToCheck)
        {
            var properties = type.Properties
                .Where(prop => prop.Name.IsFieldOrPropNameInList(TokensToMatch));

            if (!properties.Any()) { continue; }

            int propertyCount = 0;
            foreach (var property in properties)
            {
                if (property.PropertySig.RetType.TypeName == oldTypeName)
                {
                    var newPropertyName = GetNewPropertyName(newTypeName, propertyCount);

                    // Dont need to do extra work
                    if (property.Name == newPropertyName) { continue; }

                    Logger.Log($"Renaming property on type {type.Name} named `{property.Name}` with type `{property.PropertySig.RetType.TypeName}` to `{newPropertyName}`", ConsoleColor.Green);
                    property.Name = new UTF8String(newPropertyName);
                    propertyCount++;
                    overAllCount++;
                }
            }
        }

        return overAllCount;
    }

    public static string GetNewFieldName(string NewName, int fieldCount = 0)
    {
        string newFieldCount = fieldCount > 0 ? $"_{fieldCount}" : string.Empty;

        return $"{char.ToLower(NewName[0])}{NewName[1..]}{newFieldCount}";
    }

    public static string GetNewPropertyName(string newName, int propertyCount = 0)
    {
        return propertyCount > 0 ? $"{newName}_{propertyCount}" : newName;
    }

    private static void RenameType(IEnumerable<TypeDef> typesToCheck, RemapModel remap)
    {
        foreach (var type in typesToCheck)
        {
            if (type.HasNestedTypes)
            {
                RenameType(type.NestedTypes, remap);
            }

            if (remap.TypePrimeCandidate.Name is null) { continue; }

            if (remap.SearchParams.IsNested is true &&
                type.IsNested && type.Name == remap.TypePrimeCandidate.Name)
            {
                type.Name = remap.NewTypeName;
            }

            if (type.FullName == remap.TypePrimeCandidate.Name)
            {
                type.Name = remap.NewTypeName;
            }
        }
    }
}