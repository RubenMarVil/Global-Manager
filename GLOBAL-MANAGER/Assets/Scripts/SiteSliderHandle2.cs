using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SiteSliderHandle2 : MonoBehaviour
{
    public GameObject[] siteCommunication;
    private Vector2[] contentDimensionsCommunication;

    void Start()
    {
        contentDimensionsCommunication = new[] { new Vector2(0, 228), new Vector2(0, 560),
            new Vector2(0, 1060), new Vector2(0, 1726), new Vector2(0, 2559), new Vector2(0, 3561)};
    }

    public void OnSliderValueChanged(float numSites)
    {
        // Change site communication configuration visualization
        for(int i = 0; i < siteCommunication.Length; i++)
        {
            if(i < numSites - 1)
            {
                siteCommunication[i].SetActive(true);
            }
            else
            {
                siteCommunication[i].SetActive(false);
            }

            GetComponent<RectTransform>().sizeDelta = contentDimensionsCommunication[(int)numSites - 2];
        }
    }
}
