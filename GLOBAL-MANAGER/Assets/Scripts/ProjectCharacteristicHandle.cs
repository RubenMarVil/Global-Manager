using Assets.Scripts.Control;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProjectCharacteristicHandle : MonoBehaviour
{
    public Color VeryLowColor;
    public Color LowColor;
    public Color NormalColor;
    public Color HighColor;
    public Color VeryHighColor;

    void Start()
    {
        GetComponent<Text>().text = "";
    }

    public void SetValue(ProjectCharacteristicLevels characteristicValue)
    {
        switch(characteristicValue)
        {
            case ProjectCharacteristicLevels.VERY_LOW:
                GetComponent<Text>().text = "VERY LOW";
                GetComponent<Text>().color = VeryLowColor;
                break;
            case ProjectCharacteristicLevels.LOW:
                GetComponent<Text>().text = "LOW";
                GetComponent<Text>().color = LowColor;
                break;
            case ProjectCharacteristicLevels.NORMAL:
                GetComponent<Text>().text = "NORMAL";
                GetComponent<Text>().color = NormalColor;
                break;
            case ProjectCharacteristicLevels.HIGH:
                GetComponent<Text>().text = "HIGH";
                GetComponent<Text>().color = HighColor;
                break;
            case ProjectCharacteristicLevels.VERY_HIGH:
                GetComponent<Text>().text = "VERY HIGH";
                GetComponent<Text>().color = VeryHighColor;
                break;
        }
    }
}
