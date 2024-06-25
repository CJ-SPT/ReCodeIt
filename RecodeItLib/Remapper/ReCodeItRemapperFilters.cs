using dnlib.DotNet;

namespace ReCodeIt.ReMapper;

public partial class ReCodeItRemapper
{
    /// <summary>
    /// Only iterate over types that are public
    /// </summary>
    /// <param name="mapping"></param>
    /// <param name="types"></param>
    private IEnumerable<TypeDef> FilterToPublic(IEnumerable<TypeDef> types)
    {
        return types.Where(t => t.IsPublic);
    }

    /// <summary>
    /// Only iterate over types that are non-public
    /// </summary>
    /// <param name="mapping"></param>
    /// <param name="types"></param>
    private IEnumerable<TypeDef> FilterToNonPublic(IEnumerable<TypeDef> types)
    {
        return types.Where(t => t.IsNotPublic);
    }

    /// <summary>
    /// Only iterate over types that are abstract
    /// </summary>
    /// <param name="mapping"></param>
    /// <param name="types"></param>
    private IEnumerable<TypeDef> FilterToAbstract(IEnumerable<TypeDef> types)
    {
        return types.Where(t => t.IsAbstract);
    }

    /// <summary>
    /// Only iterate over types that are interfaces
    /// </summary>
    /// <param name="mapping"></param>
    /// <param name="types"></param>
    private IEnumerable<TypeDef> FilterToInterfaces(IEnumerable<TypeDef> types)
    {
        return types.Where(t => t.IsInterface);
    }
}