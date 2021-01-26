using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DropDownMainSite : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler, IPointerUpHandler
{
    public Dropdown MainSiteDropdown;
    public Image background;
    public Text label;

    public bool Deployed;

    public Sprite IdleNoSelected;
    public Sprite HoverNoSelected;
    public Sprite ActiveNoSelected;

    public Sprite IdleDeployed;
    public Sprite HoverDeployed;
    public Sprite ActiveDeployed;

    public Sprite IdleSelected;
    public Sprite HoverSelected;
    public Sprite ActiveSelected;


    void Start()
    {
        Deployed = false;
        MainSiteDropdown = GetComponent<Dropdown>();
        background = GetComponent<Image>();
        label = MainSiteDropdown.transform.GetChild(0).GetComponent<Text>();
    }

    void Update()
    {
        if (GameObject.Find("MainSiteDropdown/Dropdown List") == null && MainSiteDropdown.IsActive())
        {
            if (string.IsNullOrWhiteSpace(label.text) && Deployed)
            {
                background.sprite = IdleNoSelected;
                Deployed = false;
            }
            else if (!string.IsNullOrWhiteSpace(label.text) && Deployed)
            {
                background.sprite = IdleSelected;
                Deployed = false;
            }
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (string.IsNullOrWhiteSpace(label.text) && !Deployed)
        {
            background.sprite = HoverNoSelected;
        }
        else if (!string.IsNullOrWhiteSpace(label.text) && !Deployed)
        {
            background.sprite = HoverSelected;
        }
        else if (Deployed)
        {
            background.sprite = HoverDeployed;
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (string.IsNullOrWhiteSpace(label.text) && !Deployed)
        {
            background.sprite = IdleNoSelected;
        }
        else if (!string.IsNullOrWhiteSpace(label.text) && !Deployed)
        {
            background.sprite = IdleSelected;
        }
        else if (Deployed)
        {
            background.sprite = IdleDeployed;
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (string.IsNullOrWhiteSpace(label.text) && !Deployed)
        {
            background.sprite = ActiveNoSelected;
        }
        else if (!string.IsNullOrWhiteSpace(label.text) && !Deployed)
        {
            background.sprite = ActiveSelected;
        }
        else if (Deployed)
        {
            background.sprite = ActiveDeployed;
        }
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if (!Deployed)
        {
            background.sprite = IdleDeployed;
            Deployed = true;
        }
        else if (string.IsNullOrWhiteSpace(label.text) && Deployed)
        {
            background.sprite = IdleNoSelected;
            Deployed = false;
        }
        else if (!string.IsNullOrWhiteSpace(label.text) && Deployed)
        {
            background.sprite = IdleSelected;
            Deployed = false;
        }
    }
}
