using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProgressBar : MonoBehaviour
{
    static Slider progressBar;

    private static Animator anim;

    void Start()
    {
        progressBar = GetComponent<Slider>();

        anim = GetComponent<Animator>();
    }

    void Update()
    {
        if(progressBar.value == progressBar.maxValue)
        {
            GameObject EndUI = GameObject.FindGameObjectWithTag("FinishEndUI").transform.GetChild(3).gameObject;
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

        progressBar.value += value;
    }

    public static void AddValueConst(float value)
    {
        progressBar.value += value;
    }

    public static float GetValue()
    {
        return progressBar.value;
    }
}
