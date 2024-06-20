using ReCodeItLib.Utils;

namespace ReCodeIt.Utils;

public static class Logger
{
    static Logger()
    {
        if (File.Exists(_logPath))
        {
            File.Delete(_logPath);
            File.Create(_logPath).Close();
        }
    }

    private static string _logPath => RegistryHelper.GetRegistryValue<string>("LogPath");

    public static void ClearLog()
    {
        if (File.Exists(_logPath))
        {
            File.Delete(_logPath);
            File.Create(_logPath).Close();
        }
    }

    public static void Log(object message, ConsoleColor color = ConsoleColor.Gray, bool silent = false)
    {
        if (!silent)
        {
            Console.ForegroundColor = color;
            Console.WriteLine(message);
        }

        WriteToDisk(message);
    }

    public static void LogDebug(object message, ConsoleColor color = ConsoleColor.Gray, bool silent = false)
    {
        if (DataProvider.Settings.AppSettings.Debug)
        {
            Console.ForegroundColor = color;
            Console.WriteLine(message);
            Console.ResetColor();
            WriteToDisk(message);
        }

        WriteToDisk(message);
    }

    private static void WriteToDisk(object message)
    {
        if (DataProvider.IsCli) { return; }

        try
        {
            using (StreamWriter sw = File.AppendText(_logPath))
            {
                sw.WriteLine($"{DateTime.Now:yyyy-MM-dd HH:mm:ss} - {message}");
                sw.Close();
            }
        }
        catch (IOException ex)
        {
            // Handle potential file writing errors gracefully
            Console.WriteLine($"Error logging: {ex.Message}");
        }
    }
}