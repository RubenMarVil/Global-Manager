using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SolveCommunicationRed3BtnHandle : MonoBehaviour
{
    void Start()
    {

    }

    void Update()
    {

    }

    public void Solve()
    {
        GameObject.Find("/Global").GetComponent<Animator>().SetBool("TelephoneEvent", false);
        GameObject.Find("/Global").GetComponent<Animator>().SetBool("ShowEvent", false);
    }
}
