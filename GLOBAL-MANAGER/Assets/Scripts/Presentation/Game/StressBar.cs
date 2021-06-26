using Lean.Gui;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StressBar : MonoBehaviour
{
    static Slider stressBar;
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
        stressBar = GetComponent<Slider>();
        fillBar = stressBar.transform.GetChild(1).gameObject.transform.GetChild(0).gameObject.GetComponent<Image>();

        anim = GetComponent<Animator>();
    }

    void Update()
    {
        if (stressBar.value <= stressBar.maxValue / 10)
        {
            fillBar.color = Color1;
        }
        else if (stressBar.value <= stressBar.maxValue * 2 / 10)
        {
            fillBar.color = Color2;
        }
        else if (stressBar.value <= stressBar.maxValue * 3 / 10)
        {
            fillBar.color = Color3;
        }
        else if (stressBar.value <= stressBar.maxValue * 4 / 10)
        {
            fillBar.color = Color4;
        }
        else if (stressBar.value <= stressBar.maxValue * 5 / 10)
        {
            fillBar.color = Color5;
        }
        else if (stressBar.value <= stressBar.maxValue * 6 / 10)
        {
            fillBar.color = Color6;
        }
        else if (stressBar.value <= stressBar.maxValue * 7 / 10)
        {
            fillBar.color = Color7;
        }
        else if (stressBar.value <= stressBar.maxValue * 8 / 10)
        {
            fillBar.color = Color8;
        }
        else if (stressBar.value <= stressBar.maxValue * 9 / 10)
        {
            fillBar.color = Color9;
        }
        else
        {
            fillBar.color = Color10;
        }

        if (stressBar.value == stressBar.maxValue)
        {
            Finish.TurnOn();

            GameHandle.PauseGame();

            GameHandle.SaveGameUpdatePlayer();
        }
    }

    public static void AddValue(float value)
    {
        if(value < 0.0f)
        {
            anim.SetTrigger("DownTrigger");
        }
        else if(value > 0.0f)
        {
            anim.SetTrigger("UpTrigger");
        }

        AddValueConst(value);
        // stressBar.value += value;
    }

    public static void AddValueConst(float value)
    {
        stressBar.value += value;
    }

    public static float GetPercentageValue()
    {
        float percentage = (stressBar.value / stressBar.maxValue) * 100;

        return percentage;
    }

    public static float GetValue()
    {
        return stressBar.value;
    }
}
