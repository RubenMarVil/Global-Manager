using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

[RequireComponent(typeof(Image))]
public class TabButton : MonoBehaviour, IPointerEnterHandler, IPointerClickHandler, IPointerExitHandler
{
    public TabGroup tabGroup;

    public Image background;

    public bool dissable;

    public void OnPointerClick(PointerEventData eventData)
    {
        if(!dissable)
        {
            tabGroup.OnTabSelected(this);
        }
        
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if(!dissable)
        {
            tabGroup.OnTabEnter(this);
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if(!dissable)
        {
            tabGroup.OnTabExit(this);
        }
    }

    public void SetDissable()
    {
        dissable = true;
        tabGroup.SetDissable(this);
    }

    public void SetAble()
    {
        dissable = false;
        tabGroup.SetAble(this);
    }

    public void SetActive()
    {
        tabGroup.OnTabSelected(this);
    }

    void Start()
    {
        background = GetComponent<Image>();

        if (dissable)
        {
            tabGroup.SetDissable(this);
        }

        tabGroup.Subscribe(this);

        if(transform.GetChild(0).GetComponent<Text>().text == "General" || transform.GetChild(0).GetComponent<Text>().text == "Site 1")
        {
            tabGroup.OnTabSelected(this);
        }
    }
}
