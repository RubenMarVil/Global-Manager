using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class DropDownCountry : MonoBehaviour
{
    public Dropdown CountryDropdown;

    public List<string> CountriesList;

    public string CountrySelected;

    void Start()
    {
        CountryDropdown = GetComponent<Dropdown>();

        CountryDropdown.options.Add(new Dropdown.OptionData(""));

        CountriesList = GameConfigurationControl.GetCountries();

        foreach(string country in CountriesList)
        {
            CountryDropdown.options.Add(new Dropdown.OptionData(country));
        }
    }

    public void FirstClick()
    {
        if(string.IsNullOrEmpty(CountrySelected))
        {
            CountryDropdown.transform.GetChild(0).gameObject.SetActive(false);
            CountryDropdown.transform.GetChild(1).gameObject.SetActive(true);
        }
    }

    public void ChangeCountry(int value)
    {
        CountrySelected = CountryDropdown.options.ElementAt(value).text;
    }
}
