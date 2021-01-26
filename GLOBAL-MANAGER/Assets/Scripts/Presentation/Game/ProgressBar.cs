using Lean.Gui;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProgressBar : MonoBehaviour
{
    static Slider progressBar;

    private static Animator anim;

    public LeanWindow Finish;

    void Start()
    {
        progressBar = GetComponent<Slider>();

        anim = GetComponent<Animator>();
    }

    void Update()
    {
        if(progressBar.value == progressBar.maxValue)
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
