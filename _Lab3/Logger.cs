using System;
using System.ServiceProcess;
using System.Threading;
using System.IO;

namespace WinServLab2
{
    class Logger
    {
        FileSystemWatcher watcher;
        object obj = new object();
        bool enabled = true;
        FileManagerOptions options;
        public Logger(FileManagerOptions options)
        {
            this.options = options;
            watcher = new FileSystemWatcher(options.StorageOptions.SourceDirectory);
            watcher.Created += OnCreated;
        }

        public void Start()
        {
            watcher.EnableRaisingEvents = true;
            while (enabled)
            {
                Thread.Sleep(1000);
            }
        }

        public void Stop()
        {
            watcher.EnableRaisingEvents = false;
            enabled = false;
        }

        private void OnCreated(object source, FileSystemEventArgs newFile)
        {
            FileInfo file = new FileInfo(newFile.FullPath);
            FileManager fileManager = new FileManager(options, file);
            fileManager.Encrypt();
            fileManager.Archive();
        }
    }
}
