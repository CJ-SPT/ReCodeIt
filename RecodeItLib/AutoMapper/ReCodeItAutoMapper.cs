using Mono.Cecil;
using Mono.Collections.Generic;
using ReCodeIt.Models;
using ReCodeIt.ReMapper;
using ReCodeIt.Utils;
using ReCodeItLib.Utils;

namespace ReCodeItLib.AutoMapper;

public class ReCodeItAutoMapper
{
    private List<MappingPair> MappingPairs { get; set; } = [];

    private List<string> CompilerGeneratedClasses = [];

    private static AutoMapperSettings Settings => DataProvider.Settings.AutoMapper;

    private static bool Error { get; set; } = false;
    private int FailureCount { get; set; } = 0;

    private int TotalFieldRenameCount { get; set; } = 0;
    private int TotalPropertyRenameCount { get; set; } = 0;

    /// <summary>
    /// Start the automapping process
    /// </summary>
    public void InitializeAutoMapping()
    {
        Logger.ClearLog();
        Logger.Log($"Starting Auto Mapping...");

        // Clear any previous pairs
        MappingPairs = [];
        CompilerGeneratedClasses = [];

        DataProvider.LoadAssemblyDefinition();

        Error = false;
        FailureCount = 0;
        TotalFieldRenameCount = 0;
        TotalPropertyRenameCount = 0;

        FindCompilerGeneratedObjects(DataProvider.ModuleDefinition.Types);

        Logger.Log($"Found {CompilerGeneratedClasses.Count} Compiler generated objects");

        var types = DataProvider.ModuleDefinition.Types;

        foreach (var type in types)
        {
            MappingPairs.AddRange(FilterFieldNames(type));
            MappingPairs.AddRange(FilterPropertyNames(type));
        }

        Logger.Log(MappingPairs.Count());

        PrimaryTypeNameFilter();
        SanitizeProposedNames();
        StartRenameProcess();

        if (Error) { return; }

        WriteChanges();
    }

    /// <summary>
    /// Finds any compiler generated code so we can ignore it, its mostly LINQ garbage
    /// </summary>
    /// <param name="types"></param>
    private void FindCompilerGeneratedObjects(Collection<TypeDefinition> types)
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

            // TODO: Renaming arrays is strange, come back to this later
            .Where(p => !p.FieldType.IsArray)

            // We dont want fields in the system type ignore list
            .Where(f => !Settings.TypesToIgnore.Contains(f.FieldType.Name.TrimAfterSpecialChar()));

        // Include fields from the current type
        foreach (var field in fields)
        {
            //Logger.Log($"Collecting Field: OriginalTypeRef: {field.FieldType.Name.TrimAfterSpecialChar()} Field Name: {field.Name}");

            var typeDef = field.FieldType.Resolve();

            // Dont rename things we cant resolve
            if (typeDef is null) { continue; }

            fieldsWithTypes.Add(new MappingPair(
                typeDef,
                field.Name,
                field.FieldType.Name.Contains("Interface"),
                field.FieldType.Name.Contains("Struct"),
                field.IsPublic));
        }

        return fieldsWithTypes;
    }

    /// <summary>
    /// Pair field declaring types with their names
    /// </summary>
    /// <param name="type"></param>
    /// <returns></returns>
    private IEnumerable<MappingPair> FilterPropertyNames(TypeDefinition type)
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

            // TODO: Renaming arrays is strange, come back to this later
            .Where(p => !p.PropertyType.IsArray)

            // We dont want fields in the global ignore list
            .Where(p => !Settings.TypesToIgnore.Contains(p.PropertyType.Name.TrimAfterSpecialChar()));

        // Include fields from the current type
        foreach (var property in properties)
        {
            //Logger.Log($"Collecting Property: OriginalTypeRef: {property.PropertyType.Name.TrimAfterSpecialChar()} Field Name: {property.Name}");

            var typeDef = property.PropertyType.Resolve();

            // Dont rename things we cant resolve
            if (typeDef is null) { continue; }

            propertiesWithTypes.Add(new MappingPair(
                typeDef,
                property.Name,
                property.PropertyType.Name.Contains("Interface"),
                property.PropertyType.Name.Contains("Struct"),
                true));
        }

        return propertiesWithTypes;
    }

    /// <summary>
    /// This giant linq statement handles all of the filtering once the initial gathering of fields
    /// and properties is complete
    /// </summary>
    private void PrimaryTypeNameFilter()
    {
        // Filter types to the ones we're looking for
        var mappingPairs = MappingPairs
            // Filter based on length, short lengths dont make good class names
            .Where(pair => pair.Name.Length >= Settings.MinLengthToMatch)

            // Filter out anything that doesnt start with our specified tokens (Where
            // pair.OriginalTypeRef.Name is the property OriginalTypeRef name `Class1202` and token
            // is start identifer we are looking for `GClass`
            .Where(pair => Settings.TokensToMatch
                .Any(token => pair.OriginalTypeDefinition.Name.StartsWith(token)))

            // Filter out anything that has the same name as the type, we cant remap those
            .Where(pair => !Settings.TokensToMatch
                .Any(token => pair.Name.ToLower().StartsWith(token.ToLower())))

            // Filter based on direct name blacklist (Where pair.Name is the property name and token
            // is blacklisted item `Columns`
            .Where(pair => !Settings.PropertyFieldBlackList
                .Any(token => pair.Name.ToLower().StartsWith(token.ToLower())))

            // Filter out backing fields
            /// This is slow, but oh well
            .Where(pair => !pair.Name.ToCharArray().Contains('<')).ToList();

        MappingPairs = mappingPairs;
        SecondaryTypeNameFilter();
    }

    /// <summary>
    /// This is where we filter down based on more specific parameters
    /// </summary>
    /// <param name="mappingPairs"></param>
    private void SecondaryTypeNameFilter()
    {
        // Filter property/field names by required number of matches
        MappingPairs = MappingPairs
            .GroupBy(pair => pair.OriginalPropOrFieldName.TrimAfterSpecialChar())
            .Where(group => group.Count() > Settings.RequiredMatches)
            .SelectMany(group => group).ToList();

        FinalGroupAndSelect();
    }

    private void FinalGroupAndSelect()
    {
        MappingPairs = MappingPairs
            // We only want types once, so make it unique
            .GroupBy(pair => pair.OriginalTypeDefinition.FullName)
                .Select(group => group.First())
                    .GroupBy(pair => pair.Name)
                        .Select(group => group.First()).ToList();
    }

    /// <summary>
    /// Sanitizes and prepares mapping pairs for remapping once filtering is complete.
    /// </summary>
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
            /*
            // Try and remove any trailing 's' that exist
            if (pair.WasCollection)
            {
                if (pair.Name.ToLower().EndsWith('s'))
                {
                    pair.Name = pair.Name.Substring(0, pair.Name.Length - 1);
                }
            }
            */
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
            Logger.Log($"Original Name: {pair.OriginalTypeDefinition.FullName} : Sanitized Name: {pair.Name}");
            Logger.Log($"Matched From Name: {pair.OriginalPropOrFieldName}");
            Logger.Log($"IsInterface: {pair.IsInterface}");
            Logger.Log($"IsStruct: {pair.IsStruct}");
            Logger.Log($"------------------------------------------------------------------------");
        }

        Logger.Log($"Automatically remapped {MappingPairs.Count()} objects");
    }

    /// <summary>
    /// Start renaming assembly definitions
    /// </summary>
    private void StartRenameProcess()
    {
        // Gather up any matches we have
        foreach (var type in DataProvider.ModuleDefinition.Types.ToArray())
        {
            foreach (var pair in MappingPairs.ToArray())
            {
                GatherMatchedTypeRefs(pair, type);
            }
        }

        // Rename Types to matched types
        foreach (var pair in MappingPairs)
        {
            if (pair.NewTypeRef != null)
            {
                Logger.Log($"------------------------------------------------------------------------", ConsoleColor.Green);
                Logger.Log($"Renaming: {pair.OriginalTypeDefinition.Name} to {pair.Name}", ConsoleColor.Green);

                var fieldCount = RenameHelper.RenameAllFields(
                    pair.OriginalTypeDefinition.Name,
                    pair.Name,
                    DataProvider.ModuleDefinition.Types);

                var propCount = RenameHelper.RenameAllProperties(
                    pair.OriginalTypeDefinition.Name,
                    pair.Name,
                    DataProvider.ModuleDefinition.Types);

                TotalFieldRenameCount += fieldCount;
                TotalPropertyRenameCount += propCount;

                Logger.Log($"Renamed: {fieldCount} fields", ConsoleColor.Green);
                Logger.Log($"Renamed: {propCount} properties", ConsoleColor.Green);
                Logger.Log($"------------------------------------------------------------------------", ConsoleColor.Green);

                pair.NewTypeRef.Name = pair.Name;
                pair.HasBeenRenamed = true;
            }
        }

        // Do a final error check
        foreach (var pair in MappingPairs)
        {
            if (!pair.HasBeenRenamed)
            {
                Logger.Log($"------------------------------------------------------------------------", ConsoleColor.Red);
                Logger.Log($"Renaming: {pair.OriginalTypeDefinition.Name} to {pair.Name} has failed", ConsoleColor.Red);
                Logger.Log($"Trying to match: {pair.IsInterface}", ConsoleColor.Red);
                Logger.Log($"IsInterface: {pair.IsInterface}", ConsoleColor.Red);
                Logger.Log($"IsStruct: {pair.IsStruct}", ConsoleColor.Red);
                Logger.Log($"------------------------------------------------------------------------", ConsoleColor.Red);

                FailureCount++;
                Error = true;
            }
        }
    }

    /// <summary>
    /// Recursively handle all renaming on nested types on a given type
    /// </summary>
    /// <param name="pair"></param>
    /// <param name="type"></param>
    private void GatherMatchedTypeRefs(MappingPair pair, TypeDefinition type)
    {
        // Handle nested types recursively
        foreach (var nestedType in type.NestedTypes.ToArray())
        {
            GatherMatchedTypeRefs(pair, nestedType);
        }

        if (type == pair.OriginalTypeDefinition)
        {
            pair.NewTypeRef = type;
        }
    }

    private void WriteChanges()
    {
        var path = DataProvider.WriteAssemblyDefinition();

        Logger.Log($"-------------------------------RESULT-----------------------------------", ConsoleColor.Green);
        Logger.Log($"Complete: Assembly written to `{path}`", ConsoleColor.Green);
        Logger.Log($"Found {MappingPairs.Count()} automatic remaps", ConsoleColor.Green);
        Logger.Log($"Renamed {TotalFieldRenameCount} fields", ConsoleColor.Green);
        Logger.Log($"Renamed {TotalPropertyRenameCount} properties", ConsoleColor.Green);
        Logger.Log($"Failed to rename: {FailureCount} mapping pairs", ConsoleColor.Green);
        Logger.Log($"------------------------------------------------------------------------", ConsoleColor.Green);
    }

    /// <summary>
    /// Represents a match of a field name to obfuscated class
    /// </summary>
    /// <param name="type"></param>
    /// <param name="name"></param>
    /// <param name="isInterface"></param>
    /// <param name="isStruct"></param>
    private sealed class MappingPair(
        TypeDefinition type,
        string name,
        bool isInterface,
        bool isStruct,
        bool isPublic)
    {
        public TypeDefinition OriginalTypeDefinition { get; private set; } = type;

        /// <summary>
        /// The type reference we want to change
        /// </summary>
        public TypeDefinition NewTypeRef { get; set; }

        /// <summary>
        /// Is this field an interface?
        /// </summary>
        public bool IsInterface { get; set; } = isInterface;

        /// <summary>
        /// Is this type a struct?
        /// </summary>
        public bool IsStruct { get; set; } = isStruct;

        /// <summary>
        /// Has this type been renamed? Use for checking for failures at the end
        /// </summary>
        public bool HasBeenRenamed { get; set; } = false;

        /// <summary>
        /// This is the name we want to change the assembly class to
        /// </summary>
        public string Name { get; set; } = name;

        /// <summary>
        /// Original name of the property or field type
        /// </summary>
        public string OriginalPropOrFieldName { get; } = name;
    }
}