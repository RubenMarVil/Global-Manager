using Assets.Scripts.Control;
using UnityEngine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class GameConfigurationControl
{
    public static GameConfiguration actualGameConfiguration = new GameConfiguration();

    public static bool SaveGameConfiguration()
    {
        bool inserted = true;
        DBGameConfiguration dbGameConfiguration = new DBGameConfiguration();
        int result = dbGameConfiguration.AddGameConfiguration(actualGameConfiguration);

        if (result == -1)
        {
            Debug.Log($"[CONTROL GAME CONFIGURATION - ERROR] New game {actualGameConfiguration.CodGame} did not inserted into the database.");
            inserted = false;
        }
        else if (result == 1)
        {
            Debug.Log($"[CONTROL GAME CONFIGURATION - ERROR] New game '{actualGameConfiguration.CodGame}' inserted into the database.");

            actualGameConfiguration.CodGame = dbGameConfiguration.GetLastID();

            if(actualGameConfiguration.CodGame != 0)
            {
                inserted = AddSites();

                if(inserted)
                {
                    inserted = AddCommunicateTools();
                }
            }
            
        }

        return inserted;
    }

    private static bool AddSites()
    {
        bool inserted = true;
        DBSite dbSite = new DBSite();
        int result = dbSite.AddSitesOfGame(actualGameConfiguration.SitesList);

        if (result == -1)
        {
            Debug.Log($"[CONTROL GAME CONFIGURATION - ERROR] List of sites of the game did not inserted into the database.");
            inserted = false;
        }
        else if (result == 1)
        {
            Debug.Log($"[CONTROL GAME CONFIGURATION - ERROR] List of sites of the game inserted into the database.");

            FillCodSites(dbSite);
            FillCodSitesInCommunicate();
            inserted = AddGameSites(dbSite);
        }

        return inserted;
    }

    private static void FillCodSites(DBSite dbSite)
    {
        for(int i = 0; i < actualGameConfiguration.SitesList.Count; i++)
        {
            actualGameConfiguration.SitesList[i].CodSite = dbSite.GetCodSite(actualGameConfiguration.SitesList[i].Name);
        }
    }

    private static void FillCodSitesInCommunicate()
    {
        foreach(CommunicationConfiguration communicate in actualGameConfiguration.CommunicationsList)
        {
            communicate.CodSite1 = actualGameConfiguration.SitesList[communicate.NumSite1 - 1].CodSite;
            communicate.CodSite2 = actualGameConfiguration.SitesList[communicate.NumSite2 - 1].CodSite;
        }
    }

    private static bool AddGameSites(DBSite dbSite)
    {
        bool inserted = true;
        int result = dbSite.AddGameSites(actualGameConfiguration);

        if (result == -1)
        {
            Debug.Log($"[CONTROL GAME CONFIGURATION - ERROR] Game-Sites did not inserted into the database.");
            inserted = false;
        }
        else if (result == 1)
        {
            Debug.Log($"[CONTROL GAME CONFIGURATION - ERROR] Game-Sites inserted into the database.");
        }

        return inserted;
    }

    private static bool AddCommunicateTools()
    {
        bool inserted = true;
        DBCommunication dbCommunication = new DBCommunication();
        int result = dbCommunication.AddCommunicate(actualGameConfiguration);

        if (result == -1)
        {
            Debug.Log($"[CONTROL GAME CONFIGURATION - ERROR] Game-Sites did not inserted into the database.");
            inserted = false;
        }
        else if (result == 1)
        {
            Debug.Log($"[CONTROL GAME CONFIGURATION - ERROR] Game-Sites inserted into the database.");
        }

        return inserted;
    }

    public static List<string> GetCountries()
    {
        DBCountry dbCountry = new DBCountry();

        return dbCountry.getAllCountries();
    }

    public static List<string> GetLanguages()
    {
        DBLanguage dbLanguage = new DBLanguage();

        return dbLanguage.getAllLanguages();
    }

    public static void SetFirstConfiguration(int numSites, string clientCountry, string commonLanguage)
    {
        actualGameConfiguration.NumSites = numSites;
        actualGameConfiguration.ClientCountry = clientCountry;
        actualGameConfiguration.CommonLanguage = commonLanguage;

        actualGameConfiguration.SitesList = new List<SiteConfiguration>();
        actualGameConfiguration.CommunicationsList = new List<CommunicationConfiguration>();
        actualGameConfiguration.ProjectCharacteristicsList = new List<ProjectCharacteristic>();

        actualGameConfiguration.Player = UserControl.actualUser.Name;
    }

    public static void SetSiteConfiguration(string nameSite, string country, int teamSize, string languageLevel)
    {
        actualGameConfiguration.SitesList.Add(new SiteConfiguration(nameSite, country, teamSize, languageLevel));
    }

    public static void SetMainSite(int mainSite)
    {
        actualGameConfiguration.SitesList[mainSite - 1].MainSite = 1;
    }

    public static void SetCommunicationTools(int numSite1, int numSite2, List<string> communicationsList)
    {
        actualGameConfiguration.CommunicationsList.Add(new CommunicationConfiguration(numSite1, numSite2, communicationsList));
    }

    public static void SetProjectCharacteristic(string name, string value)
    {
        actualGameConfiguration.ProjectCharacteristicsList.Add(new ProjectCharacteristic(name, value));
    }

    public static void SetFinalConfiguration(string projectDifficulty, float initialBudget, float initialDuration)
    {
        actualGameConfiguration.InitialBudget = initialBudget;
        actualGameConfiguration.InitialDuration = initialDuration;

        actualGameConfiguration.setProjectDifficulty(projectDifficulty);
    }
}