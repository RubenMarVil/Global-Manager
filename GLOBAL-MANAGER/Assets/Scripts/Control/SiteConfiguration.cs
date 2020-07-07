using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.Control
{
    public enum CommonLanguageLevels
    {
        HIGH,
        MEDIUM,
        LOW
    }

    public class SiteConfiguration
    {
        public int CodSite { get; set; }
        public string Country { get; set; }
        public string Name { get; set; }
        public int TeamSize { get; set; }
        public CommonLanguageLevels LevelCommonLanguage { get; set; }
        public int MainSite { get; set; }

        public SiteConfiguration(string name, string country, int teamSize, string languageLevel)
        {
            Country = country;
            Name = name;
            TeamSize = teamSize;
            MainSite = 0;

            switch(languageLevel)
            {
                case "HighLevel": 
                    LevelCommonLanguage = CommonLanguageLevels.HIGH;
                    break;
                case "MediumLevel":
                    LevelCommonLanguage = CommonLanguageLevels.MEDIUM;
                    break;
                case "LowLevel":
                    LevelCommonLanguage = CommonLanguageLevels.LOW;
                    break;
            }
        }
    }
}
