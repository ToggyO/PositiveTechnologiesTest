using System;

namespace FileSearchEngine
{
    public static class Utils
    {
        public static string DateTimeNow() => DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss");

        public static void Log(string message)
        {
            Console.WriteLine($"[{DateTimeNow()}] {message}");
        }
        
        public static void PrintWithPad(string message)
        {
            Console.WriteLine($"    {message}");
        }
    }
}