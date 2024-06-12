namespace AssemblyRemapper.Utils;

internal static class Logger
{
    static Logger()
    {
        if (File.Exists(_logPath))
        {
            File.Delete(_logPath);
            File.Create(_logPath).Close();
        }
    }

    private static string _logPath = Path.Combine(AppContext.BaseDirectory, "Data", "Log.log");

    public static void Log(string message, ConsoleColor color = ConsoleColor.Gray, bool silent = false)
    {
        if (silent)
        {
            WriteToDisk(message);
            return;
        }

        Console.ForegroundColor = color;
        Console.WriteLine(message);
        Console.ResetColor();
        WriteToDisk(message);
    }

    public static void LogDebug(string message, ConsoleColor color = ConsoleColor.Gray, bool silent = false)
    {
        if (silent)
        {
            WriteToDisk(message);
            return;
        }

        if (DataProvider.AppSettings.Debug)
        {
            Console.ForegroundColor = color;
            Console.WriteLine(message);
            Console.ResetColor();
            WriteToDisk(message);
        }
    }

    private static void WriteToDisk(string message)
    {
        try
        {
            using (StreamWriter sw = File.AppendText(_logPath))
            {
                sw.WriteLine($"{DateTime.Now:yyyy-MM-dd HH:mm:ss} - {message}");
            }
        }
        catch (IOException ex)
        {
            // Handle potential file writing errors gracefully
            Console.WriteLine($"Error logging: {ex.Message}");
        }
    }
}