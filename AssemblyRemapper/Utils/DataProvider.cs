using AssemblyRemapper.Models;
using Mono.Cecil;
using Newtonsoft.Json;

namespace AssemblyRemapper.Utils;

internal static class DataProvider
{
    static DataProvider()
    {
        LoadAppSettings();
    }

    public static HashSet<RemapModel> Remaps { get; private set; } = [];

    public static Dictionary<string, HashSet<ScoringModel>> ScoringModels { get; set; } = [];

    public static Settings Settings { get; private set; }

    public static AssemblyDefinition AssemblyDefinition { get; private set; }

    public static ModuleDefinition ModuleDefinition { get; private set; }

    private static void LoadAppSettings()
    {
        var settingsPath = Path.Combine(AppContext.BaseDirectory, "Data", "Settings.jsonc");

        if (!File.Exists(settingsPath))
        {
            throw new InvalidOperationException($"path `{settingsPath}` does not exist...");
        }

        var jsonText = File.ReadAllText(settingsPath);

        JsonSerializerSettings settings = new JsonSerializerSettings
        {
            NullValueHandling = NullValueHandling.Ignore
        };

        Settings = JsonConvert.DeserializeObject<Settings>(jsonText, settings);
    }

    public static void LoadMappingFile()
    {
        if (!File.Exists(Settings.RemapperSettings.MappingPath))
        {
            throw new InvalidOperationException($"path `{Settings.RemapperSettings.MappingPath}` does not exist...");
        }

        var jsonText = File.ReadAllText(Settings.RemapperSettings.MappingPath);

        Remaps = [];
        ScoringModels = [];

        Remaps = JsonConvert.DeserializeObject<HashSet<RemapModel>>(jsonText);

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
    }

    public static void UpdateMapping()
    {
        if (!File.Exists(Settings.RemapperSettings.MappingPath))
        {
            throw new InvalidOperationException($"path `{Settings.RemapperSettings.MappingPath}` does not exist...");
        }

        JsonSerializerSettings settings = new JsonSerializerSettings
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

        File.WriteAllText(Settings.RemapperSettings.MappingPath, jsonText);
    }

    public static void LoadAssemblyDefinition()
    {
        DefaultAssemblyResolver resolver = new();
        resolver.AddSearchDirectory(Path.GetDirectoryName(Settings.RemapperSettings.AssemblyPath)); // Replace with the correct path
        ReaderParameters parameters = new() { AssemblyResolver = resolver };

        AssemblyDefinition = AssemblyDefinition.ReadAssembly(Settings.RemapperSettings.AssemblyPath, parameters);

        if (AssemblyDefinition is null)
        {
            throw new InvalidOperationException("AssemblyDefinition was null...");
        }

        var fileName = Path.GetFileName(Settings.RemapperSettings.AssemblyPath);

        foreach (var module in AssemblyDefinition.Modules.ToArray())
        {
            if (module.Name == fileName)
            {
                ModuleDefinition = module;
                return;
            }
        }

        Logger.Log($"Module `{fileName}` not found in assembly {fileName}");
    }
}