using System;
using System.Threading;

namespace FileSearchEngine
{
    public class ShutdownHandler
    {
        private static readonly AutoResetEvent _exitEvent = new (false);

        public static void StartWatching()
        {
            Console.CancelKeyPress += (_, ea) =>
            {
                Utils.Print("Завершение работы приложения...");
                _exitEvent.Set();
            };
            _exitEvent.WaitOne();
        }
    }
}