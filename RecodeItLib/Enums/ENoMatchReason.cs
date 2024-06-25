namespace ReCodeIt.Enums;

public enum ENoMatchReason
{
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