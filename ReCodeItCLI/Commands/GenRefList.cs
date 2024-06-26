using CliFx;
using CliFx.Attributes;
using CliFx.Infrastructure;
using dnlib.DotNet;

namespace ReCodeIt.Commands;

[Command("GenRefCountList", Description = "Generates a print out of the most used classes. Useful to prioritize remap targets")]
public class GenRefList : ICommand
{
    [CommandParameter(0, IsRequired = true, Description = "The absolute path to your de-obfuscated dll, remapped dll.")]
    public string AssemblyPath { get; init; }

    private static readonly List<string> Match = new()
    {
        "Class",
        "GClass",
        "GInterface",
        "Interface",
        "GStruct"
    };

    public ValueTask ExecuteAsync(IConsole console)
    {
        var references = CountTypeReferences(AssemblyPath);

        // Sort and display the top 10 most referenced types
        foreach (var pair in references.OrderByDescending(p => p.Value).Take(100))
        {
            Console.WriteLine($"{pair.Key}: {pair.Value}");
        }

        return default;
    }

    public static Dictionary<string, int> CountTypeReferences(string assemblyPath)
    {
        var typeReferenceCounts = new Dictionary<string, int>();

        using (var module = ModuleDefMD.Load(assemblyPath))
        {
            foreach (var type in module.GetTypes())
            {
                CountReferencesInType(type, typeReferenceCounts);
            }
        }

        return typeReferenceCounts;
    }

    private static void CountReferencesInType(TypeDef type, Dictionary<string, int> counts)
    {
        foreach (var method in type.Methods)
        {
            if (Match.Any(item => method.ReturnType.TypeName.StartsWith(item))) IncrementCount(method.ReturnType.TypeName, counts);

            CountReferencesInMethod(method, counts);
        }

        foreach (var field in type.Fields)
        {
            if (field.FieldType.IsValueType || field.FieldType.IsPrimitive) continue;

            if (!Match.Any(item => field.FieldType.TypeName.StartsWith(item))) continue;

            IncrementCount(field.FieldType.FullName, counts);
        }

        foreach (var property in type.Properties)
        {
            if (property.PropertySig.RetType.IsValueType || property.PropertySig.RetType.IsPrimitive) continue;

            if (!Match.Any(item => property.PropertySig.RetType.TypeName.StartsWith(item))) continue;

            IncrementCount(property.PropertySig.RetType.FullName, counts);
        }
    }

    private static void CountReferencesInMethod(MethodDef method, Dictionary<string, int> counts)
    {
        if (!method.HasBody) return;

        foreach (var instr in method.Body.Instructions)
        {
            if (instr.Operand is FieldDef fieldDef && Match.Any(item => fieldDef.FieldType.TypeName.StartsWith(item)))
            {
                IncrementCount(fieldDef.FieldType.FullName, counts);
            }

            if (instr.Operand is PropertyDef propDef && Match.Any(item => propDef.PropertySig.RetType.FullName.StartsWith(item)))
            {
                IncrementCount(propDef.PropertySig.RetType.FullName, counts);
            }

            if (instr.Operand is MethodDef methodDef && Match.Any(item => methodDef.DeclaringType.FullName.StartsWith(item)))
            {
                if (methodDef.ReturnType.IsValueType || methodDef.ReturnType.IsPrimitive || methodDef.ReturnType.IsCorLibType) continue;

                IncrementCount(methodDef.ReturnType.FullName, counts);
            }
        }
    }

    private static void IncrementCount(string typeName, Dictionary<string, int> counts)
    {
        counts[typeName] = counts.GetValueOrDefault(typeName, 0) + 1;
    }
}