using Assets.Scripts.Control;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class RecommendConfigurationVariables
{
    public static int[] fuzz_basic = { 0, 0, 3, 7 };
    public static int[] fuzz_inter = { 3, 7, 12, 16 };
    public static int[] fuzz_advan = { 12, 16, 19, 19 };

    public static Dictionary<object, object> problemBasic = new Dictionary<object, object>
    {
        { "SitesNumber", 2 },
        { "Countries", 1 },
        { "ClientMainSite", 1 },
        { "Languages", 1 },
        { "CommonLanguage", "English" },
        { "MaxTimeDifference",  2},
        { "InstabilityCountries", 0 },
        { "TeamSize", 10 },
        { "CommunicationSynchronous", 3 }
    };

    public static Dictionary<object, object> problemIntermediate = new Dictionary<object, object>
    {
        { "SitesNumber", 4 },
        { "Countries", 3 },
        { "ClientMainSite", 0 },
        { "Languages", 2 },
        { "CommonLanguage", "Majority" },
        { "MaxTimeDifference", 8 },
        { "InstabilityCountries", 1 },
        { "TeamSize", 30 },
        { "CommunicationSynchronous", 1.5 }
    };

    public static Dictionary<object, object> problemAdvanced = new Dictionary<object, object>
    {
        { "SitesNumber", 7 },
        { "Countries", 7 },
        { "ClientMainSite", 0 },
        { "Languages", 5 },
        { "CommonLanguage", "Random" },
        { "MaxTimeDifference",  18},
        { "InstabilityCountries", 3 },
        { "TeamSize", 80 },
        { "CommunicationSynchronous", 0 }
    };

    public static int numQuestionsBasic = 12;
    public static int numQuestionsIntermediate = 16;
    public static int numQuestionsAdvanced = 25;

    public static int timeEV_EVBasic = 60;
    public static int timeEV_EVIntermediate = 50;
    public static int timeEV_EVAdvanced = 35;

    public static float XfactorVERY_LOW = 1.1f;
    public static float XfactorLOW = 1.05f;
    public static float XfactorMEDIUM = 1f;
    public static float XfactorHIGH = 0.9f;
    public static float XfactorVERY_HIGH = 0.8f;

    public static int streakFailureForPositiveBasic = 2;
    public static int streakFailureForPositiveIntermediate = 4;
    public static int streakFailureForPositiveAdvanced = 6;

    public static float prodWorkerDayBasicCorrect = 1.1f;
    public static float prodWorkerDayIntermediateCorrect = 1.05f;
    public static float prodWorkerDayAdvancedCorrect = 1.05f;
    public static float prodWorkerDayBasicFailure = 0.95f;
    public static float prodWorkerDayIntermediateFailure = 0.95f;
    public static float prodWorkerDayAdvancedFailure = 0.9f;

    public static Dictionary<object, object> AdaptProblem(float basicLevel, float intermediateLevel, float advancedLevel)
    {
        Dictionary<object, object> result = new Dictionary<object, object>
        {
            { "SitesNumber", 0 },
            { "Countries", 0 },
            { "ClientMainSite", 0 },
            { "Languages", 0 },
            { "CommonLanguage", "" },
            { "MaxTimeDifference",  0},
            { "InstabilityCountries", 0 },
            { "TeamSize", 0 },
            { "CommunicationSynchronous", 0 }
        };

        result["SitesNumber"] = CalculateValue(basicLevel, intermediateLevel, advancedLevel, "SitesNumber");
        result["Countries"] = CalculateValue(basicLevel, intermediateLevel, advancedLevel, "Countries");
        result["ClientMainSite"] = CalculateValue(basicLevel, intermediateLevel, advancedLevel, "ClientMainSite");
        result["Languages"] = CalculateValue(basicLevel, intermediateLevel, advancedLevel, "Languages");
        result["MaxTimeDifference"] = CalculateValue(basicLevel, intermediateLevel, advancedLevel, "MaxTimeDifference");
        result["InstabilityCountries"] = CalculateValue(basicLevel, intermediateLevel, advancedLevel, "InstabilityCountries");
        result["TeamSize"] = CalculateValue(basicLevel, intermediateLevel, advancedLevel, "TeamSize");
        result["CommunicationSynchronous"] = CalculateValue(basicLevel, intermediateLevel, advancedLevel, "CommunicationSynchronous");

        if(basicLevel > intermediateLevel)
        {
            result["CommonLanguage"] = problemBasic["CommonLanguage"];
        }
        else if(intermediateLevel >= basicLevel && intermediateLevel > advancedLevel)
        {
            result["CommonLanguage"] = problemIntermediate["CommonLanguage"];
        }
        else if(advancedLevel >= intermediateLevel)
        {
            result["CommonLanguage"] = problemAdvanced["CommonLanguage"];
        }

        return result;
    }

    public static int GetNumQuestionsAdapted(float basicLevel, float intermediateLevel, float advancedLevel)
    {
        return Convert.ToInt32(Math.Round(Convert.ToDouble(basicLevel * numQuestionsBasic + intermediateLevel * numQuestionsIntermediate + advancedLevel * numQuestionsAdvanced)));
    }

    public static int GetTimeEV_EVAdapted(float basicLevel, float intermediateLevel, float advancedLevel)
    {
        return Convert.ToInt32(Math.Round(Convert.ToDouble(basicLevel * timeEV_EVBasic + intermediateLevel * timeEV_EVIntermediate + advancedLevel * timeEV_EVAdvanced)));
    }

    private static int CalculateValue(float basicLevel, float intermediateLevel, float advancedLevel, string key)
    {
        return Convert.ToInt32(Math.Round(Convert.ToDouble(basicLevel * Convert.ToSingle(problemBasic[key]) + intermediateLevel * Convert.ToSingle(problemIntermediate[key]) + advancedLevel * Convert.ToSingle(problemAdvanced[key]))));
    }

    public static float GetXFactor(ProjectDifficultyLevels level)
    {
        switch (level)
        {
            case (ProjectDifficultyLevels.VERY_LOW):
                return XfactorVERY_LOW;
            case (ProjectDifficultyLevels.LOW):
                return XfactorLOW;
            case (ProjectDifficultyLevels.MEDIUM):
                return XfactorMEDIUM;
            case (ProjectDifficultyLevels.HIGH):
                return XfactorHIGH;
            case (ProjectDifficultyLevels.VERY_HIGH):
                return XfactorVERY_HIGH;
        }

        return XfactorMEDIUM;
    }

    public static int GetStreakFailureForPositiveAdapted(float basicLevel, float intermediateLevel, float advancedLevel)
    {
        return Convert.ToInt32(Math.Round(Convert.ToDouble(basicLevel * streakFailureForPositiveBasic + intermediateLevel * streakFailureForPositiveIntermediate + advancedLevel * streakFailureForPositiveAdvanced)));
    }

    public static float GetProdWorkerDayCorrect(float basicLevel, float intermediateLevel, float advancedLevel)
    {
        return basicLevel * prodWorkerDayBasicCorrect + intermediateLevel * prodWorkerDayIntermediateCorrect + advancedLevel * prodWorkerDayAdvancedCorrect;
    }

    public static float GetProdWorkerDayFailure(float basicLevel, float intermediateLevel, float advancedLevel)
    {
        return basicLevel * prodWorkerDayBasicFailure + intermediateLevel * prodWorkerDayIntermediateFailure + advancedLevel * prodWorkerDayAdvancedFailure;
    }
}
