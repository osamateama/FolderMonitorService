using System;
using System.ServiceProcess;

namespace FileMonitoringWindowsService
{
    internal static class Program
    {
        /// <summary>
        /// The entry point of the application, determining the mode of operation based on the environment.
        /// </summary>
        /// <remarks>If the application is running in an interactive user environment, it starts the
        /// service in console mode,  allowing for debugging and manual control. In this mode, the service can be
        /// stopped by pressing the 'Q' key. If the application is running as a Windows Service, it initializes and runs
        /// the service in the standard  Windows Service mode.</remarks>
        static void Main()
        {
            if (Environment.UserInteractive)
            {
                // Console Mode (debugging)
                var service = new FileMonitoringWindowsService();
                service.StartForConsole();

                Console.WriteLine("Service running in Console mode. Press Q to quit.");
                while (Console.ReadKey(true).Key != ConsoleKey.Q) { }

                service.StopForConsole();
            }
            else
            {
                // Windows Service Mode
                ServiceBase[] ServicesToRun;
                ServicesToRun = new ServiceBase[]
                {
                    new FileMonitoringWindowsService()
                };
                ServiceBase.Run(ServicesToRun);
            }
        }
    }
}
