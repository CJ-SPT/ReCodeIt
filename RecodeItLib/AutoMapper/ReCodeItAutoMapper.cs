using Mono.Cecil;
using ReCodeIt.Models;
using ReCodeIt.Utils;
using ReCodeItLib.Utils;

namespace ReCodeItLib.AutoMapper;

public class ReCodeItAutoMapper
{
    private List<MappingPair> MappingPairs { get; set; } = [];

    private AutoMapperSettings Settings => DataProvider.Settings.AutoMapper;

    private static readonly List<string> SystemTypeIgnoreList = new()
    {
        "Boolean",
        "List",
        "Dictionary",
        "Byte",
        "Int16",
        "Int36",
        "Func",
        "Action"
    };

    private List<string> TokensToMatch = new()
    {
        "Class",
        "GClass"
    };

    public void InitializeAutoMapping()
    {
        Logger.ClearLog();
        Logger.Log($"Starting Auto Mapping...");

        // Clear any previous pairs
        MappingPairs = [];

        foreach (var type in DataProvider.ModuleDefinition.Types)
        {
            MappingPairs.AddRange(FilterFieldNames(type));
            MappingPairs.AddRange(FilterPropertyNames(type));
        }

        FilterTypeNames();
    }

    /// <summary>
    /// Pair field declaring types with their names
    /// </summary>
    /// <param name="type"></param>
    /// <returns></returns>
    private List<MappingPair> FilterFieldNames(TypeDefinition type)
    {
        var fieldsWithTypes = new List<MappingPair>();

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
            //Logger.Log($"Collecting Field: Type: {field.FieldType.Name.TrimAfterSpecialChar()} Field Name: {field.Name}");

            fieldsWithTypes.Add(new MappingPair(field.FieldType, field.Name));
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
            //Logger.Log($"Collecting Property: Type: {property.PropertyType.Name.TrimAfterSpecialChar()} Field Name: {property.Name}");

            propertiesWithTypes.Add(new MappingPair(property.PropertyType, property.Name));
        }

        return propertiesWithTypes;
    }

    /// <summary>
    /// Filters down match pairs to match deobfuscating names 'ClassXXXX' to field or property names
    /// that are not of the same value, also applies a length filter.
    /// </summary>
    private void FilterTypeNames()
    {
        // Filter types to the ones we're looking for
        var mappingPairs = MappingPairs
            // Filter based on length, short lengths dont make good class names
            .Where(pair => pair.Name.Length >= Settings.MinLengthToMatch)

            // Filter out anything that doesnt start with our specified tokens (Where pair.Type.Name
            // is the property Type name `Class1202` and token is start identifer we are looking for `GClass`
            .Where(pair => Settings.TokensToMatch
                .Any(token => pair.Type.Name.StartsWith(token)))

            // Filter out anything that has the same name as the type, we cant remap those
            .Where(pair => !Settings.TokensToMatch
                .Any(token => pair.Name.ToLower().StartsWith(token.ToLower())))

            // Filter based on direct name blacklist (Where pair.Name is the property name and token
            // is blacklisted item `Columns`
            .Where(pair => !Settings.PropertyFieldBlackList
                .Any(token => pair.Name.ToLower().StartsWith(token.ToLower())))

            // We only want types once, so make it unique
            .GroupBy(pair => pair.Type.FullName)
                .Select(group => group.First())
                .ToList();

        foreach (var pair in mappingPairs)
        {
            Logger.Log($"Type: {pair.Type.FullName} identifier: {pair.Name}");
        }

        MappingPairs = mappingPairs.ToList();
        Logger.Log($"Match Count {mappingPairs.Count()}");
    }

    private sealed class MappingPair(TypeReference type, string name)
    {
        public TypeReference Type { get; set; } = type;
        public string Name { get; set; } = name;
    }
}