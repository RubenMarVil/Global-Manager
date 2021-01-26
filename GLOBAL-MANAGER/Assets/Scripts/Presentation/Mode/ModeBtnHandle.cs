using Assets.Scripts.Control;
using Lean.Gui;
using Lean.Transition.Method;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ModeBtnHandle : MonoBehaviour
{
    public Text SitesNumberText;
    public Text DifficultyText;
    public Text ClientCountryText;
    public Text CommonLanguageText;
    public Text BudgetText;
    public Text DurationText;
    public Text CommunicationToolsText;
    public Text SitesText;

    public LeanWindow ModalGameInfo;

    public Animator transitionAnim;

    void Start()
    {
        GameConfigurationControl.actualGameConfiguration = new GameConfiguration();
    }

    public void RecommendBtn()
    {
        Debug.Log("[ModeHandle - INFO] Mode selected --> Recomendation");
        Debug.Log("[ModeHandle - INFO] Calculating recommendation...");

        GameConfiguration configuration = RecommendConfiguration.GetRecommendation();

        string[] countrySitesName = new string[configuration.NumSites];
        string[] languageSitesName = new string[configuration.NumSites];
        int mainSite = 0;

        for (int i = 0; i < configuration.NumSites; i++)
        {
            countrySitesName[i] = configuration.SitesList[i].Country;
            languageSitesName[i] = configuration.SitesList[i].LevelCommonLanguage.ToString();

            if(configuration.SitesList[i].MainSite == 1)
            {
                mainSite = i + 1;
            }
        }

        List<string> projectCharacteristicsName = new List<string>() { "WorkingTimeOverlap", "LanguageDifference", "CulturalDifference", "InestabilityCountries",
        "CostumerProximity", "Communication", "SitesNumber"};
        ProjectCharacteristicLevels[] projectCharacteristicsActual = GameConfigurationControl.CalculateProjectCharacteristics(configuration.NumSites,
            countrySitesName, languageSitesName, configuration.CommonLanguage, configuration.ClientCountry, mainSite, configuration.CommunicationTools.Communication);

        string info = "[MODEHANDLE - INFO] ";
        for(int i = 0; i < projectCharacteristicsActual.Length; i++)
        {
            info += $"\n\t- {projectCharacteristicsName[i]} --> {projectCharacteristicsActual[i]}";
        }

        Debug.Log(info);

        ProjectDifficultyLevels projectDifficultyActual = GameConfigurationControl.CalculateProjectDifficulty(projectCharacteristicsActual);

        // Saving game recomendation
        GameConfigurationControl.SetFirstConfiguration(configuration.NumSites, configuration.ClientCountry, configuration.CommonLanguage);

        for (int i = 0; i < configuration.NumSites; i++)
        {
            GameConfigurationControl.SetSiteConfiguration(configuration.SitesList[i].Name, configuration.SitesList[i].Country, configuration.SitesList[i].TeamSize,
                configuration.SitesList[i].LevelCommonLanguage.ToString());
        }

        GameConfigurationControl.SetMainSite(mainSite);

        GameConfigurationControl.SetCommunicationTools(configuration.CommunicationTools.Communication);

        for (int i = 0; i < projectCharacteristicsActual.Length; i++)
        {
            GameConfigurationControl.SetProjectCharacteristic(projectCharacteristicsName[i], projectCharacteristicsActual[i]);
        }

        int[] budgetDuration = CalculateBudgetDuration(configuration, projectDifficultyActual);

        GameConfigurationControl.SetFinalConfiguration(projectDifficultyActual.ToString(), budgetDuration[0], budgetDuration[1]);

        ModalGameInfo.TurnOn();
        SitesNumberText.text = configuration.NumSites.ToString();
        DifficultyText.text = projectDifficultyActual.ToString();
        ClientCountryText.text = configuration.ClientCountry;
        CommonLanguageText.text = configuration.CommonLanguage;
        BudgetText.text = $"{budgetDuration[0]} $";
        DurationText.text = $"{budgetDuration[1]} Days";
        CommunicationToolsText.text = $"{configuration.CommunicationTools.Communication[0]}, {configuration.CommunicationTools.Communication[1]}, " +
            $"{configuration.CommunicationTools.Communication[2]}";;
        SitesText.text = $"{configuration.SitesList[0].Country} with {configuration.SitesList[0].TeamSize} workers";
        for(int i = 1; i < configuration.NumSites; i++)
        {
            SitesText.text += $", {configuration.SitesList[i].Country} with {configuration.SitesList[i].TeamSize} workers";
        }
    }

    public int[] CalculateBudgetDuration(GameConfiguration configuration, ProjectDifficultyLevels difficultyLevel)
    {
        int totalWorkers = 0;
        float salaryPerHour = 0.0f;

        int[] BudgetDuration = new int[2];

        for (int i = 0; i < configuration.NumSites; i++)
        {
            int workers = configuration.SitesList[i].TeamSize;
            totalWorkers += workers;

            Country country = GameConfigurationControl.GetAllDataOfCountry(configuration.SitesList[i].Country);
            salaryPerHour += workers * country.Salary;
        }

        int numQuestions = RecommendConfiguration.GetRecommendQuestions();
        int timeEV_EV = RecommendConfiguration.GetTimeEV_EVRecommend();

        float Xfactor = RecommendConfigurationVariables.GetXFactor(difficultyLevel);

        int timeToProgress = numQuestions * timeEV_EV;
        float workOfWorkerPerDay = (100 / Convert.ToSingle(totalWorkers)) / Convert.ToSingle(timeToProgress);
        GameHandle.workOfWorkerPerDay = workOfWorkerPerDay;
        GameHandle.TimePerEvent = timeEV_EV;

        decimal budgetWithoutRound = new decimal((timeToProgress * salaryPerHour * 8) * Xfactor);
        decimal durationWithoutRound = new decimal((timeToProgress) * Xfactor);

        BudgetDuration[0] = Convert.ToInt32(Math.Round(budgetWithoutRound));
        BudgetDuration[1] = Convert.ToInt32(Math.Round(durationWithoutRound));

        Debug.Log($"Question number --> {numQuestions}\nTime between EVENT-EVENT --> {timeEV_EV}\nFactor X Difficulty --> {Xfactor}" +
            $"\nTime to progress --> {timeToProgress}\nWork of Worker per Day --> {workOfWorkerPerDay}");

        return BudgetDuration;
    }

    public void PlayGame()
    {
        transitionAnim.SetTrigger("end");

        if (GameConfigurationControl.SaveGameConfiguration())
        {
            Debug.Log("[GAME CONFIGURATION - INFO] Game saved in the database");
            StartCoroutine(LoadScene(4));
            //SceneManager.LoadScene(4, LoadSceneMode.Single);
        }
        else
        {
            Debug.Log("[GAME CONFIGURATION - ERROR] Problem occurred saving the game");
        }
    }

    public void ManualBtn()
    {
        transitionAnim.SetTrigger("end");
        Debug.Log("[ModeHandle - INFO] Mode selected --> Manual");
        StartCoroutine(LoadScene(3));
        //SceneManager.LoadScene(3, LoadSceneMode.Single);
    }

    public void BackMainMenu()
    {
        transitionAnim.SetTrigger("end");
        StartCoroutine(LoadScene(0));
        //SceneManager.LoadScene(0, LoadSceneMode.Single);
    }

    IEnumerator LoadScene(int scene)
    {
        yield return new WaitForSeconds(1.5f);
        SceneManager.LoadScene(scene, LoadSceneMode.Single);
    }
}
