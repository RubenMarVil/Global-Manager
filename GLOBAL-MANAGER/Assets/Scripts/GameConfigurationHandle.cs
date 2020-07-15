using Assets.Scripts.Control;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class GameConfigurationHandle : MonoBehaviour
{
    public GameObject siteNumber;
    public GameObject clientCountry;
    public GameObject commonLanguage;
    public GameObject[] sitesName;
    public GameObject[] sitesCountry;
    public GameObject[] sitesTeamSize;
    public GameObject[] sitesLanguageLevel;
    public GameObject mainSite;
    public GameObject[] communicationSites;
    public GameObject[] projectCharacteristics;
    public GameObject projectDifficulty;
    public GameObject initialBudget;
    public GameObject initialDuration;

    private Slider siteNumberSlider;
    private Dropdown clientCountryDropdown;
    private Dropdown commonLanguageDropdown;
    private InputField[] sitesNameInputField;
    private Dropdown[] sitesCountryDropdown;
    private Slider[] sitesTeamSizeSlider;
    private ToggleGroup[] sitesLanguageLevelToggleGroup;
    private Toggle[] sitesLanguageLevelToggleSelected;
    private ToggleGroup mainSiteToggleGroup;
    private Toggle mainSiteToggleSelected;
    private InputField initialBudgetInputField;
    private InputField initialDurationInputField;

    private bool[] sitesLanguageLevelAny;
    private bool mainSiteAny;

    void Start()
    {
        siteNumberSlider = siteNumber.GetComponent<Slider>();
        clientCountryDropdown = clientCountry.GetComponent<Dropdown>();
        commonLanguageDropdown = commonLanguage.GetComponent<Dropdown>();
        sitesNameInputField = new[] {sitesName[0].GetComponent<InputField>(), sitesName[1].GetComponent<InputField>(), sitesName[2].GetComponent<InputField>(),
                                     sitesName[3].GetComponent<InputField>(), sitesName[4].GetComponent<InputField>(), sitesName[5].GetComponent<InputField>(),
                                     sitesName[6].GetComponent<InputField>()};
        sitesCountryDropdown = new[] {sitesCountry[0].GetComponent<Dropdown>(), sitesCountry[1].GetComponent<Dropdown>(), sitesCountry[2].GetComponent<Dropdown>(),
                                      sitesCountry[3].GetComponent<Dropdown>(), sitesCountry[4].GetComponent<Dropdown>(), sitesCountry[5].GetComponent<Dropdown>(),
                                      sitesCountry[6].GetComponent<Dropdown>()};
        sitesTeamSizeSlider = new[] {sitesTeamSize[0].GetComponent<Slider>(), sitesTeamSize[1].GetComponent<Slider>(), sitesTeamSize[2].GetComponent<Slider>(),
                                     sitesTeamSize[3].GetComponent<Slider>(), sitesTeamSize[4].GetComponent<Slider>(), sitesTeamSize[5].GetComponent<Slider>(),
                                     sitesTeamSize[6].GetComponent<Slider>()};
        sitesLanguageLevelToggleGroup = new[] {sitesLanguageLevel[0].GetComponent<ToggleGroup>(), sitesLanguageLevel[1].GetComponent<ToggleGroup>(), sitesLanguageLevel[2].GetComponent<ToggleGroup>(),
                                               sitesLanguageLevel[3].GetComponent<ToggleGroup>(), sitesLanguageLevel[4].GetComponent<ToggleGroup>(), sitesLanguageLevel[5].GetComponent<ToggleGroup>(),
                                               sitesLanguageLevel[6].GetComponent<ToggleGroup>()};
        mainSiteToggleGroup = mainSite.GetComponent<ToggleGroup>();

        sitesLanguageLevelToggleSelected = new Toggle[sitesLanguageLevelToggleGroup.Length];

        sitesLanguageLevelAny = new[] { false, false, false, false, false, false, false };
        mainSiteAny = false;

        initialBudgetInputField = initialBudget.GetComponent<InputField>();
        initialDurationInputField = initialDuration.GetComponent<InputField>();
    }

    void Update()
    {
        string[] sitesCountryName = new string[Convert.ToInt32(siteNumberSlider.value)];
        string[] sitesLanguageLevelToggleNameSelected = new string[Convert.ToInt32(siteNumberSlider.value)];
        int mainSiteNum;
        
        try
        {
            mainSiteToggleSelected = mainSiteToggleGroup.ActiveToggles().ElementAt<Toggle>(0);
            mainSiteNum = Convert.ToInt32(mainSiteToggleSelected.name);
            mainSiteAny = true;
        }
        catch (ArgumentOutOfRangeException)
        {
            mainSiteAny = false;
            mainSiteNum = 0;
        }

        for (int i = 0; i < siteNumberSlider.value; i++)
        {
            try
            {
                sitesLanguageLevelToggleSelected[i] = sitesLanguageLevelToggleGroup[i].ActiveToggles().ElementAt<Toggle>(0);
                sitesLanguageLevelToggleNameSelected[i] = sitesLanguageLevelToggleSelected[i].name;
                sitesLanguageLevelAny[i] = true;
            } 
            catch (ArgumentOutOfRangeException)
            {
                sitesLanguageLevelAny[i] = false;
            }

            sitesCountryName[i] = sitesCountryDropdown[i].options[sitesCountryDropdown[i].value].text;

        }

        ProjectCharacteristicLevels[] projectCharacteristicsActual = GameConfigurationControl.CalculateProjectCharacteristics(Convert.ToInt32(siteNumberSlider.value), 
            sitesCountryName, sitesLanguageLevelToggleNameSelected, commonLanguageDropdown.options[commonLanguageDropdown.value].text,
            clientCountryDropdown.options[clientCountryDropdown.value].text, mainSiteNum);
        SetProjectCharacteristics(projectCharacteristicsActual);

        ProjectDifficultyLevels projectDifficultyActual = GameConfigurationControl.CalculateProjectDifficulty(projectCharacteristicsActual);
        projectDifficulty.GetComponent<ProjectDifficultyHandle>().SetValue(projectDifficultyActual);
    }

    private void SetProjectCharacteristics(ProjectCharacteristicLevels[] projectCharacteristicsActual)
    {
        for(int i = 0; i < projectCharacteristics.Length; i++)
        {
            projectCharacteristics[i].transform.GetChild(2).gameObject.GetComponent<ProjectCharacteristicHandle>().SetValue(projectCharacteristicsActual[i]);
        }
    }

    public void StartGameButton()
    {
        int numSites = (int)siteNumberSlider.value;
        if (CheckStartGame(numSites) && CheckCommunication(numSites))
        {
            GameConfigurationControl.SetFirstConfiguration(numSites, clientCountryDropdown.options[clientCountryDropdown.value].text,
                commonLanguageDropdown.options[commonLanguageDropdown.value].text);
            
            Debug.Log("[GAME CONFIGURATION - INFO] First configuration:\n\t- Sites Number = " + numSites +
                      "\n\t- Client Country = " + clientCountryDropdown.options[clientCountryDropdown.value].text +
                      "\n\t- Common Language = " + commonLanguageDropdown.options[commonLanguageDropdown.value].text);
            for(int i = 0; i < numSites; i++)
            {
                Debug.Log("[GAME CONFIGURATION - INFO] Site" + i+1 + " configuration:\n\t- Name = " + sitesNameInputField[i].text +
                          "\n\t- Country = " + sitesCountryDropdown[i].options[sitesCountryDropdown[i].value].text +
                          "\n\t- Team Size = " + (int)sitesTeamSizeSlider[i].value +
                          "\n\t- Language Level = " + sitesLanguageLevelToggleSelected[i].name);

                GameConfigurationControl.SetSiteConfiguration(sitesNameInputField[i].text, sitesCountryDropdown[i].options[sitesCountryDropdown[i].value].text,
                    (int)sitesTeamSizeSlider[i].value, sitesLanguageLevelToggleSelected[i].name);
            }

            GameConfigurationControl.SetMainSite(Int32.Parse(mainSiteToggleSelected.name));

            Debug.Log("[GAME CONFIGURATION - INFO] Main site = " + mainSiteToggleSelected.name);

            for (int i = 0; i < numSites - 1; i++)
            {
                for (int j = 0; j < i + 1; j++)
                {
                    GameConfigurationControl.SetCommunicationTools(communicationSites[i].transform.GetChild(j).gameObject.GetComponent<CommunicationHandle>().site1Num,
                        communicationSites[i].transform.GetChild(j).gameObject.GetComponent<CommunicationHandle>().site2Num, 
                        communicationSites[i].transform.GetChild(j).gameObject.GetComponent<CommunicationHandle>().communicationList);
                }
            }

            foreach(GameObject projectCharacteristic in projectCharacteristics)
            {
                GameConfigurationControl.SetProjectCharacteristic(projectCharacteristic.name, projectCharacteristic.transform.GetChild(2).gameObject.GetComponent<Text>().text);
            }

            GameConfigurationControl.SetFinalConfiguration(projectDifficulty.GetComponent<Text>().text, float.Parse(initialBudgetInputField.text), float.Parse(initialDurationInputField.text));
        
            if(GameConfigurationControl.SaveGameConfiguration())
            {
                Debug.Log("[GAME CONFIGURATION - INFO] Game saved in the database");
            }
            else
            {
                Debug.Log("[GAME CONFIGURATION - ERROR] Problem occurred saving the game");
            }
        }
        else
        {
            Debug.Log("[GAME CONFIGURATION - ERROR] It is necessary to fill all components of the configuration game");
        }
    }

    private bool CheckStartGame(int sites)
    {
        for(int i = 0; i < sites; i++)
        {
            if(String.IsNullOrWhiteSpace(sitesNameInputField[i].text) || !sitesLanguageLevelAny[i])
            {
                return false;
            }
        }

        if(!mainSiteAny)
        {
            return false;
        }

        return true;
    }

    private bool CheckCommunication(int sites)
    {
        for(int i = 0; i < sites - 1; i++)
        {
            for(int j = 0; j < i+1; j++)
            {
                if(communicationSites[i].transform.GetChild(j).gameObject.GetComponent<CommunicationHandle>().communicationList.Count == 0)
                {
                    return false;
                }
            }
        }

        return true;
    }

    public void RecommendConfigurationBtn()
    {
        GameConfiguration configuration = RecommendConfiguration.GetRecommendation();

        siteNumberSlider.value = configuration.NumSites;
        clientCountryDropdown.value = clientCountryDropdown.options.FindIndex(a => a.text == configuration.ClientCountry);
        commonLanguageDropdown.value = commonLanguageDropdown.options.FindIndex(a => a.text == configuration.CommonLanguage);

        for(int i = 0; i < configuration.NumSites; i++)
        {
            sitesCountryDropdown[i].value = sitesCountryDropdown[i].options.FindIndex(a => a.text == configuration.SitesList[i].Country);
            sitesTeamSizeSlider[i].value = configuration.SitesList[i].TeamSize;
        }
    }
}