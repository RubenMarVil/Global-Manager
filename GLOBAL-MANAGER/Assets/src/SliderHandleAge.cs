using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Text))]
public class SliderHandleAge : MonoBehaviour
{
    public Text dynamicTxtAge;

    private void Start()
    {
        dynamicTxtAge = GetComponent<Text>();
    }

    public void OnSliderValueChanged(float ageNumber)
    {
        dynamicTxtAge.text = ageNumber.ToString();
    }
}
