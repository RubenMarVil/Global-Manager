using Assets.Scripts.Control;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CharacteristicHandle : MonoBehaviour
{
    public Color VeryLowColor;
    public Color LowColor;
    public Color NormalColor;
    public Color HighColor;
    public Color VeryHighColor;

    public TextMeshProUGUI Text;

    void Start()
    {
        Text = GetComponent<TextMeshProUGUI>();
        Text.text = "";
    }

    public void SetValue(ProjectCharacteristicLevels characteristicLevel)
    {
        switch (characteristicLevel)
        {
            case ProjectCharacteristicLevels.VERY_LOW:
                Text.text = "VERY LOW";
                Text.color = VeryLowColor;
                break;
            case ProjectCharacteristicLevels.LOW:
                Text.text = "LOW";
                Text.color = LowColor;
                break;
            case ProjectCharacteristicLevels.NORMAL:
                Text.text = "NORMAL";
                Text.color = NormalColor;
                break;
            case ProjectCharacteristicLevels.HIGH:
                Text.text = "HIGH";
                Text.color = HighColor;
                break;
            case ProjectCharacteristicLevels.VERY_HIGH:
                Text.text = "VERY HIGH";
                Text.color = VeryHighColor;
                break;
        }
    }
}
