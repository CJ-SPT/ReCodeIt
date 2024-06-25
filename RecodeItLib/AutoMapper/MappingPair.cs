using dnlib.DotNet;

namespace ReCodeIt.AutoMapper;

/// <summary>
/// Represents a match of a field name to obfuscated class
/// </summary>
/// <param name="type"></param>
/// <param name="name"></param>
/// <param name="isInterface"></param>
/// <param name="isStruct"></param>
public sealed class MappingPair(
    TypeDef type,
    string name,
    bool isInterface,
    bool isStruct,
    bool isPublic)
{
    public TypeDef OriginalTypeDefinition { get; private set; } = type;

    /// <summary>
    /// The type reference we want to change
    /// </summary>
    public TypeDef NewTypeRef { get; set; }

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
    /// Did this match come from a method?
    /// </summary>
    public AutoMappingResult AutoMappingResult { get; set; } = AutoMappingResult.None;

    /// <summary>
    /// This is the name we want to change the assembly class to
    /// </summary>
    public string Name { get; set; } = name;

    /// <summary>
    /// Original name of the property or field type
    /// </summary>
    public string OriginalPropOrFieldName { get; } = name;
}

public enum AutoMappingResult
{
    None,
    Match_From_Field,
    Match_From_Property,
    Match_From_Method,
    Fail_From_Already_Contained_Name,
    Fail_From_New_Type_Ref_Null,
}