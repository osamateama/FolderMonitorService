using System;
using System.IO;

namespace FileMonitoringWindowsService
{
    public static class Logger
    {
        private static readonly object _lock = new object();

        public static void Log(string folder, string message, bool toConsole = false)
        {
            string logFile = Path.Combine(folder, $"ServiceLog_{DateTime.Now:yyyyMMdd}.txt");
            string line = $"[{DateTime.Now:yyyy-MM-dd HH:mm:ss}] {message}";

            lock (_lock)
            {
                File.AppendAllText(logFile, line + Environment.NewLine);
            }

            if (toConsole)
                Console.WriteLine(line);
        }
    }
}
