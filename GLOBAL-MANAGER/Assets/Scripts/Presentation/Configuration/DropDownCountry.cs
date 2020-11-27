using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Experimental.PlayerLoop;
using UnityEngine.UI;

public class DropDownCountry : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler
{
    public Dropdown CountryDropdown;
    public Image background;

    public List<string> CountriesList;

    public string CountrySelected;

    bool active;
    public Sprite DropdownIdle;
    public Sprite DropdownHover;
    public Sprite DropdownActive;
    

    void Start()
    {
        active = false;
        CountryDropdown = GetComponent<Dropdown>();
        background = GetComponent<Image>();

        CountryDropdown.options.Add(new Dropdown.OptionData(""));

        CountriesList = GameConfigurationControl.GetCountries();

        foreach(string country in CountriesList)
        {
            CountryDropdown.options.Add(new Dropdown.OptionData(country));
        }
    }

    void Update()
    {
        if(GameObject.Find("ClientCountryDropdown/Dropdown List") == null)
        {
            background.sprite = DropdownIdle;
            active = false;
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

        background.sprite = DropdownIdle;
        active = false;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if(active)
        {
            background.sprite = DropdownIdle;
            active = false;
        }
        else
        {
            background.sprite = DropdownActive;
            active = true;
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if(active)
        {
            background.sprite = DropdownHover;
        }
        else
        {

        }
    }
}
