using System;
using System.Configuration;
using System.IO;
using System.ServiceProcess;
using System.Threading;

namespace FileMonitoringWindowsService
{
    public partial class FileMonitoringWindowsService : ServiceBase
    {

        private FileSystemWatcher _watcher;
        private string _sourceFolder;
        private string _destinationFolder;
        private string _logFolder;
        private bool _stopping;

        
        public FileMonitoringWindowsService()
        {
            InitializeComponent();
            this.ServiceName = "FileMonitoringService";
            CanStop = true;
            CanPauseAndContinue = false;
            AutoLog = false; // We'll handle our own logging
        }

        #region Service lifecycle
        protected override void OnStart(string[] args)
        {

            Initialize();
            Logger.Log(_logFolder, "Service Started.");
            StartWatcher();

            // Process any files that already exist at startup
            ProcessExistingFiles();

        }
        protected override void OnStop()
        {
            _stopping = true;
            StopWatcher();
            Logger.Log(_logFolder, "Service Stopped.");
        }
        #endregion


        #region Console support
        public void StartForConsole()
        {
            Initialize();
            Logger.Log(_logFolder, "Service Started (Console).", true);
            StartWatcher();
            ProcessExistingFiles();
        }

        public void StopForConsole()
        {
            _stopping = true;
            StopWatcher();
            Logger.Log(_logFolder, "Service Stopped (Console).", true);
        }
        #endregion

        private void Initialize()
        {
            _sourceFolder = ConfigurationManager.AppSettings["SourceFolder"];
            _destinationFolder = ConfigurationManager.AppSettings["DestinationFolder"];
            _logFolder = ConfigurationManager.AppSettings["LogFolder"];

            Directory.CreateDirectory(_sourceFolder);
            Directory.CreateDirectory(_destinationFolder);
            Directory.CreateDirectory(_logFolder);
        }

        private void StartWatcher()
        {
            _watcher = new FileSystemWatcher(_sourceFolder, "*.*");
            _watcher.Created += OnCreated;
            _watcher.Renamed += OnRenamed;
            _watcher.EnableRaisingEvents = true;
        }

        private void StopWatcher()
        {
            if (_watcher != null)
            {
                _watcher.EnableRaisingEvents = false;
                _watcher.Created -= OnCreated;
                _watcher.Renamed -= OnRenamed;
                _watcher.Dispose();
                _watcher = null;
            }
        }

        private void OnCreated(object sender, FileSystemEventArgs e)
        {
            ThreadPool.QueueUserWorkItem(_ => ProcessFile(e.FullPath));
        }

        private void OnRenamed(object sender, RenamedEventArgs e)
        {
            ThreadPool.QueueUserWorkItem(_ => ProcessFile(e.FullPath));
        }

        private void ProcessExistingFiles()
        {
            foreach (var file in Directory.GetFiles(_sourceFolder))
            {
                ProcessFile(file);
            }
        }

        private void ProcessFile(string path)
        {
            try
            {
                if (_stopping || !File.Exists(path)) return;

                Logger.Log(_logFolder, $"File detected: {path}");

                string ext = Path.GetExtension(path);
                string newName = Guid.NewGuid().ToString("D") + ext;
                string dest = Path.Combine(_destinationFolder, newName);

                File.Move(path, dest);

                Logger.Log(_logFolder, $"File moved: {path} -> {dest}");
            }
            catch (Exception ex)
            {
                Logger.Log(_logFolder, $"Error processing file: {path} - {ex}");
            }
        }
    }
}
