using System;
using System.ServiceProcess;
using System.Threading;
using System.IO;

namespace WinServLab2
{
    public partial class MyFileService : ServiceBase
    {
        Logger logger;

        public MyFileService()
        {
            InitializeComponent();
            this.CanStop = true; 
            this.CanPauseAndContinue = true; 
            this.AutoLog = true; 
        }

        protected override void OnStart(string[] args)
        {
            string xmlConfigFile = Path.Join(AppDomain.CurrentDomain.BaseDirectory, "config.xml");
            string jsonConfigFile = Path.Join(AppDomain.CurrentDomain.BaseDirectory, "appsettings.json");
            FileManagerOptions options = new FileManagerOptions();

            if (new FileInfo(jsonConfigFile).Exists)
            {
                ConfigurationProvider configProvider = new ConfigurationProvider(new JsonParser());
                options.StorageOptions = configProvider.GetOptions<StorageOptions>(jsonConfigFile);
                options.EncryptOptions = configProvider.GetOptions<EncryptOptions>(jsonConfigFile);
            }
            else if (new FileInfo(xmlConfigFile).Exists)
            {
                ConfigurationProvider configProvider = new ConfigurationProvider(new XmlParser());
                options.StorageOptions = configProvider.GetOptions<StorageOptions>(xmlConfigFile);
                options.EncryptOptions = configProvider.GetOptions<EncryptOptions>(xmlConfigFile);
            }
            logger = new Logger(options);
            Thread loggerThread = new Thread(new ThreadStart(logger.Start));
            loggerThread.Start();
        }

        protected override void OnStop()
        {
            logger.Stop();
            Thread.Sleep(1000);
        }
    }
}
