using System.Collections.Concurrent;
using ReCodeItLib.Utils;

namespace ReCodeIt.Utils;

public static class Logger
{
    private static readonly ConcurrentQueue<LogMessage> _messages = new();
    private static bool Running = true;
    private static bool IsTerminated;
    static Logger()
    {
        if (File.Exists(_logPath))
        {
            File.Delete(_logPath);
            File.Create(_logPath).Close();
        }

        Task.Factory.StartNew(LogThread, TaskCreationOptions.LongRunning);
    }

    private static void LogThread()
    {
        while (Running)
        {
            Thread.Sleep(TimeSpan.FromMilliseconds(100));
            while (_messages.TryDequeue(out var message))
            {
                LogInternal(message);
            }
        }

        IsTerminated = true;
    }

    public static void Terminate()
    {
        Running = false;
    }

    public static bool IsRunning()
    {
        return !IsTerminated;
    }

    private const string _defaultFileName = "ReCodeIt.log";
    private static string _logPath => RegistryHelper.GetRegistryValue<string>("LogPath") ?? $"{AppDomain.CurrentDomain.BaseDirectory}{_defaultFileName}";

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
        _messages.Enqueue(new LogMessage() {Message = message, Color = color, Silent = silent});
    }
    
    private static void LogInternal(LogMessage message)
    {
        if (!message.Silent)
        {
            Console.ForegroundColor = message.Color;
            Console.WriteLine(message.Message);
            Console.ResetColor();
        }

        WriteToDisk(message.Message);
    }

    private static void WriteToDisk(object message)
    {
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
    private class LogMessage
    {
        public object Message { get; init; }
        public ConsoleColor Color { get; init; }
        public bool Silent { get; init; }
    }
}