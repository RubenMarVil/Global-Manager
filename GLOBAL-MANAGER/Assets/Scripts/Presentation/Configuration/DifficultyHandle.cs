using Assets.Scripts.Control;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DifficultyHandle : MonoBehaviour
{
    public Sprite VeryLow;
    public Sprite Low;
    public Sprite Medium;
    public Sprite High;
    public Sprite VeryHigh;

    public string Value;
    public Image Imagen;

    public Sprite VeryLowBar;
    public Sprite LowBar;
    public Sprite MediumBar;
    public Sprite HighBar;
    public Sprite VeryHighBar;

    public Image DifficultyBar;

    void Start()
    {
        Imagen = GetComponent<Image>();
        Value = "";
    }

    public void SetValue(ProjectDifficultyLevels characteristicLevel)
    {
        switch (characteristicLevel)
        {
            case ProjectDifficultyLevels.VERY_LOW:
                DifficultyBar.sprite = VeryLowBar;
                Value = "VERY LOW";
                Imagen.sprite = VeryLow;
                break;
            case ProjectDifficultyLevels.LOW:
                DifficultyBar.sprite = LowBar;
                Value = "LOW";
                Imagen.sprite = Low;
                break;
            case ProjectDifficultyLevels.MEDIUM:
                DifficultyBar.sprite = MediumBar;
                Value = "MEDIUM";
                Imagen.sprite = Medium;
                break;
            case ProjectDifficultyLevels.HIGH:
                DifficultyBar.sprite = HighBar;
                Value = "HIGH";
                Imagen.sprite = High;
                break;
            case ProjectDifficultyLevels.VERY_HIGH:
                DifficultyBar.sprite = VeryHighBar;
                Value = "VERY HIGH";
                Imagen.sprite = VeryHigh;
                break;
        }
    }
}
