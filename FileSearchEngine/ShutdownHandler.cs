using System;
using System.Threading;

namespace FileSearchEngine
{
    public static class ShutdownHandler
    {
        private static readonly AutoResetEvent _exitEvent = new (false);

        public static void StartWatching()
        {
            Console.CancelKeyPress += OnExit;
            AppDomain.CurrentDomain.ProcessExit += OnExit;
            _exitEvent.WaitOne();
        }

        private static void OnExit(object? sender, EventArgs e)
        {
            Utils.Log("Завершение работы приложения...");
            _exitEvent.Set();
        }
    }
}