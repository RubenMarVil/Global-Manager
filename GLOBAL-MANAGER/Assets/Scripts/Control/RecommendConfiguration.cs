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

        float basicLevel = CalculateMembership(RecommendConfigurationVariables.fuzz_basic, 14);
        float intermediateLevel = CalculateMembership(RecommendConfigurationVariables.fuzz_inter, 14);
        float advancedLevel = CalculateMembership(RecommendConfigurationVariables.fuzz_advan, 14);

        Debug.Log("[RECOMMEND CONFIGURATION - INFO]\n\t- BASIC = " + basicLevel + 
            "\n\t- INTERMEDIATE = " + intermediateLevel + "\n\t- ADVANCED = " + advancedLevel);

        Dictionary<object, object> problemAdapted = RecommendConfigurationVariables.AdaptProblem(basicLevel, intermediateLevel, advancedLevel);

        return GenerateGameConfiguration(problemAdapted); ;
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
        }

        gameConfiguration.SitesList = new List<SiteConfiguration>();
        int contCountry = 0;

        for(int i = 0; i < gameConfiguration.NumSites; i++)
        {
            gameConfiguration.SitesList.Add(new SiteConfiguration(countriesList[countriesIndexSelected[contCountry]].Name, teamSizeDivision[i], CommonLanguageLevels.HIGH));

            contCountry++;
            if(contCountry >= countriesIndexSelected.Length)
            {
                contCountry = rnd.Next(0, countriesIndexSelected.Length - 1);
            }
        }

        gameConfiguration.ClientCountry = GetClientCountry(countriesList, countriesIndexSelected, gameConfiguration.CommonLanguage);

        if((int)problemAdapted["ClientMainSite"] == 1)
        {
            foreach(SiteConfiguration site in gameConfiguration.SitesList)
            {
                if(site.Country == gameConfiguration.ClientCountry)
                {
                    site.MainSite = 1;
                }
            }
        }
        else
        {
            gameConfiguration.SitesList[rnd.Next(0, gameConfiguration.NumSites - 1)].MainSite = 1;
        }

        return gameConfiguration;
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

        System.Random rnd = new System.Random();

        for(int i = 0; i < numSites - 1; i++)
        {
            int teamSize = rnd.Next(2, 20);
            teamSizeDivision[i] = teamSize;
            restWorkers -= teamSize;
        }

        teamSizeDivision[teamSizeDivision.Length - 1] = restWorkers;

        return teamSizeDivision;
    }

    private static string GetCommonLanguageMajority(List<Country> countriesList, int[] countriesIndexSelected)
    {
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

        return languages.Keys.ElementAt(languages.Values.Max());
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

    private static string GetClientCountry(List<Country> countriesList, int[] countriesIndexSelected, string commonLanguage)
    {
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
