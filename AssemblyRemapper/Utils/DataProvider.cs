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

    public static AppSettings AppSettings { get; private set; }

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

        AppSettings = JsonConvert.DeserializeObject<AppSettings>(jsonText, settings);
    }

    public static void LoadMappingFile()
    {
        if (!File.Exists(AppSettings.MappingPath))
        {
            throw new InvalidOperationException($"path `{AppSettings.MappingPath}` does not exist...");
        }

        var jsonText = File.ReadAllText(AppSettings.MappingPath);

        Remaps = [];
        ScoringModels = [];

        Remaps = JsonConvert.DeserializeObject<HashSet<RemapModel>>(jsonText);
    }

    public static void LoadAssemblyDefinition()
    {
        DefaultAssemblyResolver resolver = new();
        resolver.AddSearchDirectory(Path.GetDirectoryName(AppSettings.AssemblyPath)); // Replace with the correct path
        ReaderParameters parameters = new() { AssemblyResolver = resolver };

        AssemblyDefinition = AssemblyDefinition.ReadAssembly(AppSettings.AssemblyPath, parameters);

        if (AssemblyDefinition is null)
        {
            throw new InvalidOperationException("AssemblyDefinition was null...");
        }

        var fileName = Path.GetFileName(AppSettings.AssemblyPath);

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