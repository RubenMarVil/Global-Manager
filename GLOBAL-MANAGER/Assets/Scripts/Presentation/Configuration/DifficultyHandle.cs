using Assets.Scripts.Control;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DifficultyHandle : MonoBehaviour
{
    public Color VeryLowColor;
    public Color LowColor;
    public Color MediumColor;
    public Color HighColor;
    public Color VeryHighColor;

    public TextMeshProUGUI Text;

    public Slider DifficultySlider;
    public Image Fill;

    void Start()
    {
        Text = GetComponent<TextMeshProUGUI>();
        Text.text = "";
    }

    public void SetValue(ProjectDifficultyLevels characteristicLevel)
    {
        switch (characteristicLevel)
        {
            case ProjectDifficultyLevels.VERY_LOW:
                DifficultySlider.value = 1;
                Fill.gameObject.SetActive(true);
                Fill.color = VeryLowColor;
                Text.text = "VERY LOW";
                Text.color = VeryLowColor;
                break;
            case ProjectDifficultyLevels.LOW:
                DifficultySlider.value = 2;
                Fill.gameObject.SetActive(true);
                Fill.color = LowColor;
                Text.text = "LOW";
                Text.color = LowColor;
                break;
            case ProjectDifficultyLevels.MEDIUM:
                DifficultySlider.value = 3;
                Fill.gameObject.SetActive(true);
                Fill.color = MediumColor;
                Text.text = "NORMAL";
                Text.color = MediumColor;
                break;
            case ProjectDifficultyLevels.HIGH:
                DifficultySlider.value = 4;
                Fill.gameObject.SetActive(true);
                Fill.color = HighColor;
                Text.text = "HIGH";
                Text.color = HighColor;
                break;
            case ProjectDifficultyLevels.VERY_HIGH:
                DifficultySlider.value = 5;
                Fill.gameObject.SetActive(true);
                Fill.color = VeryHighColor;
                Text.text = "VERY HIGH";
                Text.color = VeryHighColor;
                break;
        }
    }
}
