using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SiteHandle : MonoBehaviour
{
    public TabButton Tab;

    public int Site;
    public string Name;
    public string Country;
    public int TeamSize;
    public string LanguageLevel;

    public InputField NameInput;
    public DropDownCountry CountryDropdown;
    public Slider TeamSizeSlider;
    public ToggleGroupHandle LanguageLevelGroup;

    void Start()
    {
        
    }

    void Update()
    {
        
    }

    public void ChangeName(string name)
    {
        Name = name;
    }

    public void ChangeCountry()
    {
        Country = CountryDropdown.CountrySelected;
    }

    public void ChangeTeamSize(Single value)
    {
        TeamSize = Convert.ToInt32(value);
    }

    public void ChangeLanguageLevel()
    {
        LanguageLevel = LanguageLevelGroup.ToggleActive.name;
    }

    public void ActiveTab()
    {
        Tab.SetActive();
    }
}
