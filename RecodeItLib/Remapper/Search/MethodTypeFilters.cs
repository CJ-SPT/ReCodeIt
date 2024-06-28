using dnlib.DotNet;
using ReCodeIt.Models;
using ReCodeIt.Utils;

namespace ReCodeItLib.Remapper.Search;

internal static class MethodTypeFilters
{
    /// <summary>
    /// Filters based on method includes
    /// </summary>
    /// <param name="types"></param>
    /// <param name="parms"></param>
    /// <returns>Filtered list</returns>
    public static IEnumerable<TypeDef> FilterByInclude(IEnumerable<TypeDef> types, SearchParams parms)
    {
        if (parms.IncludeMethods.Count == 0) return types;

        List<TypeDef> filteredTypes = [];

        foreach (var type in types)
        {
            if (parms.IncludeMethods
                .All(includeName => type.Methods
                    .Any(method => method.Name.String == includeName)))
            {
                filteredTypes.Add(type);
            }
        }

        return filteredTypes.Any() ? filteredTypes : types;
    }

    /// <summary>
    /// Filters based on method excludes
    /// </summary>
    /// <param name="types"></param>
    /// <param name="parms"></param>
    /// <returns>Filtered list</returns>
    public static IEnumerable<TypeDef> FilterByExclude(IEnumerable<TypeDef> types, SearchParams parms)
    {
        if (parms.ExcludeMethods.Count == 0) return types;

        List<TypeDef> filteredTypes = [];

        foreach (var type in types)
        {
            var match = type.Methods
                .Where(method => parms.ExcludeMethods.Contains(method.Name.String));

            if (!match.Any())
            {
                filteredTypes.Add(type);
            }
        }

        return filteredTypes.Any() ? filteredTypes : types;
    }

    /// <summary>
    /// Filters based on method count
    /// </summary>
    /// <param name="types"></param>
    /// <param name="parms"></param>
    /// <returns>Filtered list</returns>
    public static IEnumerable<TypeDef> FilterByCount(IEnumerable<TypeDef> types, SearchParams parms)
    {
        if (parms.MethodCount is null) return types;

        if (parms.MethodCount >= 0)
        {
            Logger.Log("Matching method count", ConsoleColor.Yellow);
            types = types.Where(t => GetMethodCountExcludingConstructors(t) == parms.MethodCount);
        }

        return types;
    }

    /// <summary>
    /// We don't want the constructors included in the count
    /// </summary>
    /// <param name="type"></param>
    /// <returns></returns>
    private static int GetMethodCountExcludingConstructors(TypeDef type)
    {
        int count = 0;
        foreach (var method in type.Methods)
        {
            if (!method.IsConstructor && !method.IsSpecialName && !method.IsGetter && !method.IsSetter)
            {
                count++;
            }
        }
        return count;
    }
}