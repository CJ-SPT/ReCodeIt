using dnlib.DotNet;
using ReCodeIt.Models;

namespace ReCodeIt.ReMapper.Search;

internal static class CtorTypeFilters
{
    /// <summary>
    /// Search for types with a constructor of a given length
    /// </summary>
    /// <param name="parms"></param>
    /// <param name="score"></param>
    /// <returns>Filtered list</returns>
    public static IEnumerable<TypeDef> FilterByParameterCount(IEnumerable<TypeDef> types, SearchParams parms)
    {
        if (parms.ConstructorParameterCount is null) return types;

        return types
            .Where(type => type.FindConstructors()
                .Any(ctor => ctor.Parameters.Count == parms.ConstructorParameterCount));
    }
}