using Assets.Scripts.Control;
using System.Collections;
using System.Collections.Generic;
using System;
using System.Linq;
using UnityEngine;

public class RecommendConfiguration
{
    public static GameConfiguration GetRecommendation()
    {
        User player = UserControl.actualUser;

        float basicLevel = CalculateMembership(RecommendConfigurationVariables.fuzz_basic, player.Score);
        float intermediateLevel = CalculateMembership(RecommendConfigurationVariables.fuzz_inter, player.Score);
        float advancedLevel = CalculateMembership(RecommendConfigurationVariables.fuzz_advan, player.Score);

        Debug.Log("[RECOMMEND CONFIGURATION - INFO]\n\t- BASIC = " + basicLevel + 
            "\n\t- INTERMEDIATE = " + intermediateLevel + "\n\t- ADVANCED = " + advancedLevel);

        Dictionary<object, object> problemAdapted = RecommendConfigurationVariables.AdaptProblem(basicLevel, intermediateLevel, advancedLevel);

        Debug.Log($"[RECOMMEND CONFIGURATION - INFO]\n\t- Sites Number --> {problemAdapted["SitesNumber"]}" +
            $"\n\t- Countries --> {problemAdapted["Countries"]} \n\t- Client Main Site --> {problemAdapted["ClientMainSite"]}" +
            $"\n\t- Languages --> {problemAdapted["Languages"]} \n\t- Common Language --> {problemAdapted["CommonLanguage"]}" +
            $"\n\t- Max Time Difference --> {problemAdapted["MaxTimeDifference"]} \n\t- Instability Countries --> {problemAdapted["InstabilityCountries"]}" +
            $"\n\t- Team Size --> {problemAdapted["TeamSize"]} \n\t- Communication Synchronous --> {problemAdapted["CommunicationSynchronous"]}");

        return GenerateGameConfiguration(problemAdapted);
    }

    public static int GetRecommendQuestions()
    {
        User player = UserControl.actualUser;

        float basicLevel = CalculateMembership(RecommendConfigurationVariables.fuzz_basic, player.Score);
        float intermediateLevel = CalculateMembership(RecommendConfigurationVariables.fuzz_inter, player.Score);
        float advancedLevel = CalculateMembership(RecommendConfigurationVariables.fuzz_advan, player.Score);

        return RecommendConfigurationVariables.GetNumQuestionsAdapted(basicLevel, intermediateLevel, advancedLevel);
    }

    public static int GetTimeEV_EVRecommend()
    {
        User player = UserControl.actualUser;

        float basicLevel = CalculateMembership(RecommendConfigurationVariables.fuzz_basic, player.Score);
        float intermediateLevel = CalculateMembership(RecommendConfigurationVariables.fuzz_inter, player.Score);
        float advancedLevel = CalculateMembership(RecommendConfigurationVariables.fuzz_advan, player.Score);

        return RecommendConfigurationVariables.GetTimeEV_EVAdapted(basicLevel, intermediateLevel, advancedLevel);
    }

    public static int GetStreakFailureForPositiveRecommend()
    {
        User player = UserControl.actualUser;

        float basicLevel = CalculateMembership(RecommendConfigurationVariables.fuzz_basic, player.Score);
        float intermediateLevel = CalculateMembership(RecommendConfigurationVariables.fuzz_inter, player.Score);
        float advancedLevel = CalculateMembership(RecommendConfigurationVariables.fuzz_advan, player.Score);

        return RecommendConfigurationVariables.GetStreakFailureForPositiveAdapted(basicLevel, intermediateLevel, advancedLevel);
    }

    public static float GetProdWorkerDayCorrect()
    {
        User player = UserControl.actualUser;

        float basicLevel = CalculateMembership(RecommendConfigurationVariables.fuzz_basic, player.Score);
        float intermediateLevel = CalculateMembership(RecommendConfigurationVariables.fuzz_inter, player.Score);
        float advancedLevel = CalculateMembership(RecommendConfigurationVariables.fuzz_advan, player.Score);

        return RecommendConfigurationVariables.GetProdWorkerDayCorrect(basicLevel, intermediateLevel, advancedLevel);
    }

    public static float GetProdWorkerDayFailure()
    {
        User player = UserControl.actualUser;

        float basicLevel = CalculateMembership(RecommendConfigurationVariables.fuzz_basic, player.Score);
        float intermediateLevel = CalculateMembership(RecommendConfigurationVariables.fuzz_inter, player.Score);
        float advancedLevel = CalculateMembership(RecommendConfigurationVariables.fuzz_advan, player.Score);

        return RecommendConfigurationVariables.GetProdWorkerDayFailure(basicLevel, intermediateLevel, advancedLevel);
    }

    private static float CalculateMembership(int[] group, int x)
    {
        float result = 0;

        if((x < group[0]) || (x > group[3]))
        {
            result = 0;
        }
        else if((group[0] <= x) && (x <= group[1]))
        {
            result = (x - group[0]) / (float)(group[1] - group[0]);
        }
        else if((group[1] <= x) && (x <= group[2]))
        {
            result = 1;
        }
        else if((group[2] <= x) && (x <= group[3]))
        {
            result = (group[3] - x) / (float)(group[3] - group[2]);
        }

        if(Single.IsNaN(result))
        {
            result = 1;
        }

        return result;
    }

    private static GameConfiguration GenerateGameConfiguration(Dictionary<object, object> problemAdapted)
    {
        GameConfiguration gameConfiguration = new GameConfiguration();

        DBCountry dbCountry = new DBCountry();
        DBLanguage dBLanguage = new DBLanguage();
        List<Country> countriesList = dbCountry.getAllDataAllCountries();

        System.Random rnd = new System.Random();

        gameConfiguration.NumSites = (int)problemAdapted["SitesNumber"];

        int[] countriesIndexSelected = GetRandomCountries((int)problemAdapted["Countries"], countriesList, (int)problemAdapted["MaxTimeDifference"], rnd.Next(0, 20));
        foreach(int countryIndex in countriesIndexSelected)
        {
            countriesList[countryIndex].SetLanguages(dBLanguage.getLanguagesOfCountry(countriesList[countryIndex].Name));
        }

        int[] teamSizeDivision = GetTeamSizes(gameConfiguration.NumSites, (int)problemAdapted["TeamSize"]);
        /*
        switch((string)problemAdapted["CommonLanguage"])
        {
            case "English":
                gameConfiguration.CommonLanguage = "English";
                break;
            case "Majority":
                gameConfiguration.CommonLanguage = GetCommonLanguageMajority(countriesList, countriesIndexSelected);
                break;
            case "Random":
                gameConfiguration.CommonLanguage = GetCommonLanguageRandom(countriesList, countriesIndexSelected);
                break;
        }*/

        gameConfiguration.CommonLanguage = "English";

        gameConfiguration.SitesList = new List<SiteConfiguration>();
        int contCountry = 0;

        for(int i = 0; i < gameConfiguration.NumSites; i++)
        {
            gameConfiguration.SitesList.Add(new SiteConfiguration($"Site {i+1}", countriesList[countriesIndexSelected[contCountry]].Name, teamSizeDivision[i], CommonLanguageLevels.HIGH));

            contCountry++;
            if(contCountry >= countriesIndexSelected.Length)
            {
                contCountry = rnd.Next(0, countriesIndexSelected.Length - 1);
            }
        }

        gameConfiguration.ClientCountry = GetClientCountry(countriesList, countriesIndexSelected, gameConfiguration.CommonLanguage, (int)problemAdapted["ClientMainSite"]);

        if ((int)problemAdapted["ClientMainSite"] == 1)
        {
            foreach(SiteConfiguration site in gameConfiguration.SitesList)
            {
                if(site.Country == gameConfiguration.ClientCountry)
                {
                    site.MainSite = 1;
                    break;
                }
            }
        }
        else
        {
            gameConfiguration.SitesList[rnd.Next(0, gameConfiguration.NumSites - 1)].MainSite = 1;
        }

        gameConfiguration.CommunicationTools = new CommunicationConfiguration(GetCommunicationTools((int)problemAdapted["CommunicationSynchronous"]));

        return gameConfiguration;
    }

    private static List<string> GetCommunicationTools(int synchronousTools)
    {
        List<string> tools = new List<string>();
        List<string> ToolsAsync = GameConfigurationControl.GetCommunicationTools("ASYNCHRONOUS");
        List<string> ToolsSync = GameConfigurationControl.GetCommunicationTools("SYNCHRONOUS");

        for (int i = 0; i < synchronousTools; i++)
        {
            System.Random rnd = new System.Random();

            int toolNum = rnd.Next(0, 6);
            while(tools.Contains(ToolsSync[toolNum]))
            {
                toolNum = rnd.Next(0, 6);
            }
            tools.Add(ToolsSync[toolNum]);
        }

        for (int i = 0; i < (3 - synchronousTools); i++)
        {
            System.Random rnd = new System.Random();

            int toolNum = rnd.Next(0, 6);
            while (tools.Contains(ToolsAsync[toolNum]))
            {
                toolNum = rnd.Next(0, 6);
            }
            tools.Add(ToolsAsync[toolNum]);
        }

        return tools;
    }

    private static int[] GetRandomCountries(int numCountries, List<Country> countriesList, int diffTimeZone, int mainCountry)
    {
        int maxTimeZone = Convert.ToInt32(countriesList[mainCountry].TimeZone) + diffTimeZone / 2;
        int minTimeZone = Convert.ToInt32(countriesList[mainCountry].TimeZone) - diffTimeZone / 2;

        int[] listIndexCountries = new int[numCountries];
        listIndexCountries[0] = mainCountry;

        System.Random rnd = new System.Random();

        for(int i = 1; i < numCountries; i++)
        {
            bool found = false;
            while(!found)
            {
                int newIndexCountry = rnd.Next(0, 20);
                if(listIndexCountries.Contains(newIndexCountry))
                {
                    continue;
                }
                else
                {
                    if(countriesList[newIndexCountry].TimeZone >= minTimeZone && countriesList[newIndexCountry].TimeZone <= maxTimeZone)
                    {
                        listIndexCountries[i] = newIndexCountry;
                        found = true;
                    }
                    else
                    {
                        continue;
                    }
                }
            }
        }

        return listIndexCountries;
    }

    private static int[] GetTeamSizes(int numSites, int totalMembers)
    {
        int[] teamSizeDivision = new int[numSites];
        int restWorkers = totalMembers;
        int maxWorkers = 20;

        System.Random rnd = new System.Random();

        for(int i = 0; i < numSites - 1; i++)
        {
            maxWorkers = restWorkers - (2 * (numSites - i));
            if(maxWorkers > 20)
            {
                maxWorkers = 20;
            }

            int teamSize = rnd.Next(2, maxWorkers);
            teamSizeDivision[i] = teamSize;
            restWorkers -= teamSize;
        }

        teamSizeDivision[teamSizeDivision.Length - 1] = restWorkers;

        return teamSizeDivision;
    }

    private static string GetCommonLanguageMajority(List<Country> countriesList, int[] countriesIndexSelected)
    {
        string projectLanguage = "";
        Dictionary<string, int> languages = new Dictionary<string, int>();

        foreach(int countryIndex in countriesIndexSelected)
        {
            foreach(Language language in countriesList[countryIndex].LanguagesSpeak)
            {
                if(languages.ContainsKey(language.Name))
                {
                    if(language.Official)
                    {
                        languages[language.Name] += 2;
                    }
                    else
                    {
                        languages[language.Name] += 1;
                    }
                    
                }
                else
                {
                    languages.Add(language.Name, 0);
                }
            }
        }

        try
        {
            projectLanguage = languages.Keys.ElementAt(languages.Values.Max());
        } catch(ArgumentOutOfRangeException)
        {
            projectLanguage = languages.Keys.ElementAt(0);
        }

        return projectLanguage;
    }

    private static string GetCommonLanguageRandom(List<Country> countriesList, int[] countriesIndexSelected)
    {
        List<string> languages = new List<string>();

        foreach (int countryIndex in countriesIndexSelected)
        {
            foreach (Language language in countriesList[countryIndex].LanguagesSpeak)
            {
                if (languages.Contains(language.Name))
                {
                    continue;
                }
                else
                {
                    languages.Add(language.Name);
                }
            }
        }

        System.Random rnd = new System.Random();

        return languages[rnd.Next(0, languages.Count - 1)];
    }

    private static string GetClientCountry(List<Country> countriesList, int[] countriesIndexSelected, string commonLanguage, int clientProblemAdapted)
    {
        if(clientProblemAdapted == 1)
        {
            return countriesList[countriesIndexSelected[0]].Name;
        }

        foreach(int index in countriesIndexSelected)
        {
            if(countriesList[index].ContainOfficialLanguage(commonLanguage))
            {
                return countriesList[index].Name;
            }
        }

        return "United Kingdom";
    }
}
