using Mono.Cecil;
using Newtonsoft.Json;
using ReCodeIt.Models;

namespace ReCodeIt.Utils;

public static class DataProvider
{
    static DataProvider()
    {
    }

    public static List<RemapModel> Remaps { get; private set; } = [];

    public static Dictionary<string, HashSet<ScoringModel>> ScoringModels { get; set; } = [];

    public static Settings Settings { get; private set; }

    public static AssemblyDefinition AssemblyDefinition { get; private set; }

    public static AssemblyDefinition NameMangledAssemblyDefinition { get; private set; }

    public static ModuleDefinition ModuleDefinition { get; private set; }

    public static ModuleDefinition NameMangledModuleDefinition { get; private set; }

    public static void LoadAppSettings()
    {
        var settingsPath = Path.Combine(AppContext.BaseDirectory, "Data", "Settings.jsonc");

        if (!File.Exists(settingsPath))
        {
            throw new FileNotFoundException($"path `{settingsPath}` does not exist...");
        }

        var jsonText = File.ReadAllText(settingsPath);

        JsonSerializerSettings settings = new JsonSerializerSettings
        {
            NullValueHandling = NullValueHandling.Ignore
        };

        Settings = JsonConvert.DeserializeObject<Settings>(jsonText, settings);

        Logger.Log($"Settings loaded from '{settingsPath}'");
    }

    public static void SaveAppSettings()
    {
        var settingsPath = Path.Combine(AppContext.BaseDirectory, "Data", "Settings.jsonc");

        if (!File.Exists(settingsPath))
        {
            throw new FileNotFoundException($"path `{settingsPath}` does not exist...");
        }

        JsonSerializerSettings settings = new()
        {
            Formatting = Formatting.Indented
        };

        var jsonText = JsonConvert.SerializeObject(Settings, settings);

        File.WriteAllText(settingsPath, jsonText);
    }

    public static void LoadMappingFile(string path)
    {
        if (!File.Exists(path))
        {
            Logger.Log($"Error loading mapping.json from `{path}`, First time running? Please select a mapping path");
        }

        var jsonText = File.ReadAllText(path);

        Remaps = [];
        ScoringModels = [];

        Remaps = JsonConvert.DeserializeObject<List<RemapModel>>(jsonText);

        var properties = typeof(SearchParams).GetProperties();

        foreach (var remap in Remaps)
        {
            foreach (var property in properties)
            {
                if (property.PropertyType == typeof(List<string>) && property.GetValue(remap.SearchParams) is null)
                {
                    property.SetValue(remap.SearchParams, new List<string>());
                }
            }
        }

        Logger.Log($"Mapping file loaded from '{path}' containing {Remaps.Count} remaps");
    }

    public static void SaveMapping(string path)
    {
        JsonSerializerSettings settings = new()
        {
            NullValueHandling = NullValueHandling.Ignore,
            Formatting = Formatting.Indented
        };

        var jsonText = JsonConvert.SerializeObject(Remaps, settings);

        File.WriteAllText(path, jsonText);
    }

    public static void UpdateMapping(string path)
    {
        if (!File.Exists(path))
        {
            throw new FileNotFoundException($"path `{path}` does not exist...");
        }

        JsonSerializerSettings settings = new()
        {
            NullValueHandling = NullValueHandling.Ignore,
            Formatting = Formatting.Indented
        };

        var properties = typeof(SearchParams).GetProperties();

        foreach (var remap in Remaps)
        {
            foreach (var property in properties)
            {
                if (property.PropertyType == typeof(List<string>))
                {
                    var val = property.GetValue(remap.SearchParams);

                    if (val is List<string> list && list.Count > 0) { continue; }

                    property.SetValue(remap.SearchParams, null);
                }
            }
        }

        var jsonText = JsonConvert.SerializeObject(Remaps, settings);

        File.WriteAllText(path, jsonText);

        Logger.Log($"Mapping file saved to {path}");
    }

    public static void LoadAssemblyDefinition(string path)
    {
        AssemblyDefinition = null;
        ModuleDefinition = null;

        DefaultAssemblyResolver resolver = new();

        resolver.AddSearchDirectory(Path.GetDirectoryName(path)); // Replace with the correct path : (6/14) I have no idea what I met by that
        ReaderParameters parameters = new() { AssemblyResolver = resolver };

        var assemblyDefinition = AssemblyDefinition.ReadAssembly(
            path,
            parameters);

        if (assemblyDefinition is null)
        {
            throw new NullReferenceException("AssemblyDefinition was null...");
        }

        var fileName = Path.GetFileName(path);

        foreach (var module in assemblyDefinition.Modules.ToArray())
        {
            if (module.Name == fileName)
            {
                Logger.Log($"Module definition {module.Name} found");

                AssemblyDefinition = assemblyDefinition;
                ModuleDefinition = module;
                return;
            }
        }

        Logger.Log($"Module {fileName} not found in assembly {fileName}");
    }

    public static string WriteAssemblyDefinition(string path)
    {
        var filename = Path.GetFileNameWithoutExtension(path);
        var strippedPath = Path.GetDirectoryName(filename);

        filename = $"{filename}-Remapped.dll";

        var remappedPath = Path.Combine(strippedPath, filename);

        AssemblyDefinition.Write(remappedPath);

        return remappedPath;
    }
}