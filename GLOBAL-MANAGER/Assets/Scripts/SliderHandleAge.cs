using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SliderHandleAge : MonoBehaviour
{
    public void OnSliderValueChanged(float ageNumber)
    {
        GetComponent<Text>().text = ageNumber.ToString();
    }
}
