﻿using dnlib.DotNet;
using Newtonsoft.Json;
using ReCodeIt.Models;

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
        var settingsPath = Path.Combine(DataPath, "Settings.jsonc");

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
        if (IsCli) { return; }

        var settingsPath = Path.Combine(DataPath, "Settings.jsonc");

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

    public static void UpdateMapping(string path, List<RemapModel> remaps)
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

        var jsonText = JsonConvert.SerializeObject(remaps, settings);

        File.WriteAllText(path, jsonText);

        Logger.Log($"Mapping file updated with new type names and saved to {path}", ConsoleColor.Yellow);
    }

    public static ModuleDefMD LoadModule(string path)
    {
        var mcOptions = new ModuleCreationOptions(ModuleDef.CreateModuleContext());
        ModuleDefMD module = ModuleDefMD.Load(path, mcOptions);

        module.Context = mcOptions.Context;

        if (module is null)
        {
            throw new NullReferenceException("Module is null...");
        }

        return module;
    }
}