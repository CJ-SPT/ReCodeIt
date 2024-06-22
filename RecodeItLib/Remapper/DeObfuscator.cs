using Mono.Cecil;
using Mono.Cecil.Cil;
using ReCodeIt.Utils;
using System.Diagnostics;

namespace ReCodeItLib.Remapper;

public static class Deobfuscator
{
    public static void Deobfuscate(string assemblyPath)
    {
        var executablePath = Path.Combine(DataProvider.DataPath, "De4dot", "de4dot.exe");

        string token;

        using (var assemblyDefinition = AssemblyDefinition.ReadAssembly(assemblyPath))
        {
            var potentialStringDelegates = new List<MethodDefinition>();

            foreach (var type in assemblyDefinition.MainModule.Types)
            {
                foreach (var method in type.Methods)
                {
                    if (method.ReturnType.FullName != "System.String"
                        || method.Parameters.Count != 1
                        || method.Parameters[0].ParameterType.FullName != "System.Int32"
                        || method.Body == null
                        || !method.IsStatic)
                    {
                        continue;
                    }

                    if (!method.Body.Instructions.Any(x =>
                        x.OpCode.Code == Code.Callvirt &&
                        ((MethodReference)x.Operand).FullName == "System.Object System.AppDomain::GetData(System.String)"))
                    {
                        continue;
                    }

                    potentialStringDelegates.Add(method);
                }
            }

            if (potentialStringDelegates.Count != 1)
            {
                Logger.Log($"Expected to find 1 potential string delegate method; found {potentialStringDelegates.Count}. Candidates: {string.Join("\r\n", potentialStringDelegates.Select(x => x.FullName))}");
            }

            var deobfRid = potentialStringDelegates[0].MetadataToken;

            token = $"0x{((uint)deobfRid.TokenType | deobfRid.RID):x4}";

            Console.WriteLine($"Deobfuscation token: {token}");
        }

        var process = Process.Start(executablePath,
            $"--un-name \"!^<>[a-z0-9]$&!^<>[a-z0-9]__.*$&![A-Z][A-Z]\\$<>.*$&^[a-zA-Z_<{{$][a-zA-Z_0-9<>{{}}$.`-]*$\" \"{assemblyPath}\" --strtyp delegate --strtok \"{token}\"");

        process.WaitForExit();

        // Fixes "ResolutionScope is null" by rewriting the assembly
        var cleanedDllPath = Path.Combine(Path.GetDirectoryName(assemblyPath), Path.GetFileNameWithoutExtension(assemblyPath) + "-cleaned.dll");

        var resolver = new DefaultAssemblyResolver();
        resolver.AddSearchDirectory(Path.GetDirectoryName(assemblyPath));

        using (var memoryStream = new MemoryStream(File.ReadAllBytes(cleanedDllPath)))
        using (var assemblyDefinition = AssemblyDefinition.ReadAssembly(memoryStream, new ReaderParameters()
        {
            AssemblyResolver = resolver
        }))
        {
            assemblyDefinition.Write(cleanedDllPath);
        }
    }
}