using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SiteSliderHandle : MonoBehaviour
{
    public GameObject[] siteList;
    private Vector2[] contentDimensions;

    // Start is called before the first frame update
    void Start()
    {
        contentDimensions = new[] {new Vector2(0, 409), new Vector2(0, 541), 
            new Vector2(0, 665), new Vector2(0, 797), new Vector2(0, 927), new Vector2(0, 1054)};


        
    }

    public void OnSliderValueChanged(float numSites)
    {
        Debug.Log($"Sites number: {numSites}");

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

        GetComponent<RectTransform>().sizeDelta = contentDimensions[(int)numSites - 2];
    }
}
