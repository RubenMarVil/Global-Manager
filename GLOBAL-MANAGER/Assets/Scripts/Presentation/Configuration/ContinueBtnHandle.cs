using Lean.Transition.Method;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ContinueBtnHandle : MonoBehaviour
{
    void Start()
    {
        
    }

    void Update()
    {
        
    }

    public void PointerEnterHandle()
    {
        if(GetComponent<Button>().interactable)
        {
            transform.GetChild(3).GetComponent<LeanTransformLocalScaleXY>().BeginThisTransition();
        }
    }

    public void PointerExitHandle()
    {
        if(GetComponent<Button>().interactable)
        {
            transform.GetChild(4).GetComponent<LeanTransformLocalScaleXY>().BeginThisTransition();
        }
    }
}
