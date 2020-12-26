using System;
using System.Collections.Generic;
using System.ServiceProcess;
using DataModel;
using DataAccess;
using DataManager;
using System.IO;

namespace ServiceLayer
{
    public partial class DataService : ServiceBase
    {
        public DataService()
        {
            InitializeComponent();
            this.CanStop = true;
            this.CanPauseAndContinue = true;
        }

        protected override void OnStart(string[] args)
        {
            try
            {
                using (GameDevEntities db = new GameDevEntities())
                {
                    List<GameDevEssence> gameDevEsList = new List<GameDevEssence>();
                    DatabaseOperations dataAccess = new DatabaseOperations();
                    dataAccess.DbImport(out gameDevEsList, db);
                    List<string> data = dataAccess.DbConvert(gameDevEsList);
                    XmlDbSerializer xmlDbSerializer = new XmlDbSerializer();
                    xmlDbSerializer.XmlSerialize<List<string>>($"G:/Work/GameIndustryInfo.xml", data);
                }
            }
            catch (Exception ex)
            {
                using (StreamWriter sw = new StreamWriter("errors.txt"))
                {
                    sw.WriteLine(ex.Message);
                }
            }
        }

        protected override void OnStop()
        {
        }
    }
}
