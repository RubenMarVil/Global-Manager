using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DropDownLanguage : MonoBehaviour, IPointerClickHandler
{
    public Dropdown LanguageDropdown;
    public Image background;
    public Image shadow;

    public List<string> LanguagesList;

    public string LanguageSelected;

    bool active;
    public Sprite DropdownActive;
    public Sprite DropdownIdle;

    public Sprite DropdownActiveShadow;
    public Sprite DropdownIdleShadow;

    void Start()
    {
        active = false;
        LanguageDropdown = GetComponent<Dropdown>();
        background = GetComponent<Image>();

        LanguageDropdown.options.Add(new Dropdown.OptionData(""));

        LanguagesList = GameConfigurationControl.GetLanguages();

        foreach(string language in LanguagesList)
        {
            LanguageDropdown.options.Add(new Dropdown.OptionData(language));
        }
    }

    void Update()
    {
        if (GameObject.Find("CommonLanguageDropdown/Dropdown List") == null)
        {
            background.sprite = DropdownIdle;
            shadow.sprite = DropdownIdleShadow;
            active = false;
            Debug.Log("CommonLanguageDropdown to IDLE!");
        }
    }

    public void FirstClick()
    {
        if(string.IsNullOrEmpty(LanguageSelected))
        {
            LanguageDropdown.transform.GetChild(0).gameObject.SetActive(false);
            LanguageDropdown.transform.GetChild(1).gameObject.SetActive(true);
        }
    }

    public void ChangeLanguage(int value)
    {
        LanguageSelected = LanguageDropdown.options.ElementAt(value).text;

        background.sprite = DropdownIdle;
        shadow.sprite = DropdownIdleShadow;
        active = false;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (active)
        {
            background.sprite = DropdownIdle;
            shadow.sprite = DropdownIdleShadow;
            active = false;
        }
        else
        {
            background.sprite = DropdownActive;
            shadow.sprite = DropdownActiveShadow;
            active = true;
        }
    }
}
