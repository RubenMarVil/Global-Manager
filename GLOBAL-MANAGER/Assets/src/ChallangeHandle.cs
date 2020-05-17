using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using OrderChallange;
using System;
using UnityEngine.Experimental.UIElements;

public class ChallangeHandle : MonoBehaviour
{
    public void ChallangeClick()
    {
        OrderChallange.ChallengesOrderHandle.SetChallangeClick(Int32.Parse(name));
    }
}
