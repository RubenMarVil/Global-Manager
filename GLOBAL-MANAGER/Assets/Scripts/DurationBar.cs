using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UI;

public class DurationBar : MonoBehaviour
{
    static Slider durationBar;
    Image fillBar;

    public Color VeryHighColor;
    public Color HighColor;
    public Color MediumColor;
    public Color LowColor;
    public Color VeryLowColor;

    private static Animator anim;

    void Start()
    {
        durationBar = GetComponent<Slider>();
        fillBar = durationBar.transform.GetChild(1).gameObject.transform.GetChild(0).gameObject.GetComponent<Image>();

        durationBar.maxValue = GameConfigurationControl.actualGameConfiguration.InitialDuration * 365;

        durationBar.value = durationBar.maxValue;

        anim = GetComponent<Animator>();
    }

    void Update()
    {
        if (durationBar.value <= durationBar.maxValue / 5)
        {
            fillBar.color = VeryLowColor;
        }
        else if (durationBar.value <= durationBar.maxValue * 2 / 5)
        {
            fillBar.color = LowColor;
        }
        else if (durationBar.value <= durationBar.maxValue * 3 / 5)
        {
            fillBar.color = MediumColor;
        }
        else if (durationBar.value <= durationBar.maxValue * 4 / 5)
        {
            fillBar.color = HighColor;
        }
        else
        {
            fillBar.color = VeryHighColor;
        }

        if(durationBar.value == durationBar.minValue)
        {
            GameObject EndUI = GameObject.FindGameObjectWithTag("FinishEndUI").transform.GetChild(2).gameObject;
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

        durationBar.value += value;
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
