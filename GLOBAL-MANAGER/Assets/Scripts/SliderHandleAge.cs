using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Text))]
public class SliderHandleAge : MonoBehaviour
{
    public Text dynamicTxtAge;

    public void OnSliderValueChanged(float ageNumber)
    {
        dynamicTxtAge.text = ageNumber.ToString();
    }
}
