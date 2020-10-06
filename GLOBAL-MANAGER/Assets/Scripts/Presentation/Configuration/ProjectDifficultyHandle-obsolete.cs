using Assets.Scripts.Control;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProjectDifficultyHandle : MonoBehaviour
{
    public Color VeryLowColor;
    public Color LowColor;
    public Color MediumColor;
    public Color HighColor;
    public Color VeryHighColor;
    public GameObject DifficultyBar;
    public GameObject DifficultyBarFill;

    void Start()
    {
        GetComponent<Text>().text = "";
    }

    public void SetValue(ProjectDifficultyLevels characteristicValue)
    {
        switch (characteristicValue)
        {
            case ProjectDifficultyLevels.VERY_LOW:
                GetComponent<Text>().text = "VERY LOW";
                GetComponent<Text>().color = VeryLowColor;
                DifficultyBar.GetComponent<Slider>().value = 1;
                DifficultyBarFill.GetComponent<Image>().color = VeryLowColor;
                break;
            case ProjectDifficultyLevels.LOW:
                GetComponent<Text>().text = "LOW";
                GetComponent<Text>().color = LowColor;
                DifficultyBar.GetComponent<Slider>().value = 2;
                DifficultyBarFill.GetComponent<Image>().color = LowColor;
                break;
            case ProjectDifficultyLevels.MEDIUM:
                GetComponent<Text>().text = "MEDIUM";
                GetComponent<Text>().color = MediumColor;
                DifficultyBar.GetComponent<Slider>().value = 3;
                DifficultyBarFill.GetComponent<Image>().color = MediumColor;
                break;
            case ProjectDifficultyLevels.HIGH:
                GetComponent<Text>().text = "HIGH";
                GetComponent<Text>().color = HighColor;
                DifficultyBar.GetComponent<Slider>().value = 4;
                DifficultyBarFill.GetComponent<Image>().color = HighColor;
                break;
            case ProjectDifficultyLevels.VERY_HIGH:
                GetComponent<Text>().text = "VERY HIGH";
                GetComponent<Text>().color = VeryHighColor;
                DifficultyBar.GetComponent<Slider>().value = 5;
                DifficultyBarFill.GetComponent<Image>().color = VeryHighColor;
                break;
        }
    }
}
