using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BudgetDurationHandle : MonoBehaviour
{
    public int BudgetValue;
    public int DurationValue;

    public InputField BudgetInput;
    public InputField DurationInput;

    public void ChangeBudget(string input)
    {
        BudgetValue = Convert.ToInt32(input);
    }

    public void ChangeDuration(string input)
    {
        DurationValue = Convert.ToInt32(input);
    }

    public void SetBudgetRecommendation(int value)
    {
        BudgetInput.text = value.ToString();
    }

    public void SetDurationRecommendation(int value)
    {
        DurationInput.text = value.ToString();
    }
}
