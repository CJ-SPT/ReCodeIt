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

    private List<TypeDefinition> AllTypes { get; set; } = [];

    private List<string> AlreadyChangedNames { get; set; } = [];

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
        AllTypes = [];
        AlreadyChangedNames = [];

        DataProvider.LoadAssemblyDefinition();

        Error = false;
        FailureCount = 0;
        TotalFieldRenameCount = 0;
        TotalPropertyRenameCount = 0;

        FindCompilerGeneratedObjects(DataProvider.ModuleDefinition.Types);

        var types = DataProvider.ModuleDefinition.Types;

        GetAllTypes(types);

        Logger.Log($"Found {CompilerGeneratedClasses.Count - AllTypes.Count} potential remappable types");
        Logger.Log($"Found {CompilerGeneratedClasses.Count} compiler generated objects");

        foreach (var type in types)
        {
            // We dont want to do anything with compiler generated objects
            if (CompilerGeneratedClasses.Contains(type.Name))
            {
                continue;
            }

            MappingPairs.AddRange(FilterFieldNames(type));
            MappingPairs.AddRange(FilterPropertyNames(type));

            if (Settings.SearchMethods)
            {
                MappingPairs.AddRange(GatherFromMethods(type));
            }
        }

        PrimaryTypeNameFilter();
        SanitizeProposedNames();
        StartRenameProcess();

        WriteChanges();
    }

    private void GetAllTypes(Collection<TypeDefinition> types)
    {
        AllTypes.AddRange(types);

        foreach (var type in types)
        {
            if (type.HasNestedTypes)
            {
                GetAllTypes(type.NestedTypes);
            }
        }
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

    #region METHODS

    private List<MappingPair> GatherFromMethods(TypeDefinition type)
    {
        var methodsWithTypes = new List<MappingPair>();

        // Handle nested types recursively
        foreach (var nestedType in type.NestedTypes)
        {
            methodsWithTypes.AddRange(GatherFromMethods(nestedType));
        }

        var methods = type.Methods
            // We only want methods with parameters
            .Where(m => m.HasParameters)

            // Only want parameter names of a certain length
            .Where(m => m.Parameters.Any(p => p.Name.Length > Settings.MinLengthToMatch));

        // Now go over over all filterd methods manually, because fuck this with linq
        foreach (var method in methods)
        {
            var parmNames = method.Parameters.Select(p => p.Name);
            var parmTypes = method.Parameters.Select(p => p.ParameterType.Name);

            // Now over all parameters in the method
            foreach (var parm in method.Parameters)
            {
                // We dont want blacklisted items
                if (Settings.MethodParamaterBlackList.Contains(parm.ParameterType.Name.TrimAfterSpecialChar())
                    || Settings.TypesToIgnore.Contains(parm.ParameterType.Name.TrimAfterSpecialChar()))
                {
                    continue;
                }

                if (parm.ParameterType.Resolve() == null) { continue; }

                //Logger.Log($"Method Data Found");
                //Logger.Log($"Parameter count: {method.Parameters.Count}");
                //Logger.Log($"Paremeter Names: {string.Join(", ", parmNames)}");
                //Logger.Log($"Paremeter Types: {string.Join(", ", parmTypes)}\n");

                var mapPair = new MappingPair(
                    parm.ParameterType.Resolve(),
                    parm.Name,
                    parm.ParameterType.Resolve().IsInterface,
                    parm.ParameterType.Name.Contains("Struct"),
                    true);

                mapPair.AutoMappingResult = AutoMappingResult.Match_From_Method;

                methodsWithTypes.Add(mapPair);
            }
        }

        return methodsWithTypes;
    }

    #endregion METHODS

    #region FIELDS_PROPERTIES

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

            var pair = new MappingPair(
                typeDef,
                field.Name,
                field.FieldType.Name.Contains("Interface"),
                field.FieldType.Name.Contains("Struct"),
                field.IsPublic);

            pair.AutoMappingResult = AutoMappingResult.Match_From_Field;

            fieldsWithTypes.Add(pair);
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

            var mapPair = new MappingPair(
                typeDef,
                property.Name,
                property.PropertyType.Name.Contains("Interface"),
                property.PropertyType.Name.Contains("Struct"),
                true);

            mapPair.AutoMappingResult = AutoMappingResult.Match_From_Property;

            propertiesWithTypes.Add(mapPair);
        }

        return propertiesWithTypes;
    }

    #endregion FIELDS_PROPERTIES

    #region FILTER

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
            .SelectMany(group => group)
            .ToList()
            // We dont want names that already exist to be considered
            .Where(pair => AllTypes
                .Any(token => !pair.OriginalTypeDefinition.FullName.Contains(token.FullName))).ToList();

        FinalGroupAndSelect();
    }

    /// <summary>
    /// This is where we make sure everything is original
    /// </summary>
    private void FinalGroupAndSelect()
    {
        MappingPairs = MappingPairs
            // We only want types once, so make it unique
            .GroupBy(pair => pair.OriginalTypeDefinition.FullName)
                .Select(group => group.First())
                    .GroupBy(pair => pair.Name)
                        .Select(group => group.First()).ToList();
    }

    #endregion FILTER

    #region OUTPUT

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
            switch (pair.IsStruct && !pair.IsInterface)
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
            Logger.Log($"Is match from: {pair.AutoMappingResult}");
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
            if (pair.NewTypeRef != null && !AlreadyChangedNames.Contains(pair.Name))
            {
                Logger.Log($"------------------------------------------------------------------------", ConsoleColor.Green);
                Logger.Log($"Renaming: {pair.OriginalTypeDefinition.Name} to {pair.Name}", ConsoleColor.Green);
                Logger.Log($"Is match from method: {pair.AutoMappingResult}", ConsoleColor.Green);

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

                AlreadyChangedNames.Add(pair.Name);
                pair.NewTypeRef.Name = pair.Name;
                pair.HasBeenRenamed = true;
                continue;
            }

            if (pair.HasBeenRenamed) { continue; }

            // Set some error codes

            if (AlreadyChangedNames.Contains(pair.Name))
            {
                pair.AutoMappingResult = AutoMappingResult.Fail_From_Already_Contained_Name;
            }

            if (pair.NewTypeRef == null)
            {
                pair.AutoMappingResult = AutoMappingResult.Fail_From_New_Type_Ref_Null;
            }
        }

        // Do a final error check
        foreach (var pair in MappingPairs)
        {
            if (!pair.HasBeenRenamed)
            {
                Logger.Log($"------------------------------------------------------------------------", ConsoleColor.Red);
                Logger.Log($"Renaming: {pair.OriginalTypeDefinition.Name} to {pair.Name} has failed", ConsoleColor.Red);
                Logger.Log($"Result Code: {pair.AutoMappingResult}", ConsoleColor.Red);
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

        var fieldCountMatchResult = MappingPairs.Where(x => x.AutoMappingResult == AutoMappingResult.Match_From_Property).Count();
        var propertyCountMatchResult = MappingPairs.Where(x => x.AutoMappingResult == AutoMappingResult.Match_From_Property).Count();
        var methodCountMatchResult = MappingPairs.Where(x => x.AutoMappingResult == AutoMappingResult.Match_From_Method).Count();

        Logger.Log($"-------------------------------RESULT-----------------------------------", ConsoleColor.Green);
        Logger.Log($"Complete: Assembly written to `{path}`", ConsoleColor.Green);
        Logger.Log($"Found {MappingPairs.Count()} automatic remaps", ConsoleColor.Green);
        Logger.Log($"Found {fieldCountMatchResult} automatic remaps from fields", ConsoleColor.Green);
        Logger.Log($"Found {propertyCountMatchResult} automatic remaps from properties", ConsoleColor.Green);
        Logger.Log($"Found {methodCountMatchResult} automatic remaps from methods", ConsoleColor.Green);
        Logger.Log($"Renamed {TotalFieldRenameCount} fields", ConsoleColor.Green);
        Logger.Log($"Renamed {TotalPropertyRenameCount} properties", ConsoleColor.Green);
        Logger.Log($"Failed to rename: {FailureCount} mapping pairs", (FailureCount == 0 ? ConsoleColor.Green : ConsoleColor.Red));
        Logger.Log($"------------------------------------------------------------------------", ConsoleColor.Green);
    }

    #endregion OUTPUT
}