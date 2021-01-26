using Assets.Scripts.Control;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ProjectConfigurationHandle : MonoBehaviour
{
    public SiteSlider NumSites;
    public DropDownCountry ClientCountry;
    public DropDownLanguage CommonLanguage;
    public List<SiteHandle> SitesList;
    public Dropdown MainSite;
    public CommunicationProject Communication;
    public BudgetDurationHandle BudgetDuration;

    public Dropdown.OptionDataList NameSitesList;

    public List<Button> ContinueBtnList;
    public List<Button> BackBtnList;
    public List<TabButton> TabConfiguration;

    public List<CharacteristicHandle> CharacteristicsList;
    public DifficultyHandle Difficulty;

    public Animator transitionAnim;

    void Start()
    {
        GameConfigurationControl.actualGameConfiguration = new GameConfiguration();

        MainSite.options.Clear();
        MainSite.options.AddRange(NameSitesList.options.GetRange(0, 3));

        UpdateProjectCharacteristics();
    }

    void Update()
    {
        
    }

    public void CheckGeneralConfiguration()
    {
        if(!String.IsNullOrEmpty(ClientCountry.CountrySelected) && !String.IsNullOrEmpty(CommonLanguage.LanguageSelected))
        {
            ContinueBtnList[0].interactable = true;
            TabConfiguration[1].SetAble();
        }
    }

    public void CheckSitesConfiguration()
    {
        bool ok = true;

        foreach(SiteHandle site in SitesList)
        {
            if (site.Site > NumSites.NumSites)
            {
                break;
            }

            if (String.IsNullOrWhiteSpace(site.Name) || String.IsNullOrEmpty(site.Country) || String.IsNullOrEmpty(site.LanguageLevel))
            {
                ok = false;
                break;
            }
        }

        if(String.IsNullOrEmpty(MainSite.gameObject.transform.GetChild(0).GetComponent<Text>().text))
        {
            ok = false;
        }

        if(ok)
        {
            ContinueBtnList[1].interactable = true;
            TabConfiguration[2].SetAble();
        }
    }

    public void CheckCommunicationConfiguration()
    {
        if(Communication.toolsActives.Count == 3)
        {
            ContinueBtnList[2].interactable = true;
            TabConfiguration[3].SetAble();
            BackBtnList[4].interactable = true;
            CheckBudgetDuration();
        }
        else
        {
            ContinueBtnList[2].interactable = false;
            TabConfiguration[3].SetDissable();
            BackBtnList[4].interactable = false;
            ContinueBtnList[5].interactable = false;
        }
    }

    public void CheckBudgetDuration()
    {
        bool ok = true;

        if(BudgetDuration.BudgetValue == 0 || BudgetDuration.DurationValue == 0)
        {
            ok = false;
        }

        if(ok)
        {
            ContinueBtnList[5].interactable = true;
            ContinueBtnList[5].transform.GetChild(0).GetComponent<Text>().text = "PLAY!";
        }
        else
        {
            ContinueBtnList[5].interactable = false;
            ContinueBtnList[5].transform.GetChild(0).GetComponent<Text>().text = "";
        }
    }

    public void ResetTabSites()
    {
        SitesList[0].ActiveTab();
    }

    public void UpdateMainSiteDropdown()
    {
        int mainsite = MainSite.value;

        MainSite.options.Clear();

        MainSite.options.AddRange(NameSitesList.options.GetRange(0, NumSites.NumSites + 1));

        if (++mainsite <= NumSites.NumSites)
        {
            
            MainSite.value = --mainsite;
        }
        else
        {
            MainSite.value = 0;
        }
    }

    public void UpdateBudgetDurationRecommendation()
    {
        bool update = true;

        int totalWorkers = 0;
        float salaryPerHour = 0.0f;

        for(int i = 0; i < NumSites.NumSites; i++)
        {
            int workers = SitesList[i].TeamSize;
            totalWorkers += workers;

            if(String.IsNullOrEmpty(SitesList[i].Country))
            {
                update = false;
                break;
            }
            Country country = GameConfigurationControl.GetAllDataOfCountry(SitesList[i].Country);
            salaryPerHour += workers * country.Salary;
        }

        if (update)
        {
            int numQuestions = RecommendConfiguration.GetRecommendQuestions();
            int timeEV_EV = RecommendConfiguration.GetTimeEV_EVRecommend();

            float Xfactor = RecommendConfigurationVariables.GetXFactor(UpdateProjectCharacteristics());

            int timeToProgress = numQuestions * timeEV_EV;
            float workOfWorkerPerDay = (100 / Convert.ToSingle(totalWorkers)) / Convert.ToSingle(timeToProgress);
            GameHandle.workOfWorkerPerDay = workOfWorkerPerDay;
            GameHandle.TimePerEvent = timeEV_EV;
            
            decimal budgetWithoutRound = new decimal((timeToProgress * salaryPerHour * 8) * Xfactor);
            decimal durationWithoutRound = new decimal((timeToProgress) * Xfactor);

            BudgetDuration.SetBudgetRecommendation(Convert.ToInt32(Math.Round(budgetWithoutRound)));
            BudgetDuration.SetDurationRecommendation(Convert.ToInt32(Math.Round(durationWithoutRound)));
        }
    }

    public void UpdateProjectInfo()
    {
        string[] sitesCountryName = new string[NumSites.NumSites];
        string[] sitesLanguageLevel = new string[NumSites.NumSites];

        for (int i = 0; i < NumSites.NumSites; i++)
        {
            sitesCountryName[i] = SitesList[i].Country;
            sitesLanguageLevel[i] = SitesList[i].LanguageLevel;
        }

        List<string> communicationList = new List<string>();
        foreach (ToggleButton tool in Communication.toolsActives)
        {
            communicationList.Add(tool.name);
        }

        ProjectCharacteristicLevels[] projectCharacteristicsActual = GameConfigurationControl.CalculateProjectCharacteristics(NumSites.NumSites,
            sitesCountryName, sitesLanguageLevel, CommonLanguage.LanguageSelected, ClientCountry.CountrySelected, MainSite.value, communicationList);

        for (int i = 0; i < projectCharacteristicsActual.Length; i++)
        {
            CharacteristicsList[i].SetValue(projectCharacteristicsActual[i]);
        }

        UpdateProjectDifficulty(projectCharacteristicsActual);
    }

    public ProjectDifficultyLevels UpdateProjectCharacteristics()
    {
        string[] sitesCountryName = new string[NumSites.NumSites];
        string[] sitesLanguageLevel = new string[NumSites.NumSites];

        for(int i = 0; i < NumSites.NumSites; i++)
        {
            sitesCountryName[i] = SitesList[i].Country;
            sitesLanguageLevel[i] = SitesList[i].LanguageLevel;
        }

        List<string> communicationList = new List<string>();
        foreach(ToggleButton tool in Communication.toolsActives)
        {
            communicationList.Add(tool.name);
        }

        ProjectCharacteristicLevels[] projectCharacteristicsActual = GameConfigurationControl.CalculateProjectCharacteristics(NumSites.NumSites,
            sitesCountryName, sitesLanguageLevel, CommonLanguage.LanguageSelected, ClientCountry.CountrySelected, MainSite.value, communicationList);

        for(int i = 0; i < projectCharacteristicsActual.Length; i++)
        {
            CharacteristicsList[i].SetValue(projectCharacteristicsActual[i]);
        }

        return UpdateProjectDifficulty(projectCharacteristicsActual);
    }

    public ProjectDifficultyLevels UpdateProjectDifficulty(ProjectCharacteristicLevels[] projectCharacteristicsActual)
    {
        ProjectDifficultyLevels projectDifficultyActual = GameConfigurationControl.CalculateProjectDifficulty(projectCharacteristicsActual);
        Debug.Log($"[CONTROL GAME CONFIGURATION - INFO] Project Difficulty: {projectDifficultyActual}");
        Difficulty.SetValue(projectDifficultyActual);

        return projectDifficultyActual;
    }

    public void StartGameButton()
    {
        GameConfigurationControl.SetFirstConfiguration(NumSites.NumSites, ClientCountry.CountrySelected, CommonLanguage.LanguageSelected);

        for (int i = 0; i < NumSites.NumSites; i++)
        {
            GameConfigurationControl.SetSiteConfiguration(SitesList[i].Name, SitesList[i].Country, SitesList[i].TeamSize, SitesList[i].LanguageLevel);
        }

        GameConfigurationControl.SetMainSite(MainSite.value);

        GameConfigurationControl.SetCommunicationTools(Communication.toolsActives);

        foreach (CharacteristicHandle projectCharacteristic in CharacteristicsList)
        {
            GameConfigurationControl.SetProjectCharacteristic(projectCharacteristic.transform.parent.name, projectCharacteristic.Value);
        }

        GameConfigurationControl.SetFinalConfiguration(Difficulty.Value, BudgetDuration.BudgetValue, BudgetDuration.DurationValue);

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

    public void BackMainMenu()
    {
        StartCoroutine(LoadScene(2));
        //SceneManager.LoadScene(2, LoadSceneMode.Single);
    }

    IEnumerator LoadScene(int scene)
    {
        transitionAnim.SetTrigger("end");
        yield return new WaitForSeconds(1.5f);
        SceneManager.LoadScene(scene, LoadSceneMode.Single);
    }
}