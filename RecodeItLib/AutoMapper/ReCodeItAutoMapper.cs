using Mono.Cecil;
using ReCodeIt.Models;
using ReCodeIt.Utils;
using ReCodeItLib.Utils;

namespace ReCodeItLib.AutoMapper;

public class ReCodeItAutoMapper
{
    private List<MappingPair> MappingPairs { get; set; } = [];

    private List<string> CompilerGeneratedClasses = [];

    private static AutoMapperSettings Settings => DataProvider.Settings.AutoMapper;

    public void InitializeAutoMapping()
    {
        Logger.ClearLog();
        Logger.Log($"Starting Auto Mapping...");

        // Clear any previous pairs
        MappingPairs = [];
        CompilerGeneratedClasses = [];

        FindCompilerGeneratedObjects(DataProvider.ModuleDefinition.Types);

        Logger.Log($"Found {CompilerGeneratedClasses.Count} Compiler generated objects");

        var types = DataProvider.ModuleDefinition.Types;

        foreach (var type in types)
        {
            MappingPairs.AddRange(FilterFieldNames(type));
            MappingPairs.AddRange(FilterPropertyNames(type));
        }

        FilterTypeNames();
        SanitizeProposedNames();
        WriteChanges();
    }

    private void FindCompilerGeneratedObjects(Mono.Collections.Generic.Collection<TypeDefinition> types)
    {
        foreach (var typeDefinition in types)
        {
            if (typeDefinition.IsClass || typeDefinition.IsInterface || typeDefinition.IsValueType) // Check for class or struct
            {
                if (typeDefinition.HasCustomAttributes &&
                    typeDefinition.CustomAttributes.Any(attr => attr.AttributeType.FullName == "System.Runtime.CompilerServices.CompilerGeneratedAttribute"))
                {
                    string typeName = typeDefinition.Name;
                    CompilerGeneratedClasses.Add(typeName);
                    //Logger.Log($"Compiler Generated object found: {typeName}", ConsoleColor.Yellow);
                }
            }

            if (typeDefinition.NestedTypes.Count > 0)
            {
                FindCompilerGeneratedObjects(typeDefinition.NestedTypes);
            }
        }
    }

    /// <summary>
    /// Pair field declaring types with their names
    /// </summary>
    /// <param name="type"></param>
    /// <returns></returns>
    private List<MappingPair> FilterFieldNames(TypeDefinition type)
    {
        var fieldsWithTypes = new List<MappingPair>();

        if (CompilerGeneratedClasses.Contains(type.Name))
        {
            //Logger.Log($"Skipping over compiler generated object: {type.Name}");
            return fieldsWithTypes;
        }

        // Handle nested types recursively
        foreach (var nestedType in type.NestedTypes)
        {
            fieldsWithTypes.AddRange(FilterFieldNames(nestedType));
        }

        var fields = type.Fields
            // we dont want names shorter than 4
            .Where(f => f.FieldType.Name.Length > 3)

            // Skip value types
            .Where(f => !f.FieldType.IsValueType)

            // We dont want fields in the system type ignore list
            .Where(f => !Settings.TypesToIgnore.Contains(f.FieldType.Name.TrimAfterSpecialChar()));

        // Include fields from the current type
        foreach (var field in fields)
        {
            //Logger.Log($"Collecting Field: TypeRef: {field.FieldType.Name.TrimAfterSpecialChar()} Field Name: {field.Name}");

            fieldsWithTypes.Add(new MappingPair(
                field.FieldType,
                field.Name,
                field.FieldType.Name.Contains("Interface"),
                field.FieldType.Name.Contains("Struct")));
        }

        return fieldsWithTypes;
    }

    /// <summary>
    /// Pair field declaring types with their names
    /// </summary>
    /// <param name="type"></param>
    /// <returns></returns>
    private List<MappingPair> FilterPropertyNames(TypeDefinition type)
    {
        var propertiesWithTypes = new List<MappingPair>();

        if (CompilerGeneratedClasses.Contains(type.Name))
        {
            //Logger.Log($"Skipping over compiler generated object: {type.Name}");
            return propertiesWithTypes;
        }

        // Handle nested types recursively
        foreach (var nestedType in type.NestedTypes)
        {
            propertiesWithTypes.AddRange(FilterPropertyNames(nestedType));
        }

        var properties = type.Properties
            // we dont want names shorter than 4
            .Where(p => p.PropertyType.Name.Length > 3)

            // Skip value types
            .Where(p => !p.PropertyType.IsValueType)

            // We dont want fields in the global ignore list
            .Where(p => !Settings.TypesToIgnore.Contains(p.PropertyType.Name.TrimAfterSpecialChar()));

        // Include fields from the current type
        foreach (var property in properties)
        {
            //Logger.Log($"Collecting Property: TypeRef: {property.PropertyType.Name.TrimAfterSpecialChar()} Field Name: {property.Name}");

            ;

            propertiesWithTypes.Add(new MappingPair(
                property.PropertyType,
                property.Name,
                property.PropertyType.Name.Contains("Interface"),
                property.PropertyType.Name.Contains("Struct")));
        }

        return propertiesWithTypes;
    }

    /// <summary>
    /// This giant linq statement handles all of the filtering once the initial gathering of fields
    /// and properties is complete
    /// </summary>
    private void FilterTypeNames()
    {
        // Filter types to the ones we're looking for
        var mappingPairs = MappingPairs
            // Filter based on length, short lengths dont make good class names
            .Where(pair => pair.Name.Length >= Settings.MinLengthToMatch)

            // Filter out anything that doesnt start with our specified tokens (Where
            // pair.TypeRef.Name is the property TypeRef name `Class1202` and token is start
            // identifer we are looking for `GClass`
            .Where(pair => Settings.TokensToMatch
                .Any(token => pair.TypeRef.Name.StartsWith(token)))

            // Filter out anything that has the same name as the type, we cant remap those
            .Where(pair => !Settings.TokensToMatch
                .Any(token => pair.Name.ToLower().StartsWith(token.ToLower())))

            // Filter based on direct name blacklist (Where pair.Name is the property name and token
            // is blacklisted item `Columns`
            .Where(pair => !Settings.PropertyFieldBlackList
                .Any(token => pair.Name.ToLower().StartsWith(token.ToLower())))

            // Filter out backing fields
            /// This is slow, but oh well
            .Where(pair => !pair.Name.ToCharArray().Contains('<'))

            // We only want types once, so make it unique
            .GroupBy(pair => pair.TypeRef.FullName)
                .Select(group => group.First())
                    .GroupBy(pair => pair.Name)
                        .Select(group => group.First())
                            .ToList();

        MappingPairs = [.. mappingPairs];
    }

    private void SanitizeProposedNames()
    {
        foreach (var pair in MappingPairs)
        {
            char first = pair.Name.ToCharArray().ElementAt(0);

            if (first.Equals('_'))
            {
                pair.Name = string.Concat("", pair.Name.AsSpan(1));
            }

            // Re-run incase prefix removed
            first = pair.Name.ToCharArray().ElementAt(0);

            if (char.IsLower(first))
            {
                pair.Name = string.Concat(char.ToUpper(first).ToString(), pair.Name.AsSpan(1));
            }

            if (pair.IsInterface)
            {
                pair.Name = string.Concat("I", pair.Name.AsSpan(0));
            }

            // If its not an interface, its a struct or class
            switch (pair.IsStruct)
            {
                case true:
                    pair.Name = string.Concat(pair.Name, "Struct");
                    break;

                case false:
                    pair.Name = string.Concat(pair.Name, "Class");
                    break;
            }

            Logger.Log($"------------------------------------------------------------------------");
            Logger.Log($"Original Name: {pair.OriginalName} : Sanitized Name: {pair.Name}");
            Logger.Log($"Matched From Name: {pair.OriginalPropOrFieldName}");
            Logger.Log($"IsInterface: {pair.IsInterface}");
            Logger.Log($"IsStruct: {pair.IsStruct}");
            Logger.Log($"------------------------------------------------------------------------");
        }
    }

    private void WriteChanges()
    {
    }

    private sealed class MappingPair(TypeReference type, string name, bool isInterface = false, bool isStruct = false)
    {
        public TypeReference TypeRef { get; set; } = type;
        public string OriginalName { get; set; } = type.FullName;
        public bool IsInterface { get; set; } = isInterface;
        public bool IsStruct { get; set; } = isStruct;
        public string Name { get; set; } = name;

        public string OriginalPropOrFieldName { get; } = name;
    }
}