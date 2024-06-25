namespace ReCodeIt.Enums;

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