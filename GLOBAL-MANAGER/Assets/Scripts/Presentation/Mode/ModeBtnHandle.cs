using Assets.Scripts.Control;
using Lean.Transition.Method;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ModeBtnHandle : MonoBehaviour
{
    public void PointerEnterHandle()
    {
        if (GetComponent<Button>().interactable)
        {
            transform.GetChild(4).GetComponent<LeanTransformLocalScaleXY>().BeginThisTransition();
        }
    }

    public void PointerExitHandle()
    {
        if (GetComponent<Button>().interactable)
        {
            transform.GetChild(5).GetComponent<LeanTransformLocalScaleXY>().BeginThisTransition();
        }
    }

    public void RecommendBtn()
    {
        //GameConfiguration configuration = RecommendConfiguration.GetRecommendation();
        //string[] countrySitesName = new string[configuration.NumSites];
        //string[] languageSitesName = new string[configuration.NumSites];

        //for(int i = 0; i < configuration.NumSites; i++)
        //{
        //    countrySitesName[i] = configuration.SitesList[i].Country;
        //    languageSitesName[i] = configuration.SitesList[i].LevelCommonLanguage.ToString();
        //}

        //ProjectCharacteristicLevels[] projectCharacteristicsActual = GameConfigurationControl.CalculateProjectCharacteristics(configuration.NumSites,
        //    countrySitesName, languageSitesName, configuration.CommonLanguage, configuration.ClientCountry, 1);


    }

    public void ManualBtn()
    {
        SceneManager.LoadScene(3, LoadSceneMode.Single);
    }
}
