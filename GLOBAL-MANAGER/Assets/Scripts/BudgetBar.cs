using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BudgetBar : MonoBehaviour
{
    static Slider budgetBar;
    Image fillBar;

    public Color VeryHighColor;
    public Color HighColor;
    public Color MediumColor;
    public Color LowColor;
    public Color VeryLowColor;

    private static Animator anim;

    void Start()
    {
        budgetBar = GetComponent<Slider>();
        fillBar = budgetBar.transform.GetChild(1).gameObject.transform.GetChild(0).gameObject.GetComponent<Image>();

        budgetBar.maxValue = GameConfigurationControl.actualGameConfiguration.InitialBudget;

        budgetBar.value = budgetBar.maxValue;

        anim = GetComponent<Animator>();
    }

    void Update()
    {
        if (budgetBar.value <= budgetBar.maxValue / 5)
        {
            fillBar.color = VeryLowColor;
        }
        else if (budgetBar.value <= budgetBar.maxValue * 2 / 5)
        {
            fillBar.color = LowColor;
        }
        else if (budgetBar.value <= budgetBar.maxValue * 3 / 5)
        {
            fillBar.color = MediumColor;
        }
        else if (budgetBar.value <= budgetBar.maxValue * 4 / 5)
        {
            fillBar.color = HighColor;
        }
        else
        {
            fillBar.color = VeryHighColor;
        }

        if(budgetBar.value == budgetBar.minValue)
        {
            GameObject EndUI = GameObject.FindGameObjectWithTag("FinishEndUI").transform.GetChild(1).gameObject;
            EndUI.SetActive(true);

            EndUI.transform.GetChild(4).gameObject.GetComponent<Text>().text = GameHandle.correctNegativeCommunicationEvents.ToString() + "/" + GameHandle.negativeCommunicationEvents.ToString();
            EndUI.transform.GetChild(5).gameObject.GetComponent<Text>().text = GameHandle.correctNegativeCoordinationEvents.ToString() + "/" + GameHandle.negativeCoordinationEvents.ToString();
            EndUI.transform.GetChild(6).gameObject.GetComponent<Text>().text = GameHandle.correctNegativeControlEvents.ToString() + "/" + GameHandle.negativeControlEvents.ToString();

            GameHandle.PauseGame();
        }
    }

    public static void AddValue(float value)
    {
        if (value < 0.0f)
        {
            anim.SetTrigger("DownTrigger");
        }
        else if (value > 0.0f)
        {
            anim.SetTrigger("UpTrigger");
        }

        budgetBar.value += value;
    }

    public static void AddValueConst(float value)
    {
        budgetBar.value += value;
    }

    public static float GetPercentageValue()
    {
        float percentage = (budgetBar.value / budgetBar.maxValue) * 100;

        return percentage;
    }

    public static float GetValue()
    {
        return budgetBar.value;
    }
}
