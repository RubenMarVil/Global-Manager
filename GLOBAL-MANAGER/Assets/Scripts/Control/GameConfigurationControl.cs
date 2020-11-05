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

    public static Country GetAllDataOfCountry(string countryName)
    {
        DBCountry dbCountry = new DBCountry();

        return dbCountry.getAllDataOfCountry(countryName);
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

    public static void SetCommunicationTools(List<ToggleButton> communicationsList)
    {
        actualGameConfiguration.CommunicationTools = new CommunicationConfiguration(communicationsList);
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

    /*
    public static void CalculateMaxMinCulturalDifference()
    {
        DBCountry dBCountry = new DBCountry();

        List<Country> countries = dBCountry.getAllDataAllCountries();

        int maxDifference = -999999999;
        string countryMaxDifference1 = "";
        string countryMaxDifference2 = "";

        int minDifference = 999999999;
        string countryMinDifference1 = "";
        string countryMinDifference2 = "";

        for(int i = 0; i < countries.Count; i++)
        {
            for(int j = i + 1; j < countries.Count; j++)
            {
                int culturalDifference = Math.Abs(countries[i].PowerDistance - countries[j].PowerDistance) + Math.Abs(countries[i].Individualism - countries[j].Individualism) +
                    Math.Abs(countries[i].Masculinity - countries[j].Masculinity) + Math.Abs(countries[i].UncertantyAvoidance - countries[j].UncertantyAvoidance) +
                    Math.Abs(countries[i].LongTermOrientation - countries[j].LongTermOrientation) + Math.Abs(countries[i].Indulgence - countries[j].Indulgence);

                if(culturalDifference < minDifference)
                {
                    minDifference = culturalDifference;
                    countryMinDifference1 = countries[i].Name;
                    countryMinDifference2 = countries[j].Name;
                }
                else if(culturalDifference > maxDifference)
                {
                    maxDifference = culturalDifference;
                    countryMaxDifference1 = countries[i].Name;
                    countryMaxDifference2 = countries[j].Name;
                }
            }
        }

        Debug.Log(countryMinDifference1 + " - " + countryMinDifference2 + " = " + minDifference);
        Debug.Log(countryMaxDifference1 + " - " + countryMaxDifference2 + " = " + maxDifference);
    }

    public static void CalculateMaxMinDistance()
    {
        DBCountry dBCountry = new DBCountry();

        List<Country> countries = dBCountry.getAllDataAllCountries();

        double maxDifference = -999999999;
        string countryMaxDifference1 = "";
        string countryMaxDifference2 = "";

        for (int i = 0; i < countries.Count; i++)
        {
            for (int j = i + 1; j < countries.Count; j++)
            {
                double earthRadiusKm = 6371;

                double lat1 = countries[i].Latitude;
                double long1 = countries[i].Longitude;
                double lat2 = countries[j].Latitude;
                double long2 = countries[j].Longitude;

                double dLat = ConvertToRadians(lat2 - lat1);
                double dLong = ConvertToRadians(long2 - long1);

                lat1 = ConvertToRadians(lat1);
                lat2 = ConvertToRadians(lat2);

                double a = Math.Sin(dLat / 2) * Math.Sin(dLat / 2) +
                  Math.Sin(dLong / 2) * Math.Sin(dLong / 2) * Math.Cos(lat1) * Math.Cos(lat2);

                double c = 2 * Math.Atan2(Math.Sqrt(a), Math.Sqrt(1 - a));

                double distance = earthRadiusKm * c;

                if (distance > maxDifference)
                {
                    maxDifference = distance;
                    countryMaxDifference1 = countries[i].Name;
                    countryMaxDifference2 = countries[j].Name;
                }
            }
        }

        Debug.Log(countryMaxDifference1 + " - " + countryMaxDifference2 + " = " + maxDifference);
    }
    */

    public static ProjectCharacteristicLevels[] CalculateProjectCharacteristics(int numSites, string[] sitesCountry, string[] sitesLanguageLevel, string commonLanguage,
        string clientCountryName, int mainSite)
    {
        ProjectCharacteristicLevels[] projectCharacteristics = new ProjectCharacteristicLevels[7];

        DBCountry dbCountry = new DBCountry();
        DBLanguage dBLanguage = new DBLanguage();

        List<Country> countryList = new List<Country>();
        Country clientCountry = null;

        bool clientCountrySelected = true;
        bool commonLanguageSelected = true;
        bool sitesCountrySelected = true;
        bool sitesLanguagesSelected = true;
        bool mainSiteSelected = true;
        
        if(String.IsNullOrEmpty(clientCountryName)) { 
            clientCountrySelected = false;
        }
        else
        {
            clientCountry = dbCountry.getAllDataOfCountry(clientCountryName);
        }
        if(String.IsNullOrEmpty(commonLanguage)) { commonLanguageSelected = false; }
        if(mainSite == 0) { mainSiteSelected = false; }

        foreach(string countryName in sitesCountry)
        {
            if(String.IsNullOrEmpty(countryName)) { sitesCountrySelected = false; break; }
            countryList.Add(dbCountry.getAllDataOfCountry(countryName));
        }

        if (sitesCountrySelected)
        {
            foreach (Country country in countryList)
            {
                country.LanguagesSpeak = dBLanguage.getLanguagesOfCountry(country.Name);
            }
        }

        foreach(string languageLevel in sitesLanguageLevel)
        {
            if(String.IsNullOrEmpty(languageLevel)) { sitesLanguagesSelected = false; break; }
        }

        if (sitesCountrySelected)
        {
            projectCharacteristics[0] = CalculateWorkingTimeOverlap(countryList, numSites);
            Debug.Log($"[CONTROL GAME CONFIGURATION - INFO] Working Time Overlap: {projectCharacteristics[0]}");
            projectCharacteristics[2] = CalculateCulturalDifference(countryList, numSites);
            Debug.Log($"[CONTROL GAME CONFIGURATION - INFO] Cultural Difference: {projectCharacteristics[2]}");
            projectCharacteristics[3] = CalculateInestability(countryList, numSites);
            Debug.Log($"[CONTROL GAME CONFIGURATION - INFO] Inestability of any Site: {projectCharacteristics[3]}");
        }
        if (sitesCountrySelected && sitesLanguagesSelected && commonLanguageSelected)
        {
            projectCharacteristics[1] = CalculateLanguageDifference(countryList, numSites, sitesLanguageLevel, commonLanguage);
            Debug.Log($"[CONTROL GAME CONFIGURATION - INFO] Language Difference: {projectCharacteristics[1]}");
        }
        if (sitesCountrySelected && mainSiteSelected && clientCountrySelected)
        {
            projectCharacteristics[4] = CalculateCostumerProximity(countryList, sitesCountry, mainSite, clientCountry);
            Debug.Log($"[CONTROL GAME CONFIGURATION - INFO] Costumer Proximity: {projectCharacteristics[4]}");
        }
        projectCharacteristics[5] = CalculateCommunication();
        Debug.Log($"[CONTROL GAME CONFIGURATION - INFO] Communication: {projectCharacteristics[5]}");
        projectCharacteristics[6] = CalculateSitesNumber(numSites);
        Debug.Log($"[CONTROL GAME CONFIGURATION - INFO] Sites Number: {projectCharacteristics[6]}");

        return projectCharacteristics;
    }

    private static ProjectCharacteristicLevels CalculateWorkingTimeOverlap(List<Country> countryList, int numSites)
    {
        float overlapingHours = 0f;
        int count = 0;

        for(int i = 0; i < numSites; i++)
        {
            for(int j = i + 1; j < numSites; j++)
            {
                float overlaping = (Math.Min(countryList[i].TimeZone, countryList[j].TimeZone) + 7.0f) - Math.Max(countryList[i].TimeZone, countryList[j].TimeZone) + 1;

                overlapingHours += (overlaping > 0) ? overlaping : 0;
                count++;
            }
        }

        if(0 <= overlapingHours && overlapingHours < count * 0.5f)
        {
            return ProjectCharacteristicLevels.VERY_LOW;
        }
        else if(count * 0.5f <= overlapingHours && overlapingHours < count * 2f)
        {
            return ProjectCharacteristicLevels.LOW;
        }
        else if(count * 2f <= overlapingHours && overlapingHours < numSites * 4f)
        {
            return ProjectCharacteristicLevels.NORMAL;
        }
        else if(count * 4f <= overlapingHours && overlapingHours < count * 7f)
        {
            return ProjectCharacteristicLevels.HIGH;
        }
        else if(count * 7f <= overlapingHours && overlapingHours <= count * 8f)
        {
            return ProjectCharacteristicLevels.VERY_HIGH;
        }

        return ProjectCharacteristicLevels.NORMAL;
    }

    private static ProjectCharacteristicLevels CalculateLanguageDifference(List<Country> countryList, int numSites, string[] sitesLanguageLevel, string commonLanguage)
    {
        int languageLevel = 0;
        int maxLanguageLevel = numSites * 2 + numSites;

        for(int i = 0; i < numSites; i++)
        {
            if(countryList[i].ContainOfficialLanguage(commonLanguage))
            {
                languageLevel += 2;
            }
            else if(countryList[i].ContainLanguage(commonLanguage))
            {
                languageLevel += 1;
            }

            switch(sitesLanguageLevel[i])
            {
                case "HighLevel":
                    languageLevel += 1; break;
                case "LowLevel":
                    languageLevel += -1; break;
            }
        }

        if ((languageLevel < 0) || (0 <= languageLevel && languageLevel < numSites * 0.5))
        {
            return ProjectCharacteristicLevels.VERY_HIGH;
        }
        else if (numSites * 0.5 <= languageLevel && languageLevel < numSites * 1)
        {
            return ProjectCharacteristicLevels.HIGH;
        }
        else if (numSites * 1 <= languageLevel && languageLevel < numSites * 2)
        {
            return ProjectCharacteristicLevels.NORMAL;
        }
        else if (numSites * 2 <= languageLevel && languageLevel < numSites * 2.5)
        {
            return ProjectCharacteristicLevels.LOW;
        }
        else if (numSites * 2.5 <= languageLevel && languageLevel <= maxLanguageLevel)
        {
            return ProjectCharacteristicLevels.VERY_LOW;
        }

        return ProjectCharacteristicLevels.NORMAL;
    }

    private static ProjectCharacteristicLevels CalculateCulturalDifference(List<Country> countryList, int numSites)
    {
        int culturalLevel = 0;

        int count = 0;

        for (int i = 0; i < numSites; i++)
        {
            for (int j = i + 1; j < numSites; j++)
            {
                culturalLevel += Math.Abs(countryList[i].PowerDistance - countryList[j].PowerDistance) + Math.Abs(countryList[i].Individualism - countryList[j].Individualism) +
                    Math.Abs(countryList[i].Masculinity - countryList[j].Masculinity) + Math.Abs(countryList[i].UncertantyAvoidance - countryList[j].UncertantyAvoidance) +
                    Math.Abs(countryList[i].LongTermOrientation - countryList[j].LongTermOrientation) + Math.Abs(countryList[i].Indulgence - countryList[j].Indulgence);

                count++;
            }
        }

        int maxCulturalDifference = count * 298;

        if (0 <= culturalLevel && culturalLevel < count * 50)
        {
            return ProjectCharacteristicLevels.VERY_LOW;
        }
        else if (count * 50 <= culturalLevel && culturalLevel < count * 90)
        {
            return ProjectCharacteristicLevels.LOW;
        }
        else if (count * 90 <= culturalLevel && culturalLevel < count * 170)
        {
            return ProjectCharacteristicLevels.NORMAL;
        }
        else if (count * 170 <= culturalLevel && culturalLevel < count * 240)
        {
            return ProjectCharacteristicLevels.HIGH;
        }
        else if (count * 240 <= culturalLevel && culturalLevel <= maxCulturalDifference)
        {
            return ProjectCharacteristicLevels.VERY_HIGH;
        }

        return ProjectCharacteristicLevels.NORMAL;
    }

    private static ProjectCharacteristicLevels CalculateInestability(List<Country> countryList, int numSites)
    {
        float numInstabilityCountries = 0f;

        foreach(Country country in countryList)
        {
            numInstabilityCountries += country.Instability ? 1f : 0f;
        }

        if (0 <= numInstabilityCountries && numInstabilityCountries < 0.5f)
        {
            return ProjectCharacteristicLevels.VERY_LOW;
        }
        else if (0.5f <= numInstabilityCountries && numInstabilityCountries < 1f)
        {
            return ProjectCharacteristicLevels.LOW;
        }
        else if (1f <= numInstabilityCountries && numInstabilityCountries < numSites / 3f)
        {
            return ProjectCharacteristicLevels.NORMAL;
        }
        else if (numSites / 3f <= numInstabilityCountries && numInstabilityCountries < 2 * numSites / 3f)
        {
            return ProjectCharacteristicLevels.HIGH;
        }
        else if (2 * numSites / 3f <= numInstabilityCountries && numInstabilityCountries <= numSites)
        {
            return ProjectCharacteristicLevels.VERY_HIGH;
        }

        return ProjectCharacteristicLevels.NORMAL;
    }

    private static ProjectCharacteristicLevels CalculateCostumerProximity(List<Country> countryList, string[] sitesCountry, int mainSite, Country clientCountry)
    {
        if (mainSite != 0)
        {
            Country mainSiteCountry = null;

            foreach (Country country in countryList)
            {
                if (country.Name == sitesCountry[mainSite - 1])
                {
                    mainSiteCountry = country;
                }
            }

            double earthRadiusKm = 6371;

            double lat1 = mainSiteCountry.Latitude;
            double long1 = mainSiteCountry.Longitude;
            double lat2 = clientCountry.Latitude;
            double long2 = clientCountry.Longitude;

            double dLat = ConvertToRadians(lat2 - lat1);
            double dLong = ConvertToRadians(long2 - long1);

            lat1 = ConvertToRadians(lat1);
            lat2 = ConvertToRadians(lat2);

            double a = Math.Sin(dLat / 2) * Math.Sin(dLat / 2) +
              Math.Sin(dLong / 2) * Math.Sin(dLong / 2) * Math.Cos(lat1) * Math.Cos(lat2);

            double c = 2 * Math.Atan2(Math.Sqrt(a), Math.Sqrt(1 - a));

            double distance = earthRadiusKm * c;
            double maxDistance = 19880;

            if (0 <= distance && distance < 1500)
            {
                return ProjectCharacteristicLevels.VERY_LOW;
            }
            else if (1500 <= distance && distance < 5000)
            {
                return ProjectCharacteristicLevels.LOW;
            }
            else if (5000 <= distance && distance < 8000)
            {
                return ProjectCharacteristicLevels.NORMAL;
            }
            else if (8000 <= distance && distance < 12000)
            {
                return ProjectCharacteristicLevels.HIGH;
            }
            else if (12000 <= distance && distance <= maxDistance)
            {
                return ProjectCharacteristicLevels.VERY_HIGH;
            }
        }

        return ProjectCharacteristicLevels.NORMAL;
    }

    public static double ConvertToRadians(double angle)
    {
        return (Math.PI / 180) * angle;
    }
 
    private static ProjectCharacteristicLevels CalculateCommunication()
    {
        return ProjectCharacteristicLevels.NORMAL;
    }

    private static ProjectCharacteristicLevels CalculateSitesNumber(int numSites)
    {
        switch(numSites)
        {
            case 2: return ProjectCharacteristicLevels.VERY_LOW;
            case 3: return ProjectCharacteristicLevels.LOW;
            case 4: return ProjectCharacteristicLevels.NORMAL;
            case 5: return ProjectCharacteristicLevels.HIGH;
            case 6: return ProjectCharacteristicLevels.HIGH;
            case 7: return ProjectCharacteristicLevels.VERY_HIGH;
        }

        return ProjectCharacteristicLevels.NORMAL;
    }

    public static ProjectDifficultyLevels CalculateProjectDifficulty(ProjectCharacteristicLevels[] projectCharacteristicsActual)
    {
        int workingTimeOverlapValue = 0;
        int languageDifferenceValue = 0;
        int culturalDifferenceValue = 0;
        int instabilityValue = 0;
        int costumerProximityValue = 0;
        int communicationValue = 0;
        int sitesNumberValue = 0;

        switch(projectCharacteristicsActual[0])
        {
            case ProjectCharacteristicLevels.VERY_LOW:
                workingTimeOverlapValue = 5; break;
            case ProjectCharacteristicLevels.LOW:
                workingTimeOverlapValue = 4; break;
            case ProjectCharacteristicLevels.NORMAL:
                workingTimeOverlapValue = 3; break;
            case ProjectCharacteristicLevels.HIGH:
                workingTimeOverlapValue = 2; break;
            case ProjectCharacteristicLevels.VERY_HIGH:
                workingTimeOverlapValue = 1; break;
        }

        switch (projectCharacteristicsActual[1])
        {
            case ProjectCharacteristicLevels.VERY_LOW:
                languageDifferenceValue = 1; break;
            case ProjectCharacteristicLevels.LOW:
                languageDifferenceValue = 2; break;
            case ProjectCharacteristicLevels.NORMAL:
                languageDifferenceValue = 3; break;
            case ProjectCharacteristicLevels.HIGH:
                languageDifferenceValue = 4; break;
            case ProjectCharacteristicLevels.VERY_HIGH:
                languageDifferenceValue = 5; break;
        }

        switch (projectCharacteristicsActual[2])
        {
            case ProjectCharacteristicLevels.VERY_LOW:
                culturalDifferenceValue = 1; break;
            case ProjectCharacteristicLevels.LOW:
                culturalDifferenceValue = 2; break;
            case ProjectCharacteristicLevels.NORMAL:
                culturalDifferenceValue = 3; break;
            case ProjectCharacteristicLevels.HIGH:
                culturalDifferenceValue = 4; break;
            case ProjectCharacteristicLevels.VERY_HIGH:
                culturalDifferenceValue = 5; break;
        }

        switch (projectCharacteristicsActual[3])
        {
            case ProjectCharacteristicLevels.VERY_LOW:
                instabilityValue = 1; break;
            case ProjectCharacteristicLevels.LOW:
                instabilityValue = 2; break;
            case ProjectCharacteristicLevels.NORMAL:
                instabilityValue = 3; break;
            case ProjectCharacteristicLevels.HIGH:
                instabilityValue = 4; break;
            case ProjectCharacteristicLevels.VERY_HIGH:
                instabilityValue = 5; break;
        }

        switch (projectCharacteristicsActual[4])
        {
            case ProjectCharacteristicLevels.VERY_LOW:
                costumerProximityValue = 1; break;
            case ProjectCharacteristicLevels.LOW:
                costumerProximityValue = 2; break;
            case ProjectCharacteristicLevels.NORMAL:
                costumerProximityValue = 3; break;
            case ProjectCharacteristicLevels.HIGH:
                costumerProximityValue = 4; break;
            case ProjectCharacteristicLevels.VERY_HIGH:
                costumerProximityValue = 5; break;
        }

        switch (projectCharacteristicsActual[5])
        {
            case ProjectCharacteristicLevels.VERY_LOW:
                communicationValue = 5; break;
            case ProjectCharacteristicLevels.LOW:
                communicationValue = 4; break;
            case ProjectCharacteristicLevels.NORMAL:
                communicationValue = 3; break;
            case ProjectCharacteristicLevels.HIGH:
                communicationValue = 2; break;
            case ProjectCharacteristicLevels.VERY_HIGH:
                communicationValue = 1; break;
        }

        switch (projectCharacteristicsActual[6])
        {
            case ProjectCharacteristicLevels.VERY_LOW:
                sitesNumberValue = 1; break;
            case ProjectCharacteristicLevels.LOW:
                sitesNumberValue = 2; break;
            case ProjectCharacteristicLevels.NORMAL:
                sitesNumberValue = 3; break;
            case ProjectCharacteristicLevels.HIGH:
                sitesNumberValue = 4; break;
            case ProjectCharacteristicLevels.VERY_HIGH:
                sitesNumberValue = 5; break;
        }

        double difficultyValue = 0.11 * workingTimeOverlapValue + 0.11 * languageDifferenceValue + 0.13 * culturalDifferenceValue + 0.11 * instabilityValue + 
            0.17 * costumerProximityValue + 0.23 * communicationValue + 0.14 * sitesNumberValue;

        if (0.5 <= difficultyValue && difficultyValue < 1.5)
        {
            return ProjectDifficultyLevels.VERY_LOW;
        }
        else if (1.5 <= difficultyValue && difficultyValue < 2.5)
        {
            return ProjectDifficultyLevels.LOW;
        }
        else if (2.5 <= difficultyValue && difficultyValue < 3.5)
        {
            return ProjectDifficultyLevels.MEDIUM;
        }
        else if (3.5 <= difficultyValue && difficultyValue < 4.5)
        {
            return ProjectDifficultyLevels.HIGH;
        }
        else if (4.5 <= difficultyValue && difficultyValue <= 5.5)
        {
            return ProjectDifficultyLevels.VERY_HIGH;
        }

        return ProjectDifficultyLevels.MEDIUM;
    }

    public static bool InsertResultGame(float stressValue, float progressValue, float budgetValue, float durationValue, int totalNegativeEvents, int correctNegativeEvents)
    {
        bool inserted = true;
        DBGameConfiguration dbGameConfiguration = new DBGameConfiguration();
        int result = dbGameConfiguration.InsertResultGame(actualGameConfiguration.CodGame, stressValue, progressValue, budgetValue, durationValue, totalNegativeEvents, correctNegativeEvents);

        if (result == -1)
        {
            Debug.Log($"[CONTROL GAME CONFIGURATION - ERROR] Result game {actualGameConfiguration.CodGame} did not inserted into the database.");
            inserted = false;
        }
        else if (result == 1)
        {
            Debug.Log($"[CONTROL GAME CONFIGURATION - INFO] Result game '{actualGameConfiguration.CodGame}' inserted into the database.");
        }

        return inserted;
    }

    public static List<GameConfiguration> GetAllGamesOfPlayer(string username)
    {
        List<GameConfiguration> listGames;
        DBGameConfiguration dbGameConfiguration = new DBGameConfiguration();
        listGames = dbGameConfiguration.GetAllGamesOfPlayer(username);

        return listGames;
    }
}