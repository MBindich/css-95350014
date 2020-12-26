using System;
using System.ServiceProcess;
using System.Threading;
using System.IO;

namespace WinServLab2
{
    public partial class Service1 : ServiceBase
    {
        Logger logger;

        public Service1()
        {
            InitializeComponent();
            this.CanStop = true; 
            this.CanPauseAndContinue = true; 
            this.AutoLog = true; 
        }

        protected override void OnStart(string[] args)
        {
            logger = new Logger();
            Thread loggerThread = new Thread(new ThreadStart(logger.Start));
            loggerThread.Start();
        }

        protected override void OnStop()
        {
            logger.Stop();
            Thread.Sleep(1000);
        }
    }

    class Logger
    {
        FileSystemWatcher watcher;
        object obj = new object();
        bool enabled = true;
        private const string sourceDirectory = @"G:\Work\";
        private const string archiveDirectory = @"G:\WorkArchived\ArchivedFiles";
        private const string cryptDirectory = @"G:\WorkArchived\CryptedFiles";
        private const string targetDirectory = @"G:\WorkArchived\";
        public Logger()
        {
            watcher = new FileSystemWatcher(sourceDirectory);
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

            //Crypt the file
            Directory.CreateDirectory($@"{cryptDirectory}\{file.LastWriteTime.Year} year\{file.LastWriteTime.Month} month\" +
                $@"{file.LastWriteTime.Day} day\");
            FileCoder.EncryptTo(newFile.FullPath, $@"{cryptDirectory}\{file.LastWriteTime.Year} year\{file.LastWriteTime.Month} month\{file.LastWriteTime.Day} day\" +
                $@"{newFile.Name.Substring(0, newFile.Name.Length - 4)}_{file.LastWriteTime.Hour}_{file.LastWriteTime.Minute}_{file.LastWriteTime.Second}");

            //Archive crypted file from cryptDirectory to archiveDirectory
            Directory.CreateDirectory($@"{archiveDirectory}\{file.LastWriteTime.Year} year\{file.LastWriteTime.Month} month\" +
                $@"{file.LastWriteTime.Day} day\");
            FileCoder.Archive($@"{cryptDirectory}\{file.LastWriteTime.Year} year\{file.LastWriteTime.Month} month\{file.LastWriteTime.Day} day\" +
                $@"{newFile.Name.Substring(0, newFile.Name.Length - 4)}_{file.LastWriteTime.Hour}_{file.LastWriteTime.Minute}_{file.LastWriteTime.Second}", 
                $@"{archiveDirectory}\{file.LastWriteTime.Year} year\{file.LastWriteTime.Month} month\{file.LastWriteTime.Day} day\" +
                $@"{newFile.Name.Substring(0, newFile.Name.Length - 4)}_{file.LastWriteTime.Hour}_{file.LastWriteTime.Minute}_{file.LastWriteTime.Second}.zip");
            
            //UnArchive crypted file from archive Directory to cryptDirectory
            FileCoder.UnArchive($@"{archiveDirectory}\{file.LastWriteTime.Year} year\{file.LastWriteTime.Month} month\{file.LastWriteTime.Day} day\" +
                $@"{newFile.Name.Substring(0, newFile.Name.Length - 4)}_{file.LastWriteTime.Hour}_{file.LastWriteTime.Minute}_{file.LastWriteTime.Second}.zip",
                $@"{cryptDirectory}\{file.LastWriteTime.Year} year\{file.LastWriteTime.Month} month\{file.LastWriteTime.Day} day\" +
                $@"{newFile.Name.Substring(0, newFile.Name.Length - 4)}_{file.LastWriteTime.Hour}_{file.LastWriteTime.Minute}_{file.LastWriteTime.Second}");

            //Decrypt the file from cryptDirectory to targetDirectory
            FileCoder.DecryptTo($@"{cryptDirectory}\{file.LastWriteTime.Year} year\{file.LastWriteTime.Month} month\{file.LastWriteTime.Day} day\" +
                $@"{newFile.Name.Substring(0, newFile.Name.Length - 4)}_{file.LastWriteTime.Hour}_{file.LastWriteTime.Minute}_{file.LastWriteTime.Second}",
                $@"{targetDirectory}\{newFile.Name.Substring(0, newFile.Name.Length - 4)}_{file.LastWriteTime.Hour}_{file.LastWriteTime.Minute}_{file.LastWriteTime.Second}.txt");
        }
    }
}
