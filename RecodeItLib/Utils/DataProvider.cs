using dnlib.DotNet;
using Newtonsoft.Json;
using ReCodeIt.Models;
using ReCodeItLib.Utils;

namespace ReCodeIt.Utils;

public static class DataProvider
{
    static DataProvider()
    {
        if (!Directory.Exists(ReCodeItProjectsPath))
        {
            Directory.CreateDirectory(ReCodeItProjectsPath);
        }
    }

    /// <summary>
    /// Is this running in the CLI?
    /// </summary>
    public static bool IsCli { get; set; } = false;

    public static string DataPath => Path.Combine(AppContext.BaseDirectory, "Data");

    public static readonly string ReCodeItProjectsPath = Path.Combine(AppContext.BaseDirectory, "Projects");

    public static List<RemapModel> Remaps { get; set; } = [];

    public static Settings Settings { get; private set; }

    public static void LoadAppSettings()
    {
        if (IsCli)
        {
            Settings = CreateFakeSettings();
            return;
        }

        var settingsPath = Path.Combine(DataPath, "Settings.jsonc");

        var jsonText = File.ReadAllText(settingsPath);

        JsonSerializerSettings settings = new JsonSerializerSettings
        {
            NullValueHandling = NullValueHandling.Ignore
        };

        Settings = JsonConvert.DeserializeObject<Settings>(jsonText, settings);

        if (Settings is null)
        {
            Logger.Log("Settings were null, creating new settings", ConsoleColor.Red);
            Settings = CreateFakeSettings();
            SaveAppSettings();
        }

        Logger.Log($"Settings loaded from '{settingsPath}'");
    }

    public static void SaveAppSettings()
    {
        if (IsCli) { return; }

        var settingsPath = RegistryHelper.GetRegistryValue<string>("SettingsPath");

        if (!File.Exists(settingsPath))
        {
            Logger.Log($"path `{settingsPath}` does not exist. Could not save settings", ConsoleColor.Red);
            return;
        }

        JsonSerializerSettings settings = new()
        {
            Formatting = Formatting.Indented
        };

        var jsonText = JsonConvert.SerializeObject(Settings, settings);

        File.WriteAllText(settingsPath, jsonText);

        //Logger.Log($"App settings saved to {settingsPath}");
    }

    public static List<RemapModel> LoadMappingFile(string path)
    {
        if (!File.Exists(path))
        {
            Logger.Log($"Error loading mapping.json from `{path}`, First time running? Please select a mapping path in the gui", ConsoleColor.Red);
            return [];
        }

        var jsonText = File.ReadAllText(path);

        var remaps = JsonConvert.DeserializeObject<List<RemapModel>>(jsonText);

        if (remaps == null) { return []; }

        Logger.Log($"Mapping file loaded from '{path}' containing {remaps.Count} remaps");

        return remaps;
    }

    public static void SaveMapping()
    {
        JsonSerializerSettings settings = new()
        {
            NullValueHandling = NullValueHandling.Ignore,
            Formatting = Formatting.Indented
        };

        var path = Settings.Remapper.MappingPath;

        var jsonText = JsonConvert.SerializeObject(Remaps, settings);

        File.WriteAllText(path, jsonText);
        Logger.Log($"Mapping File Saved To {path}");
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

    public static ModuleDefMD LoadModule(string path)
    {
        ModuleContext modCtx = ModuleDef.CreateModuleContext();
        ModuleDefMD module = ModuleDefMD.Load(path, modCtx);

        if (module is null)
        {
            throw new NullReferenceException("Module is null...");
        }

        return module;
    }

    private static Settings CreateFakeSettings()
    {
        var settings = new Settings
        {
            AppSettings = new AppSettings
            {
                Debug = false,
                SilentMode = true
            },
            Remapper = new RemapperSettings
            {
                MappingPath = string.Empty,
                OutputPath = string.Empty,
                UseProjectMappings = false,
                MappingSettings = new MappingSettings
                {
                    RenameFields = false,
                    RenameProperties = false,
                    Publicize = false,
                    Unseal = false,
                }
            },
            AutoMapper = new AutoMapperSettings
            {
                AssemblyPath = string.Empty,
                OutputPath = string.Empty,
                RequiredMatches = 5,
                MinLengthToMatch = 7,
                SearchMethods = true,
                MappingSettings = new MappingSettings
                {
                    RenameFields = false,
                    RenameProperties = false,
                    Publicize = false,
                    Unseal = false,
                },
                TypesToIgnore = [
                    "Boolean",
                    "List",
                    "Dictionary",
                    "Byte",
                    "Int16",
                    "Int32",
                    "Func",
                    "Action",
                    "Object",
                    "String",
                    "Vector2",
                    "Vector3",
                    "Vector4",
                    "Stream",
                    "HashSet",
                    "Double",
                    "IEnumerator"
                ],
                TokensToMatch = [
                    "Class",
                    "GClass",
                    "GStruct",
                    "Interface",
                    "GInterface"
                ],
                PropertyFieldBlackList = [
                    "Columns",
                    "mColumns",
                    "Template",
                    "Condition",
                    "Conditions",
                    "Counter",
                    "Instance",
                    "Command",
                    "_template"
                ],
                MethodParamaterBlackList = [

                ],
            },
            CrossCompiler = new CrossCompilerSettings
            {
                LastLoadedProject = string.Empty,
                AutoLoadLastActiveProject = true
            }
        };

        return settings;
    }
}