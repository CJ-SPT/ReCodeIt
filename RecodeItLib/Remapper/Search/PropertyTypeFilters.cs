using dnlib.DotNet;
using ReCodeIt.Models;
using ReCodeIt.Utils;

namespace ReCodeItLib.Remapper.Search;

internal static class PropertyTypeFilters
{
    /// <summary>
    /// Filters based on property includes
    /// </summary>
    /// <param name="types"></param>
    /// <param name="parms"></param>
    /// <returns>Filtered list</returns>
    public static IEnumerable<TypeDef> FilterByInclude(IEnumerable<TypeDef> types, SearchParams parms)
    {
        if (parms.IncludeProperties.Count == 0) return types;

        List<TypeDef> filteredTypes = [];

        foreach (var type in types)
        {
            foreach (var prop in type.Properties)
            {
                if (parms.IncludeProperties.Contains(prop.Name.String))
                {
                    filteredTypes.Add(type);
                }
            }
        }

        return filteredTypes.Any() ? filteredTypes : types;
    }

    /// <summary>
    /// Filters based on property excludes
    /// </summary>
    /// <param name="types"></param>
    /// <param name="parms"></param>
    /// <returns>Filtered list</returns>
    public static IEnumerable<TypeDef> FilterByExclude(IEnumerable<TypeDef> types, SearchParams parms)
    {
        if (parms.ExcludeProperties.Count == 0) return types;

        List<TypeDef> filteredTypes = [];

        foreach (var type in types)
        {
            var match = type.Properties
                .Where(prop => parms.ExcludeProperties.Contains(prop.Name.String));

            if (!match.Any())
            {
                filteredTypes.Add(type);
            }
        }

        return filteredTypes.Any() ? filteredTypes : types;
    }

    /// <summary>
    /// Filters based on property count
    /// </summary>
    /// <param name="types"></param>
    /// <param name="parms"></param>
    /// <returns>Filtered list</returns>
    public static IEnumerable<TypeDef> FilterByCount(IEnumerable<TypeDef> types, SearchParams parms)
    {
        if (parms.PropertyCount is null) return types;

        if (parms.PropertyCount >= 0)
        {
            Logger.Log("Matching property count", ConsoleColor.Yellow);
            types = types.Where(t => t.Properties.Count == parms.PropertyCount);
        }

        return types;
    }
}