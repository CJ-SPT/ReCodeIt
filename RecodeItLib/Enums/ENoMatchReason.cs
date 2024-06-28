namespace ReCodeIt.Enums;

public enum ENoMatchReason
{
    AmbiguousMatch,
    IsEnum,
    IsNested,
    IsSealed,
    IsDerived,
    HasGenericParameters,
    HasAttribute,
    ConstructorParameterCount,
    MethodsInclude,
    MethodsExclude,
    MethodsCount,
    FieldsInclude,
    FieldsExclude,
    FieldsCount,
    PropertiesInclude,
    PropertiesExclude,
    PropertiesCount,
    NestedTypeInclude,
    NestedTypeExclude,
    NestedTypeCount,
}