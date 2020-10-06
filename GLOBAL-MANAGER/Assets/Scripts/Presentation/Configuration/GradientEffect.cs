using Lean.Transition.Method;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GradientEffect : MonoBehaviour
{
    public Color Color1;
    public Color Color2;

    public GameObject ColorTransition;

    private bool Change;

    void Start()
    {
        Change = true;
        ColorTransition.GetComponent<LeanGraphicColor>().BeginThisTransition();
    }

    public void ChangeColor()
    {
        if(Change)
        {
            ColorTransition.GetComponent<LeanGraphicColor>().Data.Color = Color2;
        }
        else
        {
            ColorTransition.GetComponent<LeanGraphicColor>().Data.Color = Color1;
        }
        ColorTransition.GetComponent<LeanGraphicColor>().BeginThisTransition();

        Change = !Change;
    }
}
