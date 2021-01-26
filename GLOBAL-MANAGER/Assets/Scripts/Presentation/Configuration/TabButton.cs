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

    public Sprite tabIdle;
    public Sprite tabHover;
    public Sprite tabActive;
    public Sprite tabDissable;

    public bool site;
    public Text siteName;

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
        tabGroup.Subscribe(this);

        if (dissable)
        {
            SetDissable();
        }
        else
        {
            SetAble();

            if(name == "TabGeneral" || name == "Site1")
            {
                tabGroup.OnTabSelected(this);
            }
        }
    }
}
