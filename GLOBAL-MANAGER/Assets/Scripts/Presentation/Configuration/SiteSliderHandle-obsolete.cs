using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SiteSliderHandle : MonoBehaviour
{
    public GameObject[] siteList;
    private Vector2[] contentDimensionsSite;

    void Start()
    {
        contentDimensionsSite = new[] {new Vector2(0, 574), new Vector2(0, 754), 
            new Vector2(0, 935), new Vector2(0, 1115), new Vector2(0, 1296), new Vector2(0, 1480)};
    }

    public void OnSliderValueChanged(float numSites)
    {
        Debug.Log($"Sites number: {numSites}");

        // Change site configuration visualization
        for(int i = 0; i < siteList.Length; i++)
        {
            if(i < numSites)
            {
                siteList[i].SetActive(true);
            }
            else
            {
                siteList[i].SetActive(false);
            }
        }

        GetComponent<RectTransform>().sizeDelta = contentDimensionsSite[(int)numSites - 2];
    }
}
