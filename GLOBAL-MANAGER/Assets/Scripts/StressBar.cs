using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StressBar : MonoBehaviour
{
    static Slider stressBar;
    Image fillBar;

    public Color VeryLowColor;
    public Color LowColor;
    public Color MediumColor;
    public Color HighColor;
    public Color VeryHighColor;

    private static Animator anim;

    void Start()
    {
        stressBar = GetComponent<Slider>();
        fillBar = stressBar.transform.GetChild(1).gameObject.transform.GetChild(0).gameObject.GetComponent<Image>();

        anim = GetComponent<Animator>();
    }

    void Update()
    {
        if(stressBar.value <= stressBar.maxValue / 5)
        {
            fillBar.color = VeryLowColor;
        }
        else if(stressBar.value <= stressBar.maxValue * 2/5)
        {
            fillBar.color = LowColor;
        }
        else if(stressBar.value <= stressBar.maxValue * 3/5)
        {
            fillBar.color = MediumColor;
        }
        else if(stressBar.value <= stressBar.maxValue * 4/5)
        {
            fillBar.color = HighColor;
        }
        else
        {
            fillBar.color = VeryHighColor;
        }

        if(stressBar.value == stressBar.maxValue)
        {
            GameObject EndUI = GameObject.FindGameObjectWithTag("FinishEndUI").transform.GetChild(0).gameObject;
            EndUI.SetActive(true);

            EndUI.transform.GetChild(4).gameObject.GetComponent<Text>().text = GameHandle.correctNegativeCommunicationEvents.ToString() + "/" + GameHandle.negativeCommunicationEvents.ToString();
            EndUI.transform.GetChild(5).gameObject.GetComponent<Text>().text = GameHandle.correctNegativeCoordinationEvents.ToString() + "/" + GameHandle.negativeCoordinationEvents.ToString();
            EndUI.transform.GetChild(6).gameObject.GetComponent<Text>().text = GameHandle.correctNegativeControlEvents.ToString() + "/" + GameHandle.negativeControlEvents.ToString();

            GameHandle.PauseGame();
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

        stressBar.value += value;
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
