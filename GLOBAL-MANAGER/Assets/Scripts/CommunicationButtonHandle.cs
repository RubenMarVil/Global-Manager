using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CommunicationButtonHandle : MonoBehaviour
{
    public bool selected;
    public Color selectedColor;
    public Color notSelectedColor;

    void Start()
    {
        selected = false;
    }

    void Update()
    {
        if(selected)
        {
            GetComponent<Image>().color = selectedColor;
        }
        else
        {
            GetComponent<Image>().color = notSelectedColor;
        }
    }

    public void clicked()
    {
        selected = !selected;
    }
}
