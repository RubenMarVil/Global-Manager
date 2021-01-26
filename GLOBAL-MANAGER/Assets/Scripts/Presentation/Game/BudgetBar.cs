using Lean.Gui;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BudgetBar : MonoBehaviour
{
    static Slider budgetBar;
    Image fillBar;

    public Color Color1;
    public Color Color2;
    public Color Color3;
    public Color Color4;
    public Color Color5;
    public Color Color6;
    public Color Color7;
    public Color Color8;
    public Color Color9;
    public Color Color10;

    private static Animator anim;

    public LeanWindow Finish;

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
        if (budgetBar.value <= budgetBar.maxValue / 10)
        {
            fillBar.color = Color1;
        }
        else if (budgetBar.value <= budgetBar.maxValue * 2 / 10)
        {
            fillBar.color = Color2;
        }
        else if (budgetBar.value <= budgetBar.maxValue * 3 / 10)
        {
            fillBar.color = Color3;
        }
        else if (budgetBar.value <= budgetBar.maxValue * 4 / 10)
        {
            fillBar.color = Color4;
        }
        else if (budgetBar.value <= budgetBar.maxValue * 5 / 10)
        {
            fillBar.color = Color5;
        }
        else if (budgetBar.value <= budgetBar.maxValue * 6 / 10)
        {
            fillBar.color = Color6;
        }
        else if (budgetBar.value <= budgetBar.maxValue * 7 / 10)
        {
            fillBar.color = Color7;
        }
        else if (budgetBar.value <= budgetBar.maxValue * 8 / 10)
        {
            fillBar.color = Color8;
        }
        else if (budgetBar.value <= budgetBar.maxValue * 9 / 10)
        {
            fillBar.color = Color9;
        }
        else
        {
            fillBar.color = Color10;
        }

        if (budgetBar.value == budgetBar.minValue)
        {
            Finish.TurnOn();

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

        AddValueConst(value);
        //budgetBar.value += value;
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
