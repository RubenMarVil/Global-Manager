using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CautionUpdate : MonoBehaviour
{
    public GameObject cautionImage;
    public GameObject stressCaution;
    public GameObject progressCaution;
    public GameObject budgetCaution;
    public GameObject durationCaution;

    public Text stressValue;
    public Text progressValue;
    public Text budgetValue;
    public Text durationValue;

    void Start()
    {
        
    }

    void Update()
    {
        if(StressBar.GetPercentageValue() > 80f)
        {
            stressCaution.SetActive(true);
            stressValue.text = Math.Round(StressBar.GetValue(), 2)*10 + "%";
            cautionImage.SetActive(true);
        }
        else
        {
            stressCaution.SetActive(false);
        }

        if (ProgressBar.GetValue() > 75f)
        {
            progressCaution.SetActive(true);
            progressValue.text = Math.Round(ProgressBar.GetValue(), 2) + "%";
            cautionImage.SetActive(true);
        }
        else
        {
            progressCaution.SetActive(false);
        }

        if (BudgetBar.GetPercentageValue() < 25f)
        {
            budgetCaution.SetActive(true);
            budgetValue.text = Math.Round(BudgetBar.GetValue(), 0) + "$";
            cautionImage.SetActive(true);
        }
        else
        {
            budgetCaution.SetActive(false);
        }

        if (DurationBar.GetPercentageValue() < 25f)
        {
            durationCaution.SetActive(true);
            durationValue.text = Math.Round(DurationBar.GetValue(), 0) + " days";
            cautionImage.SetActive(true);
        }
        else
        {
            durationCaution.SetActive(false);
        }

        if(!stressCaution.activeSelf && !progressCaution.activeSelf && !budgetCaution.activeSelf && !durationCaution.activeSelf)
        {
            cautionImage.SetActive(false);
        }
    }
}
