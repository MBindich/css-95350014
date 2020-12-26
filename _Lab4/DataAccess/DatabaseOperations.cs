using System;
using System.Collections.Generic;
using DataModel;

namespace DataAccess
{
    public class DatabaseOperations
    {
        public void DbImport(out List<GameDevEssence> gameDevEsList, GameDevEntities db)
        {
            gameDevEsList = new List<GameDevEssence>();
            int k = 0;
            foreach (var obj in db.GameIndustry)
            {
                GameDevEssence gameDevEssence = new GameDevEssence();
                gameDevEssence.GameIndustry = obj;
                gameDevEsList.Add(gameDevEssence);
                k++;
            }

            for (int i = 0; i < k; i++)
            {
                foreach (var obj in db.Games)
                {
                    if (obj.Id == gameDevEsList[i].GameIndustry.GameId)
                    {
                        gameDevEsList[i].Games = obj;
                        break;
                    }
                }

                foreach (var obj in db.Companies)
                {
                    if (obj.Id == gameDevEsList[i].GameIndustry.CompanyId)
                    {
                        gameDevEsList[i].Companies = obj;
                        break;
                    }
                }

                foreach (var obj in db.Publishers)
                {
                    if (obj.Id == gameDevEsList[i].GameIndustry.PublisherId)
                    {
                        gameDevEsList[i].Publishers = obj;
                        break;
                    }
                }

                foreach (var obj in db.Developers)
                {
                    if (obj.Id == gameDevEsList[i].GameIndustry.DeveloperId)
                    {
                        gameDevEsList[i].Developers = obj;
                        break;
                    }
                }
            }
        }

        public List<string> DbConvert(List<GameDevEssence> gameDevEsList)
        {
            List<string> dataList = new List<string>();
            foreach (var obj in gameDevEsList)
            {
                dataList.Add(obj.Games.Game);
                dataList.Add(obj.Games.Genre);
                dataList.Add(obj.Games.ReleaseDate);
                dataList.Add(obj.Companies.CompanyName);
                dataList.Add(obj.Companies.Country);
                dataList.Add(obj.Companies.EmployeesNumber.ToString());
                dataList.Add(obj.Companies.FoundationYear.ToString());
                dataList.Add(obj.Developers.DeveloperName);
                dataList.Add(obj.Developers.YearsInCompany.ToString());
                dataList.Add(obj.Publishers.Publisher);
                dataList.Add(obj.Publishers.MoneyEarned.ToString());
            }
            return dataList;
        }
        public DatabaseOperations()
        {

        }
    }
}
