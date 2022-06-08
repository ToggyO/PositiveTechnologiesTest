using System;

namespace FileSearchEngine.Validation
{
    public static class Validator
    {
        public static bool ValidateSearchOption(ConsoleKey input)
        {
            switch (input)
            {
                case ConsoleKey.D1:
                case ConsoleKey.NumPad1:
                case ConsoleKey.D2:
                case ConsoleKey.NumPad2:
                    return true;
                default:
                    return false;
            }
        }

        public static bool ApplySearchOption(ConsoleKey key) => key == ConsoleKey.D1 || key == ConsoleKey.NumPad1;
    }
}