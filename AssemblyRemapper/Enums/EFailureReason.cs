namespace AssemblyRemapper.Enums;

internal enum EFailureReason
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
    HasMethods,
    HasFields,
    HasProperties,
    HasNestedTypes,
}