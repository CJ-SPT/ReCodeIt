using dnlib.DotNet;
using System.Runtime.CompilerServices;

namespace ReCodeIt.ReMapper;

internal static class SPTPublicizer
{
    private static ModuleDefMD MainModule;

    public static void PublicizeClasses(ModuleDefMD definition)
    {
        var types = definition.GetTypes();

        foreach (var type in types)
        {
            if (type.IsNested) continue; // Nested types are handled when publicizing the parent type

            PublicizeType(type);
        }
    }

    private static void PublicizeType(TypeDef type)
    {
        // if (type.CustomAttributes.Any(a => a.AttributeType.Name ==
        // nameof(CompilerGeneratedAttribute))) { return; }

        if (!type.IsNested && !type.IsPublic || type.IsNested && !type.IsNestedPublic)
        {
            type.Attributes &= ~TypeAttributes.VisibilityMask; // Remove all visibility mask attributes
            type.Attributes |= type.IsNested ? TypeAttributes.NestedPublic : TypeAttributes.Public; // Apply a public visibility attribute
        }

        if (type.IsSealed)
        {
            type.Attributes &= ~TypeAttributes.Sealed; // Remove the Sealed attribute if it exists
        }

        foreach (var method in type.Methods)
        {
            PublicizeMethod(method);
        }

        foreach (var property in type.Properties)
        {
            if (property.GetMethod != null) PublicizeMethod(property.GetMethod);
            if (property.SetMethod != null) PublicizeMethod(property.SetMethod);
        }

        // var eventNames = new HashSet<string>(type.Events.Select(e => e.Name)); foreach (var field
        // in type.Fields) { if (eventNames.Contains(field.Name)) { continue; }
        //
        // // if (type.Name.StartsWith("GClass") || !type.Namespace.Contains("EFT") ||
        // !type.Namespace.Contains("UI") || !string.IsNullOrWhiteSpace(type.Namespace)) // if
        // (type.Namespace.Length > 0 && type.Namespace[0] > 'E') PublicizeField(field); }

        var nestedTypesToPublicize = type.NestedTypes.ToArray();

        // Workaround to not publicize some nested types that cannot be patched easily and cause
        // issues Specifically, we want to find any type that implements the "IHealthController"
        // interface and make sure none of it's nested types that implement "IEffect" are changed
        if (GetFlattenedInterfacesRecursive(type).Any(i => i.Interface.Name == "IHealthController"))
        {
            // Specifically, any type that implements the IHealthController interface needs to not
            // publicize any nested types that implement the IEffect interface
            nestedTypesToPublicize = type.NestedTypes.Where(t => t.IsAbstract || t.Interfaces.All(i => i.Interface.Name != "IEffect")).ToArray();
        }

        foreach (var nestedType in nestedTypesToPublicize)
        {
            PublicizeType(nestedType);
        }
    }

    private static void PublicizeMethod(MethodDef method)
    {
        if (method.IsCompilerControlled /*|| method.CustomAttributes.Any(a => a.AttributeType.Name == nameof(CompilerGeneratedAttribute))*/)
        {
            return;
        }

        if (method.IsPublic) return;

        // if (!CanPublicizeMethod(method)) return;

        // Workaround to not publicize a specific method so the game doesn't crash
        if (method.Name == "TryGetScreen") return;

        method.Attributes &= ~MethodAttributes.MemberAccessMask;
        method.Attributes |= MethodAttributes.Public;
    }

    // Unused for now - publicizing fields is tricky, as it often creates MonoBehaviour loading
    // errors and prevents scenes from loading, most notably breaking the initial game loader scene
    // and causing the game to CTD right after starting
    private static void PublicizeField(FieldDef field)
    {
        if (field.CustomAttributes.Any(a => a.AttributeType.Name == nameof(CompilerGeneratedAttribute))
            // || field.HasCustomAttributes
            || field.Name.StartsWith("delegate")
            || field.Name.Contains("__BackingField"))
        {
            return;
        }

        if (field.IsPublic || field.IsCompilerControlled || field.IsLiteral || field.IsStatic || field.IsInitOnly) return;

        field.Attributes &= ~FieldAttributes.FieldAccessMask;
        field.Attributes |= FieldAttributes.Public;
    }

    private static List<InterfaceImpl> GetFlattenedInterfacesRecursive(TypeDef type)
    {
        var interfaces = new List<InterfaceImpl>();

        if (type is null) return interfaces;

        if (type.Interfaces.Any())
        {
            interfaces.AddRange(type.Interfaces);
        }

        if (type.BaseType != null && !type.BaseType.Name.Contains("Object"))
        {
            var baseTypeDefinition = MainModule?.Find(type.BaseType);
            var baseTypeInterfaces = GetFlattenedInterfacesRecursive(baseTypeDefinition);

            if (baseTypeInterfaces.Any())
            {
                interfaces.AddRange(baseTypeInterfaces);
            }
        }

        return interfaces;
    }
}