using Assets.Scripts.Control;
using Mono.Data.Sqlite;
using UnityEngine;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class DBSite
{
    public int AddSitesOfGame(List<SiteConfiguration> sitesList)
    {
        int result = -1;

        try
        {
            string sqlQuery;

            foreach(SiteConfiguration newSite in sitesList)
            {
                sqlQuery = "INSERT INTO SITE(Country, Name, TeamSize, LevelCommonLanguage) VALUES('" + newSite.Country + "', '" + newSite.Name +
                           "', " + newSite.TeamSize + ", '" + newSite.LevelCommonLanguage + "');";

                result = DBSingleton.GetInstance().Insert(sqlQuery);
            }
        }
        catch (SqliteException e)
        {
            Debug.Log($"[DATABASE - ERROR] SQLiteException to get users with the code #{e}.");
        }

        return result;
    }

    public int AddGameSites(GameConfiguration game)
    {
        int result = -1;

        try
        {
            string sqlQuery;

            foreach(SiteConfiguration site in game.SitesList)
            {
                sqlQuery = "INSERT INTO SiteGame(Game, Site, MainSite) VALUES(" + game.CodGame + ", " + site.CodSite + ", " + site.MainSite + ");";

                result = DBSingleton.GetInstance().Insert(sqlQuery);
            }
        }
        catch (SqliteException e)
        {
            Debug.Log($"[DATABASE - ERROR] SQLiteException to get users with the code #{e}.");
        }

        return result;
    }

    public int GetCodSite(string NameSite)
    {
        int codSite = 0;
        try
        {
            string sqlQuery = "SELECT max(CodSite) FROM SITE WHERE Name='" + NameSite + "';";

            IDataReader data = DBSingleton.GetInstance().Read(sqlQuery);
            while (data.Read())
            {
                codSite = Int32.Parse(data["max(CodSite)"].ToString());
            }
        }
        catch (SqliteException e)
        {
            Debug.Log($"[DATABASE - ERROR] SQLiteException to insert user with the code #{e}.;");
        }

        return codSite;
    }

    public int DeleteSitesGame(int codGame)
    {
        int result = -1;

        try
        {
            string sqlQuery = "DELETE FROM SiteGame WHERE Game=" + codGame + ";";

            result = DBSingleton.GetInstance().Insert(sqlQuery);
        }
        catch (SqliteException e)
        {
            Debug.Log($"[DATABASE - ERROR] SQLiteException to get users with the code #{e}.");
        }

        return result;
    }
}