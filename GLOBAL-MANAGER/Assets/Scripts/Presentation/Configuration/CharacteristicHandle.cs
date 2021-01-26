using Assets.Scripts.Control;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CharacteristicHandle : MonoBehaviour
{
    public Sprite VeryLow;
    public Sprite Low;
    public Sprite Normal;
    public Sprite High;
    public Sprite VeryHigh;

    public string Value;
    public Image Imagen;

    void Start()
    {
        Imagen = GetComponent<Image>();
        Value = "";
    }

    public void SetValue(ProjectCharacteristicLevels characteristicLevel)
    {
        switch (characteristicLevel)
        {
            case ProjectCharacteristicLevels.VERY_LOW:
                Value = "VERY LOW";
                Imagen.sprite = VeryLow;
                break;
            case ProjectCharacteristicLevels.LOW:
                Value = "LOW";
                Imagen.sprite = Low;
                break;
            case ProjectCharacteristicLevels.NORMAL:
                Value = "NORMAL";
                Imagen.sprite = Normal;
                break;
            case ProjectCharacteristicLevels.HIGH:
                Value = "HIGH";
                Imagen.sprite = High;
                break;
            case ProjectCharacteristicLevels.VERY_HIGH:
                Value = "VERY HIGH";
                Imagen.sprite = VeryHigh;
                break;
        }
    }
}
