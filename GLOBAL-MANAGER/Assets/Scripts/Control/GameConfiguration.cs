using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.Control
{
    public enum ProjectDifficultyLevels
    {
        VERY_LOW,
        LOW,
        MEDIUM,
        HIGH,
        VERY_HIGH
    }

    public class GameConfiguration
    {
        public int CodGame { get; set; }
        public string Player { get; set; }
        public int NumSites { get; set; }
        public string ClientCountry { get; set; }
        public string CommonLanguage { get; set; }
        public ProjectDifficultyLevels ProjectDifficulty { get; set; }
        public float InitialBudget { get; set; }
        public float InitialDuration { get; set; }
        public List<SiteConfiguration> SitesList { get; set; }
        public List<CommunicationConfiguration> CommunicationsList { get; set; }
        public List<ProjectCharacteristic> ProjectCharacteristicsList { get; set; }

        public void setProjectDifficulty(string difficulty)
        {
            switch(difficulty)
            {
                case "VERY HIGH":
                    ProjectDifficulty = ProjectDifficultyLevels.VERY_HIGH;
                    break;
                case "HIGH":
                    ProjectDifficulty = ProjectDifficultyLevels.HIGH;
                    break;
                case "MEDIUM":
                    ProjectDifficulty = ProjectDifficultyLevels.MEDIUM;
                    break;
                case "LOW":
                    ProjectDifficulty = ProjectDifficultyLevels.LOW;
                    break;
                case "VERY LOW":
                    ProjectDifficulty = ProjectDifficultyLevels.VERY_LOW;
                    break;
            }
        }
    }
}
