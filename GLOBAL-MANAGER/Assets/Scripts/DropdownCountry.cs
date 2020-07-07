using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DropdownCountry : MonoBehaviour
{
    private Dropdown m_Dropdown;
    private List<string> countriesList;

    void Start()
    {
        countriesList = GameConfigurationControl.GetCountries();
        m_Dropdown = GetComponent<Dropdown>();

        foreach (string country in countriesList)
        {
            m_Dropdown.options.Add(new Dropdown.OptionData(country));
        }

        
    }
}
