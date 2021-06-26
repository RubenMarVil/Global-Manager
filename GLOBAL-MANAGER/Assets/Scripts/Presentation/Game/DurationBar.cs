using Lean.Gui;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UI;

public class DurationBar : MonoBehaviour
{
    static Slider durationBar;
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
        durationBar = GetComponent<Slider>();
        fillBar = durationBar.transform.GetChild(1).gameObject.transform.GetChild(0).gameObject.GetComponent<Image>();

        durationBar.maxValue = GameConfigurationControl.actualGameConfiguration.InitialDuration;

        durationBar.value = durationBar.maxValue;

        anim = GetComponent<Animator>();
    }

    void Update()
    {
        if (durationBar.value <= durationBar.maxValue / 10)
        {
            fillBar.color = Color1;
        }
        else if (durationBar.value <= durationBar.maxValue * 2 / 10)
        {
            fillBar.color = Color2;
        }
        else if (durationBar.value <= durationBar.maxValue * 3 / 10)
        {
            fillBar.color = Color3;
        }
        else if (durationBar.value <= durationBar.maxValue * 4 / 10)
        {
            fillBar.color = Color4;
        }
        else if (durationBar.value <= durationBar.maxValue * 5 / 10)
        {
            fillBar.color = Color5;
        }
        else if (durationBar.value <= durationBar.maxValue * 6 / 10)
        {
            fillBar.color = Color6;
        }
        else if (durationBar.value <= durationBar.maxValue * 7 / 10)
        {
            fillBar.color = Color7;
        }
        else if (durationBar.value <= durationBar.maxValue * 8 / 10)
        {
            fillBar.color = Color8;
        }
        else if (durationBar.value <= durationBar.maxValue * 9 / 10)
        {
            fillBar.color = Color9;
        }
        else
        {
            fillBar.color = Color10;
        }

        if (durationBar.value == durationBar.minValue)
        {
            Finish.TurnOn();

            GameHandle.PauseGame();

            GameHandle.SaveGameUpdatePlayer();
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
        //durationBar.value += value;
    }

    public static void AddValueConst(float value)
    {
        durationBar.value += value;
    }

    public static float GetPercentageValue()
    {
        float percentage = (durationBar.value / durationBar.maxValue) * 100;

        return percentage;
    }

    public static float GetValue()
    {
        return durationBar.value;
    }
}
