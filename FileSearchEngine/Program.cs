using System;
using System.Collections.Generic;
using System.Globalization;
using System.Threading;
using System.Threading.Tasks;

using FileSearchEngine.Engine;
using FileSearchEngine.Parser;
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
            Thread.CurrentThread.CurrentCulture = new CultureInfo("ru_RU");

            var files = new List<SearchResult>();
            var engine = new SearchEngine();
            var storage = new MemoryStorage(new FileParser());

            while (true)
            {
                Console.Clear();
                Utils.Print("Введите путь для поиска:");
                // TODO: uncomment
                // string path = Console.ReadLine();
                string path = "/home/oleg-togushakov/Projects/back/TEST/FileSearchEngine/Test";

                ConsoleKey usersInput = default;
                while (!Validator.ValidateSearchOption(usersInput))
                {
                    Utils.Print("Ищем пользователей? (да, нет):");
                    Console.WriteLine("1. Да");
                    Console.WriteLine("2. Нет");
                    usersInput = Console.ReadKey(true).Key;
                }

                ConsoleKey softInput = default;
                while (!Validator.ValidateSearchOption(softInput))
                {
                    Utils.Print("Ищем продукты? (да, нет):");
                    Console.WriteLine("1. Да");
                    Console.WriteLine("2. Нет");
                    softInput = Console.ReadKey(true).Key;
                }

                engine.SetValidExtensions(
                    SearchEngineHelper.CreateValidExtensions(
                        Validator.ApplySearchOption(usersInput), Validator.ApplySearchOption(softInput)));

                try
                {
                    Utils.Print("Поиск...");
                    
                    engine.ScanDirectoryForFiles(path, files);
                    Utils.Print("Поиск закончен");

                    await storage.Fill(files);
                    files.Clear();

                    Console.Clear();
                    storage.PrintResults();

                    await storage.Clear();

                    Utils.Print("Нажмите любую клавишу для продолжения");
                    Console.ReadKey();
                }
                catch (Exception e)
                {
                    Utils.Print("Указан неверный путь. Попробуйте еще раз!");
                    Thread.Sleep(2000);
                }
            }
        }
    }
}


