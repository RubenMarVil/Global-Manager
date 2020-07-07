using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DropdownLanguage : MonoBehaviour
{
    private Dropdown m_Dropdown;
    private List<string> languagesList;

    void Start()
    {
        languagesList = GameConfigurationControl.GetLanguages();
        m_Dropdown = GetComponent<Dropdown>();

        foreach(string language in languagesList)
        {
            m_Dropdown.options.Add(new Dropdown.OptionData(language));
        }
    }
}
