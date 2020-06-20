using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SiteSliderHandle2 : MonoBehaviour
{
    public GameObject[] siteCommunication;
    private Vector2[] contentDimensionsCommunication;

    void Start()
    {
        contentDimensionsCommunication = new[] { new Vector2(0, 566), new Vector2(0, 1062),
            new Vector2(0, 1735), new Vector2(0, 2567), new Vector2(0, 3574), new Vector2(0, 4738)};
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
