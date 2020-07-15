using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class RecommendConfigurationVariables
{
    public static int[] fuzz_basic = { 0, 0, 4, 6 };
    public static int[] fuzz_inter = { 4, 6, 13, 15 };
    public static int[] fuzz_advan = { 13, 15, 19, 19 };

    public static Dictionary<object, object> problemBasic = new Dictionary<object, object>
    {
        { "SitesNumber", 2 },
        { "Countries", 1 },
        { "ClientMainSite", 1 },
        { "Languages", 1 },
        { "CommonLanguage", "English" },
        { "MaxTimeDifference",  2},
        { "InstabilityCountries", 0 },
        { "TeamSize", 10 }
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
        { "TeamSize", 30 }
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
        { "TeamSize", 80 }
    };

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
            { "TeamSize", 0 }
        };

        result["SitesNumber"] = CalculateValue(basicLevel, intermediateLevel, advancedLevel, "SitesNumber");
        result["Countries"] = CalculateValue(basicLevel, intermediateLevel, advancedLevel, "Countries");
        result["ClientMainSite"] = CalculateValue(basicLevel, intermediateLevel, advancedLevel, "ClientMainSite");
        result["Languages"] = CalculateValue(basicLevel, intermediateLevel, advancedLevel, "Languages");
        result["MaxTimeDifference"] = CalculateValue(basicLevel, intermediateLevel, advancedLevel, "MaxTimeDifference");
        result["InstabilityCountries"] = CalculateValue(basicLevel, intermediateLevel, advancedLevel, "InstabilityCountries");
        result["TeamSize"] = CalculateValue(basicLevel, intermediateLevel, advancedLevel, "TeamSize");

        if(basicLevel >= intermediateLevel)
        {
            result["CommonLanguage"] = problemBasic["CommonLanguage"];
        }
        else if(intermediateLevel >= basicLevel && intermediateLevel >= advancedLevel)
        {
            result["CommonLanguage"] = problemIntermediate["CommonLanguage"];
        }
        else if(advancedLevel >= intermediateLevel)
        {
            result["CommonLanguage"] = problemAdvanced["CommonLanguage"];
        }

        return result;
    }

    private static int CalculateValue(float basicLevel, float intermediateLevel, float advancedLevel, string key)
    {
        return Convert.ToInt32(Math.Round(Convert.ToDouble(basicLevel * Convert.ToSingle(problemBasic[key]) + intermediateLevel * Convert.ToSingle(problemIntermediate[key]) + advancedLevel * Convert.ToSingle(problemAdvanced[key]))));
    }
}
