using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DropDownLanguage : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler, IPointerUpHandler
{
    public Dropdown LanguageDropdown;
    public Image background;

    public List<string> LanguagesList;

    public string LanguageSelected;

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
        LanguageDropdown = GetComponent<Dropdown>();
        background = GetComponent<Image>();

        LanguageDropdown.options.Add(new Dropdown.OptionData(""));

        LanguagesList = /*GameConfigurationControl.GetLanguages();*/ new List<string>() { "English", "Spanish" }; 

        foreach(string language in LanguagesList)
        {
            LanguageDropdown.options.Add(new Dropdown.OptionData(language));
        }
    }

    void Update()
    {
        if (GameObject.Find("CommonLanguageDropdown/Dropdown List") == null && LanguageDropdown.IsActive())
        {
            if(string.IsNullOrWhiteSpace(LanguageSelected) && Deployed)
            {
                background.sprite = IdleNoSelected;
                Deployed = false;
            }
            else if(!string.IsNullOrWhiteSpace(LanguageSelected) && Deployed)
            {
                background.sprite = IdleSelected;
                Deployed = false;
            }
        }
    }

    public void ChangeLanguage(int value)
    {
        LanguageSelected = LanguageDropdown.options.ElementAt(value).text;

        background.sprite = IdleSelected;
        Deployed = false;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (string.IsNullOrWhiteSpace(LanguageSelected) && !Deployed)
        {
            background.sprite = HoverNoSelected;
        }
        else if (!string.IsNullOrWhiteSpace(LanguageSelected) && !Deployed)
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
        if (string.IsNullOrWhiteSpace(LanguageSelected) && !Deployed)
        {
            background.sprite = IdleNoSelected;
        }
        else if (!string.IsNullOrWhiteSpace(LanguageSelected) && !Deployed)
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
        if (string.IsNullOrWhiteSpace(LanguageSelected) && !Deployed)
        {
            background.sprite = ActiveNoSelected;
        }
        else if (!string.IsNullOrWhiteSpace(LanguageSelected) && !Deployed)
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
        else if (string.IsNullOrWhiteSpace(LanguageSelected) && Deployed)
        {
            background.sprite = IdleNoSelected;
            Deployed = false;
        }
        else if (!string.IsNullOrWhiteSpace(LanguageSelected) && Deployed)
        {
            background.sprite = IdleSelected;
            Deployed = false;
        }
    }
}
