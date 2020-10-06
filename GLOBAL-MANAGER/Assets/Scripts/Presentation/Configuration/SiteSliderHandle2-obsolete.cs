using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SiteSliderHandle2 : MonoBehaviour
{
    public GameObject[] siteCommunication;
    private Vector2[] contentDimensionsCommunication;

    void Start()
    {
        contentDimensionsCommunication = new[] { new Vector2(0, 434), new Vector2(0, 1172),
            new Vector2(0, 2276), new Vector2(0, 3738), new Vector2(0, 5579), new Vector2(0, 7781)};
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
