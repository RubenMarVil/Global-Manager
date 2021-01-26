using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Experimental.PlayerLoop;
using UnityEngine.UI;

public class DropDownCountry : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler, IPointerUpHandler
{
    public Dropdown CountryDropdown;
    public Image background;

    public List<string> CountriesList;

    public string CountrySelected;

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
        var prueba = GameObject.Find("ClientCountryDropdown/Dropdown List");
        var prueba2 = GameObject.Find("CountryDropdown/Dropdown List");

        if(CountryDropdown.IsActive())
        {
            if((CountryDropdown.name == "ClientCountryDropdown" && GameObject.Find("ClientCountryDropdown/Dropdown List") == null) || 
                (CountryDropdown.name == "CountryDropdown" && GameObject.Find("CountryDropdown/Dropdown List") == null))
            {
                if (string.IsNullOrWhiteSpace(CountrySelected) && Deployed)
                {
                    background.sprite = IdleNoSelected;
                    Deployed = false;
                }
                else if (!string.IsNullOrWhiteSpace(CountrySelected) && Deployed)
                {
                    background.sprite = IdleSelected;
                    Deployed = false;
                }
            }
        }
    }

    public void ChangeCountry(int value)
    {
        CountrySelected = CountryDropdown.options.ElementAt(value).text;

        background.sprite = IdleSelected;
        Deployed = false;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (string.IsNullOrWhiteSpace(CountrySelected) && !Deployed)
        {
            background.sprite = HoverNoSelected;
        }
        else if (!string.IsNullOrWhiteSpace(CountrySelected) && !Deployed)
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
        if (string.IsNullOrWhiteSpace(CountrySelected) && !Deployed)
        {
            background.sprite = IdleNoSelected;
        }
        else if (!string.IsNullOrWhiteSpace(CountrySelected) && !Deployed)
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
        if (string.IsNullOrWhiteSpace(CountrySelected) && !Deployed)
        {
            background.sprite = ActiveNoSelected;
        }
        else if (!string.IsNullOrWhiteSpace(CountrySelected) && !Deployed)
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
        else if (string.IsNullOrWhiteSpace(CountrySelected) && Deployed)
        {
            background.sprite = IdleNoSelected;
            Deployed = false;
        }
        else if (!string.IsNullOrWhiteSpace(CountrySelected) && Deployed)
        {
            background.sprite = IdleSelected;
            Deployed = false;
        }
    }
}
