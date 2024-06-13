namespace AssemblyRemapper.Enums;

public enum EFailureReason
{
    None,
    IsAbstract,
    IsEnum,
    IsNested,
    IsSealed,
    IsDerived,
    IsInterface,
    IsPublic,
    HasGenericParameters,
    HasAttribute,
    IsAttribute,
    Constructor,
    HasMethods,
    HasFields,
    HasProperties,
    HasNestedTypes,
}