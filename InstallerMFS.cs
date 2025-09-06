using System.ComponentModel;
using System.ServiceProcess;

namespace FileMonitoringWindowsService
{
    [RunInstaller(true)]
    public partial class InstallerMFS : System.Configuration.Install.Installer
    {
        public InstallerMFS()
        {
            InitializeComponent();
            var processInstaller = new ServiceProcessInstaller
            {
                Account = ServiceAccount.LocalSystem
            };

            var serviceInstaller = new ServiceInstaller
            {
                ServiceName = "FileMonitoringService",
                DisplayName = "File Monitoring Service",
                StartType = ServiceStartMode.Manual,
                ServicesDependedOn = new string[] { "RpcSs", "EventLog", "LanmanWorkstation" };
            };

            Installers.Add(processInstaller);
            Installers.Add(serviceInstaller);
        }
    }
}