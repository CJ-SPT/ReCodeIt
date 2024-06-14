using ReCodeIt.Utils;

namespace ReCodeIt.Commands
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
                var remapper = new ReMapper.ReCodeItRemapper();

                Logger.ClearLog();
                Console.Clear();
                ShowStartText();

                DataProvider.LoadMappingFile();
                DataProvider.LoadAssemblyDefinition();

                remapper.InitializeRemap();
            }

            if (command == "clear")
            {
                Console.Clear();
                ShowStartText();
            }
        }

        private void ShowStartText()
        {
            Logger.Log($"-----------------------------------------------------------------", ConsoleColor.Green);
            Logger.Log($"Cj's Assembly Tool", ConsoleColor.Green);
            Logger.Log($"Version 0.1.0", ConsoleColor.Green);
            Logger.Log($"Available Commands: `remap` `clear`", ConsoleColor.Green);
            Logger.Log($"-----------------------------------------------------------------", ConsoleColor.Green);
        }
    }
}