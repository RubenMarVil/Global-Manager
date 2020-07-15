using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class Country
{
    public string Name { get; set; }
    public int PowerDistance { get; set; }
    public int Individualism { get; set; }
    public int Masculinity { get; set; }
    public int UncertantyAvoidance { get; set; }
    public int LongTermOrientation { get; set; }
    public int Indulgence { get; set; }
    public float TimeZone { get; set; }
    public float Salary { get; set; }
    public List<Language> LanguagesSpeak { get; set; }
    public bool Instability { get; set; }
    public double Latitude { get; set; }
    public double Longitude { get; set; }

    public Country(string name, int powerDistance, int individualism, int masculinity, int uncertantyAvoidance, int longTermOrientation, int indulgence,
        float timeZone, float salary, int instability, double latitude, double longitude)
    {
        Name = name;
        PowerDistance = powerDistance;
        Individualism = individualism;
        Masculinity = masculinity;
        UncertantyAvoidance = uncertantyAvoidance;
        LongTermOrientation = longTermOrientation;
        Indulgence = indulgence;
        TimeZone = timeZone;
        Salary = salary;
        Instability = (instability == 1) ? true : false;
        Latitude = latitude;
        Longitude = longitude;
    }

    public void SetLanguages(List<Language> languageslist)
    {
        LanguagesSpeak = languageslist;
    }

    public bool ContainOfficialLanguage(string language)
    {
        bool contain = false;

        foreach(Language languageItem in LanguagesSpeak)
        {
            if(languageItem.Name == language && languageItem.Official)
            {
                contain = true;
                break;
            }
        }

        return contain;
    }

    public bool ContainLanguage(string language)
    {
        bool contain = false;

        foreach(Language languageItem in LanguagesSpeak)
        {
            if(languageItem.Name == language)
            {
                contain = true;
                break;
            }
        }

        return contain;
    }
}