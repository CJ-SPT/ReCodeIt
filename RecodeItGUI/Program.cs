using Microsoft.Win32;
using ReCodeIt.GUI;
using ReCodeIt.Utils;
using ReCodeItLib.Utils;

namespace ReCodeIt;

internal static class Program
{
    /// <summary>
    /// The main entry point for the application.
    /// </summary>
    [STAThread]
    private static void Main()
    {
        RegistryHelper.SetRegistryValue(
            "DataPath",
            DataProvider.DataPath,
            RegistryValueKind.String);

        RegistryHelper.SetRegistryValue(
            "SettingsPath",
            Path.Combine(DataProvider.DataPath, "Settings.jsonc"),
            RegistryValueKind.String);

        RegistryHelper.SetRegistryValue(
            "LogPath",
            Path.Combine(DataProvider.DataPath, "Log.log"),
            RegistryValueKind.String);

        DataProvider.LoadAppSettings();

        // To customize application configuration such as set high DPI settings or default font, see https://aka.ms/applicationconfiguration.
        ApplicationConfiguration.Initialize();
        Application.Run(new ReCodeItForm());
    }
}