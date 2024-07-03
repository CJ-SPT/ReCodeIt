using Microsoft.Win32;
using ReCodeIt.GUI;
using ReCodeIt.Utils;

namespace ReCodeIt;

internal static class Program
{
    /// <summary>
    /// The main entry point for the application.
    /// </summary>
    [STAThread]
    private static void Main()
    {
        DataProvider.LoadAppSettings();

        // To customize application configuration such as set high DPI settings or default font, see https://aka.ms/applicationconfiguration.
        ApplicationConfiguration.Initialize();
        Application.Run(new ReCodeItForm());
    }
}