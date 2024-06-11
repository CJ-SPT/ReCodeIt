using AssemblyRemapper.Reflection;
using AssemblyRemapper.Utils;

namespace AssemblyRemapper.Commands
{
    internal class CommandProcessor
    {
        public CommandProcessor()
        { }

        public void CommandLoop()
        {
            ShowStartText();

            while (true)
            {
                var input = Console.ReadLine();
                ProcessCommand(input);
            }
        }

        private void ProcessCommand(string command)
        {
            if (command == "remap" || command == "Remap")
            {
                var remapper = new Remapper();

                remapper.InitializeRemap();
            }
        }

        private void ShowStartText()
        {
            Logger.Log($"-----------------------------------------------------------------", ConsoleColor.Green);
            Logger.Log($"Cj's Assembly Tool", ConsoleColor.Green);
            Logger.Log($"Version 0.1.0", ConsoleColor.Green);
            Logger.Log($"Available Commands: `remap` `help`", ConsoleColor.Green);
            Logger.Log($"-----------------------------------------------------------------", ConsoleColor.Green);
        }
    }
}