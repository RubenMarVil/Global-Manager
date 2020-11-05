using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SiteSlider : MonoBehaviour
{
    public List<TabButton> tabSites;

    public int NumSites;

    public void SliderChangeValue(float numSites)
    {
        NumSites = Convert.ToInt32(numSites);

        for(int i = 0; i < tabSites.Count; i++)
        {
            if(i < numSites)
            {
                tabSites[i].SetAble();
            }
            else
            {
                tabSites[i].SetDissable();
            }
        }
    }
}
