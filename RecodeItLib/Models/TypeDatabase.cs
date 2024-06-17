using Mono.Cecil;
using Mono.Collections.Generic;

namespace ReCodeItLib.Models;

/// <summary>
/// This is a database of type, field, property etc info collected into one place for an assembly.
/// </summary>
/// <param name="assembly"></param>
internal class TypeDatabaseModel
{
    public TypeDatabaseModel(ModuleDefinition moduleDefinition)
    {
        ModuleDefinition = moduleDefinition;

        GetAllTypes(ModuleDefinition.Types);
    }

    public ModuleDefinition ModuleDefinition { get; private set; }

    public List<TypeDefinition> Types { get; private set; } = [];

    /// <summary>
    /// Key, the type definition the property belongs too
    /// </summary>
    public Dictionary<TypeDefinition, PropertyDefinition> Properties { get; private set; }

    /// <summary>
    /// Key, the type definition the method belongs too
    /// </summary>
    public Dictionary<TypeDefinition, MethodDefinition> Methods { get; private set; }

    /// <summary>
    /// Key, the type definition the field belongs too
    /// </summary>
    public Dictionary<TypeDefinition, FieldDefinition> Fields { get; private set; }

    private void GetAllTypes(Collection<TypeDefinition> types)
    {
        foreach (var type in types)
        {
            if (type.HasNestedTypes)
            {
                GetAllTypes(type.NestedTypes);
            }

            Types.Add(type);
        }
    }
}