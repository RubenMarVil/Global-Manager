using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class DropDownLanguage : MonoBehaviour
{
    public Dropdown LanguageDropdown;

    public List<string> LanguagesList;

    public string LanguageSelected;

    void Start()
    {
        LanguageDropdown = GetComponent<Dropdown>();

        LanguageDropdown.options.Add(new Dropdown.OptionData(""));

        LanguagesList = GameConfigurationControl.GetLanguages();

        foreach(string language in LanguagesList)
        {
            LanguageDropdown.options.Add(new Dropdown.OptionData(language));
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
    }
}
