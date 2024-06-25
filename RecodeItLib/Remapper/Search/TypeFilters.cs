using dnlib.DotNet;
using ReCodeIt.Models;
using ReCodeIt.Utils;

namespace ReCodeItLib.Remapper.Search;

internal static class TypeFilters
{
    /// <summary>
    /// Filters based on public, or nested public or private if the nested flag is set. This is a
    /// required property
    /// </summary>
    /// <param name="types"></param>
    /// <param name="parms"></param>
    /// <returns>Filtered list</returns>
    public static IEnumerable<TypeDef> FilterPublic(IEnumerable<TypeDef> types, SearchParams parms)
    {
        // REQUIRED PROPERTY
        if (parms.IsPublic is true)
        {
            if (parms.IsNested is true)
            {
                Logger.Log("IsNested Public", ConsoleColor.Yellow);

                types = types.Where(t => t.IsNestedPublic);
            }
            else
            {
                Logger.Log("IsPublic is true", ConsoleColor.Yellow);
                types = types.Where(t => t.IsPublic);
            }
        }
        else if (parms.IsPublic is false)
        {
            if (parms.IsNested is true)
            {
                Logger.Log("IsNested Private or family", ConsoleColor.Yellow);

                types = types.Where(t => t.IsNestedPrivate
                                         || t.IsNestedFamily
                                         || t.IsNestedFamilyAndAssembly
                                         || t.IsNestedAssembly);
            }
            else
            {
                Logger.Log("IsPublic is false", ConsoleColor.Yellow);
                types = types.Where(t => t.IsNotPublic);
            }
        }
        else
        {
            Logger.Log("ERROR: IsPublic is null, skipping...", ConsoleColor.Red);
            return [];
        }

        return types;
    }

    /// <summary>
    /// Filters based on IsAbstract
    /// </summary>
    /// <param name="types"></param>
    /// <param name="parms"></param>
    /// <returns>Filtered list</returns>
    public static IEnumerable<TypeDef> FilterAbstract(IEnumerable<TypeDef> types, SearchParams parms)
    {
        // Filter based on abstract or not
        if (parms.IsAbstract is true)
        {
            Logger.Log("IsAbstract is true", ConsoleColor.Yellow);
            types = types.Where(t => t.IsAbstract);
        }
        else if (parms.IsAbstract is false)
        {
            Logger.Log("IsAbstract is false", ConsoleColor.Yellow);
            types = types.Where(t => !t.IsAbstract);
        }

        return types;
    }

    /// <summary>
    /// Filters based on IsInterface
    /// </summary>
    /// <param name="types"></param>
    /// <param name="parms"></param>
    /// <returns>Filtered list</returns>
    public static IEnumerable<TypeDef> FilterInterface(IEnumerable<TypeDef> types, SearchParams parms)
    {
        // Filter based on interface or not
        if (parms.IsInterface is true)
        {
            Logger.Log("IsInterface is true", ConsoleColor.Yellow);
            types = types.Where(t => t.IsInterface);
        }
        else if (parms.IsInterface is false)
        {
            Logger.Log("IsInterface is false", ConsoleColor.Yellow);
            types = types.Where(t => !t.IsInterface);
        }

        return types;
    }

    /// <summary>
    /// Filters based on IsStruct
    /// </summary>
    /// <param name="types"></param>
    /// <param name="parms"></param>
    /// <returns>Filtered list</returns>
    public static IEnumerable<TypeDef> FilterStruct(IEnumerable<TypeDef> types, SearchParams parms)
    {
        if (parms.IsStruct is true)
        {
            Logger.Log("IsStruct is true", ConsoleColor.Yellow);
            types = types.Where(t => t.IsValueType && !t.IsEnum && !t.IsClass || !t.IsInterface);
        }
        else if (parms.IsStruct is false)
        {
            Logger.Log("IsStruct is false", ConsoleColor.Yellow);
            types = types.Where(t => !t.IsValueType && t.IsClass || t.IsEnum || t.IsInterface);
        }

        return types;
    }

    /// <summary>
    /// Filters based on IsEnum
    /// </summary>
    /// <param name="types"></param>
    /// <param name="parms"></param>
    /// <returns>Filtered list</returns>
    public static IEnumerable<TypeDef> FilterEnum(IEnumerable<TypeDef> types, SearchParams parms)
    {
        // Filter based on enum or not
        if (parms.IsEnum is true)
        {
            Logger.Log("IsEnum is true", ConsoleColor.Yellow);
            types = types.Where(t => t.IsEnum);
        }
        else if (parms.IsEnum is false)
        {
            Logger.Log("IsEnum is false", ConsoleColor.Yellow);
            types = types.Where(t => !t.IsEnum);
        }

        return types;
    }

    /// <summary>
    /// Filters based on HasAttribute
    /// </summary>
    /// <param name="types"></param>
    /// <param name="parms"></param>
    /// <returns>Filtered list</returns>
    public static IEnumerable<TypeDef> FilterAttributes(IEnumerable<TypeDef> types, SearchParams parms)
    {
        // Filter based on HasAttribute or not
        if (parms.HasAttribute is true)
        {
            Logger.Log("HasAttribute is true", ConsoleColor.Yellow);
            types = types.Where(t => t.HasCustomAttributes);
        }
        else if (parms.HasAttribute is false)
        {
            Logger.Log("HasAttribute is false", ConsoleColor.Yellow);
            types = types.Where(t => !t.HasCustomAttributes);
        }

        return types;
    }

    /// <summary>
    /// Filters based on HasAttribute
    /// </summary>
    /// <param name="types"></param>
    /// <param name="parms"></param>
    /// <returns>Filtered list</returns>
    public static IEnumerable<TypeDef> FilterDerived(IEnumerable<TypeDef> types, SearchParams parms)
    {
        // Filter based on IsDerived or not
        if (parms.IsDerived is true)
        {
            Logger.Log("IsDerived is true", ConsoleColor.Yellow);
            types = types.Where(t => t.GetBaseType() is not null && t.GetBaseType().Name.String == parms.MatchBaseClass);
        }
        else if (parms.IsDerived is false)
        {
            Logger.Log("IsDerived is false", ConsoleColor.Yellow);
            types = types.Where(t => t.GetBaseType().Name.String is "System.Object");
        }

        return types;
    }
}