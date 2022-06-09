using System;
using System.Collections.Generic;

namespace FileSearchEngine.Printer
{
    public static class ResultsPrinter
    {
        public static void Print<TSource>(IEnumerable<TSource> source)
            where TSource : class
        {
            int index = 1;
            foreach (var s in source)
            {
                Utils.PrintWithPad($"{index}: {s}");
                index++;
            }
        }
    }
}