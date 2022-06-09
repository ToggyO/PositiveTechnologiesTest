using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using FileSearchEngine.Engine;
using FileSearchEngine.Storage;
using FileSearchEngine.Validation;

namespace FileSearchEngine
{
    class Program
    {
        static void Main(string[] args)
        {
            Task.Run(Do);
            ShutdownHandler.StartWatching();
        }

        static async Task Do()
        {
            var searchResults = new List<SearchResult>();
            var engine = new SearchEngine();
            var storage = new MemoryStorage();

            string path;
            ConsoleKey usersInput;
            ConsoleKey softInput;
            
            while (true)
            {
                Console.Clear();

                Utils.Log("Введите путь для поиска:");
                path = Console.ReadLine();

                GetInputOption(out usersInput,
                    "Ищем пользователей? (Нажмите соотвествующую пункту клавишу):");
                
                GetInputOption(out softInput,
                    "Ищем продукты? (Нажмите соотвествующую пункту клавишу):");
                
                engine.SetValidExtensions(
                    SearchEngineHelper.CreateValidExtensions(
                        Validator.ApplySearchOption(usersInput), Validator.ApplySearchOption(softInput)));

                try
                {
                    Utils.Log("Поиск...");
                    
                    engine.ScanDirectoryForFiles(path, searchResults);
                    Utils.Log("Поиск закончен");

                    await storage.Fill(searchResults);
                    searchResults.Clear();

                    Console.Clear();

                    storage.PrintResults();
                    await storage.Clear();

                    path = default;
                    usersInput = default;
                    softInput = default;
                }
                catch (Exception e)
                {
                    Console.Clear();
                    Utils.Log("Указан неверный путь. Попробуйте еще раз!");
                }


                GetInputOption(out ConsoleKey continuationOptionInput, "Продолжим поиск?");
                if (!Validator.ApplySearchOption(continuationOptionInput))
                    break;
            }

            Environment.Exit(0);
        }

        static void GetInputOption(out ConsoleKey input, string message)
        {
            input = default;
            while (!Validator.ValidateSearchOption(input))
            {
                Utils.Log(message);
                Utils.PrintWithPad("1. Да");
                Utils.PrintWithPad("2. Нет");
                input = Console.ReadKey(true).Key;
            }
        }
    }
}


